using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection; 

/// <summary>
/// ���׷��̹�ư �߰� ������(���� �� ���� ) 
/// </summary>
[Serializable]
public class UpgradeButtonConstructor
{
    private List<UpgradeButtonElement> _buttonElementList = new List<UpgradeButtonElement>(); // ���׷��̵� ��ư ����Ʈ

    private List<UpgradeButtonType> _buttonTypeList = new List<UpgradeButtonType>();  // ��� ��ư Ÿ���� ����ִ� ����Ʈ
    private FireWorkController _fireWorkController;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fireWorkController"></param>
    /// <param name="rootElement"></param>
    /// <param name="count">���׷��̵� ��ư ����</param>
    public UpgradeButtonConstructor(FireWorkController fireWorkController, VisualElement rootElement)
    {
        InitEnumList(); // enumList�ʱ�ȭ 
        _buttonElementList.Clear();

        this._fireWorkController = fireWorkController;

        UpgradeButtonInfo upgradeButtonInfo; // ã�� ��ư ����(�̸�, ��迩��) 
        string lockIconName = "lock-icon";
        string buttonName = "button";
        string costName = "ItemCost";
        // ���׷��̵� ��ư ĳ�� �� ����Ʈ�� �ֱ� 
        foreach (UpgradeButtonType buttonType in _buttonTypeList)
        {
            upgradeButtonInfo = CheckElement(buttonType); // ���׷��̵� ��ư ���� ã�� 

            VisualElement upgradeButtonParent = rootElement.Q<VisualElement>(upgradeButtonInfo.name); // ���׷��̵� ��ư �θ� element
            VisualElement lockElement = upgradeButtonParent.Q<VisualElement>(lockIconName); // ��� ������
            Button upgradeButton = upgradeButtonParent.Q<Button>(buttonName); // ��ư 
            Label costLabel = upgradeButton.Q<Label>(costName); // ���� 

            UpgradeButtonElement buttonElement = new UpgradeButtonElement(upgradeButton, lockElement, upgradeButtonInfo.isLocked, buttonType, 
                                                                                                                            _fireWorkController,costLabel, upgradeButtonInfo.costPropertyName); // ���� 

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
            case UpgradeButtonType.CountUp:
                upgradeButtonInfo.name = "count-upgrade-button";
                upgradeButtonInfo.isLocked = false;
                upgradeButtonInfo.costPropertyName = "CountCost";
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateCount(1);
                break;
            case UpgradeButtonType.RateUp:
                upgradeButtonInfo.name = "rate-upgrade-button";
                upgradeButtonInfo.isLocked = false;
                upgradeButtonInfo.costPropertyName = "RateCost";
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateRate(0.5f);
                break;
            case UpgradeButtonType.Further1:
                upgradeButtonInfo.name = "further1-upgrade-button";
                upgradeButtonInfo.costPropertyName = "Further1Cost";
                upgradeButtonInfo.isLocked = _fireWorkController.IsCanFurther1;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateFurtherCount1(1);
                break;
            case UpgradeButtonType.Further2:
                upgradeButtonInfo.name = "further2-upgrade-button";
                upgradeButtonInfo.costPropertyName = "Further2Cost"; 
                upgradeButtonInfo.isLocked = _fireWorkController.IsCanFurther2;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateFurtherCount2(1);
                break;
            case UpgradeButtonType.Further3:
                upgradeButtonInfo.name = "further3-upgrade-button";
                upgradeButtonInfo.costPropertyName = "Further3Cost";
                upgradeButtonInfo.isLocked = _fireWorkController.IsCanFurther3;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateFurtherCount3(1);
                break;
            case UpgradeButtonType.Further4:
                upgradeButtonInfo.name = "further4-upgrade-button";
                upgradeButtonInfo.costPropertyName = "Further4Cost"; 
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
    /// ���� �ؽ�Ʈ ������Ʈ
    /// </summary>
    public void UpdateCostText()
    {
        foreach(var buttonElement in _buttonElementList)
        {
            buttonElement.SetCostText(); 
        }
    }

    /// <summary>
    /// ��ư�� ��װų� ������� isLocked - true ��� / isLocked - false ��� ���� 
    /// </summary>
    /// <param name="buttonType"></param>
    /// <param name="isLocked"></param>
    public void LockOrUnlockButton(UpgradeButtonType buttonType, bool isLocked)
    {
        _buttonElementList.ForEach((x) =>
        {
            if (x._buttonType == buttonType)
            {
                x.IsLocked = isLocked;
                x.LockButton();
            }
        });
    }


}
public struct UpgradeButtonInfo
{
    public string name; // �̸�
    public bool isLocked;  // ��� ���� 
    public string costPropertyName; // ���� �Ӽ� �̸� 
    public Action clickEvent;
     
}
/// <summary>
/// ���׷��̵� ��ư (SO�� ������ ����..) 
/// </summary>
public class UpgradeButtonElement
{


    public UpgradeButtonType _buttonType; // �ĺ��� 
    public Button _button; // �ڱ� �ڽ�
    public VisualElement _lockElement; // ��� ������ 

    private FireWorkController _fireWorkController;
    private VisualElement _image; // �̹��� 
    private Label _nameLabel; // �̸� �ؽ�Ʈ��
    private Label _costLabel;  // ���� �ؽ�Ʈ�� 

    private bool _isLocked; // ����ִ� ���� 
    private int _cost; // ����
    private PropertyInfo _propertyInfo; // ���� ������Ƽ 

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
    /// <param name="visualElement">��ư �ڽ�</param>
    /// <param name="loackElement">��� �������̹���</param>
    /// <param name="isLocked">��� ����</param>
    /// <param name="code">�ĺ���</param>
    public UpgradeButtonElement(Button button, VisualElement loackElement,  bool isLocked, UpgradeButtonType buttonType, FireWorkController fireworkController = null, Label costLabel = null,string propertyName = null )
    {


        this._button = button;
        this._costLabel = costLabel; 
        this._lockElement = loackElement;
        this._isLocked = isLocked;
        this._buttonType = buttonType;

        if (propertyName != null)
        {
            _fireWorkController = fireworkController;
            Type type = typeof(FireWorkController);
            _propertyInfo = type.GetProperty(propertyName);
            Debug.Log(_propertyInfo.Name);
            SetCostText();
            Debug.Log(Enum.GetName(typeof(UpgradeButtonType), buttonType) + "�� ������ : " + _cost);
        }

        LockButton();
    }

    public void SetCostText()
    {
        _cost = (int)_propertyInfo.GetValue(_fireWorkController);
        Debug.Log(_cost);
        _costLabel.text = _cost.ToString(); 
    }

    public void LockButton()
    {
        if (_isLocked == true) // ��������� 
        {
            _lockElement.style.display = DisplayStyle.Flex;
            _button.style.display = DisplayStyle.None;
            // ��ư Ŭ�� �ȵǵ��� 
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
