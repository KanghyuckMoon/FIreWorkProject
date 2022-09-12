using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoneyManager : MonoBehaviour
{
    // 라벨(텍스트)
    private Label _happyMoneyLabel; // 행복 재화 표시
    private Label _moneyLabel; // 돈 재화 표시 

    private VisualElement _rootElement;
    private UIDocument _moneyUIDoc;

    public UIDocument MoneyUIDoc
    {
        get
        {
            if(_moneyUIDoc == null)
            {
                _moneyUIDoc = GetComponent<UIDocument>(); 
            }
            return _moneyUIDoc;
        }
    }

    public VisualElement RootElement
    {
        get
        {
            _rootElement ??= MoneyUIDoc.rootVisualElement; 
            return _rootElement; ; 
        }  
    }
    public void Awake()
    {
        _rootElement = MoneyUIDoc.rootVisualElement;

        _happyMoneyLabel = _rootElement.Q<Label>("happyMoney-label");
        _moneyLabel = _rootElement.Q<Label>("money-label");
    }
    public void Update()
    {
        UpdateMoneyText(); 
    }
    private void UpdateMoneyText()
    {
        _happyMoneyLabel.text = string.Format("행복도 : {0}", UserSaveDataManager.Instance.UserSaveData.happy.ToString());
        _moneyLabel.text = string.Format("돈 : {0}", UserSaveDataManager.Instance.UserSaveData.money.ToString());
    }
}
