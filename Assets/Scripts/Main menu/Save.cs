using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour
{
	private Inventory _inventory;
	private List<ItemInventory> _items;
	private int _count;
	private List<string> _allItemsInfo;
	private SaveObjects _saveObjects = new SaveObjects();
	private string _path = Application.streamingAssetsPath + "/inventory.json";
	private string _json;

	private void Start()
	{
		Load();
	}

	private void FindInventory()
	{
		_inventory = FindObjectOfType<Inventory>();
		_items = _inventory.items;
		_count = _items.Count;
		_allItemsInfo = new List<string>();
	}

	public void SaveAll()
	{
		FindInventory();
		for (int i = 0; i < _count; i++)
		{
			_allItemsInfo.Add(_items[i].id + " " + _items[i].count);
		}

		_saveObjects.items = _allItemsInfo;
		_saveObjects.caveOpen = true;
		_json = JsonUtility.ToJson(_saveObjects);
		File.WriteAllText(_path, _json);
	}

	private void Load()
	{
		_json = File.ReadAllText(_path);
		_saveObjects = JsonUtility.FromJson<SaveObjects>(_json);
		foreach (var item in _saveObjects.items)
		{
			Debug.Log(item);
		}
	}
}

[Serializable]
public class SaveObjects
{
	public List<string> items;
	public bool caveOpen;
}


