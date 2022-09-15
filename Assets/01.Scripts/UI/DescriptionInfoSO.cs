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

    // 0 ~ 100 - �� ������
    // 101 ~ 220 - ��� ������ 
    // 221 ~ 300 - ���� 
    // 301 ~ 350 - ���׷��̵�� 
    
    [Header("ID�ڵ�")]
    public int code; 
    [Header("����")]
    public string title; // ���� ���� 
    [Multiline,Header("����")]
    public string content; // ���� 
    // GIF? 

}

public enum DescriptionType
{

}
