using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
public class HaveItemManager : MonoBehaviour
{
    // 외부 참조 변수 
    private ItemChangeManager _itemChangeManager; 

    private int _shopOpenCode = 33; // 상점 오픈 업적 코드 
    
    private bool _isShopOpened; // 33 
    [SerializeField]
    private List<int> _colorItemLevelCodeList = new List<int>(); // 색 아이템단계 업적 코드 리스트 (code : 41 43 45 ) 1단계 2단계 3단계
    [SerializeField]
    private List<int> _shapeItemLevelCodeList = new List<int>();  // 모양 아이템단계 업적 코드 리스트 (code : 42 44 46 ) 1단계 2단계 3단계 

    [SerializeField]
    private List<int> _colorItemCodeList = new List<int>(); // 상점에 나올 색 아이템 코드 리스트 
    [SerializeField]
    private List<int> _shapeItemCodeList = new List<int>(); // 상점에 나올 모양 아이템 코드 리스트 

    [SerializeField] // 임시 직렬화
    private int _currentColorLevel; // 현재 색 아이템 단계 
    [SerializeField] // 임시 직렬화
    private int _currentShapeLevel; // 현재 모양 아이템 단계 

    // SO
    [SerializeField]
    private AchievementDataSO _achievementDataSO;
    [SerializeField]
    private ShopItemListSO _shopItemListSO; 

    // 프로퍼티
    public int CurrentColorLevel => _currentColorLevel;
    public int CurrentShapeLevel => _currentShapeLevel;
    public bool IsShopOpened => _isShopOpened;

    public List<int> ColorItemCodeList => _colorItemCodeList;
    public List<int> ShapeItemCodeList => _shapeItemCodeList; 

    private void Awake()
    {
        _itemChangeManager = FindObjectOfType<ItemChangeManager>();
        CheckShopItems();
    }
    private void Start()
    {
    }

    /// <summary>
    /// 현재 아이템 단계 체크 
    /// </summary>
    public void CheckShopItems()
    {
        _currentColorLevel = CheckShopItem(_colorItemLevelCodeList);
        _currentShapeLevel = CheckShopItem(_shapeItemLevelCodeList);
        _isShopOpened = AchievementManager.Instance.CheckHaveAchievement(_shopOpenCode);

        SetItemList(); // 상점에 나올 아이템 리스트 설정 
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

    /// <summary>
    /// 상점에 나올 아이템 리스트 설정 
    /// </summary>
    private void SetItemList()
    {
        _colorItemCodeList = _shopItemListSO.colorShopItemList[_currentColorLevel].itemList.ToList();
        _shapeItemCodeList = _shopItemListSO.shapeShopItemList[_currentShapeLevel].itemList.ToList();
    }
}


