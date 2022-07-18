using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireWorkController : MonoBehaviour
{
	[SerializeField]
	private VisualEffect _visualEffect = null;
	[SerializeField]
	private float _rate = 0f;


	private void Start()
	{
		UpdateRate();
	}


	/// <summary>
	/// �ֱ� ����
	/// </summary>
	public void UpdateRate()
	{
		_visualEffect.SetFloat("Rate", _rate);
	}

	/// <summary>
	/// �ֱ� ����
	/// </summary>
	/// <param name="add"></param>
	public void UpdateRate(float add)
	{
		_rate += add;
		UpdateRate();
	}

}
