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

    public void Start()
    {
        _moneyUIDoc = GetComponent<UIDocument>();
        _rootElement = _moneyUIDoc.rootVisualElement;

        _happyMoneyLabel = _rootElement.Q<Label>("happyMoney-label");
        _moneyLabel = _rootElement.Q<Label>("money-label");
    }
    public void Update()
    {
        UpdateMoneyText(); 
    }
    private void UpdateMoneyText()
    {
        _happyMoneyLabel.text = string.Format("�ູ�� : {0}", UserSaveDataManager.Instance.UserSaveData.happy.ToString());
        _moneyLabel.text = string.Format("�� : {0}", UserSaveDataManager.Instance.UserSaveData.money.ToString());
    }
}
