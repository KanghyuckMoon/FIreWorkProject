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

    private List<ItemUI> _libraryColorItemList = new List<ItemUI>(); // ������ �� ������ ����Ʈ 
    private List<ItemUI> _libraryShapeItemList = new List<ItemUI>(); // ������ ��� ������ ����Ʈ 

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
        ItemData itemData;
        int itemCode; 

        List<int> haveItemList = UserSaveDataManager.Instance.UserSaveData.haveItem; // ���� ������ �ڵ� ����Ʈ 
        int count = haveItemList.Count;
        
        for (int i = 0; i < count; i++)
        {
            libraryItemInfo = CheckItemType(_itemDataSO.itemDataList[haveItemList[i]].itemType);

            itemCode = haveItemList[i]; // ������ �ڵ� 
            itemData = _itemDataSO.GetItemData(itemCode);

            ItemUI item = new ItemUI(itemData,isPurchasable:false);

            if (libraryItemInfo.itemList.Contains(item) == false) // ������ �������� �ƴ϶�� ���� 
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
        public List<ItemUI> itemList = new List<ItemUI>();  // ������ �� ����� ����Ʈ 
    }

}