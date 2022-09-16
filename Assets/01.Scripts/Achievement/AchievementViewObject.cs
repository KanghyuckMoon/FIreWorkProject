using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems; 

public class AchievementViewObject : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
	[SerializeField] private TextMeshProUGUI _nameText;
	[SerializeField] private TextMeshProUGUI _clearText;
	[SerializeField] private Image _image;
	private RectTransform _rectTransform;

	private AchievementData _achievementData; // 업적 데이터 
	private DescriptionManager _descriptionManager; // 설명창 관리자  
    private void Awake()
    {
		_descriptionManager = FindObjectOfType<DescriptionManager>(); 
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
		_descriptionManager.ActiveAchievementDescriptoin(true, _achievementData._achievementCode);
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		_descriptionManager.ActiveAchievementDescriptoin(false, _achievementData._achievementCode);
	}
	/// <summary>
	/// UI 업데이트
	/// </summary>
	/// <param name="achievementData"></param>
	/// <param name="isGetAchievement"></param>
	public void UpdateUI(AchievementData achievementData, bool isGetAchievement)
	{
		_rectTransform ??= GetComponent<RectTransform>();
		_achievementData ??= achievementData; 

		if(isGetAchievement)
		{
			_nameText.text = achievementData._achievementName;
			_clearText.text = "달성!";
		}
		else
		{
			_nameText.text = "???";
			_clearText.text = "미달성!";
		}

		_rectTransform.localScale = Vector3.zero;
		_rectTransform.DOScale(1, 0.3f).SetEase(Ease.InOutElastic).SetDelay(achievementData._achievementCode * 0.1f);
		_image.sprite = achievementData._sprite;
	}


}
