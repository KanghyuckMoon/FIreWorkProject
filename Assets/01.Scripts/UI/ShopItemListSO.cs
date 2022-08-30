using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

[CreateAssetMenu(menuName = "SO/UI/ShopItemListSO")]
public class ShopItemListSO : ScriptableObject
{
    [Header("단계별 컬러 아이템 리스트")]
    public List<ShopItemList> colorShopItemList = new List<ShopItemList>(); 

    [Header("단계별 모양 아이템 리스트")]
    public List<ShopItemList> shapeShopItemList = new List<ShopItemList>();
}

[Serializable]
public class ShopItemList
{
    public List<int> itemList = new List<int>(); 
}

public enum ItemLevelType
{
    Lv_1 = 0, 
    Lv_2 = 1,
    Lv_3 = 2, 
}