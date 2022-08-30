using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

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
        // ���׷��̵� ��ư ĳ�� �� ����Ʈ�� �ֱ� 
        foreach (UpgradeButtonType buttonType in _buttonTypeList)
        {
            upgradeButtonInfo = CheckElement(buttonType); // ���׷��̵� ��ư ���� ã�� 

            VisualElement upgradeButtonParent = rootElement.Q<VisualElement>(upgradeButtonInfo.name); // ���׷��̵� ��ư �θ� element
            VisualElement lockElement = upgradeButtonParent.Q<VisualElement>(lockIconName); // ��� ������
            Button upgradeButton = upgradeButtonParent.Q<Button>(buttonName); // ��ư 

            UpgradeButtonElement buttonElement = new UpgradeButtonElement(upgradeButton, lockElement, upgradeButtonInfo.isLocked, buttonType); // ���� 

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
    /// ��ư�� ��װų� ������� isLocked - true ��� / isLocked - false ��� ���� 
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
    public string name; // �̸�
    public bool isLocked;  // ��� ���� 
    public Action clickEvent;
}
/// <summary>
/// ���׷��̵� ��ư (SO�� ������ ����..) 
/// </summary>
public class UpgradeButtonElement
{
    public UpgradeButtonType _type; // �ĺ��� 
    public Button _button; // �ڱ� �ڽ�
    public VisualElement _lockElement; // ��� ������ 

    private VisualElement _image; // �̹��� 
    private Label _nameLabel; // �̸� �ؽ�Ʈ��
    private Label _costLabel;  // ���� �ؽ�Ʈ�� 

    private bool _isLocked; // ����ִ� ���� 

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
