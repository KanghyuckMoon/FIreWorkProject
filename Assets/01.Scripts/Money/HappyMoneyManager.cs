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
	/// 돈 증가
	/// </summary>
	/// <param name="money"></param>
	public void AddMoney(int money)
	{
		UserSaveDataManager.Instance.UserSaveData.money += money;
	}
	/// <summary>
	/// 행복도 증가
	/// </summary>
	/// <param name="happy"></param>
	public void AddHappy(int happy)
	{
		UserSaveDataManager.Instance.UserSaveData.happy += happy;
	}

	/// <summary>
	/// 폭발을 자동으로 계산해서 행복도를 증가시킨다.
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
	/// 돈 감소
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
	/// 행복도 감소
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
