using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

[System.Serializable]
public abstract class UIComponent
{
    protected UIButtonManager _uiButtonManager;

    public abstract void UpdateSometing();

    /// <summary>
    ///  �г� ���� �ݱ� 
    /// </summary>
    public void OpenClosePanel(TemplateContainer settingPanel)
    {
        if (settingPanel.style.display.value == DisplayStyle.Flex)
        {
            settingPanel.style.display = DisplayStyle.None;
            return;
        }
        settingPanel.style.display = DisplayStyle.Flex;
    }
}
