using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementDataSO", menuName = "ScriptableObject/AchievementDataSO")]
public class AchievementDataSO : ScriptableObject
{
	public List<AchievementData> _achievementDatas = new List<AchievementData>();


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
}

