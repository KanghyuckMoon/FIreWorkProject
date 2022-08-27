using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveItemManager : MonoBehaviour
{

    private bool _isShopOpened; // 33 
    [SerializeField]
    private List<int> _colorItemLevelCodeList = new List<int>(); // �� �����۴ܰ� �ڵ� ����Ʈ (code : 41 43 45 ) 1�ܰ� 2�ܰ� 3�ܰ�
    [SerializeField]
    private List<int> _shapeItemLevelCodeList = new List<int>();  // ��� �����۴ܰ� �ڵ� ����Ʈ (code : 42 44 46 ) 1�ܰ� 2�ܰ� 3�ܰ� 

    [SerializeField]
    private List<int> _colorItemList = new List<int>(); // ���� �� ������ ����Ʈ 
    [SerializeField]
    private List<int> _shapeItemList = new List<int>(); // ���� ��� ������ ����Ʈ 

    [SerializeField] // �ӽ� ����ȭ
    private int _currentColorLevel; // ���� �� ������ �ܰ� 
    [SerializeField] // �ӽ� ����ȭ
    private int _currentShapeLevel; // ���� ��� ������ �ܰ� 

    [SerializeField]
    private ItemDataSO _itemDataSO; 
    [SerializeField]
    private AchievementDataSO _achievementDataSO; 

    // ������Ƽ
    public int CurrentColorLevel => _currentColorLevel;
    public int CurrentShapeLevel => _currentShapeLevel;
    public bool IsShopOpened => _isShopOpened;

    private void Start()
    {
        CheckShopItems(); 
    }

    /// <summary>
    /// ���� ������ �ܰ� üũ
    /// </summary>
    public void CheckShopItems()
    {
        _currentColorLevel = CheckShopItem(_colorItemLevelCodeList);
        _currentShapeLevel = CheckShopItem(_shapeItemLevelCodeList);
        _isShopOpened = AchievementManager.Instance.CheckHaveAchievement(33); 
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


}
