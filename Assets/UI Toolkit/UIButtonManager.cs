using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIButtonManager : MonoBehaviour
{
    [SerializeField]
    private VisualTreeAsset _settingTemplate; // ���� ���ø� 
    private VisualElement _settingPanel; // ���� �г� 

    private UIDocument _mainUIDoc; // ���� UIDocument 
    private VisualElement _rootElement; // �ֻ��� ������Ʈ 

    // ��ư
    private Button _settingButton; // ���� ��ư
    private Button _shopButton; // ������ư 
    private Button _libraryButton; // ���̺귯�� ��ư

    // ��(�ؽ�Ʈ)
    private Label _happyMoneyLabel; // �ູ ��ȭ ǥ��
    private Label _moneyLabel; // �� ��ȭ ǥ�� 

    private void Awake()
    {
        CashingElements(); 

    }

    /// <summary>
    /// ui������ element(������Ʈ)  ĳ�� 
    /// </summary>
    private void CashingElements()
    {
        _mainUIDoc = GetComponent<UIDocument>();
        _rootElement = _mainUIDoc.rootVisualElement;

        // ��ư ĳ�� 
        _settingButton = _rootElement.Q<Button>("SettingButton");
        //_shopButton = _rootElement.Q<Button>("ShopButton");
        //_libraryButton = _rootElement.Q<Button>("LibraryButton");

        //// �� ĳ�� 
        //_happyMoneyLabel = _rootElement.Q<Label>("HappyMoneyLabel");
        //_moneyLabel = _rootElement.Q<Label>("MoneyLabel");
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
    }

    [ContextMenu("Ȱ��ȭ �׽�Ʈ")]
    public void UpdateUI()
    {
        _settingButton.style.display = DisplayStyle.None; 
    }
}
