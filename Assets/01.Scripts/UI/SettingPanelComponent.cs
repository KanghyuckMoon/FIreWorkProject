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

    private Button _settingButton; // ���� ��ư
    private TemplateContainer _settingPanel; // ���� �г� 
    private TemplateContainer _graphicSettingPanel; // �׷��� ���� �г� 
    private TemplateContainer _soundSettingPanel; // ���� ���� �г� 

    private Button _backButton; // ���ư��� ��ư
    private Button _soundButton; // ���� ���� ��ư
    private Button _graphicButton; // �׷��� ���� ��ư
    private Button _exitButton; // ���� ���� ��ư

    private Button _applyButton; // �����ϱ� ��ư 
    
    // �׷��� ���� ���� ���� 
    private Button _graphicBackButton; // �׷��� �ڷΰ��� ��ư 
    private DropdownField _dropdownMenu;
    private Toggle _fullScreenToggle;

    // ���� ���� ���� ���� 
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
        
        // �׷��� ����
        _dropdownMenu = _graphicSettingPanel.Q<DropdownField>("graphic-setting");
        _fullScreenToggle = _graphicSettingPanel.Q<Toggle>("fullscreen-toggle");
        _applyButton = _graphicSettingPanel.Q<Button>("apply-button");
        _graphicBackButton = _graphicSettingPanel.Q<Button>("back-button");

        // ���� ���� 
        _bgmSlider = _soundSettingPanel.Q<Slider>("bgm-slider");
        _effSlider = _soundSettingPanel.Q<Slider>("eff-slider");
        _settingBackButton = _soundSettingPanel.Q<Button>("back-button");

        // ��ư �̺�Ʈ ��� 
        _settingButton.clicked += () => OpenClosePanel(_settingPanel);
        _backButton.clicked += () => OpenClosePanel(_settingPanel);
        _exitButton.clicked += () => _exit.QuitGame(); 

        // �׷��� ���� 
        _fullScreenToggle.RegisterValueChangedCallback((x) => FullScreen());
        _applyButton.clicked += () => ApplySetting();
        _graphicBackButton.clicked += () => OpenClosePanel(_graphicSettingPanel);
        _graphicButton.clicked += () => OpenClosePanel(_graphicSettingPanel);

        // ���� ���� 
        _bgmSlider.RegisterValueChangedCallback((x) => _soundSeting.SetBgmAudio(x.newValue) );
        _effSlider.RegisterValueChangedCallback((x) => _soundSeting.SetEffAudio(x.newValue));
        _settingBackButton.clicked += () => OpenClosePanel(_soundSettingPanel);
        _soundButton.clicked += () => OpenClosePanel(_soundSettingPanel);
    }

    public override void UpdateSometing()
    {
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
