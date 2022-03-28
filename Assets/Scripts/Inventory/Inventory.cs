using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public DataBase data;

    public List<ItemInventory> items = new List<ItemInventory>();
    public GameObject gameObjShow;
    public GameObject InventoryMainObject;
    public int maxCount;
    public int maxItemStack = 32;

    public Camera cam;
    private EventSystem es;

    public int currentID;
    public ItemInventory currentItem;

    public RectTransform movingObject;
    public Vector3 offset;

    public GameObject background;
    public GameObject itemPrefab;


    public void Start()
    {
        es = FindObjectOfType<EventSystem>();
        if (items.Count == 0)
        {
            AddGraphics();
        }
        
        Save save = FindObjectOfType<Save>();
        save.LoadInventory();
        
        background.SetActive(false);
    }

    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            background.SetActive(!background.activeSelf);
            if (background.activeSelf)
            {
                UpdateInventory();
            }
        }
    }

    public List<int> SearchForSameItem(int idItem)
    {
        List<int> allIdInventory = new List<int>();
        for (int i = 0; i < maxCount; i++)
        {
            if(items[i].id == idItem)
            {
                allIdInventory.Add(i);
            }
        }
        return allIdInventory;
    }
    
    public Item SearchItemById(int [] id)
    {
        foreach (var item in items)
        {
            for (var i = 0; i < id.Length; i++)
                if (item.id == id[i])
                {
                    return data.GetItemByID(id[i]);
                }
        }
        return null;
    }

    public int SumCountSameItem(int idItem)
    {
        int sum = 0;
        List<int> allIdInventory = SearchForSameItem(idItem);
        for (int i = 0; i < allIdInventory.Count; i++)
        {
            sum += items[allIdInventory[i]].count;
        }
        return sum;
    }

    public void DeleteCertainAmountItem(int idItem, int count)
    {
        int sum = SumCountSameItem(idItem);
        if (sum != 0)
        {
            List<int> allIdInventory = SearchForSameItem(idItem);
            for (int i = 0; i < allIdInventory.Count; i++)
            {
                if (count >= items[allIdInventory[i]].count)
                {
                    count -= items[allIdInventory[i]].count;
                    DeleteItem(allIdInventory[i]);
                }
                else
                {
                    items[allIdInventory[i]].count -= count;
                    UpdateInventory();
                    return;
                }
            }
        }
        Item temp = SearchItemById(new []{idItem});
        if (temp == null) FindObjectOfType<TakeTools>().ClearTool();
    }

    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.img;

        if (count > 1 && item.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id,ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();

        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public int AddItemToInventory(Item item, int count)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == item.id && items[i].count < maxItemStack)
            {                
                items[i].count += count;
                if (items[i].count > maxItemStack)
                {
                    count = items[i].count - maxItemStack;
                    items[i].count = maxItemStack;
                    UpdateInventory();
                    return AddItemToInventory(item, count);
                }
                UpdateInventory();
                return 0;
            }
        }
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == 0)
            {
                items[i].id = item.id;
                items[i].count = count;
                items[i].itemGameObj.GetComponent<Image>().sprite = data.items[item.id].img;
                UpdateInventory();
                return 0;
            }            
        }
        UpdateInventory();
        return count;
    }

    public void ThrowItem(int idInventory)
    {
        if (items[idInventory].id != 0)
        {   
            GameObject throwItem = Instantiate(itemPrefab, transform.parent.parent.parent.position + new Vector3(0,2,0), new Quaternion());
            ItemPrefabClass itemPrefabClass = throwItem.GetComponent<ItemPrefabClass>();
            itemPrefabClass.ID = items[idInventory].id;
            itemPrefabClass.Count = items[idInventory].count;

            TakeTools _takeTools;
            _takeTools = FindObjectOfType<TakeTools>();
            if (items[idInventory].id == _takeTools.Id)
            {
                _takeTools.ClearTool();
            }

            items[idInventory].id = 0;
            items[idInventory].count = 0;
            items[idInventory].itemGameObj.GetComponent<Image>().sprite = data.items[0].img;
            UpdateInventory();
            
        }
    }

    public void DeleteItem(int idInventory)
    {
        if (items[idInventory].id != 0)
        {            
            items[idInventory].id = 0;
            items[idInventory].count = 0;
            items[idInventory].itemGameObj.GetComponent<Image>().sprite = data.items[0].img;
            UpdateInventory();
        }
    }

    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ii);
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
            }

            items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }
    }

    public void SelectObject()
    {
        if (currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;
            
            AddItem(currentID, data.items[0], 0);
        }
        else
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];
            if (currentItem.id != II.id)
            {
                AddInventoryItem(currentID, II);
                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if (II.count + currentItem.count <= maxItemStack)
                {
                    II.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[II.id],II.count + currentItem.count - maxItemStack);

                    II.count = maxItemStack;
                }

                II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
            }
            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
        UpdateInventory();
    }
    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        movingObject.position = pos;
    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemGameObj = old.itemGameObj;
        New.count = old.count;

        return New;
    }
}

[System.Serializable]
public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;

    public int count;
}