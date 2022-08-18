using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireWorkController : MonoBehaviour
{

	public float Rate => _rate;
	public int Count => _count;
	public int Further1 => _further1;
	public int Further2 => _further2;
	public int Further3 => _further3;
	public int Further4 => _further4;
	public int Further5 => _further5;

	[SerializeField] private VisualEffect _visualEffect = null;
	[SerializeField] private float _rate = 0f;
	[SerializeField] private int _count = 0;
	[SerializeField] private int _further1 = 0;
	[SerializeField] private int _further2 = 0;
	[SerializeField] private int _further3 = 0;
	[SerializeField] private int _further4 = 0;
	[SerializeField] private int _further5 = 0;
	private List<float> explosiontime = new List<float>();
	public bool corotin;


	private void Start()
	{
		UpdateRate();
		if (corotin)
		{ 
			StartCoroutine(FireworkStart());
		
		}

	}

	private void Update()
	{
		Explosion();
	}

	[ContextMenu("Play")]
	public void Play()
	{
		_visualEffect.SendEvent("Play");
	}

	[ContextMenu("Stop")]
	public void Stop()
	{
		_visualEffect.SendEvent("Stop");
	}

	public IEnumerator FireworkStart()
	{
		while(true)
		{
			for(int i = 0; i < _count; ++i)
			{
				float lifeTime = Random.Range(3f, 4f);
				_visualEffect.SetFloat("lifeTime", lifeTime);
				explosiontime.Add(lifeTime);
				_visualEffect.SendEvent("Play");
				yield return new WaitForSeconds(0.1f);
			}

			yield return new WaitForSeconds(_rate);
		}
	}

	public void Explosion()
	{
		for(int i = 0; i < explosiontime.Count; ++i)
		{
			explosiontime[i] -= Time.deltaTime;
			if(explosiontime[i] <= 0f)
			{
				_visualEffect.SendEvent("Explosion");
				explosiontime.RemoveAt(i--);
			}
		}
	}

	/// <summary>
	/// VFX 프로퍼티 설정
	/// </summary>
	public void VFXSetFloat(string name, float value)
	{
		_visualEffect.SetFloat(name, value);
	}

	/// <summary>
	/// VFX 프로퍼티 설정
	/// </summary>
	public void VFXSetInt(string name, int value)
	{
		_visualEffect.SetInt(name, value);
	}

	/// <summary>
	/// 주기 설정
	/// </summary>
	public void UpdateRate()
	{
		VFXSetFloat("Rate", _rate);
	}

	/// <summary>
	/// 추가폭발1 설정
	/// </summary>
	public void UpdateFurtherCount1()
	{
		VFXSetInt("FurtherCount1", _further1);
	}

	/// <summary>
	/// 추가폭발2 설정
	/// </summary>
	public void UpdateFurtherCount2()
	{
		VFXSetInt("FurtherCount2", _further2);
	}

	/// <summary>
	/// 추가폭발3 설정
	/// </summary>
	public void UpdateFurtherCount3()
	{
		VFXSetInt("FurtherCount3", _further3);
	}

	/// <summary>
	/// 추가폭발4 설정
	/// </summary>
	public void UpdateFurtherCount4()
	{
		VFXSetInt("FurtherCount4", _further4);
	}


	/// <summary>
	/// 추가폭발5 설정
	/// </summary>
	public void UpdateFurtherCount5()
	{
		VFXSetInt("FurtherCount5", _further5);
	}

	/// <summary>
	/// 갯수 설정
	/// </summary>
	public void UpdateCount()
	{
		VFXSetInt("SpawnCount", _count);
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



	/// <summary>
	/// 추가폭발1 설정
	/// </summary>
	public void UpdateFurtherCount1(int add)
	{
		_further1 += add;
		UpdateFurtherCount1();
	}

	/// <summary>
	/// 추가폭발2 설정
	/// </summary>
	public void UpdateFurtherCount2(int add)
	{
		_further2 += add;
		UpdateFurtherCount2();
	}

	/// <summary>
	/// 추가폭발3 설정
	/// </summary>
	public void UpdateFurtherCount3(int add)
	{
		_further3 += add;
		UpdateFurtherCount3();
	}

	/// <summary>
	/// 추가폭발4 설정
	/// </summary>
	public void UpdateFurtherCount4(int add)
	{
		_further4 += add;
		UpdateFurtherCount4();
	}


	/// <summary>
	/// 추가폭발5 설정
	/// </summary>
	public void UpdateFurtherCount5(int add)
	{
		_further5 += add;
		UpdateFurtherCount5();
	}

	/// <summary>
	/// 갯수 설정
	/// </summary>
	public void UpdateCount(int add)
	{
		_count += add;
		UpdateCount();
	}

}
