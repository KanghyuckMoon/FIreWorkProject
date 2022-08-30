using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

/// <summary>
/// 업그레이버튼 중간 관리자(생성 및 관리 ) 
/// </summary>
[Serializable]
public class UpgradeButtonConstructor
{
    private List<UpgradeButtonElement> _buttonElementList = new List<UpgradeButtonElement>(); // 업그레이든 버튼 리스트

    private List<UpgradeButtonType> _buttonTypeList = new List<UpgradeButtonType>();  // 모든 버튼 타입이 담겨있는 리스트
    private FireWorkController _fireWorkController;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fireWorkController"></param>
    /// <param name="rootElement"></param>
    /// <param name="count">업그레이드 버튼 개수</param>
    public UpgradeButtonConstructor(FireWorkController fireWorkController, VisualElement rootElement)
    {
        InitEnumList(); // enumList초기화 
        _buttonElementList.Clear();

        this._fireWorkController = fireWorkController;

        UpgradeButtonInfo upgradeButtonInfo; // 찾을 버튼 정보(이름, 잠김여부) 
        string lockIconName = "lock-icon";
        string buttonName = "button";
        // 업그레이드 버튼 캐싱 후 리스트에 넣기 
        foreach (UpgradeButtonType buttonType in _buttonTypeList)
        {
            upgradeButtonInfo = CheckElement(buttonType); // 업그레이드 버튼 정보 찾기 

            VisualElement upgradeButtonParent = rootElement.Q<VisualElement>(upgradeButtonInfo.name); // 업그레이드 버튼 부모 element
            VisualElement lockElement = upgradeButtonParent.Q<VisualElement>(lockIconName); // 잠금 아이콘
            Button upgradeButton = upgradeButtonParent.Q<Button>(buttonName); // 버튼 

            UpgradeButtonElement buttonElement = new UpgradeButtonElement(upgradeButton, lockElement, upgradeButtonInfo.isLocked, buttonType); // 생성 

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
            case UpgradeButtonType.CountUp:
                upgradeButtonInfo.name = "count-upgrade-button";
                upgradeButtonInfo.isLocked = false;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateCount(1);
                break;
            case UpgradeButtonType.RateUp:
                upgradeButtonInfo.name = "rate-upgrade-button";
                upgradeButtonInfo.isLocked = false;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateRate(0.5f);
                break;
            case UpgradeButtonType.Further1:
                upgradeButtonInfo.name = "further1-upgrade-button";
                upgradeButtonInfo.isLocked = _fireWorkController.IsCanFurther1;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateFurtherCount1(1);
                break;
            case UpgradeButtonType.Further2:
                upgradeButtonInfo.name = "further2-upgrade-button";
                upgradeButtonInfo.isLocked = _fireWorkController.IsCanFurther2;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateFurtherCount2(1);
                break;
            case UpgradeButtonType.Further3:
                upgradeButtonInfo.name = "further3-upgrade-button";
                upgradeButtonInfo.isLocked = _fireWorkController.IsCanFurther3;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateFurtherCount3(1);
                break;
            case UpgradeButtonType.Further4:
                upgradeButtonInfo.name = "further4-upgrade-button";
                upgradeButtonInfo.isLocked = _fireWorkController.IsCanFurther4;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateFurtherCount4(1);
                break;
        }
        return upgradeButtonInfo;
    }

    private void InitEnumList()
    {
        _buttonTypeList.Clear();

        foreach (UpgradeButtonType buttonType in Enum.GetValues(typeof(UpgradeButtonType)))
        {
            _buttonTypeList.Add(buttonType);
        }
    }


    /// <summary>
    /// 버튼을 잠그거나 잠금해제 isLocked - true 잠금 / isLocked - false 잠금 해제 
    /// </summary>
    /// <param name="buttonType"></param>
    /// <param name="isLocked"></param>
    public void LockOrUnlockButton(UpgradeButtonType buttonType, bool isLocked)
    {
        _buttonElementList.ForEach((x) =>
        {
            if (x._type == buttonType)
            {
                x.IsLocked = isLocked;
                x.LockButton();
            }
        });
    }


}
public struct UpgradeButtonInfo
{
    public string name; // 이름
    public bool isLocked;  // 잠금 여부 
    public Action clickEvent;
}
/// <summary>
/// 업그레이드 버튼 (SO로 관리할 수도..) 
/// </summary>
public class UpgradeButtonElement
{
    public UpgradeButtonType _type; // 식별자 
    public Button _button; // 자기 자신
    public VisualElement _lockElement; // 잠금 아이콘 

    private VisualElement _image; // 이미지 
    private Label _nameLabel; // 이름 텍스트라벨
    private Label _costLabel;  // 가격 텍스트라벨 

    private bool _isLocked; // 잠겨있는 상태 

    public bool IsLocked
    {
        get => _isLocked;
        set
        {
            _isLocked = value;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="visualElement">버튼 자신</param>
    /// <param name="loackElement">잠금 아이콘이미지</param>
    /// <param name="isLocked">잠금 여부</param>
    /// <param name="code">식별자</param>
    public UpgradeButtonElement(Button button, VisualElement loackElement, bool isLocked, UpgradeButtonType type)
    {
        this._button = button;
        this._lockElement = loackElement;
        this._isLocked = isLocked;
        this._type = type;

        LockButton();
    }

    public void LockButton()
    {
        if (_isLocked == true) // 잠겨있으면 
        {
            _lockElement.style.display = DisplayStyle.Flex;
            _button.style.display = DisplayStyle.None;
            // 버튼 클릭 안되도록 
            return;
        }
        _lockElement.style.display = DisplayStyle.None;
        _button.style.display = DisplayStyle.Flex;
    }
}


public enum UpgradeButtonType
{
    CountUp,
    RateUp,
    Further1,
    Further2,
    Further3,
    Further4
}
