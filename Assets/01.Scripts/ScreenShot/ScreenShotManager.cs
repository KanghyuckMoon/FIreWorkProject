using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotManager : MonoBehaviour
{
	private string _screenShot;
	WaitForSeconds waitTime = new WaitForSeconds(0.1F);
	WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

	private void Start()
	{
		_screenShot = $"{Application.dataPath}/ScreenShots/";
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.F12))
		{
			StartCoroutine(Capture());
		}
	}

	private IEnumerator Capture()
	{
		yield return waitTime;
		yield return frameEnd;

		Texture2D screenTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		Rect area = new Rect(0f, 0f, Screen.width, Screen.height);
		screenTex.ReadPixels(area, 0, 0);
		screenTex.LoadRawTextureData(screenTex.GetRawTextureData());
		screenTex.Apply();
		Save(screenTex);
		Destroy(screenTex);
	}

	private void Save(Texture2D screenTex)
	{
		// 폴더가 존재하지 않으면 새로 생성
		if (Directory.Exists(_screenShot) == false)
		{
			Directory.CreateDirectory(_screenShot);
		}

		int index = 1;
		string totalPath = $"{_screenShot }ScreenShot{index}.png";
		while (true)
		{
			if(File.Exists(totalPath))
			{
				index += 1;
				totalPath = $"{_screenShot }ScreenShot{index}.png";
			}
			else
			{
				break;
			}
		}

		// 스크린샷 저장
		File.WriteAllBytes(totalPath, screenTex.EncodeToPNG());

	}


}
