using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireWorkController : MonoBehaviour
{

	[SerializeField] private VisualEffect _visualEffect = null;
	[SerializeField] private float _rate = 0f;
	[SerializeField] private int _count = 0;
	[SerializeField] private int _further1 = 0;
	[SerializeField] private int _further2 = 0;
	[SerializeField] private int _further3 = 0;
	[SerializeField] private int _further4 = 0;
	[SerializeField] private int _further5 = 0;
	private List<string> events = new List<string> { "", "" };


	private void Start()
	{
		UpdateRate();
	}

	/// <summary>
	/// VFX ������Ƽ ����
	/// </summary>
	public void VFXSetFloat(string name, float value)
	{
		_visualEffect.SetFloat(name, value);
	}

	/// <summary>
	/// VFX ������Ƽ ����
	/// </summary>
	public void VFXSetInt(string name, int value)
	{
		_visualEffect.SetInt(name, value);
	}

	/// <summary>
	/// �ֱ� ����
	/// </summary>
	public void UpdateRate()
	{
		VFXSetFloat("Rate", _rate);
	}

	/// <summary>
	/// �߰�����1 ����
	/// </summary>
	public void UpdateFurtherCount1()
	{
		VFXSetInt("FurtherCount1", _further1);
	}

	/// <summary>
	/// �߰�����2 ����
	/// </summary>
	public void UpdateFurtherCount2()
	{
		VFXSetInt("FurtherCount2", _further2);
	}

	/// <summary>
	/// �߰�����3 ����
	/// </summary>
	public void UpdateFurtherCount3()
	{
		VFXSetInt("FurtherCount3", _further3);
	}

	/// <summary>
	/// �߰�����4 ����
	/// </summary>
	public void UpdateFurtherCount4()
	{
		VFXSetInt("FurtherCount4", _further4);
	}


	/// <summary>
	/// �߰�����5 ����
	/// </summary>
	public void UpdateFurtherCount5()
	{
		VFXSetInt("FurtherCount5", _further5);
	}

	/// <summary>
	/// ���� ����
	/// </summary>
	public void UpdateCount()
	{
		VFXSetInt("SpawnCount", _count);
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



	/// <summary>
	/// �߰�����1 ����
	/// </summary>
	public void UpdateFurtherCount1(int add)
	{
		_further1 += add;
		UpdateFurtherCount1();
	}

	/// <summary>
	/// �߰�����2 ����
	/// </summary>
	public void UpdateFurtherCount2(int add)
	{
		_further2 += add;
		UpdateFurtherCount2();
	}

	/// <summary>
	/// �߰�����3 ����
	/// </summary>
	public void UpdateFurtherCount3(int add)
	{
		_further3 += add;
		UpdateFurtherCount3();
	}

	/// <summary>
	/// �߰�����4 ����
	/// </summary>
	public void UpdateFurtherCount4(int add)
	{
		_further4 += add;
		UpdateFurtherCount4();
	}


	/// <summary>
	/// �߰�����5 ����
	/// </summary>
	public void UpdateFurtherCount5(int add)
	{
		_further5 += add;
		UpdateFurtherCount5();
	}

	/// <summary>
	/// ���� ����
	/// </summary>
	public void UpdateCount(int add)
	{
		_count += add;
		UpdateCount();
	}

}
