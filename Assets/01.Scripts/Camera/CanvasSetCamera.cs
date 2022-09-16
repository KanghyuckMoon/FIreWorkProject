using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetCamera : MonoBehaviour
{
    private Canvas _canvas;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _canvas.planeDistance = 5;
    }
}
