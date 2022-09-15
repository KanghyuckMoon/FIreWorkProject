using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LibraryButtonConstructor
{
    private ItemChangeManager _itemChangeManager;
    private DescriptionManager _descriptionManager;

    private List<UpgradeButtonElement> _buttonElementList = new List<UpgradeButtonElement>(); // ���̺귯�� ��ư ����Ʈ

    private List<UpgradeButtonType> _buttonTypeList = new List<UpgradeButtonType>();  // ��� ��ư Ÿ���� ����ִ� ����Ʈ
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

        UpgradeButtonInfo upgradeButtonInfo; // ã�� ��ư ����(�̸�, ��迩��) 
        string lockIconName = "lock-icon";
        string buttonName = "button";
        // ���׷��̵� ��ư ĳ�� �� ����Ʈ�� �ֱ� 
        foreach (UpgradeButtonType buttonType in _buttonTypeList)
        {
            if (buttonType == UpgradeButtonType.CountUp || buttonType == UpgradeButtonType.RateUp || buttonType == UpgradeButtonType.Renewal) continue;

            upgradeButtonInfo = CheckElement(buttonType); // ���׷��̵� ��ư ���� ã�� 

            VisualElement upgradeButtonParent = rootElement.Q<VisualElement>(upgradeButtonInfo.name); // ���׷��̵� ��ư �θ� element
            VisualElement lockElement = upgradeButtonParent.Q<VisualElement>(lockIconName); // ��� ������
            Button upgradeButton = upgradeButtonParent.Q<Button>(buttonName); // ��ư 

            UpgradeButtonElement buttonElement = new UpgradeButtonElement(upgradeButton, lockElement, upgradeButtonInfo.isOpened, buttonType); // ���� 

            upgradeButton.clicked += upgradeButtonInfo.clickEvent; // Ŭ�� �̺�Ʈ �ֱ� 

            _buttonElementList.Add(buttonElement);
        }
    }

    /// <summary>
    /// ��ư���� �̸�, ��ݿ���, Ŭ���̺�Ʈ ��ȯ  
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
