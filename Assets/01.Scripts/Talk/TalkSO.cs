using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalkSO", menuName = "ScriptableObject/TalkSO")]
public class TalkSO : ScriptableObject
{
	public List<TalkData> talkDatas = new List<TalkData>();
}

[System.Serializable]
public class TalkData
{
	public enum TalkObject
	{
		None,
		Player,
		Other,
	}

	public TalkObject talkObject;
	public string othername;
	public string content;
}