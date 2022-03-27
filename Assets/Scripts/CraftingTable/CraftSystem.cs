using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    public GameObject canvas;
    public List<IDCraftItem> craftItem;
    public GameObject craftButton;
    public GameObject craftPlace;

    public void Start()
    {
        for (int i = 0; i < craftItem.Count; i++)
        {
            GameObject mybutton = Instantiate(craftButton, craftPlace.transform);
            mybutton.GetComponent<CraftButton>().idButton = i;
        }
        canvas.SetActive(false);
    }

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

public class IDCraftItem
{
    public int idCraft;
    public int countCraft;
    public List<int> idNeed;
    public List<int> countNeed;
}