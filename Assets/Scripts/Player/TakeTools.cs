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

        if (Input.GetKey(KeyCode.Alpha4))
        {
            if (_id != 17) TakeTool(new []{17});
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (Id == 17)
            {
                Heal();
            }
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            ClearTool();
        }
    }

    public void ClearTool()
    {
        Id = 0;
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

    private void Heal(int heal = 15)
    {
        PlayerHealth _playerHealth = FindObjectOfType<PlayerHealth>();
        if (_playerHealth.Health < 100)
        {
            _playerHealth.AddHealth(heal);
            _inventory.DeleteCertainAmountItem(_id, 1);
        }
    }
}
