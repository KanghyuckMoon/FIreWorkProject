using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;

[Serializable]
public class ShopPanelComponent : UIComponent
{
    // �ܺ� ���� ���� 
    private HaveItemManager _haveItemManager;
    private ShopManager _shopManager;
    private LibraryPanelComponent _libraryPanelComponent;

    private Button _shopButton; // ���� ��ư
    private VisualElement _lockIcon; // ��� ������ 
    private TemplateContainer _shopPanel;

    private Button _shopBackButton; // �ڷΰ��� ��ư 
    private VisualElement _colorItemParent; // �� ������ �θ� ������Ʈ
    private VisualElement _shapeItemParent; // ��� ������ �θ� ������Ʈ 
    private VisualElement _itemParent; // �θ� ������Ʈ 

    private List<ShopItemUI> _shopColorItemList = new List<ShopItemUI>();
    private List<ShopItemUI> _shopShapeItemList = new List<ShopItemUI>();

    [SerializeField]
    private ItemDataSO _itemDataSO;
    [SerializeField]
    private int _shopOpenCode = 53;
    private bool _isOpenShop = false; 

    public void Init(UIButtonManager uIButtonManager, HaveItemManager haveItemManager,ShopManager shopManager, LibraryPanelComponent libraryPanelComponent)
    {
        _uiButtonManager = uIButtonManager;
        _haveItemManager = haveItemManager;
        _shopManager = shopManager;
        _libraryPanelComponent = libraryPanelComponent; 

        _shopPanel = _uiButtonManager.RootElement.Q<TemplateContainer>("ShopTemplate");
        _shopPanel.style.display = DisplayStyle.None;
        _shopButton = _uiButtonManager.RootElement.Q<Button>("shop-button");
        _lockIcon = _uiButtonManager.RootElement.Q<VisualElement>("shopLock-icon");

        _shopBackButton = _shopPanel.Q<Button>("back-button");
        
        _colorItemParent = _shopPanel.Q<VisualElement>("colorItem-scrollview");
        _shapeItemParent = _shopPanel.Q<VisualElement>("shapeItem-scrollview");
        _itemParent = _shopPanel.Q<VisualElement>("item-panel"); 

        _shopButton.clicked += () => OpenClosePanel(_shopPanel); 
        _shopBackButton.clicked += () => OpenClosePanel(_shopPanel);

        foreach(ItemType itemType in Enum.GetValues(typeof(ItemType))) // ���� ������ ���� 
        {
            CreateShopItem(itemType); 
        }
    }
    

    public override void UpdateSometing()
    {
        if(_isOpenShop == false)
        {
            UnlockShop();
        }
    }

    public void UnlockShop()
    {
        if(AchievementManager.Instance.CheckHaveAchievement(_shopOpenCode) == true)
        {
            _lockIcon.style.display = DisplayStyle.None;
            _isOpenShop = true; 
        }
    }
    /// <summary>
   /// </summary>
    /// <param name="itemType"></param>
    public void CreateShopItem(ItemType itemType)
    {
        List<int> itemCodeList = new List<int>(); // ������ ������ �ڵ� ����Ʈ 
        List<ShopItemUI> itemList = new List<ShopItemUI>(); // ������ ������ �޾ƿ� ����Ʈ 
        VisualElement parent = new VisualElement();  // ������ ���� ��ġ 
        switch (itemType)
        {
            case ItemType.Color:
                itemCodeList = _haveItemManager.ColorItemCodeList.ToList();
                itemList = _shopColorItemList;
                parent = _itemParent; 
                break;
            case ItemType.Shape:
                itemCodeList = _haveItemManager.ShapeItemCodeList.ToList();
                itemList = _shopShapeItemList;
                parent = _itemParent; 
                break; 
        }
        InstantiateItems(itemCodeList, itemList, parent); 
    }

    /// <summary>
    /// ���� ������ ���� 
    /// </summary>
    /// <param name="itemCodeList">������ ������</param>
    /// <param name="itemList">������ ������ ���� ����Ʈ </param>
    /// <param name="parent">������ ������ ��ġ</param>
    private void InstantiateItems(List<int> itemCodeList,List<ShopItemUI> itemList,VisualElement parent)
    {
        int count = itemCodeList.Count;

        for (int i =0; i < count; i++)
        {
            int itemCode = itemCodeList[i];

            ShopItemUI item = new ShopItemUI(_itemDataSO.GetItemData(itemCode),
                buyCheckEvent: () =>_shopManager.BuyItem(itemCode),
                librartUpdateEvent: () => _libraryPanelComponent.CreateHaveItems());
            
            if (itemList.Contains(item) == false) // ������ �������� �ƴ϶�� ���� 
            {
                itemList.Add(item);
                parent.Add(item);
            }
            else
            {
                item = null; 
            }
        }

    }
}

public enum ItemType
{
    Color,
    Shape 
}
