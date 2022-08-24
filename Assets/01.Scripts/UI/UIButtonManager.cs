using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIButtonManager : MonoBehaviour
{
    private UpgradeButtonConstructor _upgradeButtonConstructor; // ���׷��̵� ��ư ����, ������ 
    private SettingPanelComponent _settingPanelComponent; // ���� �г� �����ڤ�

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


    // ��(�ؽ�Ʈ)
    private Label _happyMoneyLabel; // �ູ ��ȭ ǥ��
    private Label _moneyLabel; // �� ��ȭ ǥ�� 

    private FireWorkController _fireWorkController;

    // ������Ƽ 
    public VisualElement RootElement => _rootElement; 

    private void Awake()
    {
        CashingElements();
        SetButtons();
    }

    private void Start()
    {
        _settingPanelComponent = new SettingPanelComponent(); 
        _settingPanelComponent.Init(this); // ���� ��ư, �г� ĳ�� 
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
          _fireWorkController = FindObjectOfType<FireWorkController>(); 

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
