using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    
    public void SpawnResource(int[] id, int[] count)
    {
        for (int i = 0; i < id.Length; i++)
        {
            if (count[i] == 0) break;
            GameObject throwItem = Instantiate(itemPrefab, transform.position, new Quaternion());
            ItemPrefabClass itemPrefabClass = throwItem.GetComponent<ItemPrefabClass>();
            itemPrefabClass.ID = id[i];
            itemPrefabClass.Count = count[i];
        }
    }
}
