using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementChecker
{
	private PopUpManager _popUpManager;

	public PopUpManager PopUpManager
	{
		get
		{
			_popUpManager ??= GameObject.FindObjectOfType<PopUpManager>();
			return _popUpManager;
		}
	}



	public AchievementChecker(AchievementDataSO achievementDataSO)
	{
		_achievementDataSO = achievementDataSO;
		_achievements.Add(new Achievement(0, x => x.happy >= 100)); //나무
		_achievements.Add(new Achievement(1, x => x.happy >= 200)); //돌
		_achievements.Add(new Achievement(2, x => x.happy >= 450)); //잔디
		_achievements.Add(new Achievement(3, x => x.happy >= 600)); //행성잔디
		_achievements.Add(new Achievement(4, x => x.happy >= 800)); //구름1,2
		_achievements.Add(new Achievement(5, x => x.happy >= 1000)); //구름3,4
		_achievements.Add(new Achievement(6, x => x.happy >= 2000)); //달
		_achievements.Add(new Achievement(7, x => x.happy >= 3000)); //집1
		_achievements.Add(new Achievement(8, x => x.happy >= 4000)); //집2
		_achievements.Add(new Achievement(9, x => x.happy >= 5000)); //집3
		_achievements.Add(new Achievement(10, x => x.happy >= 6000)); //집4
		_achievements.Add(new Achievement(11, x => x.happy >=  7000)); //집5
		_achievements.Add(new Achievement(12, x => x.happy >= 8000)); //집6
		_achievements.Add(new Achievement(13, x => x.further1ColorItemCode == x.further2ColorItemCode)); //하트 구름
		_achievements.Add(new Achievement(14, x => x.happy >= 11000)); //램프1
		_achievements.Add(new Achievement(15, x => x.happy >= 12500)); //램프2
		_achievements.Add(new Achievement(16, x => x.happy >= 14000)); //램프3
		_achievements.Add(new Achievement(17, x => x.happy >= 15500)); //램프4
		_achievements.Add(new Achievement(18, x => x.happy >= 17000)); //램프5
		_achievements.Add(new Achievement(19, x => x.happy >= 20000)); //유니티짱1
		_achievements.Add(new Achievement(20, x => x.money >= 100)); //또다른행성
		_achievements.Add(new Achievement(21, x => x.happy >= 25000 && x.money >= 100)); //또행성의 잔디
		_achievements.Add(new Achievement(22, x => x.happy >= 30000 && x.money >= 100)); //또행성의 나무
		_achievements.Add(new Achievement(23, x => x.happy >= 35000 && x.money >= 100)); //또행성의 구름
		_achievements.Add(new Achievement(24, x => x.happy >= 40000 && x.money >= 100)); //또행성의 집1
		_achievements.Add(new Achievement(25, x => x.happy >= 45000 && x.money >= 100)); //또행성의 집2
		_achievements.Add(new Achievement(26, x => x.happy >= 50000 && x.money >= 100)); //또행성의 집3
		_achievements.Add(new Achievement(27, x => x.happy >= 55000 && x.money >= 100)); //또행성의 집4
		_achievements.Add(new Achievement(28, x => x.money >= 70000 && x.money >= 100)); //또행성의 유니티짱
		_achievements.Add(new Achievement(29, x => x.happy >= 85000)); //유니티짱2
		_achievements.Add(new Achievement(30, x => x.happy >= 30000)); //2차 폭발 개방
		_achievements.Add(new Achievement(31, x => x.happy >= 80000)); //3차 폭발 개방
		_achievements.Add(new Achievement(32, x => x.happy >= 150000)); //4차 폭발 개방
		_achievements.Add(new Achievement(33, x => x.haveAchievement.Count >= 5)); //라이브러리 개방
		_achievements.Add(new Achievement(34, x => x.happy >= 50000)); //리뉴얼 개방
		_achievements.Add(new Achievement(35, x => x.renewal >= 1)); //빛조절 개방
		_achievements.Add(new Achievement(36, x => x.renewal >= 2)); //크기조절 개방
		_achievements.Add(new Achievement(37, x => x.money >= 1000)); //유니티짱3 개방
		_achievements.Add(new Achievement(38, x => x.money >= 2000)); //장식구1 개방
		_achievements.Add(new Achievement(39, x => x.money >= 3000)); //장식구2 개방
		_achievements.Add(new Achievement(40, x => x.money >= 4000)); //색깔아이템1세트 개방
		_achievements.Add(new Achievement(41, x => x.money >= 5000)); //모양아이템1세트 개방
		_achievements.Add(new Achievement(42, x => x.money >= 6000)); //색깔아이템2세트 개방
		_achievements.Add(new Achievement(43, x => x.money >= 7000)); //모양아이템2세트 개방
		_achievements.Add(new Achievement(44, x => x.money >= 8000)); //색깔아이템마지막세트 개방
		_achievements.Add(new Achievement(45, x => x.money >= 9000)); //모양아이템마지막세트 개방
		_achievements.Add(new Achievement(46, x => _click >= 50)); //특별한 색깔 아이템 추가
		_achievements.Add(new Achievement(47, x => Time.time > 3600)); //특별한 모양 아이템 추가
		_achievements.Add(new Achievement(48, x => x.haveAchievement.Count >= 49)); //엔딩
		_achievements.Add(new Achievement(49, x => true)); //게임 시작
		_achievements.Add(new Achievement(50, x => x.happy >= 50)); //강화에 대해
		_achievements.Add(new Achievement(51, x => x.haveAchievement.Count >= 20)); //많은 업적 달성
		_achievements.Add(new Achievement(52, x => x.haveAchievement.Count >= 20 && x.renewal >= 1)); //다시 한번 많은 업적 달성!
		_achievements.Add(new Achievement(53, x => x.happy >= 55000 && x.haveAchievement.Count >= 7)); //상점 개방
	}

	private AchievementDataSO _achievementDataSO = null;
	private List<Achievement> _achievements = new List<Achievement>();
	public static int Click
	{
		get
		{
			return _click;
		}
		set
		{
			_click = value;
		}
	}
	private static int _click = 0;

	public void CheckAchievement()
	{
		foreach(var achievement in _achievements)
		{
			if(UserSaveDataManager.Instance.UserSaveData.haveAchievement.Contains(achievement.itemCode))
			{
				continue;
			}
			else
			{
				if(achievement.requirement.Invoke(UserSaveDataManager.Instance.UserSaveData) == true)
				{
					UserSaveDataManager.Instance.UserSaveData.haveAchievement.Add(achievement.itemCode);
					AchievementManager.Instance.SendMessageToObsevers();
					FunctionInvoke(achievement.itemCode);

					var achievementData = _achievementDataSO._achievementDatas.Find(x => x._achievementCode == achievement.itemCode);

					if (!achievementData._isCantView)
					{
						PopUpManager.SetAchievement(achievementData);
					}

				}
			}
		}
	}

	/// <summary>
	/// 업적 가지기
	/// </summary>
	/// <param name="index"></param>
	public void GetAchievement(int index)
	{
		var achievement = _achievements.Find(x => x.itemCode == index);

		UserSaveDataManager.Instance.UserSaveData.haveAchievement.Add(achievement.itemCode);
		AchievementManager.Instance.SendMessageToObsevers();
		FunctionInvoke(achievement.itemCode);

		var achievementData = _achievementDataSO._achievementDatas.Find(x => x._achievementCode == achievement.itemCode);

		if (!achievementData._isCantView)
		{
			PopUpManager.SetAchievement(achievementData);
		}
	}

	/// <summary>
	/// 함수 실행
	/// </summary>
	public void FunctionInvoke(int itemCode)
	{
		var achievementData = _achievementDataSO._achievementDatas.Find(x => x._achievementCode == itemCode);
		if(achievementData._functionName == null || achievementData._functionName == "")
		{
			return;
		}

		Type type = typeof(AchievementMethod);
		MethodInfo myClass_FunCallme = type.GetMethod(achievementData._functionName, BindingFlags.Static | BindingFlags.Public);
		myClass_FunCallme.Invoke(null, new object[] { achievementData._functionParameter });
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
}