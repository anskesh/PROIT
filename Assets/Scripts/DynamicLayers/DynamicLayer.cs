using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayer : MonoBehaviour
{
	private SpriteRenderer _sprite; 
	private Transform _target;
	private void Awake()
	{
		if (gameObject.TryGetComponent(out SpriteRenderer sprite)) _sprite = sprite;
		else _sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
		_target = FindObjectOfType<PlayerConfig>().transform.GetChild(0);
	}

	private void LateUpdate()
	{
		if (_target.position.y > transform.position.y)
		{
			_sprite.sortingOrder = 2;
		}
		else
		{
			_sprite.sortingOrder = 0;
		}
	}
}
