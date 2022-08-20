using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementChecker
{
	private List<Achievement> _achievements = new List<Achievement>();

	public AchievementChecker()
	{
		_achievements.Add(new Achievement(0, x => x.happy >= 100));
		_achievements.Add(new Achievement(1, x => x.happy >= 200));
		_achievements.Add(new Achievement(2, x => x.happy >= 300));
		_achievements.Add(new Achievement(3, x => x.happy >= 400));
		_achievements.Add(new Achievement(4, x => x.happy >= 500));
		_achievements.Add(new Achievement(5, x => x.happy >= 600));
		_achievements.Add(new Achievement(6, x => x.happy >= 700));
		_achievements.Add(new Achievement(7, x => x.happy >= 800));
		_achievements.Add(new Achievement(8, x => x.happy >= 900));
		_achievements.Add(new Achievement(9, x => x.happy >= 1000));
		_achievements.Add(new Achievement(10, x => x.happy >= 1100));
		_achievements.Add(new Achievement(11, x => x.happy >= 1200));
	}

	public void CheckAchievement()
	{
		foreach(var achievement in _achievements)
		{
			if(achievement.isAchieve)
			{
				continue;
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