using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _descriptionPanelRect; 

    private void MoveDescription(Vector2 pos)
    {
        _descriptionPanelRect.anchoredPosition = pos; 
    }
    public void ActiveDescription(bool isActive, Vector2 pos)
    {
        Debug.Log("설명 활성화");
        if(isActive == true)
        {
            _descriptionPanelRect.gameObject.SetActive(true);
            MoveDescription(pos);
            return; 
        }
        _descriptionPanelRect.gameObject.SetActive(false);
    }
}
