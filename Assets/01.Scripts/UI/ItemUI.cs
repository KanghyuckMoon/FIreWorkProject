using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

public class ItemUI : VisualElement
{
    private Image _icon;
    private ItemData _itemData;
    public ItemUI(ItemData itemData)
    {
        _itemData = itemData; 

        _icon = new Image();
        Add(_icon);

        this.AddToClassList("");
        _icon.AddToClassList(""); 
    }
}
