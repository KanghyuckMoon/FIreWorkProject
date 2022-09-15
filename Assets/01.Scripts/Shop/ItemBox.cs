using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using UnityEditor.UIElements; 
using System;

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
	private DescriptionManager DescriptionManager
    {
		get
        {
			_descriptionManager ??= GameObject.FindObjectOfType<DescriptionManager>();
			return _descriptionManager;
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

	private DescriptionManager _descriptionManager;

	public ItemBox(ItemData itemData,ItemDataSO itemDataSO)
		: base(itemData)
	{
		_itemDataSO = itemDataSO; 
		_itemCode = itemData.itemCode;

		_button.clicked += ChangeFirework;
		//_button.RegisterCallback<MouseOverEvent>((x) => DescriptionManager()); 

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
