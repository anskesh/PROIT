using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangerScenes : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			ChangeScene(0);
		}
	}

	public void ChangeScene(int targetScene)
	{
		if (gameObject.TryGetComponent(out Save save))
		{
			save.SaveAll();
		}
		SceneManager.LoadScene(targetScene);
	}
}
