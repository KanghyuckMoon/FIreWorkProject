using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;

/// <summary>
/// ���׷��̹�ư �߰� ������(���� �� ���� ) 
/// </summary>
public class UpgradeButtonConstructor : MonoBehaviour
{
    private FireWorkController _fireWorkController;
    private DescriptionManager _descriptionManager; 

    private VisualElement _rootElement;
    private UIDocument _mainUIDoc;

    private VisualElement _descriptionPanel;

    private Button _backButton; // �ݱ� ��ư 

    private List<UpgradeButtonElement> _buttonElementList = new List<UpgradeButtonElement>(); // ���׷��̵� ��ư ����Ʈ

    private List<UpgradeButtonType> _buttonTypeList = new List<UpgradeButtonType>();  // ��� ��ư Ÿ���� ����ִ� ����Ʈ

    private bool _isOpenFurther2 = false;
    private bool _isOpenRenewal = false;

    [SerializeField]
    private GameObject _backgroundCanvas; 

    private void Awake()
    {
        _fireWorkController = FindObjectOfType<FireWorkController>();
        _descriptionManager = FindObjectOfType<DescriptionManager>(); 
        _mainUIDoc = GetComponent<UIDocument>();
        _rootElement = _mainUIDoc.rootVisualElement;
    }

    private void Start()
    {
        Init(); 
    }

