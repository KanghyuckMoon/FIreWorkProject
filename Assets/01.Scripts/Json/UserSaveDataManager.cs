using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserSaveDataManager : Singleton<UserSaveDataManager>
{
	public static UserSaveData UserSaveData
	{ 
		get
		{
			return _userSaveData;
		}
		set
		{
			_userSaveData = value;
		}
	}

	private static UserSaveData _userSaveData = null;
	private static string _dataPath = Application.persistentDataPath + "/Save/";
	private static string _SaveFileName = "Save.txt";

	private void Awake()
	{
		if(GetCheckBool())
		{
			Load();
		}
	}

	/// <summary>
	/// ���� ������ ����
	/// </summary>
	public static void Save()
	{
		if (!File.Exists(_dataPath + _SaveFileName))
		{
			Directory.CreateDirectory(_dataPath);
		}
		string jsonData = JsonUtility.ToJson(UserSaveData);
		File.WriteAllText(_dataPath + _SaveFileName, jsonData);
	}

	/// <summary>
	/// ���� ������ �ҷ�����
	/// </summary>
	public static void Load()
	{
		if (File.Exists(_dataPath + _SaveFileName))
		{
			string jsonData = File.ReadAllText(_dataPath + _SaveFileName);
			UserSaveData saverData = JsonUtility.FromJson<UserSaveData>(jsonData);
			UserSaveData = saverData;
		}
	}

	/// <summary>
	/// ���̺��� ���� �ִ��� üũ
	/// </summary>
	/// <returns></returns>
	public static bool GetCheckBool()
	{
		return File.Exists(_dataPath + _SaveFileName);

	}

}
