using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIButtonManager : MonoBehaviour
{
    private UpgradeButtonConstructor _upgradeButtonConstructor; 

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
    private Button _countUpgradeButton;
    private Button _further1UpgradeButton;
    private Button _further2UpgradeButton;
    private Button _further3UpgradeButton;
    private Button _further4UpgradeButton;
    private Button _rateUpgradeButton;

    // 라벨(텍스트)
    private Label _happyMoneyLabel; // 행복 재화 표시
    private Label _moneyLabel; // 돈 재화 표시 

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
        //  _fireWorkController = FindObjectOfType<FireWorkController>(); 

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
        //_happyMoneyLabel = _rootElement.Q<Label>("HappyMoneyLabel");
        //_moneyLabel = _rootElement.Q<Label>("MoneyLabel");

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

        _further1UpgradeButton.clickable.activators.Add(new ManipulatorActivationFilter { button = MouseButton.RightMouse });

    }

    /// <summary>
    /// 하단 UI 열고 닫기 
    /// </summary>
    private void OpenCloseUI()
    {

    }

    // 생성 - 리스트 - further값에 따라 잠금 
    // 체크 
}


/// <summary>
/// 업그레이버튼 중간 관리자(생성 및 관리 ) 
/// </summary>
public class UpgradeButtonConstructor
{
    private List<ButtonElement> _buttonElementList = new List<ButtonElement>(); // 업그레이든 버튼 리스트
    private List<UpgradeButtonType> _buttonTypeList = new List<UpgradeButtonType>();  
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
        
        // 업그레이드 버튼 캐싱 후 리스트에 넣기 
        foreach(UpgradeButtonType buttonType in _buttonTypeList)
        {
            upgradeButtonInfo = CheckElement(buttonType); // 업그레이드 버튼 정보 찾기 

            Button upgradeButton = rootElement.Q<Button>(upgradeButtonInfo.name); // 업그레이드 버튼 
            VisualElement lockElement = upgradeButton.Q<VisualElement>(lockIconName); // 잠금 아이콘
            ButtonElement buttonElement = new ButtonElement(upgradeButton, lockElement, upgradeButtonInfo.isLocked, buttonType); // 생성 

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
        public string name; // 이름
        public bool isLocked;  // 잠금 여부 
        public Action clickEvent; 
    }
}

/// <summary>
/// 업그레이드 버튼 
/// </summary>
public class ButtonElement
{
    private UpgradeButtonType _type; // 식별자 
    private Button _button; // 자기 자신
    private VisualElement _lockElement; // 잠금 아이콘 

    private VisualElement _image; // 이미지 
    private Label _nameLabel; // 이름 텍스트라벨
    private Label _costLabel;  // 가격 텍스트라벨 

    private bool _isLocked; // 잠겨있는 상태 

    /// <summary>
    /// 
    /// </summary>
    /// <param name="visualElement">버튼 자신</param>
    /// <param name="loackElement">잠금 아이콘이미지</param>
    /// <param name="isLocked">잠금 여부</param>
    /// <param name="code">식별자</param>
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
            // 버튼 클릭 안되도록 
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
