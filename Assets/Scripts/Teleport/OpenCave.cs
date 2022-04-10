using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenCave : MonoBehaviour
{
	[SerializeField] private Sprite openCave;
	public bool IsOpen { get; set; } = false;

	private void Start()
	{
		FindObjectOfType<Load>().LoadCave();
		if (IsOpen)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = openCave;
			gameObject.GetComponent<CircleCollider2D>().enabled = false;
			var child = gameObject.transform.GetChild(0).gameObject;
			child.SetActive(true);
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.TryGetComponent(out Movement player) && !IsOpen)
		{
			gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
			if (Input.GetKey(KeyCode.E) && HasKey())
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = openCave;
				gameObject.GetComponent<CircleCollider2D>().enabled = false;
				var child = gameObject.transform.GetChild(0).gameObject;
				child.SetActive(true);
				IsOpen = true;
				var save = FindObjectOfType<Save>();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.TryGetComponent(out Movement player))
		{
			gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
		}
	}

	private bool HasKey()
	{
		var key = FindObjectOfType<Inventory>().SearchForSameItem(13);
		if (key.Count == 0) StartCoroutine(Utils.ShowText("ключ"));
		return key.Count > 0;
	}
}
