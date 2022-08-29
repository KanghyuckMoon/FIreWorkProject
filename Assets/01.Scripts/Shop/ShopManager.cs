using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	[SerializeField] private ItemDataSO _itemDataSO;

	/// <summary>
	/// ������ �߰��ϱ�
	/// </summary>
	/// <param name="itemCode"></param>
	public void GetItem(int itemCode)
	{
		UserSaveDataManager.Instance.UserSaveData.haveItem.Add(itemCode);
	}

	/// <summary>
	/// ������ ����
	/// </summary>
	/// <param name="itemCode"></param>
	public void BuyItem(int itemCode)
	{
		if(HappyMoneyManager.Instance.RemoveHappy(_itemDataSO.GetItemData(itemCode).money))
		{
			Debug.Log("���� �Ϸ�");
			UserSaveDataManager.Instance.UserSaveData.haveItem.Add(itemCode);
		}	
		else
		{
			Debug.Log("���� ����");
		}
	}
}
