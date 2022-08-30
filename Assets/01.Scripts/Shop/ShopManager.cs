using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	[SerializeField] private ItemDataSO _itemDataSO;

	/// <summary>
	/// 아이템 추가하기
	/// </summary>
	/// <param name="itemCode"></param>
	public void GetItem(int itemCode)
	{
		UserSaveDataManager.Instance.UserSaveData.haveItem.Add(itemCode);
	}

	/// <summary>
	/// 아이템 구매
	/// </summary>
	/// <param name="itemCode"></param>
	public void BuyItem(int itemCode)
	{
		if(HappyMoneyManager.Instance.RemoveHappy(_itemDataSO.GetItemData(itemCode).money))
		{
			Debug.Log("구매 완료");
			UserSaveDataManager.Instance.UserSaveData.haveItem.Add(itemCode);
		}	
		else
		{
			Debug.Log("구매 실패");
		}
	}
}
