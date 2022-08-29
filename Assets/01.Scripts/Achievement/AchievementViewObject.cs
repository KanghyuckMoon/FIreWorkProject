using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class AchievementViewObject : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _nameText;
	[SerializeField] private TextMeshProUGUI _contentText;
	[SerializeField] private TextMeshProUGUI _clearText;
	private RectTransform _rectTransform;

	/// <summary>
	/// UI 업데이트
	/// </summary>
	/// <param name="achievementData"></param>
	/// <param name="isGetAchievement"></param>
	public void UpdateUI(AchievementData achievementData, bool isGetAchievement)
	{
		_rectTransform ??= GetComponent<RectTransform>();

		_nameText.text = achievementData._achievementName;
		_contentText.text = achievementData._content;

		if(isGetAchievement)
		{
			_clearText.text = "달성!";
		}
		else
		{
			_clearText.text = "미달성!";
		}

		_rectTransform.localScale = Vector3.zero;
		_rectTransform.DOScale(1, 0.3f).SetEase(Ease.InOutElastic).SetDelay(achievementData._achievementCode * 0.1f);
	}

}
