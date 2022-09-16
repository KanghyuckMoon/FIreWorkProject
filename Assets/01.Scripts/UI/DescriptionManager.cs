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

    public DescriptionInfoSO DescriptionInfoSO => _descriptionInfoSO; 
    private void Awake()
    {
        
    }

    /// <summary>
    /// UI빌더 버튼 설명창
    /// </summary>
    /// <param name="button"></param>
    /// <param name="code"></param>
    public void SetDescriptionClickEvent(UnityEngine.UIElements.Button button,int code)
    {
        button.RegisterCallback<MouseOverEvent>((x) => ActiveDescription(true, code));
        button.RegisterCallback<MouseOutEvent>((x) => ActiveDescription(false, code));
    }

    /// <summary>
    /// 설명창 활성화
    /// </summary>
    /// <param name="isActive"></param>
    public void ActiveDescription(bool isActive, int code)
    {
        Debug.Log("설명 활성화");
        Debug.Log("코드 : " + code);
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
}

[System.Serializable]
public class Description
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
}

public enum DescriptionType_
{
    // 색 아이템 
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
    // 모양 아이템 

    // 업적 

    // 업그레이드바 
}
