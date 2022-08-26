using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementGet : MonoBehaviour, Observer
{
	public ObservationObject ObservationObject 
	{
		get => _observationObject;
		set => _observationObject = value;
	}

	private ObservationObject _observationObject;

	[SerializeField] private int _achievementCode = 0;

	public void AddObservation()
	{
		ObservationObject.AddObserver(this);
	}

	public void ReceiveMessage()
	{
		UserSaveDataManager.Instance.UserSaveData.haveAchievement.Contains(_achievementCode);
	}

	/// <summary>
	/// ������ �޼��ߴ��� Ȯ���ϴ� �ڵ�
	/// </summary>
	public static bool CheckAchievement(int achievementCode)
	{
		return UserSaveDataManager.Instance.UserSaveData.haveAchievement.Contains(achievementCode);
	}
}
