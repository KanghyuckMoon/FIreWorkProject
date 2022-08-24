using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChangeManager : MonoBehaviour
{
	private enum CurrentSettingMode
	{
		Further1,
		Further2,
		Further3,
		Further4,
	}

	public FireWorkController FireWorkController
	{ 
		get
		{
			_fireWorkController ??= FindObjectOfType<FireWorkController>();
			return _fireWorkController;
		}
	}

	[SerializeField] private FireWorkController _fireWorkController;
	[SerializeField] private CurrentSettingMode _currentSettingMode;
	[SerializeField] private float _intensity = 5f;

	/// <summary>
	/// 받은 아이템데이터로 불꽃놀이 커스터마이즈, 불꽃놀이 변경
	/// </summary>
	/// <param name="itemData"></param>
	public void ChangeFirework(ItemData itemData)
	{
		switch (itemData.itemType)
		{
			case EItem.None:
				break;
			case EItem.Color:
				switch (_currentSettingMode)
				{
					case CurrentSettingMode.Further1:
						FireWorkController.ChangeFurtherColor1(IntensityChangeGradient(itemData.gradient_1), IntensityChangeGradient(itemData.gradient_2), IntensityChangeGradient(itemData.gradient_3));
						break;
					case CurrentSettingMode.Further2:
						FireWorkController.ChangeFurtherColor2(IntensityChangeGradient(itemData.gradient_1), IntensityChangeGradient(itemData.gradient_2), IntensityChangeGradient(itemData.gradient_3));
						break;
					case CurrentSettingMode.Further3:
						FireWorkController.ChangeFurtherColor3(IntensityChangeGradient(itemData.gradient_1), IntensityChangeGradient(itemData.gradient_2), IntensityChangeGradient(itemData.gradient_3));
						break;
					case CurrentSettingMode.Further4:
						FireWorkController.ChangeFurtherColor4(IntensityChangeGradient(itemData.gradient_1), IntensityChangeGradient(itemData.gradient_2), IntensityChangeGradient(itemData.gradient_3));
						break;
				}
				break;
			case EItem.Texture:
				switch (_currentSettingMode)
				{
					case CurrentSettingMode.Further1:
						FireWorkController.ChangeFurtherTexture1(itemData.texture2D);
						break;
					case CurrentSettingMode.Further2:
						FireWorkController.ChangeFurtherTexture2(itemData.texture2D);
						break;
					case CurrentSettingMode.Further3:
						FireWorkController.ChangeFurtherTexture3(itemData.texture2D);
						break;
					case CurrentSettingMode.Further4:
						FireWorkController.ChangeFurtherTexture4(itemData.texture2D);
						break;
				}
				break;
		}
	}

	private Gradient IntensityChangeGradient(Gradient gradient)
	{
		Gradient gradientNew = new Gradient();
		var colorkeys = gradient.colorKeys;
		gradientNew.alphaKeys = gradient.alphaKeys;
		for(int i = 1; i < colorkeys.Length; ++i)
		{
			int j = i;
			Color color = SetColor(colorkeys[j].color);
			colorkeys[j].color = color;
		}
		gradientNew.SetKeys(colorkeys, gradientNew.alphaKeys);
		return gradientNew;
	}

	private Color SetColor(Color hdrColor)
	{
		float factor = Mathf.Pow(2, _intensity);
		hdrColor = new Color(hdrColor.r * factor, hdrColor.g * factor, hdrColor.b * factor);
		return hdrColor;
	}
}
