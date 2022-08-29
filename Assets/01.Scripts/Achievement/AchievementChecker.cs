using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementChecker
{
	public AchievementChecker(AchievementDataSO achievementDataSO)
	{
		_achievementDataSO = achievementDataSO;
		_achievements.Add(new Achievement(0, x => x.happy >= 100)); //나무
		_achievements.Add(new Achievement(1, x => x.happy >= 300)); //돌
		_achievements.Add(new Achievement(2, x => x.happy >= 700)); //잔디
		_achievements.Add(new Achievement(3, x => x.happy >= 1500)); //행성잔디
		_achievements.Add(new Achievement(4, x => x.happy >= 3100)); //구름1,2
		_achievements.Add(new Achievement(5, x => x.happy >= 6300)); //구름3,4
		_achievements.Add(new Achievement(6, x => x.happy >= 12700)); //달
		_achievements.Add(new Achievement(7, x => x.happy >= 20000)); //집1
		_achievements.Add(new Achievement(8, x => x.happy >= 21000)); //집2
		_achievements.Add(new Achievement(9, x => x.happy >= 22000)); //집3
		_achievements.Add(new Achievement(10, x => x.happy >= 23000)); //집4
		_achievements.Add(new Achievement(11, x => x.happy >= 24000)); //집5
		_achievements.Add(new Achievement(12, x => x.happy >= 25000)); //집6
		_achievements.Add(new Achievement(13, x => x.further1ColorItemCode == x.further2ColorItemCode && x.further2ColorItemCode == x.further3ColorItemCode && x.further3ColorItemCode == x.further4ColorItemCode)); //하트 구름
		_achievements.Add(new Achievement(14, x => x.happy >= 30000)); //램프1
		_achievements.Add(new Achievement(15, x => x.happy >= 31000)); //램프2
		_achievements.Add(new Achievement(16, x => x.happy >= 32000)); //램프3
		_achievements.Add(new Achievement(17, x => x.happy >= 33000)); //램프4
		_achievements.Add(new Achievement(18, x => x.happy >= 34000)); //램프5
		_achievements.Add(new Achievement(19, x => x.happy >= 40000)); //유니티짱1
		_achievements.Add(new Achievement(20, x => x.money >= 1000)); //또다른행성
		_achievements.Add(new Achievement(21, x => x.happy >= 80000)); //또행성의 잔디
		_achievements.Add(new Achievement(22, x => x.happy >= 90000)); //또행성의 나무
		_achievements.Add(new Achievement(23, x => x.happy >= 100000)); //또행성의 구름
		_achievements.Add(new Achievement(24, x => x.happy >= 20000)); //또행성의 집1
		_achievements.Add(new Achievement(25, x => x.happy >= 22000)); //또행성의 집2
		_achievements.Add(new Achievement(26, x => x.happy >= 24000)); //또행성의 집3
		_achievements.Add(new Achievement(27, x => x.happy >= 26000)); //또행성의 집4
		_achievements.Add(new Achievement(28, x => x.money >= 10000)); //또행성의 유니티짱
		_achievements.Add(new Achievement(29, x => x.happy >= 1200)); //유니티짱2
		_achievements.Add(new Achievement(30, x => x.happy >= 10000)); //2차 폭발 개방
		_achievements.Add(new Achievement(31, x => x.happy >= 100000)); //3차 폭발 개방
		_achievements.Add(new Achievement(32, x => x.happy >= 1000000)); //4차 폭발 개방
		_achievements.Add(new Achievement(33, x => x.happy >= 1500 && x.haveAchievement.Count >= 5)); //상점 개방
		_achievements.Add(new Achievement(34, x => x.haveAchievement.Count >= 3)); //라이브러리 개방
		_achievements.Add(new Achievement(35, x => x.happy >= 30000)); //리뉴얼 개방
		_achievements.Add(new Achievement(36, x => x.renewal >= 1)); //빛조절 개방
		_achievements.Add(new Achievement(37, x => x.renewal >= 2)); //크기조절 개방
		_achievements.Add(new Achievement(38, x => x.money >= 20000)); //유니티짱3 개방
		_achievements.Add(new Achievement(39, x => x.money >= 30000)); //장식구1 개방
		_achievements.Add(new Achievement(40, x => x.money >= 40000)); //장식구2 개방
		_achievements.Add(new Achievement(41, x => x.money >= 50000)); //색깔아이템1세트 개방
		_achievements.Add(new Achievement(42, x => x.money >= 60000)); //모양아이템1세트 개방
		_achievements.Add(new Achievement(43, x => x.money >= 70000)); //색깔아이템2세트 개방
		_achievements.Add(new Achievement(44, x => x.money >= 80000)); //모양아이템2세트 개방
		_achievements.Add(new Achievement(45, x => x.money >= 90000)); //색깔아이템마지막세트 개방
		_achievements.Add(new Achievement(46, x => x.money >= 100000)); //모양아이템마지막세트 개방
		_achievements.Add(new Achievement(47, x => _click >= 50)); //특별한 색깔 아이템 추가
		_achievements.Add(new Achievement(48, x => Time.time > 3600)); //특별한 모양 아이템 추가
		_achievements.Add(new Achievement(49, x => x.haveAchievement.Count >= 49)); //엔딩
		_achievements.Add(new Achievement(50, x => true)); //게임 시작
		_achievements.Add(new Achievement(51, x => x.happy >= 1000)); //강화에 대해
		_achievements.Add(new Achievement(52, x => x.haveAchievement.Count >= 20)); //많은 업적 달성
		_achievements.Add(new Achievement(53, x => x.haveAchievement.Count >= 20 && x.renewal >= 1)); //다시 한번 많은 업적 달성!
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
				}
			}
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