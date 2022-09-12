using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChangeManager : MonoBehaviour
{
	public enum CurrentSettingMode
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
	/// πﬁ¿∫ æ∆¿Ã≈€µ•¿Ã≈Õ∑Œ ∫“≤…≥Ó¿Ã ƒøΩ∫≈Õ∏∂¿Ã¡Ó, ∫“≤…≥Ó¿Ã ∫Ø∞Ê
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
						UserSaveDataManager.Instance.UserSaveData.further1ColorLight = _intensity;
						UserSaveDataManager.Instance.UserSaveData.further1ColorItemCode = itemData.itemCode;
						FireWorkController.ChangeFurtherColor1(IntensityChangeGradient(itemData.gradient_1), IntensityChangeGradient(itemData.gradient_2), IntensityChangeGradient(itemData.gradient_3));
						break;
					case CurrentSettingMode.Further2:
						UserSaveDataManager.Instance.UserSaveData.further2ColorLight = _intensity;
						UserSaveDataManager.Instance.UserSaveData.further2ColorItemCode = itemData.itemCode;
						FireWorkController.ChangeFurtherColor2(IntensityChangeGradient(itemData.gradient_1), IntensityChangeGradient(itemData.gradient_2), IntensityChangeGradient(itemData.gradient_3));
						break;
					case CurrentSettingMode.Further3:
						UserSaveDataManager.Instance.UserSaveData.further3ColorLight = _intensity;
						UserSaveDataManager.Instance.UserSaveData.further3ColorItemCode = itemData.itemCode;
						FireWorkController.ChangeFurtherColor3(IntensityChangeGradient(itemData.gradient_1), IntensityChangeGradient(itemData.gradient_2), IntensityChangeGradient(itemData.gradient_3));
						break;
					case CurrentSettingMode.Further4:
						UserSaveDataManager.Instance.UserSaveData.further4ColorLight = _intensity;
						UserSaveDataManager.Instance.UserSaveData.further4ColorItemCode = itemData.itemCode;
						FireWorkController.ChangeFurtherColor4(IntensityChangeGradient(itemData.gradient_1), IntensityChangeGradient(itemData.gradient_2), IntensityChangeGradient(itemData.gradient_3));
						break;
				}
				break;
			case EItem.Texture:
				switch (_currentSettingMode)
				{
					case CurrentSettingMode.Further1:
						UserSaveDataManager.Instance.UserSaveData.further1TextureItemCode = itemData.itemCode;
						FireWorkController.ChangeFurtherTexture1(itemData.texture2D);
						break;
					case CurrentSettingMode.Further2:
						UserSaveDataManager.Instance.UserSaveData.further2TextureItemCode = itemData.itemCode;
						FireWorkController.ChangeFurtherTexture2(itemData.texture2D);
						break;
					case CurrentSettingMode.Further3:
						UserSaveDataManager.Instance.UserSaveData.further3TextureItemCode = itemData.itemCode;
						FireWorkController.ChangeFurtherTexture3(itemData.texture2D);
						break;
					case CurrentSettingMode.Further4:
						UserSaveDataManager.Instance.UserSaveData.further4TextureItemCode = itemData.itemCode;
						FireWorkController.ChangeFurtherTexture4(itemData.texture2D);
						break;
				}
				break;
		}
	}

	/// <summary>
	/// ∫“≤…≥Ó¿Ã¿« ∫˚ ºº±‚ ∫Ø∞Ê
	/// </summary>
	/// <param name="value"></param>
	public void ChangeItensity(float value)
	{
		_intensity = value * 0.1f;
	}

	/// <summary>
	/// ∫“≤…≥Ó¿Ã º¯º≠ ∫Ø∞Ê
	/// </summary>
	/// <param name="value"></param>
	public void ChangeFurther(CurrentSettingMode currentSettingMode)
	{
		_currentSettingMode = currentSettingMode;
	}

	/// <summary>
	/// ∫“≤…≥Ó¿Ã¿« ≈©±‚ ∫Ø∞Ê
	/// </summary>
	/// <param name="value"></param>
	public void ChangeSize(float value)
	{
		switch (_currentSettingMode)
		{
			case CurrentSettingMode.Further1:
				UserSaveDataManager.Instance.UserSaveData.further1Size = value;
				FireWorkController.ChangeSizeFurther1(value);
				break;
			case CurrentSettingMode.Further2:
				UserSaveDataManager.Instance.UserSaveData.further2Size = value;
				FireWorkController.ChangeSizeFurther2(value);
				break;
			case CurrentSettingMode.Further3:
				UserSaveDataManager.Instance.UserSaveData.further3Size = value;
				FireWorkController.ChangeSizeFurther3(value);
				break;
			case CurrentSettingMode.Further4:
				UserSaveDataManager.Instance.UserSaveData.further4Size = value;
				FireWorkController.ChangeSizeFurther4(value);
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
