using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TakeTools : MonoBehaviour
{
    [CanBeNull] private Sprite _tool;
    [CanBeNull] private Item _item;
    private int _id = 0;
    private string _name = "";
    private SpriteRenderer _spriteTool = null;
    private Inventory _inventory;
    public float MultiplierDamage { get; private set; } = 1f;

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
            if (_id != 3 || _id != 6) TakeTool(new []{3, 6});
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (_id != 1 || _id != 4) TakeTool(new []{1, 4});
        }
        
        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (_id != 2 || _id != 5) TakeTool(new []{2, 5});
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
        MultiplierDamage = 1;
        _id = 0;
        _name = "";
        _tool = null;
        _spriteTool.sprite = _tool;
    }
    private void TakeTool(int[] ids)
    {
        _item = _inventory.SearchItemById(ids);
        if (_item == null) return;
        _tool = _item.img;
        _id = _item.id;
        _name = _item.name;
        if (_item.name.Contains("Топазн")) MultiplierDamage = 1.5f;
        _spriteTool.sprite = _tool;
    }

    private void Heal(int heal = 15)
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth.Health < 100)
        {
            playerHealth.AddHealth(heal);
            _inventory.DeleteCertainAmountItem(_id, 1);
        }
    }
}
