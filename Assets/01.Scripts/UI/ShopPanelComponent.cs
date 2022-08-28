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

    private Button _shopButton; // 상점 버튼
    private TemplateContainer _shopPanel;

    private Button _shopBackButton; // 뒤로가기 버튼 
    private VisualElement _colorItemParent; // 색 아이템 부모 오브젝트
    private VisualElement _shapeItemParent; // 모양 아이템 부모 오브젝트 

    private List<ItemUI> _shopColorItemList = new List<ItemUI>();
    private List<ItemUI> _shopShapeItemList = new List<ItemUI>();

    [SerializeField]
    private ItemDataSO _itemDataSO;

    public void Init(UIButtonManager uIButtonManager, HaveItemManager haveItemManager,ShopManager shopManager)
    {
        _uiButtonManager = uIButtonManager;
        _haveItemManager = haveItemManager;
        _shopManager = shopManager; 

        _shopPanel = _uiButtonManager.RootElement.Q<TemplateContainer>("ShopTemplate");
        _shopButton = _uiButtonManager.RootElement.Q<Button>("shop-button");

        _shopBackButton = _shopPanel.Q<Button>("back-button");
        _colorItemParent = _shopPanel.Q<VisualElement>("colorItem-scrollview");
        _shapeItemParent = _shopPanel.Q<VisualElement>("shapeItem-scrollview"); 

        _shopButton.clicked += () => OpenClosePanel(_shopPanel); 
        _shopBackButton.clicked += () => OpenClosePanel(_shopPanel);

        foreach(ItemType itemType in Enum.GetValues(typeof(ItemType))) // 상점 아이템 생성 
        {
            CreateShopItem(itemType); 
        }
    }
    

    public override void UpdateSometing()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// ItemType에 따른 상점아이템 생성 
    /// </summary>
    /// <param name="itemType"></param>
    public void CreateShopItem(ItemType itemType)
    {
        List<int> itemCodeList = new List<int>(); // 생성할 아이템 코드 리스트 
        List<ItemUI> itemList = new List<ItemUI>(); // 생성된 아이템 받아올 리스트 
        VisualElement parent = new VisualElement();  // 아이템 생성 위치 
        switch (itemType)
        {
            case ItemType.Color:
                itemCodeList = _haveItemManager.ColorItemCodeList.ToList();
                itemList = _shopColorItemList;
                parent = _colorItemParent; 
                break;
            case ItemType.Shape:
                itemCodeList = _haveItemManager.ShapeItemCodeList.ToList();
                itemList = _shopShapeItemList;
                parent = _shapeItemParent; 
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
    private void InstantiateItems(List<int> itemCodeList,List<ItemUI> itemList,VisualElement parent)
    {
        int count = itemCodeList.Count;
        int itemCode; 
        ItemData itemData;

        for (int i =0; i < count; i++)
        {
            itemCode = itemCodeList[i];
            itemData = _itemDataSO.GetItemData(itemCode);

            ItemUI item = new ItemUI(itemData,() =>_shopManager.BuyItem(itemCode));
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
