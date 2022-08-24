using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    private ItemChangeManager ItemChangeManager
	{
        get
		{
            _itemChangeManager ??= FindObjectOfType<ItemChangeManager>();
            return _itemChangeManager;
		}
	}

    [SerializeField] private int _itemCode;
	[SerializeField] private ItemChangeManager _itemChangeManager;
    [SerializeField] private ItemDataSO _itemDataSO;
	[SerializeField] private float _debugValue;

	//private ItemData _itemData;

	void Start()
    {
        //_itemData = _itemDataSO.GetItemData(_itemCode);
    }

    //[ContextMenu("DebugChangeItemData")]
    public void DebugChangeItemData()
    {
        //_itemData = _itemDataSO.GetItemData(_itemCode);
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
