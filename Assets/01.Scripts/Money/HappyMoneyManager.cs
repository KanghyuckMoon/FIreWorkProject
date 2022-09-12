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
	/// ������ �ڵ����� ����ؼ� �ູ���� ������Ų��.
	/// </summary>
	public void AddHappyAuto()
	{
		int happy = 0;
		happy += FireWorkController.Further1;
		happy *= FireWorkController.Further2 > 0 ? FireWorkController.Further2 : 1;

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
			PopUpManager.SetPopUp("���� �����մϴ�");
			return false;
		}
		UserSaveDataManager.Instance.UserSaveData.money -= money;
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
			PopUpManager.SetPopUp("�ູ�� �����մϴ�");
			return false;
		}
		UserSaveDataManager.Instance.UserSaveData.happy -= happy;
		return true;
	}
}
