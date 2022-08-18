using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShot : MonoBehaviour
{
	private AudioSource _audioSource;
	private float _time;

	private void OnEnable()
	{
		_audioSource ??= GetComponent<AudioSource>();
		_time = Time.time + _audioSource.clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale);
	}

	void Update()
    {
        if(_time < Time.time)
		{
			gameObject.SetActive(false);
		}
    }
}
