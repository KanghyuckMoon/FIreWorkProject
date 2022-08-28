using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopUpManager : MonoBehaviour
{
	[SerializeField] private Image _popupObject;
	[SerializeField] private TextMeshProUGUI _text;
	
	/// <summary>
	/// ÆË¾÷Ã¢ ÀÛµ¿
	/// </summary>
	/// <param name="text"></param>
	public void SetPopUp(string text)
	{
		_text.text = text;
		_popupObject.gameObject.SetActive(true);
		_popupObject.rectTransform.localScale = Vector3.one;
		_popupObject.rectTransform.DOScale(0, 0.3f).SetDelay(0.3f).SetEase(Ease.InOutExpo).OnComplete(() => _popupObject.gameObject.SetActive(false));
	}
}
