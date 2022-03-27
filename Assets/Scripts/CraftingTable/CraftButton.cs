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
    public Inventory inventory;
    public CraftSystem dataCraft;

    public Image img;
    public Text textCraft;
    public Text textNeed;

    public GameObject itemPrefab;

    public void Start()
    {
        data = FindObjectOfType<DataBase>();
        dataCraft = FindObjectOfType<CraftSystem>();
        inventory = FindObjectOfType<Inventory>();
        
        idCraft = dataCraft.craftItem[idButton].idCraft;
        countCraft = dataCraft.craftItem[idButton].countCraft;

        idNeed = new List<int>();
        countNeed = new List<int>();

        idNeed.AddRange(dataCraft.craftItem[idButton].idNeed);
        countNeed.AddRange(dataCraft.craftItem[idButton].countNeed);

        textCraft.text = data.items[idCraft].name;
        textNeed.text = null;
        for (int i = 0; i < idNeed.Count; i++)
        {
            textNeed.text += data.items[idNeed[i]].name + " x" + countNeed[i] + "\n";
        }

        img.sprite = data.items[idCraft].img;
    }

    private void ThrowItem()
    {
        if (idCraft != 0)
        {
            GameObject throwItem = Instantiate(itemPrefab, transform.parent.parent.parent.position + new Vector3(0, -2, 0), new Quaternion());
            ItemPrefabClass itemPrefabClass = throwItem.GetComponent<ItemPrefabClass>();
            itemPrefabClass.ID = idCraft;
            itemPrefabClass.Count = countCraft;
        }
    }

    public void TouchToCraft()
    {        
        for (int i = 0; i < idNeed.Count; i++)
        {
            if (inventory.SumCountSameItem(idNeed[i]) < countNeed[i]) return;
        }

        for (int i = 0; i < idNeed.Count; i++)
        {
            inventory.DeleteCertainAmountItem(idNeed[i], countNeed[i]);
        }

        ThrowItem();
    }
    
}
