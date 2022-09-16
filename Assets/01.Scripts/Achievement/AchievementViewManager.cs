using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AchievementViewManager : MonoBehaviour
{
	[SerializeField] private Canvas _achievementCanvas;
	[SerializeField] private RectTransform _achievementBackground;
	[SerializeField] private AchievementDataSO _achievementDataSO;
	[SerializeField] private AchievementViewObject _achievementViewObject;
	[SerializeField] private Image _backgroundImage;
	private bool _isOpen = false;
	private Vector2 _moveVector = Vector2.zero;
	private float _maxScroll = 0f;

	private bool _isMoveOn = false;
	private Vector2 _downVector = Vector2.zero;


	[ContextMenu("업적 창 열기")]
	/// <summary>
	/// 업적창 열기
	/// </summary>
	public void OpenAchievementView()
	{
		_isOpen = true;
		_achievementCanvas.gameObject.SetActive(true);
		_achievementBackground.DOKill();
		_achievementBackground.localScale = Vector3.one;
		_achievementBackground.anchoredPosition = new Vector2(0, -3000);
		_backgroundImage.DOKill();
		_backgroundImage.DOFade(1, 0.3f);

		if (_achievementBackground.childCount != _achievementDataSO._achievementDatas.Count)
		{
			_maxScroll = _achievementDataSO._achievementDatas.Count * 250;
			//생성
			for (; _achievementBackground.childCount != _achievementDataSO._achievementDatas.Count;)
			{
				int itemCode = _achievementBackground.childCount;
				AchievementViewObject achievemenViewtObject = Instantiate(_achievementViewObject, _achievementBackground);
				achievemenViewtObject.UpdateUI(_achievementDataSO.GetAchievementData(itemCode), UserSaveDataManager.Instance.UserSaveData.haveAchievement.Contains(itemCode));
			}

		}
		else
		{

			for (int itemCode = 0; itemCode < _achievementDataSO._achievementDatas.Count; ++itemCode)
			{
				AchievementViewObject achievemenViewtObject = _achievementBackground.GetChild(itemCode).GetComponent<AchievementViewObject>();
				achievemenViewtObject.UpdateUI(_achievementDataSO.GetAchievementData(itemCode), UserSaveDataManager.Instance.UserSaveData.haveAchievement.Contains(itemCode));
			}
		}
	}

	[ContextMenu("업적 창 닫기")]
	/// <summary>
	/// 업적 창 닫기
	/// </summary>
	public void CloseAchievementView()
	{
		_isOpen = false;
		_backgroundImage.DOKill();
		_backgroundImage.DOFade(0, 0.3f);
		_achievementBackground.DOScale(0, 0.3f).OnComplete(() => _achievementCanvas.gameObject.SetActive(false));
	}

	private void Update()
	{
		if(_isOpen)
		{
			if(Input.GetMouseButtonDown(0))
			{
				_isMoveOn = true;
				_downVector = Input.mousePosition;
			}
			else if(Input.GetMouseButtonUp(0))
			{
				_isMoveOn = false;
			}

			if(_isMoveOn)
			{
				Vector2 subtractVector =  (Vector2)Input.mousePosition - _downVector;
				_downVector = Input.mousePosition;

				Vector2 moveVector = _achievementBackground.anchoredPosition + subtractVector;
				_achievementBackground.anchoredPosition = moveVector;
			}
		}
	}

}
