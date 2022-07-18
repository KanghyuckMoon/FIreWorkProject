using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireWorkController : MonoBehaviour
{
	[SerializeField]
	private VisualEffect _visualEffect = null;

	private void Start()
	{
		_visualEffect.SetFloat("Rate", 5f);
	}

}
