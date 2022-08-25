using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _centerTransform = null;
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _zoomSpeed = 1;
    [SerializeField] private float _distance = 3;
    [SerializeField] private float _smoothTime;
    [SerializeField] private Vector3 _velocity;
    float _yRotationInput = 0f;
    float _xRotationInput = 0f;

    Vector3 inputVal = Vector3.zero;

    float _yMoveInput = 0f;
    float _xMoveInput = 0f;
    Vector3 _moveVector = Vector3.zero;

    // Update is called once per frame
    void LateUpdate()
    {
        ZoomInOut();
        MoveMouseCamera();
        //MoveKeyboardCamera();
        CameraPositionSetting();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();
        inputVal = input;
    }

    private void MoveMouseCamera()
    {
        if(Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            float _xMove = Input.GetAxis("Mouse X");
            float _yMove = Input.GetAxis("Mouse Y");
            _yRotationInput += _yMove * _moveSpeed * Time.deltaTime;
            _xRotationInput += _xMove * _moveSpeed * Time.deltaTime;

        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    //private void MoveKeyboardCamera()
    //{
    //    _xMoveInput = Input.GetAxis("Horizontal");
    //    _yMoveInput = Input.GetAxis("Vertical");
    //}

    private void ZoomInOut()
	{
        _distance = Input.GetAxis("Mouse ScrollWheel");
    }

    private void CameraPositionSetting()
    {
        _moveVector = transform.position + (inputVal * _moveSpeed * Time.deltaTime); //transform.right * (_xMoveInput * _moveSpeed * Time.deltaTime) + transform.up * (_yMoveInput * _moveSpeed * Time.deltaTime) + (transform.forward* _distance *_zoomSpeed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, _moveVector, ref _velocity, _smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-_yRotationInput, _xRotationInput, 0), _smoothTime);;
    }

    /// <summary>
    /// Å¸°Ù ¼³Á¤
    /// </summary>
    /// <param name="targetTransform"></param>
    public void SetTarget(Transform targetTransform)
	{
        _xMoveInput = 0;
        _yMoveInput = 0;
        transform.position = targetTransform.position + -transform.forward * 9;

    }
}
