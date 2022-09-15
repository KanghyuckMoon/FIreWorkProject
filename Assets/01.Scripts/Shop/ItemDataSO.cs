using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "ScriptableObject/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
	public float intensity = 10f;
	public List<ItemData> itemDataList = new List<ItemData>();
	
	public ItemData GetItemData(int itemCode)
	{
		return itemDataList.Find(x => x.itemCode == itemCode);
	}

	[ContextMenu("모든 색깔 아이템의 빛 세기 변경")]
	public void ChangeIntensity()
	{
		for(int i = 0; i < itemDataList.Count; ++i)
		{
			IntensityChangeGradient(itemDataList[i].gradient_1);
			IntensityChangeGradient(itemDataList[i].gradient_2);
			IntensityChangeGradient(itemDataList[i].gradient_3);
		}
	}

	private void IntensityChangeGradient(Gradient gradient)
	{
		var colorkeys = gradient.colorKeys;
		gradient.alphaKeys = gradient.alphaKeys;

		Color color = SetColor(colorkeys[0].color);
		colorkeys[0].color = color;
		gradient.SetKeys(colorkeys, gradient.alphaKeys);
	}
	private Color SetColor(Color hdrColor)
	{
		float factor = Mathf.Pow(2, intensity);
		hdrColor = new Color(hdrColor.r * factor, hdrColor.g * factor, hdrColor.b * factor);
		return hdrColor;
	}
}

[System.Serializable]
public class ItemData
{
	public int itemCode;
	public int money;
	public EItem itemType;
	[GradientUsage(true)] public Gradient gradient_1;
	[GradientUsage(true)] public Gradient gradient_2;
	[GradientUsage(true)] public Gradient gradient_3;
	public Texture2D texture2D;
	public int directionCode;
}
