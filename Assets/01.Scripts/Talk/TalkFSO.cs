using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalkSO", menuName = "ScriptableObject/TalkSO")]
public class TalkFSO : ScriptableObject
{
	public List<TalkDataF> talkDatas = new List<TalkDataF>();
}

[System.Serializable]
public class TalkDataF
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
	public List<OptionData> _optionDatas = new List<OptionData>();
	public string _functionName;
	public string _functionParameters;
}

[System.Serializable]
public class OptionData
{
	public string _contents;
	public float _value;
	public TalkFSO _nextTalkSO;
	public List<FucntionData> _functions;
}


[System.Serializable]
public class FucntionData
{
	public string _functionName;
	public string _functionParameters;
}

