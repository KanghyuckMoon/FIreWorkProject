using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;

[Serializable]
public class ShopPanelComponent : UIComponent
{
    // 외부 참조 변수 
    private HaveItemManager _haveItemManager;
    private ShopManager _shopManager;
    private LibraryPanelComponent _libraryPanelComponent;

    private Button _shopButton; // 상점 버튼
    private VisualElement _lockIcon; // 잠금 아이콘 
    private TemplateContainer _shopPanel;

    private Button _shopBackButton; // 뒤로가기 버튼 
    private VisualElement _colorItemParent; // 색 아이템 부모 오브젝트
    private VisualElement _shapeItemParent; // 모양 아이템 부모 오브젝트 
    private VisualElement _itemParent; // 부모 오브젝트 

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

        foreach(ItemType itemType in Enum.GetValues(typeof(ItemType))) // 상점 아이템 생성 
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
        List<int> itemCodeList = new List<int>(); // 생성할 아이템 코드 리스트 
        List<ShopItemUI> itemList = new List<ShopItemUI>(); // 생성된 아이템 받아올 리스트 
        VisualElement parent = new VisualElement();  // 아이템 생성 위치 
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
    /// 상점 아이템 생성 
    /// </summary>
    /// <param name="itemCodeList">생성할 아이템</param>
    /// <param name="itemList">생성된 아이템 저장 리스트 </param>
    /// <param name="parent">생성될 아이템 위치</param>
    private void InstantiateItems(List<int> itemCodeList,List<ShopItemUI> itemList,VisualElement parent)
    {
        int count = itemCodeList.Count;

        for (int i =0; i < count; i++)
        {
            int itemCode = itemCodeList[i];

            ShopItemUI item = new ShopItemUI(_itemDataSO.GetItemData(itemCode),
                buyCheckEvent: () =>_shopManager.BuyItem(itemCode),
                librartUpdateEvent: () => _libraryPanelComponent.CreateHaveItems());
            
            if (itemList.Contains(item) == false) // 생성된 아이템이 아니라면 생성 
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
