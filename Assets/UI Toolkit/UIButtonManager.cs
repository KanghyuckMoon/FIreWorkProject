using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIButtonManager : MonoBehaviour
{
    private UpgradeButtonConstructor _upgradeButtonConstructor; 

    [SerializeField]
    private VisualTreeAsset _settingTemplate; // ���� ���ø� 
    private VisualElement _settingPanel; // ���� �г� 

    private UIDocument _mainUIDoc; // ���� UIDocument 
    private VisualElement _rootElement; // �ֻ��� ������Ʈ 
    private VisualElement _gameScreen; // mainUI ȭ�� 
    private VisualElement _bottomPanel; // �ϴ� UI 

    // ��ư
    private Button _settingButton; // ���� ��ư
    private Button _shopButton; // ������ư 
    private Button _libraryButton; // ���̺귯�� ��ư
    private Button _closeOpenButton; // UI ���� �ݱ� ��ư 

    // �ϴ� UI��ư�� 
    private Button _countUpgradeButton;
    private Button _further1UpgradeButton;
    private Button _further2UpgradeButton;
    private Button _further3UpgradeButton;
    private Button _further4UpgradeButton;
    private Button _rateUpgradeButton;

    // ��(�ؽ�Ʈ)
    private Label _happyMoneyLabel; // �ູ ��ȭ ǥ��
    private Label _moneyLabel; // �� ��ȭ ǥ�� 

    private FireWorkController _fireWorkController;
    private void Awake()
    {
        CashingElements();
        SetButtons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // ���� ȭ�� UI Ȱ��ȭ ��Ȱ��ȭ 
            _gameScreen.style.display = _gameScreen.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
            //_gameScreen.visible = _gameScreen.visible == false ? true : false; 
        }
    }
    /// <summary>
    /// ui������ element(������Ʈ)  ĳ�� 
    /// </summary>
    private void CashingElements()
    {
        //  _fireWorkController = FindObjectOfType<FireWorkController>(); 

        _mainUIDoc = GetComponent<UIDocument>();
        _rootElement = _mainUIDoc.rootVisualElement;
        _gameScreen = _rootElement.Q<VisualElement>("game_screen");
        _bottomPanel = _rootElement.Q<VisualElement>("bottomLeft_panel"); 
        // ��ư ĳ�� 
        _settingButton = _rootElement.Q<Button>("setting-button");
        //_shopButton = _rootElement.Q<Button>("ShopButton");
        //_libraryButton = _rootElement.Q<Button>("LibraryButton");
        _closeOpenButton = _rootElement.Q<Button>("close-open-button");


        //// �� ĳ�� 
        //_happyMoneyLabel = _rootElement.Q<Label>("HappyMoneyLabel");
        //_moneyLabel = _rootElement.Q<Label>("MoneyLabel");

        // ���׷��̵� ��ư ����
        _upgradeButtonConstructor = new UpgradeButtonConstructor(_fireWorkController,_rootElement);
    }

    /// <summary>
    /// �� ��ư�� �̺�Ʈ �ֱ� 
    /// </summary>
    private void SetButtons()
    {
        /*
        _settingButton.clicked +=
            _shopButton.clicked += 
        _libraryButton.clicked += 
        */
        //  _closeOpenButton.clicked += _bottomPanel.GetClasses

        _further1UpgradeButton.clickable.activators.Add(new ManipulatorActivationFilter { button = MouseButton.RightMouse });

    }

    /// <summary>
    /// �ϴ� UI ���� �ݱ� 
    /// </summary>
    private void OpenCloseUI()
    {

    }

    // ���� - ����Ʈ - further���� ���� ��� 
    // üũ 
}


/// <summary>
/// ���׷��̹�ư �߰� ������(���� �� ���� ) 
/// </summary>
public class UpgradeButtonConstructor
{
    private List<ButtonElement> _buttonElementList = new List<ButtonElement>(); // ���׷��̵� ��ư ����Ʈ
    private List<UpgradeButtonType> _buttonTypeList = new List<UpgradeButtonType>();  
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
        
        // ���׷��̵� ��ư ĳ�� �� ����Ʈ�� �ֱ� 
        foreach(UpgradeButtonType buttonType in _buttonTypeList)
        {
            upgradeButtonInfo = CheckElement(buttonType); // ���׷��̵� ��ư ���� ã�� 

            Button upgradeButton = rootElement.Q<Button>(upgradeButtonInfo.name); // ���׷��̵� ��ư 
            VisualElement lockElement = upgradeButton.Q<VisualElement>(lockIconName); // ��� ������
            ButtonElement buttonElement = new ButtonElement(upgradeButton, lockElement, upgradeButtonInfo.isLocked, buttonType); // ���� 

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
                upgradeButtonInfo.isLocked = _fireWorkController.IsCanFurther1;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateCount(1);
                break;
            case UpgradeButtonType.RateUp:
                upgradeButtonInfo.name = "rate-upgrade-button";
                upgradeButtonInfo.isLocked = _fireWorkController.IsCanFurther1;
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

        foreach(UpgradeButtonType buttonType in Enum.GetValues(typeof(UpgradeButtonType)))
        {
            _buttonTypeList.Add(buttonType); 
        }
    }
    private struct UpgradeButtonInfo
    {
        public string name; // �̸�
        public bool isLocked;  // ��� ���� 
        public Action clickEvent; 
    }
}

/// <summary>
/// ���׷��̵� ��ư 
/// </summary>
public class ButtonElement
{
    private UpgradeButtonType _type; // �ĺ��� 
    private Button _button; // �ڱ� �ڽ�
    private VisualElement _lockElement; // ��� ������ 

    private VisualElement _image; // �̹��� 
    private Label _nameLabel; // �̸� �ؽ�Ʈ��
    private Label _costLabel;  // ���� �ؽ�Ʈ�� 

    private bool _isLocked; // ����ִ� ���� 

    /// <summary>
    /// 
    /// </summary>
    /// <param name="visualElement">��ư �ڽ�</param>
    /// <param name="loackElement">��� �������̹���</param>
    /// <param name="isLocked">��� ����</param>
    /// <param name="code">�ĺ���</param>
    public ButtonElement(Button button, VisualElement loackElement, bool isLocked, UpgradeButtonType type)
    {
        this._button = button;
        this._lockElement = loackElement;
        this._isLocked = isLocked;
        this._type = type; 
    }

    public void LockButton()
    {
        if(_isLocked == true)
        {
            _lockElement.style.display = DisplayStyle.None;
            //_button.
            // ��ư Ŭ�� �ȵǵ��� 
            return;
        }
        _lockElement.style.display = DisplayStyle.Flex;
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
