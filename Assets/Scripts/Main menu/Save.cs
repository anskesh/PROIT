using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;

public class Save : MonoBehaviour
{
	[CanBeNull] private Inventory _inventory;
	private List<ItemInventory> _items;
	private int _count;
	private List<string> _allItemsInfo;
	private SaveObjects _saveObjects = new SaveObjects();
	private readonly  string _path = Application.streamingAssetsPath + "/allDataToSave.json";
	private string _json;
	private ItemInventory _template;

	/*private void Start()
	{
		Load();
	}*/

	private void FindInventory()
	{
		_inventory = FindObjectOfType<Inventory>();
		if (!_inventory) return;
		_items = _inventory.items;
		_count = _items.Count;
		_allItemsInfo = new List<string>();
	}

	public void SaveAll()
	{
		FindInventory();
		if (!_inventory) return;
		for (int i = 0; i < _count; i++)
		{
			_allItemsInfo.Add(_items[i].id + " " + _items[i].count);
		}

		_saveObjects.items = _allItemsInfo;
		_saveObjects.caveOpen = true;
		_json = JsonUtility.ToJson(_saveObjects);
		File.WriteAllText(_path, _json);
	}

	public void Load()
	{
		FindInventory();
		if (!_inventory) return;
		int id = 0;
		int count = 0;
		string[] items;
		_json = File.ReadAllText(_path);
		_saveObjects = JsonUtility.FromJson<SaveObjects>(_json);
		
		int index = 0;
		foreach (var item in _saveObjects.items)
		{
			_template = new ItemInventory();
			items = item.Split(' ');
			id = Convert.ToInt32(items[0]);
			count = Convert.ToInt32(items[1]);

			_template.id = id;
			_template.count = count;
			_inventory.AddInventoryItem(index, _template);
			index++;

		}
	}
	public void NewGame()
	{
		_json = "{}";
		File.WriteAllText(_path, _json);
	}
}

[Serializable]
public class SaveObjects
{
	public List<string> items;
	public bool caveOpen;
}


