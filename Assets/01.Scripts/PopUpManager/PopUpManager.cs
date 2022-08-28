using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopUpManager : MonoBehaviour
{
	[SerializeField] private Image _popupObject;
	[SerializeField] private TextMeshProUGUI _text;
	[SerializeField] private Image _achievementPopupObject;
	[SerializeField] private TextMeshProUGUI _achievementtext;

	private AchievementData _currentAchievementData = null;
	private Queue<AchievementData> _achievementDatas = new Queue<AchievementData>();

	/// <summary>
	/// 팝업창 작동
	/// </summary>
	/// <param name="text"></param>
	public void SetPopUp(string text)
	{
		_text.text = text;
		_popupObject.gameObject.SetActive(true);
		_popupObject.rectTransform.localScale = Vector3.one;
		_popupObject.rectTransform.DOScale(0, 0.3f).SetDelay(0.3f).SetEase(Ease.InOutExpo).OnComplete(() => _popupObject.gameObject.SetActive(false));
	}

	/// <summary>
	/// 업적 달성 알림
	/// </summary>
	/// <param name="achievementData"></param>
	public void SetAchievement(AchievementData achievementData)
	{
		if(_currentAchievementData != null)
		{
			_achievementDatas.Enqueue(achievementData);
		}
		else
		{
			_currentAchievementData = achievementData;
			_achievementtext.text = achievementData._achievementName;
			_achievementPopupObject.gameObject.SetActive(true);
			_achievementPopupObject.rectTransform.localScale = Vector3.one;
			_achievementPopupObject.rectTransform.DOShakeAnchorPos(0.5f);
			_achievementPopupObject.rectTransform.DOScale(0, 1f).SetDelay(1f).SetEase(Ease.InOutExpo)
				.OnComplete(() => _achievementPopupObject.rectTransform.DOScale(0, 1f).SetEase(Ease.InOutElastic)
				.OnComplete(() => CancleAchievement())) ;
		}
	}

	private void CancleAchievement()
	{
		_achievementPopupObject.gameObject.SetActive(false);
		if(_achievementDatas.Count > 0)
		{
			SetAchievement(_achievementDatas.Dequeue());
		}
	}
}
