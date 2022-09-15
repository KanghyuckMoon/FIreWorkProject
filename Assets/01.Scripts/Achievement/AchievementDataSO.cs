using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementDataSO", menuName = "ScriptableObject/AchievementDataSO")]
public class AchievementDataSO : ScriptableObject
{
	public List<AchievementData> _achievementDatas = new List<AchievementData>();

	public Sprite _debugChangeSprite;

	public AchievementData GetAchievementData(int itemCode)
	{
		return _achievementDatas.Find(x => x._achievementCode == itemCode);
	}

	[ContextMenu("AllChangeSprite")]
	public void AllChangeSprite()
	{
		for(int i = 0; i < _achievementDatas.Count; ++i)
		{
			_achievementDatas[i]._sprite = _debugChangeSprite;
		}
	}

}

[System.Serializable]
public class AchievementData
{
	public int _achievementCode;
	public string _achievementName;
	public string _content;
	public string _functionName;
	public string _functionParameter;
	public bool _isCantView = false;
	public Sprite _sprite = null;
}

