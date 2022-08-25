using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

[System.Serializable]
public class SettingPanelComponent : UIComponent
{
    private GrapicSetting _grapicSetting;

    private Button _settingButton; // 설정 버튼
    private TemplateContainer _settingPanel; // 설정 패널 
    private TemplateContainer _graphicSettingPanel; // 그래픽 설정 패널 

    private Button _backButton; // 돌아가기 버튼
    private Button _soundButton; // 사운드 설정 버튼
    private Button _graphicButton; // 그래픽 설정 버튼
    private Button _endButton; // 게임 종료 버튼

    private Button _applyButton; // 적용하기 버튼 
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
    /// 설정 패널 열고 닫기 
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
    /// 화면 최대 
    /// </summary>
    private void FullScreen()
    {
        Debug.Log("변경이요");
        _grapicSetting.ChangeFoolScreen(); 
    }
}
