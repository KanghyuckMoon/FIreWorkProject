using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveItemManager : MonoBehaviour
{

    private bool _isShopOpened; // 33 
    [SerializeField]
    private List<int> _colorItemLevelCodeList = new List<int>(); // 색 아이템단계 코드 리스트 (code : 41 43 45 ) 1단계 2단계 3단계
    [SerializeField]
    private List<int> _shapeItemLevelCodeList = new List<int>();  // 모양 아이템단계 코드 리스트 (code : 42 44 46 ) 1단계 2단계 3단계 

    [SerializeField]
    private List<int> _colorItemList = new List<int>(); // 나올 색 아이템 리스트 
    [SerializeField]
    private List<int> _shapeItemList = new List<int>(); // 나올 모양 아이템 리스트 

    [SerializeField] // 임시 직렬화
    private int _currentColorLevel; // 현재 색 아이템 단계 
    [SerializeField] // 임시 직렬화
    private int _currentShapeLevel; // 현재 모양 아이템 단계 

    [SerializeField]
    private ItemDataSO _itemDataSO; 
    [SerializeField]
    private AchievementDataSO _achievementDataSO; 

    // 프로퍼티
    public int CurrentColorLevel => _currentColorLevel;
    public int CurrentShapeLevel => _currentShapeLevel;
    public bool IsShopOpened => _isShopOpened;

    private void Start()
    {
        CheckShopItems(); 
    }

    /// <summary>
    /// 현재 아이템 단계 체크
    /// </summary>
    public void CheckShopItems()
    {
        _currentColorLevel = CheckShopItem(_colorItemLevelCodeList);
        _currentShapeLevel = CheckShopItem(_shapeItemLevelCodeList);
        _isShopOpened = AchievementManager.Instance.CheckHaveAchievement(33); 
    }

    /// <summary>
    /// 아이템 단계 체크 
    /// </summary>          
    private int CheckShopItem(List<int> itemCodeList)
    {
        int maxIndex = itemCodeList.Count - 1; // 최대 인덱스 
        int index = 0; // 

        for (int i = 0; i < maxIndex; i++)
        {
            if (AchievementManager.Instance.CheckHaveAchievement(itemCodeList[index]))
            {
                ++index;
                continue; 
            }
            break; 
        }
        return index;
    }


}
