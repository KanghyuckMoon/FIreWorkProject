using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class SoundSetting : MonoBehaviour
{

	[SerializeField] AudioMixer _audioMixer;
	[SerializeField] Slider _bgmAudioSlider;
	[SerializeField] Slider _effAudioSlider;

	/// <summary>
	/// 슬라이더 받아오오기 
	/// </summary>
	/// <param name="bgmAudioSlider"></param>
	/// <param name="effAudioSlider"></param>
	public void InitSlider(Slider bgmAudioSlider, Slider effAudioSlider)
    {
		this._bgmAudioSlider = bgmAudioSlider;
		this._effAudioSlider = effAudioSlider; 
    }
	public void SetBgmAudio(float bgmValue)
    {
		_audioMixer.SetFloat("BGMVolume",bgmValue);
		UserSaveDataManager.Instance.UserSaveData.bgmVoulume = _bgmAudioSlider.value;
		UserSaveDataManager.Save();
	}

	public void SetEffAudio(float effValue)
    {
		_audioMixer.SetFloat("BGMVolume", effValue);
		UserSaveDataManager.Instance.UserSaveData.effVoulume = _effAudioSlider.value;
		UserSaveDataManager.Save();
	}

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

	public IEnumerator ApplySettingSound(float bgmvalue, float effvlaue)
	{
		while(_bgmAudioSlider == null || _effAudioSlider == null)
        {
			yield return null; 
        }
		_bgmAudioSlider.value = bgmvalue;
		_effAudioSlider.value = effvlaue;
		_audioMixer.SetFloat("BGMVolume", bgmvalue);
		_audioMixer.SetFloat("EFFVolume", effvlaue);
	}
}
