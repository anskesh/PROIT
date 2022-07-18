using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuickInventory : MonoBehaviour
{
    [SerializeField] private InventoryCell _inventory;
    private List<Cell> _quickInventory = new List<Cell>();
    private Move _move;

    private void Awake()
    {
        _quickInventory = GetComponentsInChildren<Cell>().ToList();
        _move = FindObjectOfType<Move>();
    }

    private void OnEnable()
    {
        _inventory.InventoryChanged += Render;
        _move.ItemChanged += Render;
    }
    
    private void OnDisable()
    {
        _inventory.InventoryChanged -= Render;
        _move.ItemChanged -= Render;
    }

    private void Render(List<Cell> cells)
    {
        for (int i = 0; i < _inventory.Column; i++)
        {
            _quickInventory[i].Render(cells[i]);
        }
    }
}
