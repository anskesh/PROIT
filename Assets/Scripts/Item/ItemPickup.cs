using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject dataControler;
    public Item colItem;
    public int countPickup;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dataControler.GetComponent<Inventory>().ThrowItem(0);            
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            colItem = collision.GetComponent<ItemPrefabClass>().item;
            countPickup = collision.GetComponent<ItemPrefabClass>().Count;
            countPickup = dataControler.GetComponent<Inventory>().AddItemToInventory(colItem, countPickup);
            if (countPickup == 0)
            {
                collision.GetComponent<ItemPrefabClass>().DestroyItem();
            }
            else
            {
                collision.GetComponent<ItemPrefabClass>().Count = countPickup;
            }
        }
    }
}
