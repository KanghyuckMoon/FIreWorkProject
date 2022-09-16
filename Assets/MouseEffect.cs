using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MouseEffect : MonoBehaviour
{
    public Image _mouseEffectImage;
    Vector3 point;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MouseDownEffect();
        }
    }

    void MouseDownEffect()
    {
        point = Input.mousePosition;   

        Sequence seq = DOTween.Sequence();

        seq.Restart();

        seq.AppendCallback(() => _mouseEffectImage.rectTransform.position = point);

        seq.AppendCallback(() => _mouseEffectImage.gameObject.SetActive(true));
        seq.Append(_mouseEffectImage.DOFade(1, 0.18f));
        seq.Join(_mouseEffectImage.transform.DOScale(1.2f, 0.18f));
        seq.Append(_mouseEffectImage.DOFade(0, 0.1f));
        seq.Join(_mouseEffectImage.transform.DOScale(0, 0.1f));
    }
}
