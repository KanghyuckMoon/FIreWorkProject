using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AchievementObject : MonoBehaviour, Observer
{
	public ObservationObject ObservationObject 
	{
		get => AchievementManager.Instance;
		set
		{
			
		}
	}

	[SerializeField] private int _achievementCode = 0;
	[SerializeField] private GameObject _effectObject;
	[SerializeField] private bool _isEffectOff = false;
	private bool _isPlayingAnimation = false;

	private void Awake()
	{
		AddObservation();
		gameObject.SetActive(false);
	}

	public void AddObservation()
	{
		ObservationObject.AddObserver(this);
	}

	public void ReceiveMessage()
	{
		if(AchievementManager.Instance.CheckHaveAchievement(_achievementCode))
		{
			gameObject.SetActive(true);
			if(!_isPlayingAnimation)
			{
				_isPlayingAnimation = true;
				ActiveAnimation();
			}
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	private void ActiveAnimation()
	{
		if(_isEffectOff)
		{
			return;
		}
		if(_effectObject != null)
		{
			_effectObject.SetActive(true);
		}		


		Vector3 originPosition = transform.position;
		Vector3 changePosition = originPosition + new Vector3(0, 3, 0);
		Vector3 originSize = transform.localScale;
		Vector3 changeSize = originSize * 1.5f;
		transform.localScale = originSize * 0.001f;
		transform.position = changePosition;
		transform.DOScale(changeSize, 0.5f).SetEase(Ease.OutQuart).OnComplete(() => transform.DOScale(originSize, 1).SetEase(Ease.InExpo));

		if(_effectObject == null)
		{
			transform.DOMoveY(originPosition.y, 1).SetDelay(0.5f).SetEase(Ease.InExpo);
		}
		else
		{
			transform.DOMoveY(originPosition.y, 1).SetDelay(0.5f).OnComplete(() => _effectObject.SetActive(false)).SetEase(Ease.InExpo);
		}
	}

	private void EmissiveObject()
	{

	}
}
