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
	private readonly  string _path = Application.streamingAssetsPath + "/allDataToSave.json";
	private string _json;

	public void SaveAll(int sceneNumber)
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
		if (!_inventory) return;
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
		if (health != null) _saveObjects.health = health.Health;
	}

	private void SaveCave()
	{
		var cave = FindObjectOfType<OpenCave>();
		if (cave != null) _saveObjects.caveOpen = cave.IsOpen;
	}
	
	public void NewGame()
	{
		_json = "{\"items\":[\"0 0\",\"0 0\",\"0 0\",\"0 0\",\"0 0\",\"0 0\",\"0 0\",\"0 0\",\"0 0\",\"0 0\",\"0 0\",\"0 0\"],\"caveOpen\":false,\"health\":100}";
	}
}

[Serializable]
public class SaveObjects
{
	public List<string> items;
	public bool caveOpen;
	public int health;
}


