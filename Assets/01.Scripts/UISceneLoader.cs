using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneLoader : MonoBehaviour
{
	private void Start()
	{
		SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
	}
}
