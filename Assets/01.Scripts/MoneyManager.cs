using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>
{
	public int GetMoney()
	{
		return UserSaveDataManager.UserSaveData.money;
	}
	
	public void SpendMoney(int spend)
	{
		UserSaveDataManager.UserSaveData.money -= spend;
		UpdateUI();
	}

	public void AddMoney(int add)
	{
		UserSaveDataManager.UserSaveData.money += add;
		UpdateUI();
	}


	public int GetHappy()
	{
		return UserSaveDataManager.UserSaveData.money;
	}

	public void SpendHappy(int spend)
	{
		UserSaveDataManager.UserSaveData.money -= spend;
		UpdateUI();
	}

	public void AddHappy(int add)
	{
		UserSaveDataManager.UserSaveData.money += add;
		UpdateUI();
	}

	public void UpdateUI()
	{

	}
}
