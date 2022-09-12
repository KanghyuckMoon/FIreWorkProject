using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public static class TalkMethod
{

	/// <summary>
	/// 테스트용 함수
	/// </summary>
	/// <param name="sceneName"></param>
	public static void TestFunction(object obj)
	{
		Debug.Log("HelloWorld");
	}

	public static void ShakeImage(object name, object power)
    {
		Sequence seq = DOTween.Sequence();
		float p = float.Parse((string)power);

		GameObject _image = GameObject.Find((string)name);
		Vector3 originPos = _image.transform.position;
		
		seq.Append(_image.transform.DOShakePosition(0.2f, p, 80));

		seq.Append(_image.transform.DOMove(originPos, 0.1f));
    }
}
