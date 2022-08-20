using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "ScriptableObject/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
	public List<ItemData> itemDataList = new List<ItemData>();
	
	public ItemData GetItemData(int itemCode)
	{
		return itemDataList.Find(x => x.itemCode == itemCode);
	}
}

[System.Serializable]
public class ItemData
{
	public int itemCode;
	public EItem itemType;
	public Gradient gradient_1;
	public Gradient gradient_2;
	public Gradient gradient_3;
	public Texture2D texture2D;

}
