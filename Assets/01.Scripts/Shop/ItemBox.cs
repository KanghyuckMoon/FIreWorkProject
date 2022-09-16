using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using UnityEditor.UIElements; 
using System;

// 라이브러리 아이템 
public class ItemBox : ItemUI
{
    private ItemChangeManager ItemChangeManager
	{
        get
		{
            _itemChangeManager ??= GameObject.FindObjectOfType<ItemChangeManager>();
            return _itemChangeManager;
		}
	}


	public int ItemCode
    {
		get
        {
			return _itemCode;
        }
		set
        {
			_itemCode = value;
        }
    }

    [SerializeField] private int _itemCode;
	[SerializeField] private ItemChangeManager _itemChangeManager;
    [SerializeField] private ItemDataSO _itemDataSO;
	[SerializeField] private float _debugValue;


	public ItemBox(ItemData itemData,ItemDataSO itemDataSO)
		: base(itemData)
	{
		_itemDataSO = itemDataSO; 
		_itemCode = itemData.itemCode;

		_button.clicked += ChangeFirework;
	}

	[ContextMenu("ChangeFirework")]
    public void ChangeFirework()
	{
        //ItemChangeManager.ChangeFirework(_itemData);
        ItemChangeManager.ChangeFirework(_itemDataSO.GetItemData(_itemCode));
    }

	[ContextMenu("ChangeGradient")]
	public void ChangeGradient()
	{
		foreach(var item in _itemDataSO.itemDataList)
		{
			IntensityChangeGradient(item.gradient_1);
			IntensityChangeGradient(item.gradient_2);
			IntensityChangeGradient(item.gradient_3);
		}
	}

	private Gradient IntensityChangeGradient(Gradient gradient)
	{
		var colorkeys = gradient.colorKeys;
		Color color = SetColor(colorkeys[0].color);
		colorkeys[0].color = color;
		gradient.SetKeys(colorkeys, gradient.alphaKeys);
		return gradient;
	}

	private Color SetColor(Color hdrColor)
	{
		float factor = Mathf.Pow(2, _debugValue);
		hdrColor = new Color(hdrColor.r * factor, hdrColor.g * factor, hdrColor.b * factor);
		return hdrColor;
	}
}
