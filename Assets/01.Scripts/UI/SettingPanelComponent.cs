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
    private Button _graphicBackButton; // �׷��� �ڷΰ��� ��ư 
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
        _applyButton = _graphicSettingPanel.Q<Button>("apply-button");
        _graphicBackButton = _graphicSettingPanel.Q<Button>("back-button"); 

        Debug.Log(_dropdownMenu.name);

        // ��ư �̺�Ʈ ��� 
        _settingButton.clicked += () => OpenCloseSetting(_settingPanel);
        _backButton.clicked += () => OpenCloseSetting(_settingPanel);

        // �׷��� ���� 
        _fullScreenToggle.RegisterValueChangedCallback((x) => FullScreen());
        _applyButton.clicked += () => ApplySetting();
        _graphicBackButton.clicked += () => OpenCloseSetting(_graphicSettingPanel);
        _graphicButton.clicked += () => OpenCloseSetting(_graphicSettingPanel);
    }

    public override void UpdateSometing()
    {
    }

    /// <summary>
    /// ���� �г� ���� �ݱ� 
    /// </summary>
    public void OpenCloseSetting(TemplateContainer settingPanel)
    {
        if(settingPanel.style.display.value == DisplayStyle.Flex)
        {
            settingPanel.style.display = DisplayStyle.None;
            return;
        }
        settingPanel.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// ȭ�� �ִ� 
    /// </summary>
    private void FullScreen()
    {
        Debug.Log("�����̿�");
        _grapicSetting.ChangeFoolScreen(); 
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void ApplySetting()
    {
        _grapicSetting.ApplySettingScreen(); 
    }
}
