using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public int idButton;
    private Item craftItem;

    private List<Item> needItems;
    private DataBase data;
    

    public void Start()
    {
        data = FindObjectOfType<DataBase>();
        
    }
}
