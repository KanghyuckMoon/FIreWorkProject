using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoneyManager : MonoBehaviour
{
    // ��(�ؽ�Ʈ)
    private Label _happyMoneyLabel; // �ູ ��ȭ ǥ��
    private Label _moneyLabel; // �� ��ȭ ǥ�� 

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
                _happyMoneyLabel.text = string.Format("�ູ�� : {0}", happyMoney + 1);

                yield return null;
            }
        }

        else if(money != UserSaveDataManager.Instance.UserSaveData.money)
        {
            for (int i = 0; i < UserSaveDataManager.Instance.UserSaveData.happy; i++)
            {
                _happyMoneyLabel.text = string.Format("�ູ�� : {0}", happyMoney + 1);

                yield return null;
            }
        }

        happyMoney = UserSaveDataManager.Instance.UserSaveData.happy;
        money = UserSaveDataManager.Instance.UserSaveData.money;
    }
}
