using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/UI/DescriptionInfoSO")]
public class DescriptionInfoSO : ScriptableObject
{
    [Header("ID �ڵ�")]
    [Header(" 0 ~ 100 - �� ������")]
    [Header("101 ~ 220 - ��� ������")]
    [Header("300 ~ 400 - ����")]
    [Header("500 ~ 600 - ���׷��̵��")]
    public List<DescriptionInfo> descriptionList = new List<DescriptionInfo>();
    
    /// <summary>
    /// ���� ���� ��ȯ 
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public DescriptionData GetDescriptionData(int code)
    {
        // ������ ������ Ÿ�� Ȯ�� 
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
    [Header("���� ������ ����")]
    public DescriptionType descriptionType;
    [Header("���� ������")]
    public List<DescriptionData> descriptionData = new List<DescriptionData>(); 
}

[System.Serializable]
public class DescriptionData
{

    // 0 ~ 100 - �� ������
    // 101 ~ 220 - ��� ������ 
    // 300 ~ 400 - ���� 
    // 500 ~ 600 - ���׷��̵�� 
    
    [Header("ID�ڵ�")]
    public int code; 
    [Header("����")]
    public string title; // ���� ���� 
    [TextArea(2,4),Header("����")]
    public string content; // ���� 
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
