using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

[System.Serializable]
public class SettingPanelComponent : UIComponent
{
    private GrapicSetting _grapicSetting;
    private SoundSetting _soundSeting;
    private Exit _exit; 

    private Button _settingButton; // 설정 버튼
    private TemplateContainer _settingPanel; // 설정 패널 
    private TemplateContainer _graphicSettingPanel; // 그래픽 설정 패널 
    private TemplateContainer _soundSettingPanel; // 사운드 설정 패널 

    private Button _backButton; // 돌아가기 버튼
    private Button _soundButton; // 사운드 설정 버튼
    private Button _graphicButton; // 그래픽 설정 버튼
    private Button _exitButton; // 게임 종료 버튼

    private Button _applyButton; // 적용하기 버튼 
    
    // 그래픽 설정 관련 변수 
    private Button _graphicBackButton; // 그래픽 뒤로가기 버튼 
    private DropdownField _dropdownMenu;
    private Toggle _fullScreenToggle;

    // 사운드 설정 관련 변수 
    private Slider _bgmSlider;
    private Slider _effSlider;
    private Button _settingBackButton;
    
    private void CashingElement(VisualElement parent, VisualElement findElement,string findName)
    {
        Type type = findElement.GetType();

 //       findElement = parent.Q <type.> (findName);
    }

    public void Init(UIButtonManager uiButtonManager, GrapicSetting grapicSetting, SoundSetting soundSetting,Exit exit)
    {
        _uiButtonManager = uiButtonManager; 
        _grapicSetting = grapicSetting;
        _soundSeting = soundSetting;
        _exit = exit; 

        _soundSeting.InitSlider(_bgmSlider, _effSlider); 

        _settingButton = uiButtonManager.RootElement.Q<Button>("setting-button");

        _settingPanel = uiButtonManager.RootElement.Q<TemplateContainer>("SettingTemplate");
        _graphicSettingPanel = _settingPanel.Q<TemplateContainer>("GraphicSettingTemplate");
        _soundSettingPanel = _settingPanel.Q<TemplateContainer>("SoundSettingTemplate");

        _backButton = _settingPanel.Q<Button>("back-button");
        _soundButton = _settingPanel.Q<Button>("sound-button");
        _graphicButton = _settingPanel.Q<Button>("graphic-button");
        _exitButton = _settingPanel.Q<Button>("exit-button");
        
        // 그래픽 세팅
        _dropdownMenu = _graphicSettingPanel.Q<DropdownField>("graphic-setting");
        _fullScreenToggle = _graphicSettingPanel.Q<Toggle>("fullscreen-toggle");
        _applyButton = _graphicSettingPanel.Q<Button>("apply-button");
        _graphicBackButton = _graphicSettingPanel.Q<Button>("back-button");

        // 사운드 세팅 
        _bgmSlider = _soundSettingPanel.Q<Slider>("bgm-slider");
        _effSlider = _soundSettingPanel.Q<Slider>("eff-slider");
        _settingBackButton = _soundSettingPanel.Q<Button>("back-button");

        // 버튼 이벤트 등록 
        _settingButton.clicked += () => OpenClosePanel(_settingPanel);
        _backButton.clicked += () => OpenClosePanel(_settingPanel);
        _exitButton.clicked += () => _exit.QuitGame(); 

        // 그래픽 세팅 
        _fullScreenToggle.RegisterValueChangedCallback((x) => FullScreen());
        _applyButton.clicked += () => ApplySetting();
        _graphicBackButton.clicked += () => OpenClosePanel(_graphicSettingPanel);
        _graphicButton.clicked += () => OpenClosePanel(_graphicSettingPanel);

        // 사운드 세팅 
        _bgmSlider.RegisterValueChangedCallback((x) => _soundSeting.SetBgmAudio(x.newValue) );
        _effSlider.RegisterValueChangedCallback((x) => _soundSeting.SetEffAudio(x.newValue));
        _settingBackButton.clicked += () => OpenClosePanel(_soundSettingPanel);
        _soundButton.clicked += () => OpenClosePanel(_soundSettingPanel);
    }

    public override void UpdateSometing()
    {
    }



    /// <summary>
    /// 화면 최대 
    /// </summary>
    private void FullScreen()
    {
        Debug.Log("변경이요");
        _grapicSetting.ChangeFoolScreen(); 
    }

    /// <summary>
    /// 설정 적용
    /// </summary>
    private void ApplySetting()
    {
        _grapicSetting.ApplySettingScreen(); 
    }

}
