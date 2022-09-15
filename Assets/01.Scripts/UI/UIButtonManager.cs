using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIButtonManager : MonoBehaviour
{
    public AchievementViewManager AchievementViewManager
    {
        get
        {
            _achievementViewManager ??= FindObjectOfType<AchievementViewManager>();
            return _achievementViewManager;
        }
    }

     // public UpgradeButtonConstructor upgradeButtonConstructor; // ���׷��̵� ��ư ����, ������ 

    private SettingPanelComponent _settingPanelComponent; // ���� �г� ������
    [SerializeField]
    private ShopPanelComponent _shopPanelComponent; // ���� �г� ������ 
    [SerializeField]
    private LibraryPanelComponent _libraryPanelComponent; // ���̺귯�� �г� ������ 

    [SerializeField]
    private VisualTreeAsset _settingTemplate; // ���� ���ø�
    [SerializeField]
    private UpgradeButtonConstructor _upgradeUI; 
    private VisualElement _settingPanel; // ���� �г� 


    private UIDocument _mainUIDoc; // ���� UIDocument 
    private VisualElement _rootElement; // �ֻ��� ������Ʈ 
    private VisualElement _gameScreen; // mainUI ȭ�� 
    private VisualElement _moneyElement; // ���� ��� �� �ؽ�Ʈ 
    private VisualElement _bottomPanel; // �ϴ� UI 

    // ��ư
    private Button _closeOpenButton; // ���׷��̵�UI ���� �ݱ� ��ư 
    private Button _achievementButton; // ���� ��ư 
    private Button _upgradeOpenButton; // ���׷��̵�UI Ȱ��ȭ ��ư 

    // �ϴ� UI��ư�� 


    // �ܺ� ĳ�� ���� 
    private FireWorkController _fireWorkController;
    private HaveItemManager _haveItemManager;
    private ShopManager _shopManager;
    private GrapicSetting _graphicSetting;
    private SoundSetting _soundSetting;
    private Exit _exit;
    private ItemChangeManager _itemChangeManager; // 
    private AchievementViewManager _achievementViewManager;

    // ������Ƽ 
    public VisualElement RootElement => _rootElement;
//    public UpgradeButtonConstructor UpgradeButtonConstructor => _upgradeButtonConstructor; 
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
        _shopPanelComponent.Init(this, _haveItemManager, _shopManager, _libraryPanelComponent);
        _libraryPanelComponent.Init(this, _haveItemManager, _itemChangeManager, _fireWorkController, AchievementViewManager);

        _upgradeUI.ActiveUpgradeUI(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // ���� ȭ�� UI Ȱ��ȭ ��Ȱ��ȭ 
            _gameScreen.style.display = _gameScreen.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
            _moneyElement.style.display = _moneyElement.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex; 
            //_gameScreen.visible = _gameScreen.visible == false ? true : false; 
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _settingPanelComponent.OpenSetting(); 
        }

        _libraryPanelComponent.UpdateSometing();
       // upgradeButtonConstructor.UpdateSomething();
        _shopPanelComponent.UpdateSometing(); 
    }

    /// <summary>
    /// ui������ element(������Ʈ)  ĳ�� 
    /// </summary>
    private void CashingElements()
    {
        // �ܺ� ���� ĳ�� 
        _haveItemManager = FindObjectOfType<HaveItemManager>();
        _fireWorkController = FindObjectOfType<FireWorkController>();
        _itemChangeManager = FindObjectOfType<ItemChangeManager>();
        _shopManager = FindObjectOfType<ShopManager>();
        _graphicSetting = FindObjectOfType<GrapicSetting>();
        _soundSetting = FindObjectOfType<SoundSetting>();
        _exit = FindObjectOfType<Exit>();

        _upgradeUI = FindObjectOfType<UpgradeButtonConstructor>();

        _moneyElement = FindObjectOfType<MoneyManager>().RootElement; 

        _mainUIDoc = GetComponent<UIDocument>();
        _rootElement = _mainUIDoc.rootVisualElement;
        _gameScreen = _rootElement.Q<VisualElement>("game_screen");
        _bottomPanel = _rootElement.Q<VisualElement>("bottom-panel");
        
        // ��ư ĳ�� 
        _closeOpenButton = _rootElement.Q<Button>("close-open-button");
        _achievementButton = _rootElement.Q<Button>("achievement-button");
        _upgradeOpenButton = _rootElement.Q<Button>("upgradeUI-button");
        //// �� ĳ�� 


        // ���׷��̵� ��ư ����
        //upgradeButtonConstructor = new UpgradeButtonConstructor(_fireWorkController, _rootElement);
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
        _closeOpenButton.clicked += () =>
        {
            OpenCloseUI();
            Debug.Log("�ݰų� �����");
        };
        _achievementButton.clicked += () => _achievementViewManager.OpenAchievementView();
        _upgradeOpenButton.clicked += () =>
        {
            _upgradeUI.ActiveUpgradeUI(true); 
            Debug.Log("���׷��̵�â ������");
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



    // �׽�Ʈ�� �Լ� 
    [ContextMenu("���̺귯�� ������ ���� ")]
    public void CreateLibraryItems()
    {
        _libraryPanelComponent.CreateHaveItems();
    }

    [ContextMenu("���� ������ ����")]
    public void CreateShopItems()
    {
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType))) // ���� ������ ���� 
        {
            _shopPanelComponent.CreateShopItem(itemType);
        }
    }
}
