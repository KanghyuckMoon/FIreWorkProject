using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChangeManager : MonoBehaviour
{
	public enum CurrentSettingMode
	{
		Further0,
		Further1,
		Further2,
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
	[SerializeField] private ItemDataSO _itemDataSO;
	[SerializeField] private int _itemCode;

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
						UserSaveDataManager.Instance.UserSaveData.further1ColorLight = ChangeItensityIndex(_intensity);
						UserSaveDataManager.Instance.UserSaveData.further1ColorItemCode = itemData.itemCode;
						FireWorkController.ChangeFurtherColor1(IntensityChangeGradient(itemData.gradient_1), IntensityChangeGradient(itemData.gradient_2), IntensityChangeGradient(itemData.gradient_3));
						break;
					case CurrentSettingMode.Further2:
						UserSaveDataManager.Instance.UserSaveData.further2ColorLight = ChangeItensityIndex(_intensity);
						UserSaveDataManager.Instance.UserSaveData.further2ColorItemCode = itemData.itemCode;
						FireWorkController.ChangeFurtherColor2(IntensityChangeGradient(itemData.gradient_1), IntensityChangeGradient(itemData.gradient_2), IntensityChangeGradient(itemData.gradient_3));
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
				}
				break;
			case EItem.Direction:
				switch (_currentSettingMode)
				{
					case CurrentSettingMode.Further0:
						UserSaveDataManager.Instance.UserSaveData.furtherDirectionCode0 = itemData.directionCode;
						FireWorkController.ChangeFurtherDirection0(itemData.directionCode);
						break;
					case CurrentSettingMode.Further1:
						UserSaveDataManager.Instance.UserSaveData.furtherDirectionCode1 = itemData.directionCode;
						FireWorkController.ChangeFurtherDirection1(itemData.directionCode);
						break;
					case CurrentSettingMode.Further2:
						UserSaveDataManager.Instance.UserSaveData.furtherDirectionCode2 = itemData.directionCode;
						FireWorkController.ChangeFurtherDirection2(itemData.directionCode);
						break;
				}
				break;
		}
	}

	/// <summary>
	/// ∫“≤…≥Ó¿Ã¿« ∫˚ ºº±‚ ∫Ø∞Ê
	/// </summary>
	/// <param name="value"></param>
	public void ChangeItensity(int index)
	{
		switch(index)
		{
			case 0:
				_intensity = 3f;
				break;
			case 1:
				_intensity = 5f;
				break;
			case 2:
				_intensity = 8f;
				break;
		}
	}

	private int ChangeItensityIndex(float value)
	{
		if(value < 5f)
		{
			return 0;
		}
		else if (value < 8f)
		{
			return 1;
		}
		else
		{
			return 2;
		}
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
	public void ChangeSize(int index)
	{
		switch (_currentSettingMode)
		{
			case CurrentSettingMode.Further1:
				UserSaveDataManager.Instance.UserSaveData.further1Size = index;
				switch (index)
				{
					case 0:
						FireWorkController.ChangeSizeFurther1(30);
						break;
					case 1:
						FireWorkController.ChangeSizeFurther1(60);
						break;
					case 2:
						FireWorkController.ChangeSizeFurther1(90);
						break;
				}
				break;
			case CurrentSettingMode.Further2:
				UserSaveDataManager.Instance.UserSaveData.further2Size = index;
				switch (index)
				{
					case 0:
						FireWorkController.ChangeSizeFurther2(30);
						break;
					case 1:
						FireWorkController.ChangeSizeFurther2(60);
						break;
					case 2:
						FireWorkController.ChangeSizeFurther2(90);
						break;
				}
				break;
		}
	}


	[ContextMenu("ChangeFirework")]
	public void ChangeFirework()
	{
		ChangeFirework(_itemDataSO.GetItemData(_itemCode));
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
