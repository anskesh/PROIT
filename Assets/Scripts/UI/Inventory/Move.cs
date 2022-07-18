using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Move : MonoBehaviour
{
    [SerializeField] private InventoryCell _inventory;
    private Cell _move;
    private List<Cell> _cells;
    private Cell _tempCell;
    private int _index;
    
    public event Action<List<Cell>> ItemChanged;

    private void Start()
    {
        _move = GetComponent<Cell>();
        _move.Clear();
        _cells = _inventory.GetSomeCells(_inventory.CountCell);
        
        foreach (var cell in _cells)
        {
            cell.Clicked += TakeCell;
        }
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
        
        if (Input.GetMouseButtonDown(0))
        {
            CheckClick();
        }
    }

    private int CheckClick()
    {
        if (_move.Count == 0) return 0;
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        
        foreach (var raycast in raysastResults) 
            if (raycast.gameObject.CompareTag("Inventory")) return 0;
        
        _tempCell.Render(_move);
        _inventory.ThrowItem(_index, FindObjectOfType<Player>().transform);
        _move.Clear();
        return 1;
    }
    
    private void TakeCell(Cell cell, string index)
    {
        int.TryParse(string.Join("", index.Where(c => char.IsDigit(c))), out _index);
        
        if (_move.Count == 0)
        {
            _move.Render(cell);
            cell.Clear();
            _tempCell = cell;
            ItemChanged?.Invoke(_inventory.GetSomeCells());
            return;
        }
        
        if (cell.Count == 0 && _move.Count > 0)
        {
            cell.Render(_move);
            _move.Clear();
            _tempCell = null;
            ItemChanged?.Invoke(_inventory.GetSomeCells());
            return;
        }

        if (cell.Count > 0 && _move.Count > 0)
        {
            if (cell.ID == _move.ID)
            {
                if (cell.Count + _move.Count <= _inventory.MaxStack)
                {
                    cell.Count += _move.Count;
                    cell.ChangeCount();
                    _move.Clear();
                } 
                else if (cell.Count + _move.Count > _inventory.MaxStack)
                {
                    _move.Count -= _inventory.MaxStack - cell.Count;
                    cell.Count = _inventory.MaxStack;
                    cell.ChangeCount();
                    
                    if (_move.Count == 0) _move.Clear();
                    else
                    {
                        _tempCell.Render(cell);
                        cell.Render(_move);
                        _move.Clear();
                        _tempCell = null;
                    }
                }
            }
            else
            {
                _tempCell.Render(cell);
                cell.Render(_move);
                _move.Clear();
                _tempCell = null;
            }

            ItemChanged?.Invoke(_inventory.GetSomeCells());
        }
    }
}
