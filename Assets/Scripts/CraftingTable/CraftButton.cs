using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour
{
    public int idButton;
    private int idCraft;
    private List<int> idNeed;
    private List<int> countNeed;
    private int countCraft;

    public DataBase data;
    public CraftSystem dataCraft;

    public Image img;
    public Text textCraft;
    public Text textNeed;

    public void Start()
    {
        data = FindObjectOfType<DataBase>();
        dataCraft = FindObjectOfType<CraftSystem>();    
        
        idCraft = dataCraft.craftItem[idButton].idCraft;
        countCraft = dataCraft.craftItem[idButton].countCraft;
        Debug.Log(idCraft);
        Debug.Log(countCraft);

        idNeed = new List<int>();
        countNeed = new List<int>();

        Debug.Log(idNeed.Count);
        Debug.Log(countNeed.Count);

        idNeed.AddRange(dataCraft.craftItem[idButton].idNeed);
        countNeed.AddRange(dataCraft.craftItem[idButton].countNeed);
                
               
        img.sprite = data.items[idCraft].img;
    }
}
