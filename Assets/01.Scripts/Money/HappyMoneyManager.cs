using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyMoneyManager : Singleton<HappyMoneyManager>
{
	private FireWorkController _fireWorkController;
	private PopUpManager _popUpManager;
	public PopUpManager PopUpManager
	{
		get
		{
			_popUpManager ??= FindObjectOfType<PopUpManager>();
			return _popUpManager;
		}
		set
		{
			_popUpManager = value;
		}
	}
	public int Money
	{
		get
		{
			return UserSaveDataManager.Instance.UserSaveData.money;
		}
		set
		{
			UserSaveDataManager.Instance.UserSaveData.money = value;
		}
	}
	public int Happy
	{
		get
		{
			return UserSaveDataManager.Instance.UserSaveData.happy;
		}
		set
		{
			UserSaveDataManager.Instance.UserSaveData.happy = value;
		}
	}
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
		int gethappy = happy;

		switch(UserSaveDataManager.Instance.UserSaveData.renewal)
		{
			case 0:
				break;
			case 1:
				gethappy = (int)(gethappy * 1.2f);
				break;
			default:
				gethappy = (int)(gethappy * 1.4f);
				break;
		}
		UserSaveDataManager.Instance.UserSaveData.happy += gethappy;
	}

	/// <summary>
	/// 폭발을 자동으로 계산해서 행복도를 증가시킨다.
	/// </summary>
	public void AddHappyAuto()
	{
		int happy = 0;
		happy += FireWorkController.Further1;
		happy *= FireWorkController.Further2 > 0 ? FireWorkController.Further2 : 1;

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
			PopUpManager.SetPopUp("돈이 부족합니다");
			return false;
		}
		UserSaveDataManager.Instance.UserSaveData.money -= money;
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
			PopUpManager.SetPopUp("행복이 부족합니다");
			return false;
		}
		UserSaveDataManager.Instance.UserSaveData.happy -= happy;
		return true;
	}
}
