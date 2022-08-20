using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyMoneyManager : Singleton<HappyMoneyManager>
{
	private FireWorkController _fireWorkController;

	public FireWorkController FireWorkController
	{
		get
		{
			_fireWorkController ??= FindObjectOfType<FireWorkController>();
			return _fireWorkController;
		}
	}

	/// <summary>
	/// �� ����
	/// </summary>
	/// <param name="money"></param>
	public void AddMoney(int money)
	{
		UserSaveDataManager.Instance.UserSaveData.money += money;
	}
	/// <summary>
	/// �ູ�� ����
	/// </summary>
	/// <param name="happy"></param>
	public void AddHappy(int happy)
	{
		UserSaveDataManager.Instance.UserSaveData.happy += happy;
	}

	/// <summary>
	/// ������ �ڵ����� ����ؼ� �ູ���� ������Ų��.
	/// </summary>
	public void AddHappyAuto()
	{
		int happy = 0;
		happy += FireWorkController.Further1;
		happy *= FireWorkController.Further2 > 0 ? FireWorkController.Further2 : 1;
		happy *= FireWorkController.Further3 > 0 ? FireWorkController.Further3 : 1;
		happy *= FireWorkController.Further4 > 0 ? FireWorkController.Further4 : 1;

		AddHappy(happy);
	}

	/// <summary>
	/// �� ����
	/// </summary>
	/// <param name="money"></param>
	/// <returns></returns>
	public bool RemoveMoney(int money)
	{
		if (UserSaveDataManager.Instance.UserSaveData.money < money)
		{ 
			return false;
		}
		UserSaveDataManager.Instance.UserSaveData.money += money;
		return true;
	}
	/// <summary>
	/// �ູ�� ����
	/// </summary>
	/// <param name="happy"></param>
	/// <returns></returns>
	public bool RemoveHappy(int happy)
	{
		if(UserSaveDataManager.Instance.UserSaveData.happy < happy)
		{
			return false;
		}
		UserSaveDataManager.Instance.UserSaveData.happy -= happy;
		return true;
	}
}
