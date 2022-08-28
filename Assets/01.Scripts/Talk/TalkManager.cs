using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TalkManager : MonoBehaviour
{
	[SerializeField] private Canvas _talkCanvas;
	[SerializeField] private Image _contentBackground;
	[SerializeField] private Image _nameBackground;
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
			OpenAnimation();
			UserSaveDataManager.Instance.UserSaveData.isViewCutScene[index] = true;
			_currentTalkSO = _talkSOList[index];
			EneableTalk(_currentTalkSO);
		}
	}

	private void OpenAnimation()
	{
		_nameBackground.rectTransform.DOKill();
		_nameBackground.rectTransform.localScale = Vector3.zero;
		_nameBackground.rectTransform.anchoredPosition = new Vector2(0, -96);
		_nameBackground.rectTransform.DOAnchorPos(new Vector2(0, 352), 1);
		_nameBackground.rectTransform.DOScale(1, 1);

		_contentBackground.rectTransform.DOKill();
		_contentBackground.rectTransform.localScale = Vector3.zero;
		_contentBackground.rectTransform.anchoredPosition = new Vector2(0, -160);
		_contentBackground.rectTransform.DOAnchorPos(new Vector2(0, 150), 1);
		_contentBackground.rectTransform.DOScale(1, 1);
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
		_playerObject.rectTransform.anchoredPosition = new Vector2(-100, -100);
		_npcObject.rectTransform.anchoredPosition = new Vector2(100, -100);
		SetCharacterImage();
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
				SetCharacterImage();

			}

		}
	}

	private void SetCharacterImage()
	{
		if (_currentTalkSO.talkDatas[_currentIndex].talkObject == TalkDataF.TalkObject.Player)
		{
			EnableCharacter(_playerObject, new Vector2(485, 485));
			DisableCharacter(_npcObject, new Vector2(-250, 400));
		}
		else if (_currentTalkSO.talkDatas[_currentIndex].talkObject == TalkDataF.TalkObject.Other)
		{
			DisableCharacter(_playerObject, new Vector2(400, 400));
			EnableCharacter(_npcObject, new Vector2(-335, 485));
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

	private void EnableCharacter(Image image, Vector2 move)
	{
		image.DOKill();
		image.DOColor(Color.white, 0.3f);
		image.rectTransform.DOScale(1f, 0.3f);
		image.rectTransform.DOAnchorPos(move, 0.3f);
	}
	private void DisableCharacter(Image image, Vector2 move)
	{
		image.DOKill();
		image.DOColor(new Color(0.7f, 0.7f, 0.7f), 0.3f);
		image.rectTransform.DOScale(0.8f, 0.3f);
		image.rectTransform.DOAnchorPos(move, 0.3f);
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
