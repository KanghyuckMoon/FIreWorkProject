using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
	/// <summary>
	/// ���� ����
	/// </summary>
	public void QuitGame()
	{
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit(); // ���ø����̼� ����
#endif
	}
}
