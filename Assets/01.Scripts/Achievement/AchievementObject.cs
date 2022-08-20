using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementObject : MonoBehaviour, Observer
{
	public ObservationObject ObservationObject 
	{
		get => AchievementManager.Instance;
		set
		{
			
		}
	}

	[SerializeField] private int _achievementCode = 0;

	private void Awake()
	{
		AddObservation();
		gameObject.SetActive(false);
	}

	public void AddObservation()
	{
		ObservationObject.AddObserver(this);
	}

	public void ReceiveMessage()
	{
		if(AchievementManager.Instance.CheckHaveAchievement(_achievementCode))
		{
			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}


}
