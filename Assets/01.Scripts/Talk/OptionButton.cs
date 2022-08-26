using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class OptionButton : MonoBehaviour
{
	private Button Button
	{
		get
		{
			_button ??= GetComponent<Button>();
			return _button;
		}
	}

	[SerializeField] private TextMeshProUGUI _textMeshProUGUI;
	private TalkFSO _talkSO;
	private Button _button;
	private TalkManager _talkManager;
	private OptionData _optionData;

	private void Start()
	{
		Button.onClick.AddListener(() => _talkManager.EneableTalk(_talkSO));
	}


	/// <summary>
	/// 옵션 설정
	/// </summary>
	/// <param name="talkSO"></param>
	public void OptionSetting(OptionData optionData, TalkFSO talkSO ,TalkManager talkManager)
	{
		this._optionData = optionData;
		this._talkSO = talkSO;
		this._talkManager = talkManager;
		this._textMeshProUGUI.text = optionData._contents;
	}

	/// <summary>
	/// 함수 실행
	/// </summary>
	public void FunctionInvoke()
	{
		if(_optionData?._functions != null)
		{
			foreach(var function in _optionData._functions)
			{
				Type type = typeof(TalkMethod);
				MethodInfo myClass_FunCallme = type.GetMethod(function._functionName, BindingFlags.Static | BindingFlags.Public);
				myClass_FunCallme.Invoke(null, new object[] { function._functionParameters });
			}
		}
	}
}




