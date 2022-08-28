using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkManager : MonoBehaviour
{
	[SerializeField] private Canvas _talkCanvas;
	[SerializeField] private TalkFSO _currentTalkSO = null;
	[SerializeField] private List<TalkFSO> _talkSOList = new List<TalkFSO>();
	[SerializeField] private Image _playerObject;
	[SerializeField] private Image _npcObject;
	[SerializeField] private TextMeshProUGUI _nameText;
	[SerializeField] private TextMeshProUGUI _contentsText;
	[SerializeField] private Transform _optionTransform;
	[SerializeField] private GameObject _optionObject;
	[SerializeField] private float _originDelay = 0.1f;
	[SerializeField] private float _currentDelay = 0.1f;

	private int _currentIndex = 0;
	private int _stringIndex = 0;
	private List<OptionButton> _optionButtons = new List<OptionButton>();
	private bool _isTalking = false;
	private bool _isOptionActive = false;
	private Queue<int> _viewCutSceneList = new Queue<int>();

	public void OpenCutScene(int index)
	{
		if (UserSaveDataManager.Instance.UserSaveData.isViewCutScene[index])
		{
			return;
		}
		if(_currentTalkSO != null)
		{
			_viewCutSceneList.Enqueue(index);
		}
		else
		{
			UserSaveDataManager.Instance.UserSaveData.isViewCutScene[index] = true;
			_currentTalkSO = _talkSOList[index];
			EneableTalk(_currentTalkSO);
		}
	}

	/// <summary>
	/// TalkSO를 받아서 대화 진행
	/// </summary>
	/// <param name="talkSO"></param>
	public void EneableTalk(TalkFSO talkSO)
	{
		if(talkSO == null)
		{
			CancleTalk();
			return;
		}

		_currentTalkSO = talkSO;
		_talkCanvas.gameObject.SetActive(true);
		_isTalking = true;
		_isOptionActive = false;
		_currentIndex = 0;
		_stringIndex = 0;
		_contentsText.text = "";
		_optionTransform.gameObject.SetActive(false);
		SetNameText();
	}

	[ContextMenu("DebugEnableTalk")]
	/// <summary>
	/// 인스펙터로 들어간 TalkSO로 테스트
	/// </summary>
	public void DebugEnableTalk()
	{
		_talkCanvas.gameObject.SetActive(true);
		_isTalking = true;
		_isOptionActive = false;
		_currentIndex = 0;
		_stringIndex = 0;
		_contentsText.text = "";
		_optionTransform.gameObject.SetActive(false);
		SetNameText();
	}

	/// <summary>
	/// 대화창 끄기
	/// </summary>
	public void CancleTalk()
	{
		_isTalking = false;
		_talkCanvas.gameObject.SetActive(false);
		_currentTalkSO = null;

		if(_viewCutSceneList.Count > 0)
		{
			OpenCutScene(_viewCutSceneList.Dequeue());
		}
	}

	private void SetNameText()
	{
		if (_currentTalkSO.talkDatas[_currentIndex].talkObject == TalkDataF.TalkObject.Player)
		{
			_nameText.text = "플레이어";
		}
		else
		{
			_nameText.text = _currentTalkSO.talkDatas[_currentIndex].othername;
		}
	}

	private void ShowTalk()
	{
		if(_currentDelay > 0f)
		{
			_currentDelay -= Time.deltaTime;
			return;
		}

		if(_stringIndex < _currentTalkSO.talkDatas[_currentIndex].content.Length)
		{
			_contentsText.text += _currentTalkSO.talkDatas[_currentIndex].content[_stringIndex];
			_currentDelay = _originDelay;
			_stringIndex += 1;
		}
		else if(Input.GetMouseButtonDown(0))
		{

			if (_currentIndex == _currentTalkSO.talkDatas.Count - 1)
			{
				if(_currentTalkSO.talkDatas[_currentIndex]._optionDatas != null)
				{
					if(!_isOptionActive)
					{
						GenerateOptions();
					}
				}
				else
				{
					CancleTalk();
				}
				return;
			}
			else
			{
				_contentsText.text = "";
				_stringIndex = 0;
				_currentIndex += 1;
				_currentDelay = _originDelay;
				SetNameText();
			}

		}
	}

	private void SkipTalk()
	{
		if (_stringIndex < _currentTalkSO.talkDatas[_currentIndex].content.Length)
		{
			if (Input.GetMouseButtonDown(0))
			{
				_stringIndex = _currentTalkSO.talkDatas[_currentIndex].content.Length;
				_contentsText.text = _currentTalkSO.talkDatas[_currentIndex].content;
			}
		}
	}

	private void GenerateOptions()
	{
		var optionDatas = _currentTalkSO.talkDatas[_currentIndex]._optionDatas;
		for(int i = 0; i < _optionButtons.Count; ++i)
		{
			_optionButtons[i].gameObject.SetActive(false);
		}
		
		for (int i = 0; i < optionDatas.Count; ++i)
		{
			OptionButton optionButton = null;
			if (i < _optionButtons.Count)
			{
				optionButton = _optionButtons[i];
			}
			else
			{
				optionButton = Instantiate(_optionObject, _optionTransform).GetComponent<OptionButton>();
				_optionButtons.Add(optionButton);
			}
			optionButton.gameObject.SetActive(true);
			optionButton.OptionSetting(optionDatas[i], optionDatas[i]._nextTalkSO, this);
		}
		_optionTransform.gameObject.SetActive(true);
		_isOptionActive = true;
	}

	public void Update()
	{
		if(_isTalking)
		{
			SkipTalk();
			ShowTalk();
		}
	}

}
