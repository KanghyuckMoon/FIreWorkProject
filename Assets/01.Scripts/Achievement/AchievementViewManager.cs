using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AchievementViewManager : MonoBehaviour
{
	[SerializeField] private Canvas _achievementCanvas;
	[SerializeField] private RectTransform _achievementBackground;
	[SerializeField] private AchievementDataSO _achievementDataSO;
	[SerializeField] private AchievementViewObject _achievementViewObject;
	private bool _isOpen = false;
	private Vector2 _moveVector = Vector2.zero;
	private float _maxScroll = 0f;


	[ContextMenu("업적 창 열기")]
	/// <summary>
	/// 업적창 열기
	/// </summary>
	public void OpenAchievementView()
	{
		_isOpen = true;
		_achievementCanvas.gameObject.SetActive(true);

		if(_achievementBackground.childCount != _achievementDataSO._achievementDatas.Count)
		{
			_maxScroll = _achievementDataSO._achievementDatas.Count * 200;
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
		_achievementCanvas.gameObject.SetActive(false);
	}

	private void Update()
	{
		if(_isOpen)
		{
			if(Input.GetKeyDown(KeyCode.W))
			{
				if (0 > _moveVector.y)
				{
					return;
				}
				_moveVector.y -= 300;
				_achievementBackground.anchoredPosition = _moveVector;
			}
			else if (Input.GetKeyDown(KeyCode.S))
			{
				if (_maxScroll < _moveVector.y)
				{
					return;
				}
				_moveVector.y += 300;
				_achievementBackground.anchoredPosition = _moveVector;
			}
		}
	}

}
