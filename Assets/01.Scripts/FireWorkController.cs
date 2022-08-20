using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static Utill.VFX;

public class FireWorkController : MonoBehaviour
{

	public float Rate => _rate;
	public int Count => _count;
	public int Further1 => _further1;
	public int Further2 => _further2;
	public int Further3 => _further3;
	public int Further4 => _further4;
	public bool IsCanFurther1
	{
		get
		{
			return true;
		}
	}
	public bool IsCanFurther2
	{
		get
		{
			return _further1 > 10;
		}
	}
	public bool IsCanFurther3
	{
		get
		{
			return _further2 > 10;
		}
	}
	public bool IsCanFurther4
	{
		get
		{
			return _further3 > 10;
		}
	}

	[SerializeField] private VisualEffect _visualEffect = null;
	[SerializeField] private float _rate = 5f;
	[SerializeField] private int _count = 0;
	[SerializeField] private int _further1 = 0;
	[SerializeField] private int _further2 = 0;
	[SerializeField] private int _further3 = 0;
	[SerializeField] private int _further4 = 0;
	[SerializeField] private Gradient _furtherColor1;
	[SerializeField] private Gradient _furtherColor2;
	[SerializeField] private Gradient _furtherColor3;
	[SerializeField] private Gradient _furtherColor4;
	[SerializeField] private Texture2D _furtherTexture1;
	[SerializeField] private Texture2D _furtherTexture2;
	[SerializeField] private Texture2D _furtherTexture3;
	[SerializeField] private Texture2D _furtherTexture4;


	private List<float> _explosiontime = new List<float>();
	public bool corotin;


	private void Start()
	{
		UpdateRate();
		StartCoroutine(FireworkStart());
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
				_explosiontime.Add(lifeTime);
				_visualEffect.SendEvent("Play");
				yield return new WaitForSeconds(0.1f);
			}

			yield return new WaitForSeconds(_rate);
		}
	}

	public void Explosion()
	{
		for(int i = 0; i < _explosiontime.Count; ++i)
		{
			_explosiontime[i] -= Time.deltaTime;
			if(_explosiontime[i] <= 0f)
			{
				_visualEffect.SendEvent("Explosion");
				_explosiontime.RemoveAt(i--);
			}
		}
	}

	/// <summary>
	/// 주기 설정
	/// </summary>
	public void UpdateRate()
	{
		VFXSetFloat(_visualEffect, "Rate", _rate);
	}

	/// <summary>
	/// 추가폭발1 설정
	/// </summary>
	public void UpdateFurtherCount1()
	{
		VFXSetInt(_visualEffect, "FurtherCount1", _further1);
	}

	/// <summary>
	/// 추가폭발2 설정
	/// </summary>
	public void UpdateFurtherCount2()
	{
		VFXSetInt(_visualEffect, "FurtherCount2", _further2);
	}

	/// <summary>
	/// 추가폭발3 설정
	/// </summary>
	public void UpdateFurtherCount3()
	{
		VFXSetInt(_visualEffect, "FurtherCount3", _further3);
	}

	/// <summary>
	/// 추가폭발4 설정
	/// </summary>
	public void UpdateFurtherCount4()
	{
		VFXSetInt(_visualEffect, "FurtherCount4", _further4);
	}

	/// <summary>
	/// 추가폭발1 색상 지정
	/// </summary>
	public void UpdateFurtherColor1()
	{
		VFXSetGradient(_visualEffect, "FurtherColor1", _furtherColor1);
	}

	/// <summary>
	/// 추가폭발2 색상 지정
	/// </summary>
	public void UpdateFurtherColor2()
	{
		VFXSetGradient(_visualEffect, "FurtherColor2", _furtherColor2);
	}

	/// <summary>
	/// 추가폭발3 색상 지정
	/// </summary>
	public void UpdateFurtherColor3()
	{
		VFXSetGradient(_visualEffect, "FurtherColor3", _furtherColor3);
	}

	/// <summary>
	/// 추가폭발4 색상 지정
	/// </summary>
	public void UpdateFurtherColor4()
	{
		VFXSetGradient(_visualEffect, "FurtherColor4", _furtherColor4);
	}

	/// <summary>
	/// 추가폭발1 색상 지정
	/// </summary>
	public void UpdateFurtherTexture1()
	{
		VFXSetTexture(_visualEffect, "FurtherTexture1", _furtherTexture1);
	}

	/// <summary>
	/// 추가폭발2 색상 지정
	/// </summary>
	public void UpdateFurtherTexture2()
	{
		VFXSetTexture(_visualEffect, "FurtherTexture2", _furtherTexture2);
	}

	/// <summary>
	/// 추가폭발3 색상 지정
	/// </summary>
	public void UpdateFurtherTexture3()
	{
		VFXSetTexture(_visualEffect, "FurtherTexture3", _furtherTexture3);
	}

	/// <summary>
	/// 추가폭발4 색상 지정
	/// </summary>
	public void UpdateFurtherTexture4()
	{
		VFXSetTexture(_visualEffect, "FurtherTexture4", _furtherTexture4);
	}

	/// <summary>
	/// 주기 증가
	/// </summary>
	/// <param name="add"></param>
	public void UpdateRate(float add)
	{
		_rate -= add;
		if(_rate < 0.3f)
		{
			_rate = 0.3f;
			Debug.Log("Rete is MIN, Can't more Upgrade");
		}

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
	/// 갯수 설정
	/// </summary>
	public void UpdateCount(int add)
	{
		_count += add;
	}

	/// <summary>
	/// 추가폭발 색상 1변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherColor1(Gradient gradient)
	{
		_furtherColor1 = gradient;
		UpdateFurtherColor1();
	}
	/// <summary>
	/// 추가폭발 색상 2변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherColor2(Gradient gradient)
	{
		_furtherColor2 = gradient;
		UpdateFurtherColor2();
	}
	/// <summary>
	/// 추가폭발 색상 3변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherColor3(Gradient gradient)
	{
		_furtherColor3 = gradient;
		UpdateFurtherColor3();
	}
	/// <summary>
	/// 추가폭발 색상 4변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherColor4(Gradient gradient)
	{
		_furtherColor4 = gradient;
		UpdateFurtherColor4();
	}

	/// <summary>
	/// 추가폭발 색상 1변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherTexture1(Texture2D texture)
	{
		_furtherTexture1 = texture;
		UpdateFurtherTexture1();
	}
	/// <summary>
	/// 추가폭발 색상 2변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherTexture2(Texture2D texture)
	{
		_furtherTexture2 = texture;
		UpdateFurtherTexture2();
	}
	/// <summary>
	/// 추가폭발 색상 3변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherTexture3(Texture2D texture)
	{
		_furtherTexture3 = texture;
		UpdateFurtherTexture3();
	}
	/// <summary>
	/// 추가폭발 색상 4변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherTexture4(Texture2D texture)
	{
		_furtherTexture4 = texture;
		UpdateFurtherTexture4();
	}

	[ContextMenu("RefreshUpdateFurtherColor")]
	/// <summary>
	/// 현재 색상 새로고침
	/// </summary>
	public void RefreshUpdateFurtherColor()
	{
		UpdateFurtherColor1();
		UpdateFurtherColor2();
		UpdateFurtherColor3();
		UpdateFurtherColor4();
	}
	/// <summary>
	/// 현재 텍스쳐 새로고침
	/// </summary>
	public void RefreshUpdateFurtherTexture()
	{
		UpdateFurtherTexture1();
		UpdateFurtherTexture2();
		UpdateFurtherTexture3();
		UpdateFurtherTexture4();
	}
}
