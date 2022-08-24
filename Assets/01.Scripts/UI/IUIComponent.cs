using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class UIComponent
{
    protected UIButtonManager _uiButtonManager;

    public virtual void Init(UIButtonManager uiButtonManager)
    {
        _uiButtonManager = uiButtonManager; 
    }
    public abstract void UpdateSometing(); 
}
