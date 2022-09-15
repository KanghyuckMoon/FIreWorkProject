using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DetailsUI : MonoBehaviour
{
    public static DetailsUI instance;

    public TextMeshProUGUI _text;
    

    private void Awake()
    {
        instance = this;
    }

    public void AddingScore(int money)
    {
        Sequence seq = DOTween.Sequence();

        Vector3 _originPos = _text.transform.position;

        _text.text = string.Format("+{0}", money);

        _text.gameObject.SetActive(true);

        seq.Append(_text.transform.DOMoveY(_text.transform.position.y + 20, 1.1f));

        seq.AppendCallback(() =>
        {
            _text.gameObject.SetActive(false);
            _text.transform.position = _originPos;
        });

        //seq.Rewind();
    }
}