    private void Update()
    {
        
        if (_isOpenFurther2 == false && _fireWorkController.IsCanFurther2 == true)
        {
            LockOrUnlockButton(UpgradeButtonType.Further2, true);
            _isOpenFurther2 = true;
        }
        if (_isOpenRenewal == false && _fireWorkController.IsCanRenewal == true)
        {
            LockOrUnlockButton(UpgradeButtonType.Renewal, true);
            _isOpenRenewal = true;
        }
    }
    private void Init()
    {
        _rootElement.style.display = DisplayStyle.None; 

        _buttonElementList = new List<UpgradeButtonElement>();
        _buttonTypeList = new List<UpgradeButtonType>(); 

        _descriptionPanel = _rootElement.Q<VisualElement>("description-panel");
        _descriptionPanel.style.display = DisplayStyle.None;

        _backButton = _rootElement.Q<Button>("back-button");
        _backButton.clicked += () =>
        {
            ActiveUpgradeUI(false); 
            Debug.Log("���׷��̵�â ������");
        };
        InitEnumList(); // enumList�ʱ�ȭ 
        _buttonElementList.Clear();

        UpgradeButtonInfo upgradeButtonInfo; // ã�� ��ư ����(�̸�, ��迩��) 
        string lockIconName = "lock-icon";
        string buttonName = "button";
        string costName = "ItemCost";
        // ���׷��̵� ��ư ĳ�� �� ����Ʈ�� �ֱ� 
        foreach (UpgradeButtonType buttonType in _buttonTypeList)
        {
            if (buttonType == UpgradeButtonType.Renewal) continue;   // �ӽ� ��ŵ 

            upgradeButtonInfo = CheckElement(buttonType); // ���׷��̵� ��ư ���� ã�� 

            VisualElement upgradeButtonParent = _rootElement.Q<VisualElement>(upgradeButtonInfo.name); // ���׷��̵� ��ư �θ� element
            VisualElement lockElement = upgradeButtonParent.Q<VisualElement>(lockIconName); // ��� ������
            Button upgradeButton = upgradeButtonParent.Q<Button>(buttonName); // ��ư 
            Label costLabel = upgradeButton.Q<Label>(costName); // ���� 

            UpgradeButtonElement buttonElement = new UpgradeButtonElement(upgradeButton, lockElement, upgradeButtonInfo.isOpened, buttonType,
                                                                                                                            _fireWorkController, costLabel, upgradeButtonInfo.costPropertyName); // ���� 

            upgradeButton.clicked += upgradeButtonInfo.clickEvent; // Ŭ�� �̺�Ʈ �ֱ� 
            //upgradeButton.RegisterCallback<MouseOverEvent>((x) => ActiveDescription(x,upgradeButton));
            //upgradeButton.RegisterCallback<MouseOutEvent>((x) => DisableDescription(x));
            
            // ����â Ȱ��ȭ �̺�Ʈ ��� 
            upgradeButton.RegisterCallback<MouseOverEvent>((x) => _descriptionManager.ActiveDescription(true,(int)buttonType + (int)DescriptionType.Upgrade));
            upgradeButton.RegisterCallback<MouseOutEvent>((x) => _descriptionManager.ActiveDescription(false, (int)buttonType + (int)DescriptionType.Upgrade));


            _buttonElementList.Add(buttonElement);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fireWorkController"></param>
    /// <param name="rootElement"></param>
    /// <param name="count">���׷��̵� ��ư ����</param>
    public UpgradeButtonConstructor(FireWorkController fireWorkController, VisualElement rootElement)
    {
        _descriptionPanel = _rootElement.Q<VisualElement>("description-panel");
        _descriptionPanel.style.display = DisplayStyle.None;


        InitEnumList(); // enumList�ʱ�ȭ 
        _buttonElementList.Clear();

        this._fireWorkController = fireWorkController;

        UpgradeButtonInfo upgradeButtonInfo; // ã�� ��ư ����(�̸�, ��迩��) 
        string lockIconName = "lock-icon";
        string buttonName = "button";
        string costName = "ItemCost";
        // ���׷��̵� ��ư ĳ�� �� ����Ʈ�� �ֱ� 
        foreach (UpgradeButtonType buttonType in _buttonTypeList)
        {
            upgradeButtonInfo = CheckElement(buttonType); // ���׷��̵� ��ư ���� ã�� 

            VisualElement upgradeButtonParent = rootElement.Q<VisualElement>(upgradeButtonInfo.name); // ���׷��̵� ��ư �θ� element
            VisualElement lockElement = upgradeButtonParent.Q<VisualElement>(lockIconName); // ��� ������
            Button upgradeButton = upgradeButtonParent.Q<Button>(buttonName); // ��ư 
            Label costLabel = upgradeButton.Q<Label>(costName); // ���� 

            UpgradeButtonElement buttonElement = new UpgradeButtonElement(upgradeButton, lockElement, upgradeButtonInfo.isOpened, buttonType,
                                                                                                                            _fireWorkController, costLabel, upgradeButtonInfo.costPropertyName); // ���� 

            upgradeButton.clicked += upgradeButtonInfo.clickEvent; // Ŭ�� �̺�Ʈ �ֱ� 
            upgradeButton.RegisterCallback<MouseOverEvent>((x) =>
            { ActiveDescription(x, upgradeButton);
                Debug.Log("���콺 ����");
            });
            upgradeButton.RegisterCallback<MouseOutEvent>(DisableDescription);

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
                upgradeButtonInfo.isOpened = true;
                upgradeButtonInfo.costPropertyName = "CountCost";
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateCount(1);
                break;
            case UpgradeButtonType.RateUp:
                upgradeButtonInfo.name = "rate-upgrade-button";
                upgradeButtonInfo.isOpened = true;
                upgradeButtonInfo.costPropertyName = "RateCost";
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateRate(0.5f);
                break;
            case UpgradeButtonType.Renewal:
                upgradeButtonInfo.name = "renewal-button";
                upgradeButtonInfo.isOpened = _fireWorkController.IsCanRenewal;
                upgradeButtonInfo.costPropertyName = null;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.Renewal();
                break;
            case UpgradeButtonType.Further1:
                upgradeButtonInfo.name = "further1-upgrade-button";
                upgradeButtonInfo.costPropertyName = "Further1Cost";
                upgradeButtonInfo.isOpened = _fireWorkController.IsCanFurther1;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateFurtherCount1(1);
                break;
            case UpgradeButtonType.Further2:
                upgradeButtonInfo.name = "further2-upgrade-button";
                upgradeButtonInfo.costPropertyName = "Further2Cost";
                upgradeButtonInfo.isOpened = _fireWorkController.IsCanFurther2;
                upgradeButtonInfo.clickEvent = () => _fireWorkController.UpdateFurtherCount2(1);
                break;
        }
        return upgradeButtonInfo;
    }

    private void InitEnumList()
    {
        if(_buttonTypeList == null)
        {
            Debug.Log("ERROR");
            _buttonTypeList = new List<UpgradeButtonType>(); 
        }
        _buttonTypeList.Clear();

     
        foreach (UpgradeButtonType buttonType in Enum.GetValues(typeof(UpgradeButtonType)))
        {
            _buttonTypeList.Add(buttonType);
        }
    }

    /// <summary>
    /// ����â Ȱ��ȭ
    /// </summary>
    public void ActiveDescription(MouseOverEvent e, VisualElement v)
    {
        //e.mousePosition
        _descriptionPanel.style.display = DisplayStyle.Flex;
        v.Insert(v.childCount, _descriptionPanel);
    }

    /// <summary>
    /// ����â Ȱ��ȭ
    /// </summary>
    public void DisableDescription(MouseOutEvent e)
    {
        _descriptionPanel.style.display = DisplayStyle.None;
    }

    public void ActiveUpgradeUI(bool isActive)
    {
        if(isActive == true)
        {
            _rootElement.style.display = DisplayStyle.Flex;
            _backgroundCanvas.SetActive(true); 
            return;
        }
        _rootElement.style.display = DisplayStyle.None;
        _backgroundCanvas.SetActive(false);
    }

    /// <summary>
    /// ���� �ؽ�Ʈ ������Ʈ
    /// </summary>
    public void UpdateCostText()
    {
        foreach (var buttonElement in _buttonElementList)
        {
            buttonElement.SetCostText();
        }
    }

    /// <summary>
    /// ��ư�� ��װų� ������� isLocked - true ��� / isLocked - false ��� ���� 
    /// </summary>
    /// <param name="buttonType"></param>
    /// <param name="isOpened"></param>
    public void LockOrUnlockButton(UpgradeButtonType buttonType, bool isOpened)
    {
        _buttonElementList.ForEach((x) =>
        {
            if (x._buttonType == buttonType)
            {
                x.IsOpened = isOpened;
                x.LockButton();
            }
        });
    }


}


public struct UpgradeButtonInfo
{
    public string name; // �̸�
    public bool isOpened;  // ��� ���� 
    public string costPropertyName; // ���� �Ӽ� �̸� 
    public Action clickEvent;

}


/// <summary>
/// ���׷��̵� ��ư (SO�� ������ ����..) 
/// </summary>
public class UpgradeButtonElement
{


    public UpgradeButtonType _buttonType; // �ĺ��� 
    public Button _button; // �ڱ� �ڽ�
    public VisualElement _lockElement; // ��� ������ 

    private FireWorkController _fireWorkController;
    private VisualElement _image; // �̹��� 
    private Label _nameLabel; // �̸� �ؽ�Ʈ��
    private Label _costLabel;  // ���� �ؽ�Ʈ�� 

    private bool _isOpened; // ����ִ� ���� 
    private int _cost; // ����
    private PropertyInfo _propertyInfo; // ���� ������Ƽ 

    public bool IsOpened
    {
        get => _isOpened;
        set
        {
            _isOpened = value;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="visualElement">��ư �ڽ�</param>
    /// <param name="loackElement">��� �������̹���</param>
    /// <param name="isLocked">��� ����</param>
    /// <param name="code">�ĺ���</param>
    public UpgradeButtonElement(Button button, VisualElement loackElement, bool isLocked, UpgradeButtonType buttonType, FireWorkController fireworkController = null, Label costLabel = null, string propertyName = null)
    {
        this._button = button;
        this._costLabel = costLabel;
        this._lockElement = loackElement;
        this._isOpened = isLocked;
        this._buttonType = buttonType;

        if (propertyName != null)
        {
            _fireWorkController = fireworkController;
            Type type = typeof(FireWorkController);
            _propertyInfo = type.GetProperty(propertyName);
            Debug.Log(_propertyInfo.Name);
            SetCostText();
            Debug.Log(Enum.GetName(typeof(UpgradeButtonType), buttonType) + "�� ������ : " + _cost);
        }

        LockButton();
    }

    public void SetCostText()
    {
        _cost = (int)_propertyInfo.GetValue(_fireWorkController);
        Debug.Log(_cost);
        _costLabel.text = _cost.ToString();
    }

    public void LockButton()
    {
        if (_isOpened == true) // ���������� 
        {
            _lockElement.style.display = DisplayStyle.None;
            _button.style.display = DisplayStyle.Flex;
            // ��ư Ŭ�� �ȵǵ��� 
            return;
        }
        _lockElement.style.display = DisplayStyle.Flex;
        _button.style.display = DisplayStyle.None;

    }
}


public enum UpgradeButtonType
{
    CountUp,
    RateUp,
    Further1,
    Further2,
    Renewal,

}
