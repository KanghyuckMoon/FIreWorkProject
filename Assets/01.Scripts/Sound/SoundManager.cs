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
	/// �ʱ�ȭ
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
	/// ��� ���� ��������
	/// </summary>
	private void GetBGMAudioSource()
	{

		//���ο� ����� �ҽ� �����
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
	/// ����Ʈ ����� �ҽ��� ����
	/// </summary>
	private void GenerateEFFAudioSource()
	{
		int count = (int)AudioEFFType.Count;
		for (int i = 1; i < count; ++i)
		{
			//Ű�� ����� Ŭ�� ��������
			string key = System.Enum.GetName(typeof(AudioEFFType), i);
			AudioClip audioClip = AddressablesManager.Instance.GetResource<AudioClip>(key);

			//���ο� ����� �ҽ� �����
			GameObject obj = new GameObject(key);
			obj.transform.SetParent(transform);
			AudioSource audioSource = obj.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _effAudioGroup;
			audioSource.clip = audioClip;
			audioSource.playOnAwake = false;

			//����� �ҽ��� �߰��ϱ�
			_effAudioClips.Add((AudioEFFType)i, audioClip);
		}
	}

	/// <summary>
	/// ȿ���� ���
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
	/// ������� ���
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
	/// ��� ȿ���� ���
	/// </summary>
	public void PlayShot()
	{
		PlayEFF(AudioEFFType.Shot);
	}
	/// <summary>
	/// �����ϴ� ȿ���� ���
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
