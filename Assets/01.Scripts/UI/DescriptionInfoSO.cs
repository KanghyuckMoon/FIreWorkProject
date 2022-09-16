using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/UI/DescriptionInfoSO")]
public class DescriptionInfoSO : ScriptableObject
{
    [Header("ID 코드")]
    [Header(" 0 ~ 100 - 색 아이템")]
    [Header("101 ~ 220 - 모양 아이템")]
    [Header("300 ~ 400 - 업적")]
    [Header("500 ~ 600 - 업그레이드바")]
    public List<DescriptionInfo> descriptionList = new List<DescriptionInfo>();
    
    /// <summary>
    /// 설명 정보 반환 
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public DescriptionData GetDescriptionData(int code)
    {
        // 설명할 아이템 타입 확인 
        DescriptionType descriptionType = DescriptionType.None;
        if (code < (int)DescriptionType.ColorItem)
        {
            descriptionType = DescriptionType.ColorItem;
        }
        else if (code < (int)DescriptionType.ShapeItem)
        {
            descriptionType = DescriptionType.ShapeItem;
        }
        else if (code < (int)DescriptionType.Achievement)
        {
            descriptionType = DescriptionType.Achievement;
        }
        else if (code < (int)DescriptionType.Upgrade)
        {
            descriptionType = DescriptionType.Upgrade;
        }

        var v = descriptionList.Find((x) => x.descriptionType == descriptionType);
        return v.descriptionData.Find((x) => x.code == code);
        //return descriptionList.Find(x => x.code == code);
    }

}

[System.Serializable]
public class DescriptionInfo
{
    [Header("설명 아이템 종류")]
    public DescriptionType descriptionType;
    [Header("설명 데이터")]
    public List<DescriptionData> descriptionData = new List<DescriptionData>(); 
}

[System.Serializable]
public class DescriptionData
{

    // 0 ~ 100 - 색 아이템
    // 101 ~ 220 - 모양 아이템 
    // 300 ~ 400 - 업적 
    // 500 ~ 600 - 업그레이드바 
    
    [Header("ID코드")]
    public int code; 
    [Header("제목")]
    public string title; // 설명 제목 
    [TextArea(2,4),Header("내용")]
    public string content; // 내용 
    // GIF? 

}

public enum DescriptionType : int
{
    None = -1, 
    ColorItem = 0,
    ShapeItem = 101,
    Achievement = 300,
    Upgrade = 500

}
