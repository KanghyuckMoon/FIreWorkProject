using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
	[SerializeField] private CameraController _camera;

	private void Update()
	{
		Click();
	}

	private void Click()
	{
		if(Input.GetMouseButtonDown(0))
		{
			CheckObject();
		}

	}

	private void CheckObject()
	{
		Vector3 originPosition = _camera.transform.position;
		Vector3 direction = _camera.transform.forward;
		RaycastHit hit;

		if (Physics.Raycast(originPosition, direction, out hit))
		{
			if(hit.collider.gameObject.layer == 10)
			{
				_camera.SetTarget(hit.collider.transform);
			}
		}
	}

}
