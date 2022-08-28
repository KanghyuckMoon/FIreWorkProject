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

    private Button _shopButton; // ���� ��ư
    private TemplateContainer _shopPanel;

    private Button _shopBackButton; // �ڷΰ��� ��ư 
    private VisualElement _colorItemParent; // �� ������ �θ� ������Ʈ
    private VisualElement _shapeItemParent; // ��� ������ �θ� ������Ʈ 

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

        foreach(ItemType itemType in Enum.GetValues(typeof(ItemType))) // ���� ������ ���� 
        {
            CreateShopItem(itemType); 
        }
    }
    

    public override void UpdateSometing()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// ItemType�� ���� ���������� ���� 
    /// </summary>
    /// <param name="itemType"></param>
    public void CreateShopItem(ItemType itemType)
    {
        List<int> itemCodeList = new List<int>(); // ������ ������ �ڵ� ����Ʈ 
        List<ItemUI> itemList = new List<ItemUI>(); // ������ ������ �޾ƿ� ����Ʈ 
        VisualElement parent = new VisualElement();  // ������ ���� ��ġ 
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
    /// ���� ������ ���� 
    /// </summary>
    /// <param name="itemCodeList">������ ������</param>
    /// <param name="itemList">������ ������ ���� ����Ʈ </param>
    /// <param name="parent">������ ������ ��ġ</param>
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
