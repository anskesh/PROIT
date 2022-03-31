using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
	[CanBeNull] private Inventory _inventory;
	private List<ItemInventory> _items;
	private int _count;
	private List<string> _allItemsInfo;
	private SaveObjects _saveObjects = new SaveObjects();
	private readonly string _path = Application.streamingAssetsPath + "/allDataToSave.json";
	private string _json;

	public void SaveAll()
	{
		SaveCave();
		SaveHealth();
		SaveInventory();
		_json = JsonUtility.ToJson(_saveObjects);
		File.WriteAllText(_path, _json);
	}

	private void SaveInventory()
	{
		_inventory = FindObjectOfType<Inventory>();
		if (_inventory == null) return;
		_items = _inventory.items;
		_count = _items.Count;
		_allItemsInfo = new List<string>();
		for (var i = 0; i < _count; i++)
		{
			_allItemsInfo.Add(_items[i].id + " " + _items[i].count);
		}
		_saveObjects.items = _allItemsInfo;
	}
	private void SaveHealth()
	{
		var health = FindObjectOfType<PlayerHealth>();
		if (health == null) return;
		_saveObjects.health = health.Health;
	}

	private void SaveCave()
	{
		var cave = FindObjectOfType<OpenCave>();
		if (cave == null) return;
		_saveObjects.caveOpen = cave.IsOpen;
	}

	public void Resume()
	{
		_json = File.ReadAllText(_path);
		_saveObjects = JsonUtility.FromJson<SaveObjects>(_json);
	}
	
	public void NewGame()
	{
		_saveObjects.health = 100;
		_saveObjects.caveOpen = false;
		_saveObjects.items = new List<string> {"0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0"};
	}
}

[Serializable]
public class SaveObjects
{
	public List<string> items;
	public bool caveOpen;
	public int health;
}


