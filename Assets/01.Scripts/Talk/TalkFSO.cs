using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalkSO", menuName = "ScriptableObject/TalkSO")]
public class TalkFSO : ScriptableObject
{
	public List<TalkDataF> talkDatas = new List<TalkDataF>();
	public Sprite _debugPlayerSprite;
	public Sprite _debugOtherSprite;

	[ContextMenu("ChangeSprite")]
	/// <summary>
	/// 스프라이트 모두 변경
	/// </summary>
	public void ChangeSprite()
	{
		foreach(var data in talkDatas)
		{
			data._playerSprite = _debugPlayerSprite;
			data._otherSprite = _debugOtherSprite;
		}
	}
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
	public string _functionParameters_1;
	public string _functionParameters_2;
	public Sprite _playerSprite;
	public Sprite _otherSprite;
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

