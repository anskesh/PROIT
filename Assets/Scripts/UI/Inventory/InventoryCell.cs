using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryCell : MonoBehaviour
{
	[SerializeField] private List<ItemAsset> _items;
	[SerializeField] private GameObject _template;
	[SerializeField] private int maxStack = 32;
	[SerializeField] private int countCell = 12;
	
	private List<Cell> _inventory = new List<Cell>();
	
	public int CountCell => countCell;
	public int MaxStack => maxStack;
	public int Column { get; } = 6;
	
	public event Action<List<Cell>> InventoryChanged;
	
	private void Awake()
	{
		_inventory = GetComponentsInChildren<Cell>().ToList();
		
	}

	private void Start()
	{
		FindObjectOfType<Load>().LoadTestInventory();
	}

	public void Render(List<ItemAsset> items)
	{
		int index = 0;
		foreach (var itemAsset in items)
		{
			var cell = _inventory[index];
			cell.Render(itemAsset);
			index++;
			
		}
		InventoryChanged?.Invoke(GetSomeCells());
	}

	public int AddItem(int id, int count)
	{
		foreach (var cell in _inventory)
		{
			if (cell.ID == id && cell.Count < maxStack)
			{
				if (cell.Count + count > maxStack)
				{
					count -= maxStack - cell.Count;
					cell.Count = maxStack;
					cell.ChangeCount();
					InventoryChanged?.Invoke(GetSomeCells());
				}
				else
				{
					cell.Count += count;
					cell.ChangeCount();
					InventoryChanged?.Invoke(GetSomeCells());
					return 0;
				}
			}
			if (cell.Count == 0 && count > 0 && count <= maxStack)
			{
				var item = SearchItemById(id);
				item.Count = count;
				cell.Render(item);
				InventoryChanged?.Invoke(GetSomeCells());
				return 0;
			}
		}
		InventoryChanged?.Invoke(GetSomeCells());
		return count;
	}
	
	public ItemAsset SearchItemById(int id)
	{
		foreach (var item in _items)
		{
			if (item.ID == id) return item;
		}
		return _items[0];
	}

	public List<Cell> GetSomeCells(int count = 6)
	{
		var cells = new List<Cell>();
		for (int i = 0; i < count; i++)
		{
			cells.Add(_inventory[i]);
		}
		return cells;
	}
	
	public void ThrowItem(int idCell, Transform parent)
	{
		var cell = _inventory[idCell];
		if (cell.Count == 0) return;
		
		GameObject throwItem = Instantiate(_template, parent.position + new Vector3(2.5f, 0, 0), new Quaternion());
		ItemPrefabClass itemPrefabClass = throwItem.GetComponent<ItemPrefabClass>();
		
		itemPrefabClass.ID = cell.ID;
		itemPrefabClass.Count = cell.Count;
		
		cell.Clear();
		
		InventoryChanged?.Invoke(GetSomeCells());
	}
}
