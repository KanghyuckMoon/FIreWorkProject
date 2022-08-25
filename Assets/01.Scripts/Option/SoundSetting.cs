using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSetting : MonoBehaviour
{

	[SerializeField] AudioMixer _audioMixer;
	[SerializeField] Slider _bgmAudioSlider;
	[SerializeField] Slider _effAudioSlider;


	/// <summary>
	/// 소리 설정 적용
	/// </summary>
	public void ApplySettingSound()
	{
		_audioMixer.SetFloat("BGMVolume", _bgmAudioSlider.value);
		_audioMixer.SetFloat("EFFVolume", _effAudioSlider.value);
		UserSaveDataManager.Instance.UserSaveData.bgmVoulume = _bgmAudioSlider.value;
		UserSaveDataManager.Instance.UserSaveData.bgmVoulume = _effAudioSlider.value;
		UserSaveDataManager.Save();
	}

	public void ApplySettingSound(float bgmvalue, float effvlaue)
	{
		_bgmAudioSlider.value = bgmvalue;
		_effAudioSlider.value = effvlaue;
		_audioMixer.SetFloat("BGMVolume", bgmvalue);
		_audioMixer.SetFloat("EFFVolume", effvlaue);
	}
}
