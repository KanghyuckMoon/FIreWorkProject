using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

[Serializable]
public class LibraryPanelComponent : UIComponent
{
    private HaveItemManager _haveItemManager;

    private Button _libraryButton; // ���� ��ư
    private TemplateContainer _libraryPanel;

    private Button _libraryBackButton; // �ڷΰ��� ��ư 
    private VisualElement _colorItemParent; // �� ������ �θ� ������Ʈ
    private VisualElement _shapeItemParent; // ��� ������ �θ� ������Ʈ 

    private VisualElement _further1Button;
    private VisualElement _further2Button;
    private VisualElement _further3Button;
    private VisualElement _further4Button;

    private Slider _slider1;
    private Slider _slider2; 

    private List<ItemBox> _libraryColorItemList = new List<ItemBox>(); // ������ �� ������ ����Ʈ 
    private List<ItemBox> _libraryShapeItemList = new List<ItemBox>(); // ������ ��� ������ ����Ʈ 

    [SerializeField]
    private ItemDataSO _itemDataSO;
    public void Init(UIButtonManager uIButtonManager, HaveItemManager haveItemManager)
    {
        _uiButtonManager = uIButtonManager;
        _haveItemManager = haveItemManager;

        _libraryButton = _uiButtonManager.RootElement.Q<Button>("library-button");
        _libraryPanel = _uiButtonManager.RootElement.Q<TemplateContainer>("LibraryTemplate");
        _libraryPanel.style.display = DisplayStyle.None; 

        _libraryBackButton = _libraryPanel.Q<Button>("back-button");
        _colorItemParent = _libraryPanel.Q<VisualElement>("colorItem-scrollview");
        _shapeItemParent = _libraryPanel.Q<VisualElement>("shapeItem-scrollview");

        _further1Button = _libraryPanel.Q<VisualElement>("further1-button");
        _further2Button = _libraryPanel.Q<VisualElement>("further2-button");
        _further3Button = _libraryPanel.Q<VisualElement>("further3-button");
        _further4Button = _libraryPanel.Q<VisualElement>("further4-button");

        // ��ư �̺�Ʈ ��� 
        _libraryButton.clicked += () => OpenClosePanel(_libraryPanel);
        _libraryBackButton.clicked += () => OpenClosePanel(_libraryPanel);

        CreateHaveItems();
    }

    public override void UpdateSometing()
    {
        throw new System.NotImplementedException();
    }


    /// <summary>
    /// ���� ���� ������ ���� 
    /// </summary>
    public void CreateHaveItems()
    {
        LibraryItemInfo libraryItemInfo;
        int itemCode; 

        List<int> haveItemList = UserSaveDataManager.Instance.UserSaveData.haveItem; // ���� ������ �ڵ� ����Ʈ 
        int count = haveItemList.Count;
        
        for (int i = 0; i < count; i++)
        {
            libraryItemInfo = CheckItemType(_itemDataSO.itemDataList[haveItemList[i]].itemType);

            itemCode = haveItemList[i]; // ������ �ڵ� 
            ItemData itemData = _itemDataSO.GetItemData(itemCode);

            ItemBox item = new ItemBox(itemData,_itemDataSO);

            if (IsContainItem(libraryItemInfo.itemList, item.ItemCode) == false) // ������ �������� �ƴ϶�� ���� 
            {
                libraryItemInfo.itemList.Add(item);
                libraryItemInfo.parent.Add(item);
            }
            else
            {
                item = null;
            }
        }
    }

    /// <summary>
    /// ������ �ڵ尡 �����ϸ� True ���� / ������  False ���� 
    /// </summary>
    /// <param name="itemList"></param>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    private bool IsContainItem(List<ItemBox> itemList, int itemCode)
    {
        int count = itemList.Count; 
        for(int i =0;i < count; i++)
        {
            if(itemList[i].ItemCode == itemCode)
            {
                return true; 
            }
        }
        return false; 
    }

    /// <summary>
    /// ������ ������ �θ� ���� 
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    private LibraryItemInfo CheckItemType(EItem itemType)
    {
        LibraryItemInfo libraryItemInfo = new LibraryItemInfo();
        switch (itemType)
        {
            case EItem.Color:
                libraryItemInfo.parent = _colorItemParent;
                libraryItemInfo.itemList = _libraryColorItemList;
                break;

            case EItem.Texture:
                libraryItemInfo.parent = _shapeItemParent;
                libraryItemInfo.itemList = _libraryShapeItemList;
                break;
        }
        return libraryItemInfo;
    }

    private class LibraryItemInfo
    {
        public VisualElement parent; // ������ ��ġ (�θ� ������Ʈ) 
        public List<ItemBox> itemList = new List<ItemBox>();  // ������ �� ����� ����Ʈ 
    }

}
