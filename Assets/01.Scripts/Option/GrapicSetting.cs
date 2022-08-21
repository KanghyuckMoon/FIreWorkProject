using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapicSetting : MonoBehaviour
{
	public bool IsFoolScreen
	{
		get
		{
			return _isFoolScreen;
		}
		set
		{
			_isFoolScreen = value;
		}
	}

	public int Width
	{
		get
		{
			return _width;
		}
		set
		{
			_width = value;
		}
	}

	public int Height
	{
		get
		{
			return _height;
		}
		set
		{
			_height = value;
		}
	}

	private bool _isFoolScreen = true;
	private int _width = 1920;
	private int _height = 1080;

	/// <summary>
	/// ����Ƽ ���� ����
	/// </summary>
	/// <param name="index"></param>
	public void ChangeGrapicSetting(int index)
	{
		QualitySettings.SetQualityLevel(index, true);
	}

	/// <summary>
	/// ��üȭ�� ���� ����
	/// </summary>
	public void ChangeFoolScreen()
	{
		IsFoolScreen = !IsFoolScreen;
	}

	/// <summary>
	/// ȭ�� �ػ� ����
	/// </summary>
	/// <param name="width"></param>
	/// <param name="height"></param>
	public void ChangeSize(int width, int height)
	{
		Width = width;
		Height = height;
	}

	/// <summary>
	/// ȭ�� ���� ����
	/// </summary>
	public void ApplySettingScreen()
	{
		Screen.SetResolution(Width, Height, IsFoolScreen);
	}
}