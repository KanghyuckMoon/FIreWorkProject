using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementChecker
{
	private List<Achievement> _achievements = new List<Achievement>();

	public AchievementChecker()
	{
		_achievements.Add(new Achievement(0, x => x.happy >= 1000));
	}

	public void CheckAchievement()
	{
		foreach(var achievement in _achievements)
		{
			if(achievement.isAchieve)
			{
				return;
			}
			else
			{
				if(achievement.requirement.Invoke(UserSaveDataManager.Instance.UserSaveData) == true)
				{
					UserSaveDataManager.Instance.UserSaveData.haveAchievement.Add(achievement.itemCode);
					achievement.isAchieve = true;
					AchievementManager.Instance.SendMessageToObsevers();
				}
			}
		}
	}
}

public class Achievement
{
	public Achievement(int itemCode, Predicate<UserSaveData> requirement)
	{
		this.itemCode = itemCode;
		this.requirement = requirement;
	}

	public int itemCode;
	public Predicate<UserSaveData> requirement;
	public bool isAchieve;
}