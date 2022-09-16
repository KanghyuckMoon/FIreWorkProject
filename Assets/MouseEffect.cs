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
        //point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        //        Input.mousePosition.y, -Camera.main.transform.position.z));   

        Sequence seq = DOTween.Sequence();

        seq.Restart();

        seq.AppendCallback(() => _mouseEffectImage.rectTransform.position = point);

        seq.AppendCallback(() => _mouseEffectImage.gameObject.SetActive(true));
        seq.Append(_mouseEffectImage.DOFade(1, 0.1f));
        seq.Join(_mouseEffectImage.transform.DOScale(1.2f, 0.1f));
        seq.Append(_mouseEffectImage.DOFade(0, 0.15f));
        seq.Join(_mouseEffectImage.transform.DOScale(0, 0.15f));
    }
}
