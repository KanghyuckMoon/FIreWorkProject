using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

[System.Serializable]
public class SettingPanelComponent : UIComponent
{
    private GrapicSetting _grapicSetting;

    private Button _settingButton; // ���� ��ư
    private TemplateContainer _settingPanel; // ���� �г� 
    private TemplateContainer _graphicSettingPanel; // �׷��� ���� �г� 

    private Button _backButton; // ���ư��� ��ư
    private Button _soundButton; // ���� ���� ��ư
    private Button _graphicButton; // �׷��� ���� ��ư
    private Button _endButton; // ���� ���� ��ư

    private Button _applyButton; // �����ϱ� ��ư 
    private DropdownField _dropdownMenu;
    private Toggle _fullScreenToggle; 
    public override void Init(UIButtonManager uiButtonManager)
    {
        base.Init(uiButtonManager);

        _grapicSetting = new GrapicSetting(); 

        _settingButton = uiButtonManager.RootElement.Q<Button>("setting-button");
        _settingPanel = uiButtonManager.RootElement.Q<TemplateContainer>("SettingTemplate");
        _graphicSettingPanel = _settingPanel.Q<TemplateContainer>("GraphicSettingTemplate");

        _backButton = _settingPanel.Q<Button>("back-button");
        _soundButton = _settingPanel.Q<Button>("sound-button");
        _graphicButton = _settingPanel.Q<Button>("graphic-button");
        _endButton = _settingPanel.Q<Button>("end-button");
        
        _dropdownMenu = _graphicSettingPanel.Q<DropdownField>("graphic-setting");
        _fullScreenToggle = _graphicSettingPanel.Q<Toggle>("fullscreen-toggle");

        Debug.Log(_dropdownMenu.name);


        _settingButton.clicked += () => OpenCloseSetting();
        _backButton.clicked += () => OpenCloseSetting();
        _fullScreenToggle.RegisterValueChangedCallback((x) => FullScreen()); 
    }

    public override void UpdateSometing()
    {
    }

    /// <summary>
    /// ���� �г� ���� �ݱ� 
    /// </summary>
    public void OpenCloseSetting()
    {
        if(_settingPanel.style.display.value == DisplayStyle.Flex)
        {
            _settingPanel.style.display = DisplayStyle.None;
            return;
        }
        _settingPanel.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// ȭ�� �ִ� 
    /// </summary>
    private void FullScreen()
    {
        Debug.Log("�����̿�");
        _grapicSetting.ChangeFoolScreen(); 
    }
}
