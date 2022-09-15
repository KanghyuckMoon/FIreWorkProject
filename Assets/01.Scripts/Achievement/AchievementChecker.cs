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
		_achievements.Add(new Achievement(0, x => x.happy >= 100)); //����
		_achievements.Add(new Achievement(1, x => x.happy >= 200)); //��
		_achievements.Add(new Achievement(2, x => x.happy >= 450)); //�ܵ�
		_achievements.Add(new Achievement(3, x => x.happy >= 600)); //�༺�ܵ�
		_achievements.Add(new Achievement(4, x => x.happy >= 800)); //����1,2
		_achievements.Add(new Achievement(5, x => x.happy >= 1000)); //����3,4
		_achievements.Add(new Achievement(6, x => x.happy >= 2000)); //��
		_achievements.Add(new Achievement(7, x => x.happy >= 3000)); //��1
		_achievements.Add(new Achievement(8, x => x.happy >= 4000)); //��2
		_achievements.Add(new Achievement(9, x => x.happy >= 5000)); //��3
		_achievements.Add(new Achievement(10, x => x.happy >= 6000)); //��4
		_achievements.Add(new Achievement(11, x => x.happy >=  7000)); //��5
		_achievements.Add(new Achievement(12, x => x.happy >= 8000)); //��6
		_achievements.Add(new Achievement(13, x => x.further1ColorItemCode == x.further2ColorItemCode)); //��Ʈ ����
		_achievements.Add(new Achievement(14, x => x.happy >= 11000)); //����1
		_achievements.Add(new Achievement(15, x => x.happy >= 12500)); //����2
		_achievements.Add(new Achievement(16, x => x.happy >= 14000)); //����3
		_achievements.Add(new Achievement(17, x => x.happy >= 15500)); //����4
		_achievements.Add(new Achievement(18, x => x.happy >= 17000)); //����5
		_achievements.Add(new Achievement(19, x => x.happy >= 20000)); //����Ƽ¯1
		_achievements.Add(new Achievement(20, x => x.money >= 100)); //�Ǵٸ��༺
		_achievements.Add(new Achievement(21, x => x.happy >= 25000 && x.money >= 100)); //���༺�� �ܵ�
		_achievements.Add(new Achievement(22, x => x.happy >= 30000 && x.money >= 100)); //���༺�� ����
		_achievements.Add(new Achievement(23, x => x.happy >= 35000 && x.money >= 100)); //���༺�� ����
		_achievements.Add(new Achievement(24, x => x.happy >= 40000 && x.money >= 100)); //���༺�� ��1
		_achievements.Add(new Achievement(25, x => x.happy >= 45000 && x.money >= 100)); //���༺�� ��2
		_achievements.Add(new Achievement(26, x => x.happy >= 50000 && x.money >= 100)); //���༺�� ��3
		_achievements.Add(new Achievement(27, x => x.happy >= 55000 && x.money >= 100)); //���༺�� ��4
		_achievements.Add(new Achievement(28, x => x.money >= 70000 && x.money >= 100)); //���༺�� ����Ƽ¯
		_achievements.Add(new Achievement(29, x => x.happy >= 85000)); //����Ƽ¯2
		_achievements.Add(new Achievement(30, x => x.happy >= 30000)); //2�� ���� ����
		_achievements.Add(new Achievement(31, x => x.happy >= 80000)); //3�� ���� ����
		_achievements.Add(new Achievement(32, x => x.happy >= 150000)); //4�� ���� ����
		_achievements.Add(new Achievement(33, x => x.haveAchievement.Count >= 5)); //���̺귯�� ����
		_achievements.Add(new Achievement(34, x => x.happy >= 50000)); //������ ����
		_achievements.Add(new Achievement(35, x => x.renewal >= 1)); //������ ����
		_achievements.Add(new Achievement(36, x => x.renewal >= 2)); //ũ������ ����
		_achievements.Add(new Achievement(37, x => x.money >= 1000)); //����Ƽ¯3 ����
		_achievements.Add(new Achievement(38, x => x.money >= 2000)); //��ı�1 ����
		_achievements.Add(new Achievement(39, x => x.money >= 3000)); //��ı�2 ����
		_achievements.Add(new Achievement(40, x => x.money >= 4000)); //���������1��Ʈ ����
		_achievements.Add(new Achievement(41, x => x.money >= 5000)); //��������1��Ʈ ����
		_achievements.Add(new Achievement(42, x => x.money >= 6000)); //���������2��Ʈ ����
		_achievements.Add(new Achievement(43, x => x.money >= 7000)); //��������2��Ʈ ����
		_achievements.Add(new Achievement(44, x => x.money >= 8000)); //��������۸�������Ʈ ����
		_achievements.Add(new Achievement(45, x => x.money >= 9000)); //�������۸�������Ʈ ����
		_achievements.Add(new Achievement(46, x => _click >= 50)); //Ư���� ���� ������ �߰�
		_achievements.Add(new Achievement(47, x => Time.time > 3600)); //Ư���� ��� ������ �߰�
		_achievements.Add(new Achievement(48, x => x.haveAchievement.Count >= 49)); //����
		_achievements.Add(new Achievement(49, x => true)); //���� ����
		_achievements.Add(new Achievement(50, x => x.happy >= 50)); //��ȭ�� ����
		_achievements.Add(new Achievement(51, x => x.haveAchievement.Count >= 20)); //���� ���� �޼�
		_achievements.Add(new Achievement(52, x => x.haveAchievement.Count >= 20 && x.renewal >= 1)); //�ٽ� �ѹ� ���� ���� �޼�!
		_achievements.Add(new Achievement(53, x => x.happy >= 55000 && x.haveAchievement.Count >= 7)); //���� ����
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
	/// ���� ������
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
	/// �Լ� ����
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