using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
	private AudioMixer _audioMixer;
	private AudioMixerGroup _bgmAudioGroup;
	private AudioMixerGroup _effAudioGroup;
	private AudioSource _bgmAudioSource = null;
	private Dictionary<AudioBGMType, AudioClip> _bgmAudioClips = new Dictionary<AudioBGMType, AudioClip>();
	private Dictionary<AudioEFFType, AudioClip> _effAudioClips = new Dictionary<AudioEFFType, AudioClip>();
	private AudioBGMType _currentBGMType = AudioBGMType.Count;
	private List<OneShot> _effOneShots = new List<OneShot>();
	private bool _isInit = false;

	public override void Awake()
	{
		base.Awake();
		Init();
	}

	/// <summary>
	/// 초기화
	/// </summary>
	private void Init()
	{
		if (_isInit)
		{
			return;
		}
		_isInit = true;

		_audioMixer = AddressablesManager.Instance.GetResource<AudioMixer>("MainMixer");

		var groups = _audioMixer.FindMatchingGroups(string.Empty);
		_bgmAudioGroup = groups[1];
		_effAudioGroup = groups[2];

		GetBGMAudioSource();
		GenerateEFFAudioSource();
	}

	/// <summary>
	/// 배경 음악 가져오기
	/// </summary>
	private void GetBGMAudioSource()
	{

		//새로운 오디오 소스 만들기
		GameObject obj = new GameObject("BGM");
		obj.transform.SetParent(transform);
		AudioSource audioSource = obj.AddComponent<AudioSource>();
		audioSource.outputAudioMixerGroup = _bgmAudioGroup;
		audioSource.clip = null;
		audioSource.playOnAwake = true;
		audioSource.loop = true;

		_bgmAudioSource = audioSource;

		int count = (int)AudioBGMType.Count;
		for (int i = 1; i < count; ++i)
		{
			string key = System.Enum.GetName(typeof(AudioBGMType), i);
			AudioClip audioClip = AddressablesManager.Instance.GetResource<AudioClip>(key);
			_bgmAudioClips.Add((AudioBGMType)i, audioClip);
		}
	}

	/// <summary>
	/// 이펙트 오디오 소스들 생성
	/// </summary>
	private void GenerateEFFAudioSource()
	{
		int count = (int)AudioEFFType.Count;
		for (int i = 1; i < count; ++i)
		{
			//키와 오디오 클립 가져오기
			string key = System.Enum.GetName(typeof(AudioEFFType), i);
			AudioClip audioClip = AddressablesManager.Instance.GetResource<AudioClip>(key);

			//새로운 오디오 소스 만들기
			GameObject obj = new GameObject(key);
			obj.transform.SetParent(transform);
			AudioSource audioSource = obj.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _effAudioGroup;
			audioSource.clip = audioClip;
			audioSource.playOnAwake = false;

			//오디오 소스에 추가하기
			_effAudioClips.Add((AudioEFFType)i, audioClip);
		}
	}

	/// <summary>
	/// 효과음 재생
	/// </summary>
	/// <param name="audioEFFType"></param>
	public void PlayEFF(AudioEFFType audioEFFType)
	{
		if (!_isInit)
		{
			Init();
		}

		OneShot(_effAudioClips[audioEFFType], 1f);
	}

	/// <summary>
	/// 배경음악 재생
	/// </summary>
	/// <param name="audioBGMType"></param>
	public void PlayBGM(AudioBGMType audioBGMType)
	{
		if (!_isInit)
		{
			Init();
		}

		if (_currentBGMType == audioBGMType)
		{
			return;
		}

		_currentBGMType = audioBGMType;

		_bgmAudioSource.Stop();
		_bgmAudioSource.clip = _bgmAudioClips[audioBGMType];
		_bgmAudioSource.Play();
	}

	/// <summary>
	/// 쏘는 효과음 재생
	/// </summary>
	public void PlayShot()
	{
		PlayEFF(AudioEFFType.Shot);
	}
	/// <summary>
	/// 폭발하는 효과음 재생
	/// </summary>
	public void PlayFire()
	{
		PlayEFF(AudioEFFType.Fire);
	}


	private void OneShot(AudioClip clip, float volume)
	{
		foreach(var oneShot in _effOneShots)
		{
			if(!oneShot.gameObject.activeSelf)
			{
				AudioSource OneShotaudioSource = oneShot.GetComponent<AudioSource>();
				OneShotaudioSource.clip = clip;
				OneShotaudioSource.spatialBlend = 1f;
				OneShotaudioSource.volume = volume;
				OneShotaudioSource.gameObject.SetActive(true);
				OneShotaudioSource.Play();
				return;
			}
		}
		OneShotGeneration(clip, volume);
	}

	private void OneShotGeneration(AudioClip clip, float volume)
	{
		GameObject gameObject = new GameObject("One shot audio");
		gameObject.SetActive(false);
		gameObject.transform.position = Vector3.zero;
		AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
		audioSource.clip = clip;
		audioSource.spatialBlend = 1f;
		audioSource.volume = volume;
		audioSource.outputAudioMixerGroup = _effAudioGroup;
		_effOneShots.Add((OneShot)gameObject.AddComponent(typeof(OneShot)));
		gameObject.SetActive(true);
		audioSource.Play();		
	}
}
