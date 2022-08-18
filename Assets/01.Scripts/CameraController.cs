using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _centerTransform = null;
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _zoomSpeed = 1;
    [SerializeField] private float _distance = 3;
    [SerializeField] private float _smoothTime;
    [SerializeField] private Vector3 _velocity;
    Vector3 _yVector = Vector3.zero;
    Vector3 _xVector = Vector3.zero;
    Vector3 _moveVector = Vector3.zero;

    // Update is called once per frame
    void LateUpdate()
    {
        ZoomInOut();
        MoveCamera();
        CameraPositionSetting();
    }

    private void MoveCamera()
    {
        if(Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            float _xMove = Input.GetAxis("Mouse X");
            float _yMove = Input.GetAxis("Mouse Y");
            _yVector = Vector3.up * _yMove * _moveSpeed * Time.deltaTime;
            _xVector = transform.right * _xMove * _moveSpeed * Time.deltaTime;

            transform.LookAt(_centerTransform);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void ZoomInOut()
	{
        _distance -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;

        if (_distance < 1.0f) _distance = 1.0f;
        if (_distance > 100.0f) _distance = 100.0f;
    }

    private void CameraPositionSetting()
    {
        _moveVector = _xVector + _yVector + _centerTransform.position - transform.forward * _distance;
        transform.position = Vector3.SmoothDamp(transform.position, _moveVector, ref _velocity, _smoothTime);
    }
}
