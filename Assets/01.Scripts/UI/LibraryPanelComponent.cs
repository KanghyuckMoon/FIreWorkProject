using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

[Serializable]
public class LibraryPanelComponent : UIComponent
{
    private AchievementViewManager _achievementViewManager;
    private ItemChangeManager _itemChangeMnager;
    private LibraryButtonConstructor _libraryButtonConstructor;  // ���̺귯�� ��ư ������ 
    private HaveItemManager _haveItemManager;
    private FireWorkController _fireWorkController; 

    private Button _libraryButton; // ���� ��ư
    private TemplateContainer _libraryPanel;

    private Button _libraryBackButton; // �ڷΰ��� ��ư 

    private VisualElement _colorItemParent; // �� ������ �θ� ������Ʈ
    private VisualElement _shapeItemParent; // ��� ������ �θ� ������Ʈ 
    private VisualElement _buttonParent; // 

    private VisualElement _libraryLockIcon; 

    private VisualElement _further1SettingButton;
    private VisualElement _further2SettingButton;
    private VisualElement _further3SettingButton;
    private VisualElement _further4SettingButton;

    private Slider _intensitySlider;
    private Slider _sizeSlider;
    private Slider _heightSlider; 

    private List<ItemBox> _libraryColorItemList = new List<ItemBox>(); // ������ �� ������ ����Ʈ 
    private List<ItemBox> _libraryShapeItemList = new List<ItemBox>(); // ������ ��� ������ ����Ʈ 

    [SerializeField]
    private ItemDataSO _itemDataSO;
    [SerializeField]
    private int _libraryOpenCode = 33;
    private bool _isShopOpen = false; 

    private int _intensitySliderOpenCode = 35;
    private int _sizeSliderOpenCode = 36;

    private bool _isOpenIntensitySlider;
    private bool _isOpenSizeSlider;


    public void Init(UIButtonManager uIButtonManager, HaveItemManager haveItemManager, ItemChangeManager itemChangeManager, FireWorkController fireWorkController, AchievementViewManager achievementViewManager)
    {
        _uiButtonManager = uIButtonManager;
        _haveItemManager = haveItemManager;
        _itemChangeMnager = itemChangeManager;
        _achievementViewManager = achievementViewManager;
        _fireWorkController = fireWorkController; 

        _libraryButton = _uiButtonManager.RootElement.Q<Button>("library-button");
        _libraryPanel = _uiButtonManager.RootElement.Q<TemplateContainer>("LibraryTemplate");

        _libraryLockIcon = _uiButtonManager.RootElement.Q<VisualElement>("libraryLock-icon");

        _libraryPanel.style.display = DisplayStyle.None;

        _libraryBackButton = _libraryPanel.Q<Button>("back-button");

        _colorItemParent = _libraryPanel.Q<VisualElement>("colorItem-scrollview");
        _shapeItemParent = _libraryPanel.Q<VisualElement>("shapeItem-scrollview");
        _buttonParent = _libraryPanel.Q<VisualElement>("buttonParent");

        _further1SettingButton = _libraryPanel.Q<VisualElement>("further1-setting- button");
        _further2SettingButton = _libraryPanel.Q<VisualElement>("further2-setting-button");
        _further3SettingButton = _libraryPanel.Q<VisualElement>("further3-setting-button");
        _further4SettingButton = _libraryPanel.Q<VisualElement>("further4-setting-button");

        _intensitySlider = _libraryPanel.Q<Slider>("intensity-slider");
        _sizeSlider = _libraryPanel.Q<Slider>("size-slider");
        _heightSlider = _libraryPanel.Q<Slider>("height-slider");

        // ��ư �̺�Ʈ ��� 
        _libraryButton.clicked += () => OpenClosePanel(_libraryPanel);
        _libraryBackButton.clicked += () => OpenClosePanel(_libraryPanel);

        _intensitySlider.RegisterValueChangedCallback((x) => _itemChangeMnager.ChangeItensity(x.newValue));
        _sizeSlider.RegisterValueChangedCallback((x) => _itemChangeMnager.ChangeSize(x.newValue));
        _heightSlider.RegisterValueChangedCallback((x) => _fireWorkController.SetHeight(x.newValue)); 

        _sizeSlider.Q<Label>().style.color = Color.white;
        _intensitySlider.Q<Label>().style.color = Color.white;
        _heightSlider.Q<Label>().style.color = Color.white; 

        _sizeSlider.style.display = DisplayStyle.None; 
        _intensitySlider.style.display = DisplayStyle.None;
        _heightSlider.style.display = DisplayStyle.Flex; 

        _libraryButtonConstructor = new LibraryButtonConstructor(fireWorkController, itemChangeManager, _buttonParent);

        CreateHaveItems();
        LockOrUnlockSlider(); 
    }

    public override void UpdateSometing()
    {
        _libraryButtonConstructor.UpdateSometing();
        LockOrUnlockSlider(); 

        if(_isShopOpen == false)
        {
            UnlockShopButton();
        }
           }
    
    /// <summary>
    /// ���� ��ư ���� 
    /// </summary>
    private void UnlockShopButton()
    {
        if (AchievementManager.Instance.CheckHaveAchievement(_libraryOpenCode) == true)
        {
            _libraryLockIcon.style.display = DisplayStyle.None;
            _isShopOpen = true;
        }
    }

    public void LockOrUnlockSlider()
    {
        if(_isOpenSizeSlider == false && AchievementManager.Instance.CheckHaveAchievement(_sizeSliderOpenCode) == true)
        {
            _sizeSlider.style.display = DisplayStyle.Flex;
            _isOpenSizeSlider = true;
        }
        if (_isOpenIntensitySlider == false && AchievementManager.Instance.CheckHaveAchievement(_intensitySliderOpenCode) == true)
        {
            _intensitySlider.style.display = DisplayStyle.Flex;
            _isOpenIntensitySlider = true;
        }

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

            ItemBox item = new ItemBox(itemData, _itemDataSO);

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
        for (int i = 0; i < count; i++)
        {
            if (itemList[i].ItemCode == itemCode)
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
