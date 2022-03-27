using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    public GameObject canvas;
    public List<IDCraftItem> craftItem;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canvas.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canvas.SetActive(false);
        }
    }
}

[System.Serializable]

public class IDItems
{
    public int idCraft;
    public int count;    
}

[System.Serializable]

public class IDCraftItem
{
    public int idCraft;
    public int count;
    public List<IDItems> needItem;
}