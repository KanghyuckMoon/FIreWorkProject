using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserSaveDataManager : Singleton<UserSaveDataManager>
{
	public UserSaveData UserSaveData
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

	[SerializeField] private UserSaveData _userSaveData = null;
	private static string _dataPath = "";
	private static string _SaveFileName = "Save.txt";

	public override  void Awake()
	{
		base.Awake();

		_dataPath = Application.persistentDataPath + "/Save/";

		if (GetCheckBool())
		{
			Load();
		}
	}

	/// <summary>
	/// ���� ������ ����
	/// </summary>
	public static void Save()
	{
		//if (!File.Exists(_dataPath + _SaveFileName))
		//{
		//	Directory.CreateDirectory(_dataPath);
		//}
		//string jsonData = JsonUtility.ToJson(Instance.UserSaveData);
		//File.WriteAllText(_dataPath + _SaveFileName, jsonData);
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
			Instance.UserSaveData = saverData;
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
