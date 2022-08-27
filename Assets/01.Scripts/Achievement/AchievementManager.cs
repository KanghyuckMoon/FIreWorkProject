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
	private AchievementChecker _achievementChecker = null;

	private void Start()
	{
		_achievementChecker = new AchievementChecker(_achievementDataSO);
		SendMessageToObsevers();
	}

	private void Update()
	{
		_achievementChecker.CheckAchievement();
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
	/// 해당 업적을 클리어했는지 체크
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	public bool CheckHaveAchievement(int code)
	{
		return UserSaveDataManager.Instance.UserSaveData.haveAchievement.Contains(code);
	}


}
