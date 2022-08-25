using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIButtonManager : MonoBehaviour
{
    private UpgradeButtonConstructor _upgradeButtonConstructor; // 업그레이드 버튼 생성, 관리자 
    private SettingPanelComponent _settingPanelComponent; // 설정 패널 관리자ㅣ

    [SerializeField]
    private VisualTreeAsset _settingTemplate; // 설정 템플릿 
    private VisualElement _settingPanel; // 설정 패널 

    private UIDocument _mainUIDoc; // 메인 UIDocument 
    private VisualElement _rootElement; // 최상위 오브젝트 
    private VisualElement _gameScreen; // mainUI 화면 
    private VisualElement _bottomPanel; // 하단 UI 

    // 버튼
    private Button _settingButton; // 설정 버튼
    private Button _shopButton; // 상점버튼 
    private Button _libraryButton; // 라이브러리 버튼
    private Button _closeOpenButton; // UI 열고 닫기 버튼 

    // 하단 UI버튼들 


    // 라벨(텍스트)
    private Label _happyMoneyLabel; // 행복 재화 표시
    private Label _moneyLabel; // 돈 재화 표시 

    private FireWorkController _fireWorkController;

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
        _settingPanelComponent.Init(this); // 설정 버튼, 패널 캐싱 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 게임 화면 UI 활성화 비활성화 
            _gameScreen.style.display = _gameScreen.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
            //_gameScreen.visible = _gameScreen.visible == false ? true : false; 
        }
        UpdateMoneyText(); 
    }

    /// <summary>
    /// ui빌더의 element(오브젝트)  캐싱 
    /// </summary>
    private void CashingElements()
    {
          _fireWorkController = FindObjectOfType<FireWorkController>(); 

        _mainUIDoc = GetComponent<UIDocument>();
        _rootElement = _mainUIDoc.rootVisualElement;
        _gameScreen = _rootElement.Q<VisualElement>("game_screen");
        _bottomPanel = _rootElement.Q<VisualElement>("bottomLeft_panel"); 
        // 버튼 캐싱 
        _settingButton = _rootElement.Q<Button>("setting-button");
        //_shopButton = _rootElement.Q<Button>("ShopButton");
        //_libraryButton = _rootElement.Q<Button>("LibraryButton");
        _closeOpenButton = _rootElement.Q<Button>("close-open-button");


        //// 라벨 캐싱 
        _happyMoneyLabel = _rootElement.Q<Label>("happyMoney-label");
        _moneyLabel = _rootElement.Q<Label>("money-label");

        // 업그레이드 버튼 생성
        _upgradeButtonConstructor = new UpgradeButtonConstructor(_fireWorkController,_rootElement);
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
        //  _closeOpenButton.clicked += _bottomPanel.GetClasses
    }

    /// <summary>
    /// 하단 UI 열고 닫기 
    /// </summary>
    private void OpenCloseUI()
    {

    }
    public void UpdateMoneyText()
    {
        _happyMoneyLabel.text = string.Format("행복 재화 : {0}",UserSaveDataManager.Instance.UserSaveData.happy.ToString());
        _moneyLabel.text = string.Format("돈 재화 : {0}",UserSaveDataManager.Instance.UserSaveData.money.ToString()); 
    }
}
