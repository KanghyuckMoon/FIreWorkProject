using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
public class HaveItemManager : MonoBehaviour
{
    // �ܺ� ���� ���� 
    private ItemChangeManager _itemChangeManager; 

    private int _shopOpenCode = 33; // ���� ���� ���� �ڵ� 
    
    private bool _isShopOpened; // 33 
    [SerializeField]
    private List<int> _colorItemLevelCodeList = new List<int>(); // �� �����۴ܰ� ���� �ڵ� ����Ʈ (code : 41 43 45 ) 1�ܰ� 2�ܰ� 3�ܰ�
    [SerializeField]
    private List<int> _shapeItemLevelCodeList = new List<int>();  // ��� �����۴ܰ� ���� �ڵ� ����Ʈ (code : 42 44 46 ) 1�ܰ� 2�ܰ� 3�ܰ� 

    [SerializeField]
    private List<int> _colorItemCodeList = new List<int>(); // ������ ���� �� ������ �ڵ� ����Ʈ 
    [SerializeField]
    private List<int> _shapeItemCodeList = new List<int>(); // ������ ���� ��� ������ �ڵ� ����Ʈ 

    [SerializeField] // �ӽ� ����ȭ
    private int _currentColorLevel; // ���� �� ������ �ܰ� 
    [SerializeField] // �ӽ� ����ȭ
    private int _currentShapeLevel; // ���� ��� ������ �ܰ� 

    // SO
    [SerializeField]
    private AchievementDataSO _achievementDataSO;
    [SerializeField]
    private ShopItemListSO _shopItemListSO; 

    // ������Ƽ
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
    /// ���� ������ �ܰ� üũ 
    /// </summary>
    public void CheckShopItems()
    {
        _currentColorLevel = CheckShopItem(_colorItemLevelCodeList);
        _currentShapeLevel = CheckShopItem(_shapeItemLevelCodeList);
        _isShopOpened = AchievementManager.Instance.CheckHaveAchievement(_shopOpenCode);

        SetItemList(); // ������ ���� ������ ����Ʈ ���� 
    }

    /// <summary>
    /// ������ �ܰ� üũ 
    /// </summary>          
    private int CheckShopItem(List<int> itemCodeList)
    {
        int maxIndex = itemCodeList.Count - 1; // �ִ� �ε��� 
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
    /// ������ ���� ������ ����Ʈ ���� 
    /// </summary>
    private void SetItemList()
    {
        _colorItemCodeList = _shopItemListSO.colorShopItemList[_currentColorLevel].itemList.ToList();
        _shapeItemCodeList = _shopItemListSO.shapeShopItemList[_currentShapeLevel].itemList.ToList();
    }
}


