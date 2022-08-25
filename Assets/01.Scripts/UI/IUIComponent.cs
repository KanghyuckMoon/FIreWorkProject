using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class UIComponent
{
    protected UIButtonManager _uiButtonManager;

    public abstract void UpdateSometing(); 
}
