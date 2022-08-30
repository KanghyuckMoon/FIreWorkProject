using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    public LayerMask _layerMask;

    void Start()
    {
        
    }

    void Update()
    {
        IsWall();
    }

    public void IsWall()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 1f, _layerMask);

        //cols.t
    }
}
