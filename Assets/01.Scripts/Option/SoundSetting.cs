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
	}
}
