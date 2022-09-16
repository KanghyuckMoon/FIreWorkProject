using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LibraryButtonConstructor
{
    private ItemChangeManager _itemChangeManager;
    private DescriptionManager _descriptionManager;

    private List<UpgradeButtonElement> _buttonElementList = new List<UpgradeButtonElement>(); // 라이브러리 버튼 리스트

    private List<UpgradeButtonType> _buttonTypeList = new List<UpgradeButtonType>();  // 모든 버튼 타입이 담겨있는 리스트
    private FireWorkController _fireWorkController;

    private bool _isOpenFurther2 =  false;
    private bool _isOpenFurther3 = false;
    private bool _isOpenFurther4 = false;

    public LibraryButtonConstructor(FireWorkController fireWorkController, ItemChangeManager itemChangeManager, DescriptionManager descriptionManager,VisualElement rootElement)
    {
        InitEnumList();
        _buttonElementList.Clear();

        this._itemChangeManager = itemChangeManager;
        this._descriptionManager = descriptionManager; 
        this._fireWorkController = fireWorkController;

        UpgradeButtonInfo upgradeButtonInfo; // 찾을 버튼 정보(이름, 잠김여부) 
        string lockIconName = "lock-icon";
        string buttonName = "button";
        // 업그레이드 버튼 캐싱 후 리스트에 넣기 
        foreach (UpgradeButtonType buttonType in _buttonTypeList)
        {
            if (buttonType == UpgradeButtonType.CountUp || buttonType == UpgradeButtonType.RateUp || buttonType == UpgradeButtonType.Renewal) continue;

            upgradeButtonInfo = CheckElement(buttonType); // 업그레이드 버튼 정보 찾기 

            VisualElement upgradeButtonParent = rootElement.Q<VisualElement>(upgradeButtonInfo.name); // 업그레이드 버튼 부모 element
            VisualElement lockElement = upgradeButtonParent.Q<VisualElement>(lockIconName); // 잠금 아이콘
            Button upgradeButton = upgradeButtonParent.Q<Button>(buttonName); // 버튼 

            UpgradeButtonElement buttonElement = new UpgradeButtonElement(upgradeButton, lockElement, upgradeButtonInfo.isOpened, buttonType); // 생성 

            upgradeButton.clicked += upgradeButtonInfo.clickEvent; // 클릭 이벤트 넣기 

            _buttonElementList.Add(buttonElement);
        }
    }

    /// <summary>
    /// 버튼마다 이름, 잠금여부, 클릭이벤트 반환  
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private UpgradeButtonInfo CheckElement(UpgradeButtonType type)
    {
        UpgradeButtonInfo upgradeButtonInfo = new UpgradeButtonInfo();
        switch (type)
        {
            case UpgradeButtonType.Further1:
                upgradeButtonInfo.name = "further1-setting-button";
                upgradeButtonInfo.isOpened = _fireWorkController.IsCanFurther1;
                upgradeButtonInfo.clickEvent = () => _itemChangeManager.ChangeFurther(ItemChangeManager.CurrentSettingMode.Further1);
                break;
            case UpgradeButtonType.Further2:
                upgradeButtonInfo.name = "further2-setting-button";
                upgradeButtonInfo.isOpened = _fireWorkController.IsCanFurther2;
                upgradeButtonInfo.clickEvent = () => _itemChangeManager.ChangeFurther(ItemChangeManager.CurrentSettingMode.Further2);
                break;
        }
        return upgradeButtonInfo;
    }

    public void UpdateSometing()
    {
        if(_fireWorkController.IsCanFurther2 == true && _isOpenFurther2 == false)
        {
            LockOrUnlockButton(UpgradeButtonType.Further2, true);
            _isOpenFurther2 = true; 
        }
    }
    private void InitEnumList()
    {
        _buttonTypeList.Clear();

        foreach (UpgradeButtonType buttonType in Enum.GetValues(typeof(UpgradeButtonType)))
        {
            _buttonTypeList.Add(buttonType);
        }
    }


    public void LockOrUnlockButton(UpgradeButtonType buttonType, bool isOpened)
    {
        _buttonElementList.ForEach((x) =>
        {
            if (x._buttonType == buttonType)
            {
                x.IsOpened = isOpened;
                x.LockButton();
            }
        });
    }
}
