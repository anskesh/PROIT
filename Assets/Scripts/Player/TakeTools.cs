using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TakeTools : MonoBehaviour
{
    [CanBeNull] private Sprite _tool;
    [CanBeNull] private Item _item;
    private int _id;
    private string _name;
    private SpriteRenderer _spriteTool;
    private Inventory _inventory;

    public int Id
    {
        get => _id;
        private set => _id = value;
    }

    public string Name
    {
        get => _name;
        private set => _name = value;
    }
    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _spriteTool = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            TakeTool(new []{3, 6});
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            TakeTool(new []{1, 4});
        }
        
        if (Input.GetKey(KeyCode.Alpha3))
        {
            TakeTool(new []{2, 5});
        }
    }

    public void ClearTool()
    {
        _spriteTool.sprite = null;
    }
    private void TakeTool(int[] ids)
    {
        _item = _inventory.SearchItemById(ids);
        if (_item == null) return;
        _tool = _item.img;
        _id = _item.id;
        _spriteTool.sprite = _tool;
    }
}
