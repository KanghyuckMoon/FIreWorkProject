using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/UI/DescriptionInfoSO")]
public class DescriptionInfoSO : ScriptableObject
{
    public List<DescriptionInfo> descriptionList = new List<DescriptionInfo>();
    public DescriptionInfo GetDescriptionData(int code)
    {
        return descriptionList.Find(x => x.code == code);
    }

}

[System.Serializable]
public class DescriptionInfo
{

    // 0 ~ 100 - 색 아이템
    // 101 ~ 220 - 모양 아이템 
    // 221 ~ 300 - 업적 
    // 301 ~ 350 - 업그레이드바 
    
    [Header("ID코드")]
    public int code; 
    [Header("제목")]
    public string title; // 설명 제목 
    [Multiline,Header("내용")]
    public string content; // 내용 
    // GIF? 

}

public enum DescriptionType
{

}
