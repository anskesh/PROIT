using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportTrigger : MonoBehaviour
{
	[SerializeField] private UnityEvent ChangeScene;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			ChangeScene?.Invoke();
		}
	}
}
