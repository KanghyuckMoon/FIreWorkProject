using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AchievementMethod
{
	public static void TestMethod(object obj)
	{
		Debug.Log("abs");
	}

	/// <summary>
	/// ÄÆ¾À ¿ÀÇÂ
	/// </summary>
	/// <param name="address"></param>
	public static void OpenCutScene(string index)
	{
		GameObject.FindObjectOfType<TalkManager>().OpenCutScene(Convert.ToInt16(index));
	}

}
