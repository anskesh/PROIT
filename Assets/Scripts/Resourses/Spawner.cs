using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    
    public void SpawnResource(int id, int count)
    {
        GameObject throwItem = Instantiate(itemPrefab, transform.position, new Quaternion());
        ItemPrefabClass itemPrefabClass = throwItem.GetComponent<ItemPrefabClass>();
        itemPrefabClass.ID = id;
        itemPrefabClass.Count = count;
    }
}
