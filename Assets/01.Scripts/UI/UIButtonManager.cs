using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIButtonManager : MonoBehaviour
{
    private UpgradeButtonConstructor _upgradeButtonConstructor; // ���׷��̵� ��ư ����, ������ 
    private SettingPanelComponent _settingPanelComponent; // ���� �г� ������
    [SerializeField]
    private ShopPanelComponent _shopPanelComponent; // ���� �г� ������ 

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
    private Button _closeOpenButton; // ���׷��̵�UI ���� �ݱ� ��ư 

    // �ϴ� UI��ư�� 


    // ��(�ؽ�Ʈ)
    private Label _happyMoneyLabel; // �ູ ��ȭ ǥ��
    private Label _moneyLabel; // �� ��ȭ ǥ�� 

    // �ܺ� ĳ�� ���� 
    private FireWorkController _fireWorkController;
    private HaveItemManager _haveItemManager;
    private ShopManager _shopManager;
    private GrapicSetting _graphicSetting;
    private SoundSetting _soundSetting;
    private Exit _exit;

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
        //_shopPanelComponent = new ShopPanelComponent(); 
        _settingPanelComponent.Init(this, _graphicSetting, _soundSetting, _exit); // ���� ��ư, �г� ĳ�� 
        _shopPanelComponent.Init(this, _haveItemManager, _shopManager);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // ���� ȭ�� UI Ȱ��ȭ ��Ȱ��ȭ 
            _gameScreen.style.display = _gameScreen.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
            //_gameScreen.visible = _gameScreen.visible == false ? true : false; 
        }
        UpdateMoneyText();
    }

    /// <summary>
    /// ui������ element(������Ʈ)  ĳ�� 
    /// </summary>
    private void CashingElements()
    {
        // �ܺ� ���� ĳ�� 
        _haveItemManager = FindObjectOfType<HaveItemManager>(); 
        _fireWorkController = FindObjectOfType<FireWorkController>();
        _shopManager = FindObjectOfType<ShopManager>(); 
        _graphicSetting = FindObjectOfType<GrapicSetting>();
        _soundSetting = FindObjectOfType<SoundSetting>();
        _exit = FindObjectOfType<Exit>();

        _mainUIDoc = GetComponent<UIDocument>();
        _rootElement = _mainUIDoc.rootVisualElement;
        _gameScreen = _rootElement.Q<VisualElement>("game_screen");
        _bottomPanel = _rootElement.Q<VisualElement>("bottom-panel");
        // ��ư ĳ�� 
        _settingButton = _rootElement.Q<Button>("setting-button");
        _shopButton = _rootElement.Q<Button>("shop-button");
        _libraryButton = _rootElement.Q<Button>("library-button");
        _closeOpenButton = _rootElement.Q<Button>("close-open-button");


        //// �� ĳ�� 
        _happyMoneyLabel = _rootElement.Q<Label>("happyMoney-label");
        _moneyLabel = _rootElement.Q<Label>("money-label");

        // ���׷��̵� ��ư ����
        _upgradeButtonConstructor = new UpgradeButtonConstructor(_fireWorkController, _rootElement);
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
        _libraryButton.clicked += () => OpenCloseUI();
        _closeOpenButton.clicked += () =>
        {
            OpenCloseUI();
            Debug.Log("�ݰų� �����");
        };
    }


    /// <summary>
    /// �ϴ� UI ���� �ݱ� 
    /// </summary>
    private void OpenCloseUI()
    {
        _bottomPanel.ToggleInClassList("offsetUp");
        _bottomPanel.ToggleInClassList("offsetDown");
    }

    public void UpdateMoneyText()
    {
        _happyMoneyLabel.text = string.Format("�ູ ��ȭ : {0}", UserSaveDataManager.Instance.UserSaveData.happy.ToString());
        _moneyLabel.text = string.Format("�� ��ȭ : {0}", UserSaveDataManager.Instance.UserSaveData.money.ToString());
    }
}
