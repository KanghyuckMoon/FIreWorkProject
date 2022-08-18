using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyMoneyManager : Singleton<HappyMoneyManager>
{
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
