using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	[SerializeField] private ItemDataSO _itemDataSO;

	public void GetItem(int itemCode)
	{
		UserSaveDataManager.Instance.UserSaveData.haveItem.Add(itemCode);
	}

	public void BuyItem(int itemCode)
	{
		if(HappyMoneyManager.Instance.Happy >= _itemDataSO.GetItemData(itemCode).money)
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
