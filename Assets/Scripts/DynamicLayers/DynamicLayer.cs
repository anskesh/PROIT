using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayer : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.TryGetComponent(out IMineable mineable))
		{
			float x = transform.position.x;
			float y = transform.position.y;
			if (col.transform.position.y < transform.position.y) transform.position = new Vector3(x, y, 1);
			else if (col.transform.position.y > transform.position.y) transform.position = new Vector3(x, y, 0);
		}
	}
}
