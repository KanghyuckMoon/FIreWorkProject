using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIButtonManager : MonoBehaviour
{
    private UpgradeButtonConstructor _upgradeButtonConstructor; // 업그레이드 버튼 생성, 관리자 

    private SettingPanelComponent _settingPanelComponent; // 설정 패널 관리자
    [SerializeField]
    private ShopPanelComponent _shopPanelComponent; // 상점 패널 관리자 
    [SerializeField]
    private LibraryPanelComponent _libraryPanelComponent; // 라이브러리 패널 관리자 

    [SerializeField]
    private VisualTreeAsset _settingTemplate; // 설정 템플릿 
    private VisualElement _settingPanel; // 설정 패널 


    private UIDocument _mainUIDoc; // 메인 UIDocument 
    private VisualElement _rootElement; // 최상위 오브젝트 
    private VisualElement _gameScreen; // mainUI 화면 
    private VisualElement _bottomPanel; // 하단 UI 

    // 버튼
    private Button _closeOpenButton; // 업그레이드UI 열고 닫기 버튼 

    // 하단 UI버튼들 


    // 외부 캐싱 변수 
    private FireWorkController _fireWorkController;
    private HaveItemManager _haveItemManager;
    private ShopManager _shopManager;
    private GrapicSetting _graphicSetting;
    private SoundSetting _soundSetting;
    private Exit _exit;
    private ItemChangeManager _itemChangeManager; // 


    // 프로퍼티 
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
        _settingPanelComponent.Init(this, _graphicSetting, _soundSetting, _exit); // 설정 버튼, 패널 캐싱 
        _shopPanelComponent.Init(this, _haveItemManager, _shopManager, _libraryPanelComponent);
        _libraryPanelComponent.Init(this, _haveItemManager, _itemChangeManager, _fireWorkController);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 게임 화면 UI 활성화 비활성화 
            _gameScreen.style.display = _gameScreen.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
            //_gameScreen.visible = _gameScreen.visible == false ? true : false; 
        }
    }

    /// <summary>
    /// ui빌더의 element(오브젝트)  캐싱 
    /// </summary>
    private void CashingElements()
    {
        // 외부 변수 캐싱 
        _haveItemManager = FindObjectOfType<HaveItemManager>();
        _fireWorkController = FindObjectOfType<FireWorkController>();
        _itemChangeManager = FindObjectOfType<ItemChangeManager>();
        _shopManager = FindObjectOfType<ShopManager>();
        _graphicSetting = FindObjectOfType<GrapicSetting>();
        _soundSetting = FindObjectOfType<SoundSetting>();
        _exit = FindObjectOfType<Exit>();

        _mainUIDoc = GetComponent<UIDocument>();
        _rootElement = _mainUIDoc.rootVisualElement;
        _gameScreen = _rootElement.Q<VisualElement>("game_screen");
        _bottomPanel = _rootElement.Q<VisualElement>("bottom-panel");
        // 버튼 캐싱 
        _closeOpenButton = _rootElement.Q<Button>("close-open-button");


        //// 라벨 캐싱 


        // 업그레이드 버튼 생성
        _upgradeButtonConstructor = new UpgradeButtonConstructor(_fireWorkController, _rootElement);
    }

    /// <summary>
    /// 각 버튼에 이벤트 넣기 
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
            Debug.Log("닫거나 열어라");
        };
    }


    /// <summary>
    /// 하단 UI 열고 닫기 
    /// </summary>
    private void OpenCloseUI()
    {
        _bottomPanel.ToggleInClassList("offsetUp");
        _bottomPanel.ToggleInClassList("offsetDown");
    }



    // 테스트용 함수 
    [ContextMenu("라이브러리 아이템 생성 ")]
    public void CreateLibraryItems()
    {
        _libraryPanelComponent.CreateHaveItems();
    }

    [ContextMenu("상점 아이템 생성")]
    public void CreateShopItems()
    {
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType))) // 상점 아이템 생성 
        {
            _shopPanelComponent.CreateShopItem(itemType);
        }
    }
}
