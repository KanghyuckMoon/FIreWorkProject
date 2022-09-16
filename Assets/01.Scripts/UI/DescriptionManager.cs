using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DescriptionManager : MonoBehaviour
{
    [SerializeField]
    private Description _description; 
    [SerializeField]
    private RectTransform _descriptionPanelRect;
    [SerializeField]
    private DescriptionInfoSO _descriptionInfoSO;
    [SerializeField]
    private AchievementDataSO _achievementDataSO;
    [SerializeField]
    private ItemDataSO _itemDataSO; 

    public DescriptionInfoSO DescriptionInfoSO => _descriptionInfoSO; 
    /// <summary>
    /// UI���� ��ư ����â
    /// </summary>
    /// <param name="button"></param>
    /// <param name="code"></param>
    public void SetDescriptionClickEvent(UnityEngine.UIElements.Button button,int code)
    {
        button.RegisterCallback<MouseOverEvent>((x) => ActiveDescription(true, code));
        button.RegisterCallback<MouseOutEvent>((x) => ActiveDescription(false, code));
    }

    /// <summary>
    /// ����â Ȱ��ȭ
    /// </summary>
    /// <param name="isActive"></param>
    public void ActiveDescription(bool isActive, int code)
    {
        Debug.Log("���� Ȱ��ȭ");
        Debug.Log("�ڵ� : " + code);
        if(isActive == true)    
        {
            DescriptionData descriptionInfo = _descriptionInfoSO.GetDescriptionData(code);
            _description.titleText.text = descriptionInfo.title;
            _description.contentText.text = descriptionInfo.content;

            _descriptionPanelRect.gameObject.SetActive(true);
            _descriptionPanelRect.anchoredPosition = Input.mousePosition;
            return; 
        }
        _descriptionPanelRect.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���� ����â Ȱ��ȭ
    /// </summary>
    public void ActiveAchievementDescriptoin(bool isActive, int code)
    {
        if (isActive == true)
        {
            Debug.Log("code : " + code); 
            AchievementData achievementData = _achievementDataSO._achievementDatas.Find((x) => x._achievementCode == code);
            _description.titleText.text = achievementData._achievementName;
            _description.contentText.text = achievementData._content;

            _descriptionPanelRect.gameObject.SetActive(true);
            _descriptionPanelRect.anchoredPosition = Input.mousePosition;
            return;
        }
        _descriptionPanelRect.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Description
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    public UnityEngine.UI.Image image; 
}

public enum DescriptionType_
{
    // �� ������ 
    Color_1,
    Color_2,
    Color_3,
    Color_4,
    Color_5,
    Color_6,
    Color_7,
    Color_8,
    Color_9,
    Color_10,
    Color_11,
    Color_12,
    Color_13,
    Color_14,
    Color_15,
    Color_16,
    Color_17,
    Color_18,
    Color_19,
    // ��� ������ 

    // ���� 

    // ���׷��̵�� 
}
