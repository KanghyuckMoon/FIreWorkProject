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
	/// 주기 설정
	/// </summary>
	public void UpdateRate()
	{
		_visualEffect.SetFloat("Rate", _rate);
	}

	/// <summary>
	/// 주기 증가
	/// </summary>
	/// <param name="add"></param>
	public void UpdateRate(float add)
	{
		_rate += add;
		UpdateRate();
	}

}
