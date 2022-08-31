using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _centerTransform = null;
    [SerializeField] private float _moveSpeed = 1;
    [Range(0.1f, 400f)] [SerializeField] private float _zoomSpeed = 1;


    [Range(0.1f, 200f)] [SerializeField] private float limite = 160;

    [SerializeField] private float _viewSpeed = 200;
    [SerializeField] private float _distance = 3;
    [SerializeField] private float _smoothTime;
    [SerializeField] private Vector3 _velocity;
    float _yRotationInput = 0f;
    float _xRotationInput = 0f;

    Vector3 inputVal = Vector3.zero;
    Vector2 viewVal = Vector3.forward;

    float _yMoveInput = 0f;
    float _xMoveInput = 0f;
    Vector3 _moveVector = Vector3.zero;
    Vector3 _upVector = Vector3.zero;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        HardHit();
        //ZoomInOut();
        MoveMouseCamera();
        //MoveKeyboardCamera();
        CameraPositionSetting();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();
        inputVal = input.normalized;
    }

    public void OnView(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        viewVal = input.normalized;


        _xRotationInput += viewVal.x * _viewSpeed * Time.deltaTime;
        _yRotationInput += viewVal.y * _viewSpeed * Time.deltaTime;
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

 //   private void ZoomInOut()
	//{
 //       _distance = Input.GetAxis("Mouse ScrollWheel");
 //   }

    private void HardHit()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void CameraPositionSetting()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-_yRotationInput, _xRotationInput, 0), _smoothTime);
        Vector3 vec = transform.position;
        
        
        _moveVector = transform.position + (transform.right * inputVal.x * _moveSpeed * Time.deltaTime) + (transform.forward * inputVal.z * (_moveSpeed + _zoomSpeed) * Time.deltaTime);// + (transform.forward * _distance * _zoomSpeed * Time.deltaTime); //transform.right * (_xMoveInput * _moveSpeed * Time.deltaTime) + transform.up * (_yMoveInput * _moveSpeed * Time.deltaTime) + (transform.forward* _distance *_zoomSpeed * Time.deltaTime);
        
        transform.position = Vector3.SmoothDamp(transform.position, _moveVector, ref _velocity, _smoothTime);

        _upVector = transform.position + transform.InverseTransformPoint(transform.position + (transform.up * inputVal.y * _moveSpeed * Time.deltaTime));
        transform.position = Vector3.SmoothDamp(transform.position, _upVector, ref _velocity, _smoothTime);

        if (Mathf.Abs(transform.position.x) >= limite || Mathf.Abs(transform.position.y) >= limite || Mathf.Abs(transform.position.z) >= limite)
        {
            transform.position = vec;
        }
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
