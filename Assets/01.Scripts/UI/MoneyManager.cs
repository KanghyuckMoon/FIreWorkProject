using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    //private VFXUnityEventHandler _vFXUnityEventHandler; 
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

        //_vFXUnityEventHandler = FindObjectOfType<VFXUnityEventHandler>();
        //_vFXUnityEventHandler.UnityEvent.AddListener(() => UpdateText());
    }

    public void Update()
    {
        UpdateMoneyText();
    }

    void UpdateMoneyText()
    {
        _happyMoneyLabel.text = string.Format("행복도 : {0}", UserSaveDataManager.Instance.UserSaveData.happy);
        _moneyLabel.text = string.Format("돈 : {0}", UserSaveDataManager.Instance.UserSaveData.money);
            
        //int a = happyMoney;
        //int b = money;

        //if (happyMoney != UserSaveDataManager.Instance.UserSaveData.happy)
        //{
        //    for (int i = a; i < UserSaveDataManager.Instance.UserSaveData.happy; i++)
        //    {
        //        _happyMoneyLabel.text = string.Format("행복도 : {0}", happyMoney + 1);
        //        yield return new WaitForSecondsRealtime(0.1f);
        //    }
        //}

        //else if(money != UserSaveDataManager.Instance.UserSaveData.money)
        //{
        //    for (int i = b; i < UserSaveDataManager.Instance.UserSaveData.money; i++)
        //    {
        //        _moneyLabel.text = string.Format("행복도 : {0}", money + 1);

        //        yield return new WaitForSecondsRealtime(0.1f);
        //    }
        //}

        //happyMoney = UserSaveDataManager.Instance.UserSaveData.happy;
        //money = UserSaveDataManager.Instance.UserSaveData.money;
    }
}
