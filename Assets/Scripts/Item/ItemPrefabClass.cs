using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemPrefabClass : MonoBehaviour
{
    private int _id;
    
    public int Count { get; set; }
    public int ID
    {
        get => _id;
        set => _id = value;
    }

    [NonSerialized] public Item item;

    public void Start()
    {
        item = FindObjectOfType<DataBase>().GetItemByID(_id);
        gameObject.GetComponent<SpriteRenderer>().sprite = item.img;
        gameObject.GetComponentInChildren<Text>().text = "x" + Count;
        StartCoroutine(RemoveResource());
    }

    private IEnumerator RemoveResource()
    {
        yield return new WaitForSeconds(60f);
        DestroyItem();
    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
