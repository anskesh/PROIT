using System;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;
using System.IO;

public class Load : MonoBehaviour
{
	[CanBeNull] private Inventory _inventory;
	private List<ItemInventory> _items;
	private int _count;
	private List<string> _allItemsInfo;
	private SaveObjects _saveObjects;
	private ItemInventory _template;
	private readonly  string _path = Application.streamingAssetsPath + "/allDataToSave.json";
	private string _json;

	private void LoadJson()
	{
		_json = File.ReadAllText(_path);
		_saveObjects = JsonUtility.FromJson<SaveObjects>(_json);
	}

	public void LoadInventory()
	{
		LoadJson();
		_inventory = FindObjectOfType<Inventory>();
		if (!_inventory) return;
		
		var index = 0;
		foreach (var item in _saveObjects.items)
		{
			_template = new ItemInventory();
			var items = item.Split(' ');
			var id = Convert.ToInt32(items[0]);
			var count = Convert.ToInt32(items[1]);

			_template.id = id;
			_template.count = count;
			_inventory.AddInventoryItem(index, _template);
			index++;
		}
	}

	public void LoadTestInventory()
	{
		LoadJson();
		var _inv = FindObjectOfType<InventoryCell>();
		var _template = new List<ItemAsset>();
		
		foreach (var item in _saveObjects.items)
		{
			var items = item.Split(' ');
			var id = Convert.ToInt32(items[0]);
			var count = Convert.ToInt32(items[1]);
			
			var assetItem = _inv.SearchItemById(id);
			assetItem.Count = count;
			_template.Add(assetItem);
		}
		_inv.Render(_template);
	}
	
	public void LoadHealth()
	{
		LoadJson();
		FindObjectOfType<Player>().Health = _saveObjects.health;
	}
	
	public void LoadCave()
	{
		LoadJson();
		FindObjectOfType<OpenCave>().IsOpen = _saveObjects.caveOpen;
	}
}
