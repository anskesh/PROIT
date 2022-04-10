using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Inventory _inventory;
    
    private Item colItem;
    private int countPickup;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _inventory.ThrowItem(0, transform);            
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            colItem = collision.GetComponent<ItemPrefabClass>().item;
            countPickup = collision.GetComponent<ItemPrefabClass>().Count;
            countPickup = _inventory.AddItemToInventory(colItem, countPickup);
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
