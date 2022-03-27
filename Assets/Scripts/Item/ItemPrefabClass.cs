using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabClass : MonoBehaviour
{
    [SerializeField] private int id;
    public int Count { get; set; }
    public int ID
    {
        get => id;
        set => id = value;
    }

    [NonSerialized] public Item item;

    public void Start()
    {
        item = FindObjectOfType<DataBase>().GetItemByID(id);
        gameObject.GetComponent<SpriteRenderer>().sprite = item.img;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
