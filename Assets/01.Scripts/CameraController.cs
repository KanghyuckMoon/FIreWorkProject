using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _centerTransform = null;
    [SerializeField]
    private float _speed;
    private Vector2 m_Input;

    // Update is called once per frame
    void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if(Input.GetMouseButton(2))
        {

            float xMove = Input.GetAxis("Mouse X");
            float yMove = Input.GetAxis("Mouse Y");
            transform.position += new Vector3(0, yMove, 0) * Time.deltaTime;
            transform.position += transform.right * xMove * Time.deltaTime;
            transform.LookAt(_centerTransform);
        }
    }
}
