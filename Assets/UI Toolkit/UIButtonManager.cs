using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIButtonManager : MonoBehaviour
{
    [SerializeField]
    private VisualTreeAsset _settingTemplate; // 설정 템플릿 
    private VisualElement _settingPanel; // 설정 패널 

    private UIDocument _mainUIDoc; // 메인 UIDocument 
    private VisualElement _rootElement; // 최상위 오브젝트 
    private VisualElement _gameScreen; // mainUI 화면 

    // 버튼
    private Button _settingButton; // 설정 버튼
    private Button _shopButton; // 상점버튼 
    private Button _libraryButton; // 라이브러리 버튼

    // 라벨(텍스트)
    private Label _happyMoneyLabel; // 행복 재화 표시
    private Label _moneyLabel; // 돈 재화 표시 

    private void Awake()
    {
        CashingElements();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 게임 화면 UI 활성화 비활성화 
            _gameScreen.style.display = _gameScreen.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex; 
        }
    }
    /// <summary>
    /// ui빌더의 element(오브젝트)  캐싱 
    /// </summary>
    private void CashingElements()
    {
        _mainUIDoc = GetComponent<UIDocument>();
        _rootElement = _mainUIDoc.rootVisualElement;
        _gameScreen = _rootElement.Q<VisualElement>("game_screen"); 
        // 버튼 캐싱 
        _settingButton = _rootElement.Q<Button>("SettingButton");
        //_shopButton = _rootElement.Q<Button>("ShopButton");
        //_libraryButton = _rootElement.Q<Button>("LibraryButton");

        //// 라벨 캐싱 
        //_happyMoneyLabel = _rootElement.Q<Label>("HappyMoneyLabel");
        //_moneyLabel = _rootElement.Q<Label>("MoneyLabel");
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
    }

}
