using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabClass : MonoBehaviour
{
    public Item item;
    public int count;

    public void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.img;
    }

    

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
