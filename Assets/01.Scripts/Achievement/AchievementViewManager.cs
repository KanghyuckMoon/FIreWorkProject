using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AchievementViewManager : MonoBehaviour
{
	[SerializeField] private Canvas _achievementCanvas;
	[SerializeField] private Transform _achievementBackground;
	[SerializeField] private AchievementDataSO _achievementDataSO;
	[SerializeField] private AchievementViewObject _achievementViewObject;

	[ContextMenu("���� â ����")]
	/// <summary>
	/// ����â ����
	/// </summary>
	public void OpenAchievementView()
	{
		_achievementCanvas.gameObject.SetActive(true);

		//����
		for(; _achievementBackground.childCount != _achievementDataSO._achievementDatas.Count;)
		{
			int itemCode = _achievementBackground.childCount;
			AchievementViewObject achievemenViewtObject = Instantiate(_achievementViewObject, _achievementBackground);
			achievemenViewtObject.UpdateUI(_achievementDataSO.GetAchievementData(itemCode) , UserSaveDataManager.Instance.UserSaveData.haveAchievement.Contains(itemCode));
		}
		
		for (int itemCode = 0; itemCode < _achievementDataSO._achievementDatas.Count; ++itemCode)
		{
			AchievementViewObject achievemenViewtObject = _achievementBackground.GetChild(itemCode).GetComponent<AchievementViewObject>();
			achievemenViewtObject.UpdateUI(_achievementDataSO.GetAchievementData(itemCode), UserSaveDataManager.Instance.UserSaveData.haveAchievement.Contains(itemCode));
		}

	}

	[ContextMenu("���� â �ݱ�")]
	/// <summary>
	/// ���� â �ݱ�
	/// </summary>
	public void CloseAchievementView()
	{
		_achievementCanvas.gameObject.SetActive(false);
	}



}
