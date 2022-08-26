using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

public class ItemUI : VisualElement
{
    private Image _icon; 
    
    public ItemUI()
    {

        _icon = new Image();
        Add(_icon);

        this.AddToClassList("");
        _icon.AddToClassList(""); 
    }
}
