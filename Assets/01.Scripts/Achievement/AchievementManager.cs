using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>, ObservationObject
{
	public List<Observer> Obsevers 
	{
		get => _obsevers;
		set => _obsevers = value;
	}

	[SerializeField] private AchievementDataSO _achievementDataSO;

	private List<Observer> _obsevers = new List<Observer>();

	private void Start()
	{
		SendMessageToObsevers();
	}

	public void AddObserver(Observer observer)
	{
		Obsevers.Add(observer);
	}

	[ContextMenu("SendMessageToObsevers")]
	public void SendMessageToObsevers()
	{
		foreach(Observer observer in Obsevers)
		{
			observer.ReceiveMessage();
		}
	}

	/// <summary>
	/// �ش� ������ Ŭ�����ߴ��� üũ
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	public bool CheckHaveAchievement(int code)
	{
		return UserSaveDataManager.Instance.UserSaveData.haveAchievement.Contains(code);
	}


}
