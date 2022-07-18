using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public Item GetItemByID(int id = 0)
    {
	    var item = items.Where(elem => elem.id == id);
        return item.First();
    }
}

[System.Serializable]

public class Item
{
    public int id;
    public string name;
    public Sprite img;
}
