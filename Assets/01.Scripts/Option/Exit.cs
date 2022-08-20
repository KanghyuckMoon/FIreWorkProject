using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
	/// <summary>
	/// 게임 종료
	/// </summary>
	public void QuitGame()
	{
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit(); // 어플리케이션 종료
#endif
	}
}
