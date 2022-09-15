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

    int happyMoney;
    int money;

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
    public void UpdateText()
    {
        StartCoroutine(UpdateMoneyText());
    }

    IEnumerator UpdateMoneyText()
    {
        if (happyMoney != UserSaveDataManager.Instance.UserSaveData.happy)
        {
            for (int i = 0; i < UserSaveDataManager.Instance.UserSaveData.happy; i++)
            {
                _happyMoneyLabel.text = string.Format("행복도 : {0}", happyMoney + 1);

                yield return null;
            }
        }

        else if(money != UserSaveDataManager.Instance.UserSaveData.money)
        {
            for (int i = 0; i < UserSaveDataManager.Instance.UserSaveData.happy; i++)
            {
                _happyMoneyLabel.text = string.Format("행복도 : {0}", happyMoney + 1);

                yield return null;
            }
        }

        happyMoney = UserSaveDataManager.Instance.UserSaveData.happy;
        money = UserSaveDataManager.Instance.UserSaveData.money;
    }
}
