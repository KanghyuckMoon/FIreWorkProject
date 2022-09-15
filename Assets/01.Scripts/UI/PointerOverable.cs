using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerOverable : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private GameObject _descriptionPanel; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        _descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _descriptionPanel.SetActive(false);
    }

}
