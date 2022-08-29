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
		_achievements.Add(new Achievement(0, x => x.happy >= 100)); //����
		_achievements.Add(new Achievement(1, x => x.happy >= 300)); //��
		_achievements.Add(new Achievement(2, x => x.happy >= 700)); //�ܵ�
		_achievements.Add(new Achievement(3, x => x.happy >= 1500)); //�༺�ܵ�
		_achievements.Add(new Achievement(4, x => x.happy >= 3100)); //����1,2
		_achievements.Add(new Achievement(5, x => x.happy >= 6300)); //����3,4
		_achievements.Add(new Achievement(6, x => x.happy >= 12700)); //��
		_achievements.Add(new Achievement(7, x => x.happy >= 20000)); //��1
		_achievements.Add(new Achievement(8, x => x.happy >= 21000)); //��2
		_achievements.Add(new Achievement(9, x => x.happy >= 22000)); //��3
		_achievements.Add(new Achievement(10, x => x.happy >= 23000)); //��4
		_achievements.Add(new Achievement(11, x => x.happy >= 24000)); //��5
		_achievements.Add(new Achievement(12, x => x.happy >= 25000)); //��6
		_achievements.Add(new Achievement(13, x => x.further1ColorItemCode == x.further2ColorItemCode && x.further2ColorItemCode == x.further3ColorItemCode && x.further3ColorItemCode == x.further4ColorItemCode)); //��Ʈ ����
		_achievements.Add(new Achievement(14, x => x.happy >= 30000)); //����1
		_achievements.Add(new Achievement(15, x => x.happy >= 31000)); //����2
		_achievements.Add(new Achievement(16, x => x.happy >= 32000)); //����3
		_achievements.Add(new Achievement(17, x => x.happy >= 33000)); //����4
		_achievements.Add(new Achievement(18, x => x.happy >= 34000)); //����5
		_achievements.Add(new Achievement(19, x => x.happy >= 40000)); //����Ƽ¯1
		_achievements.Add(new Achievement(20, x => x.money >= 1000)); //�Ǵٸ��༺
		_achievements.Add(new Achievement(21, x => x.happy >= 80000)); //���༺�� �ܵ�
		_achievements.Add(new Achievement(22, x => x.happy >= 90000)); //���༺�� ����
		_achievements.Add(new Achievement(23, x => x.happy >= 100000)); //���༺�� ����
		_achievements.Add(new Achievement(24, x => x.happy >= 20000)); //���༺�� ��1
		_achievements.Add(new Achievement(25, x => x.happy >= 22000)); //���༺�� ��2
		_achievements.Add(new Achievement(26, x => x.happy >= 24000)); //���༺�� ��3
		_achievements.Add(new Achievement(27, x => x.happy >= 26000)); //���༺�� ��4
		_achievements.Add(new Achievement(28, x => x.money >= 10000)); //���༺�� ����Ƽ¯
		_achievements.Add(new Achievement(29, x => x.happy >= 1200)); //����Ƽ¯2
		_achievements.Add(new Achievement(30, x => x.happy >= 10000)); //2�� ���� ����
		_achievements.Add(new Achievement(31, x => x.happy >= 100000)); //3�� ���� ����
		_achievements.Add(new Achievement(32, x => x.happy >= 1000000)); //4�� ���� ����
		_achievements.Add(new Achievement(33, x => x.happy >= 1500 && x.haveAchievement.Count >= 5)); //���� ����
		_achievements.Add(new Achievement(34, x => x.haveAchievement.Count >= 3)); //���̺귯�� ����
		_achievements.Add(new Achievement(35, x => x.happy >= 30000)); //������ ����
		_achievements.Add(new Achievement(36, x => x.renewal >= 1)); //������ ����
		_achievements.Add(new Achievement(37, x => x.renewal >= 2)); //ũ������ ����
		_achievements.Add(new Achievement(38, x => x.money >= 20000)); //����Ƽ¯3 ����
		_achievements.Add(new Achievement(39, x => x.money >= 30000)); //��ı�1 ����
		_achievements.Add(new Achievement(40, x => x.money >= 40000)); //��ı�2 ����
		_achievements.Add(new Achievement(41, x => x.money >= 50000)); //���������1��Ʈ ����
		_achievements.Add(new Achievement(42, x => x.money >= 60000)); //��������1��Ʈ ����
		_achievements.Add(new Achievement(43, x => x.money >= 70000)); //���������2��Ʈ ����
		_achievements.Add(new Achievement(44, x => x.money >= 80000)); //��������2��Ʈ ����
		_achievements.Add(new Achievement(45, x => x.money >= 90000)); //��������۸�������Ʈ ����
		_achievements.Add(new Achievement(46, x => x.money >= 100000)); //�������۸�������Ʈ ����
		_achievements.Add(new Achievement(47, x => _click >= 50)); //Ư���� ���� ������ �߰�
		_achievements.Add(new Achievement(48, x => Time.time > 3600)); //Ư���� ��� ������ �߰�
		_achievements.Add(new Achievement(49, x => x.haveAchievement.Count >= 49)); //����
		_achievements.Add(new Achievement(50, x => true)); //���� ����
		_achievements.Add(new Achievement(51, x => x.happy >= 1000)); //��ȭ�� ����
		_achievements.Add(new Achievement(52, x => x.haveAchievement.Count >= 20)); //���� ���� �޼�
		_achievements.Add(new Achievement(53, x => x.haveAchievement.Count >= 20 && x.renewal >= 1)); //�ٽ� �ѹ� ���� ���� �޼�!
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