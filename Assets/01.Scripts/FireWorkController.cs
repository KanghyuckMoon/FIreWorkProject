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

	public HappyMoneyManager HappyMoneyManager
	{
		get
		{
			_happyMoneyManager ??= FindObjectOfType<HappyMoneyManager>();
			return _happyMoneyManager;
		}
	}

	[SerializeField] private VisualEffect _visualEffect = null;
	[SerializeField] private float _rate = 5f;
	[SerializeField] private int _count = 0;
	[SerializeField] private int _further1 = 0;
	[SerializeField] private int _further2 = 0;
	[SerializeField] private int _further3 = 0;
	[SerializeField] private int _further4 = 0;
	[SerializeField] private Gradient _furtherColor1_1;
	[SerializeField] private Gradient _furtherColor1_2;
	[SerializeField] private Gradient _furtherColor1_3;
	[SerializeField] private Gradient _furtherColor2_1;
	[SerializeField] private Gradient _furtherColor2_2;
	[SerializeField] private Gradient _furtherColor2_3;
	[SerializeField] private Gradient _furtherColor3_1;
	[SerializeField] private Gradient _furtherColor3_2;
	[SerializeField] private Gradient _furtherColor3_3;
	[SerializeField] private Gradient _furtherColor4_1;
	[SerializeField] private Gradient _furtherColor4_2;
	[SerializeField] private Gradient _furtherColor4_3;
	[SerializeField] private Texture2D _furtherTexture1;
	[SerializeField] private Texture2D _furtherTexture2;
	[SerializeField] private Texture2D _furtherTexture3;
	[SerializeField] private Texture2D _furtherTexture4;
	[SerializeField] private ItemDataSO _itemDataSO;
	[SerializeField] private HappyMoneyManager _happyMoneyManager;

	private List<float> _explosiontime1 = new List<float>();
	private List<float> _explosiontime2 = new List<float>();
	private List<float> _explosiontime3 = new List<float>();
	private List<float> _explosiontime4 = new List<float>();
	public bool corotin;
	public bool saveDatasetting;

	private void Start()
	{
		if(saveDatasetting)
		{
			SaveDataSetting();
		}

		UpdateRate();
		StartCoroutine(FireworkStart());
	}

	private void Update()
	{
		Explosion();
	}

	/// <summary>
	/// 저장 데이터기반 설정
	/// </summary>
	public void SaveDataSetting()
	{
		var itemChanger = FindObjectOfType<ItemChangeManager>();

		//1차 설정
		var itemData1 = _itemDataSO.GetItemData(UserSaveDataManager.Instance.UserSaveData.further1ColorItemCode);
		var itemData2 = _itemDataSO.GetItemData(UserSaveDataManager.Instance.UserSaveData.further2ColorItemCode);
		var itemData3 = _itemDataSO.GetItemData(UserSaveDataManager.Instance.UserSaveData.further3ColorItemCode);
		var itemData4 = _itemDataSO.GetItemData(UserSaveDataManager.Instance.UserSaveData.further4ColorItemCode);
		var itemDataTexture1 = _itemDataSO.GetItemData(UserSaveDataManager.Instance.UserSaveData.further1TextureItemCode);
		var itemDataTexture2 = _itemDataSO.GetItemData(UserSaveDataManager.Instance.UserSaveData.further2TextureItemCode);
		var itemDataTexture3 = _itemDataSO.GetItemData(UserSaveDataManager.Instance.UserSaveData.further3TextureItemCode);
		var itemDataTexture4 = _itemDataSO.GetItemData(UserSaveDataManager.Instance.UserSaveData.further4TextureItemCode);

		itemChanger.ChangeItensity(UserSaveDataManager.Instance.UserSaveData.further1ColorLight);
		itemChanger.ChangeFurther(ItemChangeManager.CurrentSettingMode.Further1);
		itemChanger.ChangeFirework(itemData1);
		itemChanger.ChangeFirework(itemDataTexture1);
		itemChanger.ChangeSize(UserSaveDataManager.Instance.UserSaveData.further1Size);

		itemChanger.ChangeItensity(UserSaveDataManager.Instance.UserSaveData.further2ColorLight);
		itemChanger.ChangeFurther(ItemChangeManager.CurrentSettingMode.Further2);
		itemChanger.ChangeFirework(itemData2);
		itemChanger.ChangeFirework(itemDataTexture2);
		itemChanger.ChangeSize(UserSaveDataManager.Instance.UserSaveData.further2Size);

		itemChanger.ChangeItensity(UserSaveDataManager.Instance.UserSaveData.further3ColorLight);
		itemChanger.ChangeFurther(ItemChangeManager.CurrentSettingMode.Further3);
		itemChanger.ChangeFirework(itemData3);
		itemChanger.ChangeFirework(itemDataTexture3);
		itemChanger.ChangeSize(UserSaveDataManager.Instance.UserSaveData.further3Size);

		itemChanger.ChangeItensity(UserSaveDataManager.Instance.UserSaveData.further4ColorLight);
		itemChanger.ChangeFurther(ItemChangeManager.CurrentSettingMode.Further4);
		itemChanger.ChangeFirework(itemData4);
		itemChanger.ChangeFirework(itemDataTexture4);
		itemChanger.ChangeSize(UserSaveDataManager.Instance.UserSaveData.further4Size);

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
				float lifeTime = Random.Range(3f, 3.5f);
				float further1lifeTime = 0f;
				float further2lifeTime = 0f;
				float further3lifeTime = 0f;
				_visualEffect.SetFloat("lifeTime", lifeTime);
				_explosiontime1.Add(lifeTime);
				if(IsCanFurther2)
				{
					further1lifeTime = Random.Range(1f, 1.2f);
					_visualEffect.SetFloat("FurtherLifeTime1", further1lifeTime);
					_explosiontime1.Add(further1lifeTime + lifeTime);
				}
				if(IsCanFurther3)
				{
					further2lifeTime = Random.Range(0.8f, 1f);
					_visualEffect.SetFloat("FurtherLifeTime2", further2lifeTime);
					_explosiontime1.Add(further2lifeTime + further1lifeTime + lifeTime);
				}
				if (IsCanFurther4)
				{
					further3lifeTime = Random.Range(0.8f, 1f);
					_visualEffect.SetFloat("FurtherLifeTime3", further3lifeTime);
					_explosiontime1.Add(further3lifeTime + further2lifeTime + further1lifeTime + lifeTime);
				}

				_visualEffect.SendEvent("Play");


				yield return new WaitForSeconds(0.1f);
			}

			yield return new WaitForSeconds(_rate);
		}
	}

	public void Explosion()
	{
		TimeCheckList(_explosiontime1, 1);
		TimeCheckList(_explosiontime2, 2);
		TimeCheckList(_explosiontime3, 3);
		TimeCheckList(_explosiontime4, 4);
	}

	private void TimeCheckList(List<float> explosionList, int further = 1)
	{
		for (int i = 0; i < explosionList.Count; ++i)
		{
			explosionList[i] -= Time.deltaTime;
			if (explosionList[i] <= 0f)
			{
				_visualEffect.SendEvent("Explosion");
				switch (further)
				{
					case 1:
						_happyMoneyManager.AddHappy(_further1);
						break;
					case 2:
						_happyMoneyManager.AddHappy(_further2);
						break;
					case 3:
						_happyMoneyManager.AddHappy(_further3);
						break;
					case 4:
						_happyMoneyManager.AddHappy(_further4);
						break;
				}
				explosionList.RemoveAt(i--);
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
		VFXSetGradient(_visualEffect, "FurtherColor1_1", _furtherColor1_1);
		VFXSetGradient(_visualEffect, "FurtherColor1_2", _furtherColor1_2);
		VFXSetGradient(_visualEffect, "FurtherColor1_3", _furtherColor1_3);
	}

	/// <summary>
	/// 추가폭발2 색상 지정
	/// </summary>
	public void UpdateFurtherColor2()
	{
		VFXSetGradient(_visualEffect, "FurtherColor2_1", _furtherColor2_1);
		VFXSetGradient(_visualEffect, "FurtherColor2_2", _furtherColor2_2);
		VFXSetGradient(_visualEffect, "FurtherColor2_3", _furtherColor2_3);
	}

	/// <summary>
	/// 추가폭발3 색상 지정
	/// </summary>
	public void UpdateFurtherColor3()
	{
		VFXSetGradient(_visualEffect, "FurtherColor3_1", _furtherColor3_1);
		VFXSetGradient(_visualEffect, "FurtherColor3_2", _furtherColor3_2);
		VFXSetGradient(_visualEffect, "FurtherColor3_3", _furtherColor3_3);
	}

	/// <summary>
	/// 추가폭발4 색상 지정
	/// </summary>
	public void UpdateFurtherColor4()
	{
		VFXSetGradient(_visualEffect, "FurtherColor4_1", _furtherColor4_1);
		VFXSetGradient(_visualEffect, "FurtherColor4_2", _furtherColor4_2);
		VFXSetGradient(_visualEffect, "FurtherColor4_3", _furtherColor4_3);
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
	public void ChangeFurtherColor1(Gradient gradient1, Gradient gradient2, Gradient gradient3)
	{
		_furtherColor1_1 = gradient1;
		_furtherColor1_2 = gradient2;
		_furtherColor1_3 = gradient3;
		UpdateFurtherColor1();
	}
	/// <summary>
	/// 추가폭발 색상 2변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherColor2(Gradient gradient1, Gradient gradient2, Gradient gradient3)
	{
		_furtherColor2_1 = gradient1;
		_furtherColor2_2 = gradient2;
		_furtherColor2_3 = gradient3;
		UpdateFurtherColor2();
	}
	/// <summary>
	/// 추가폭발 색상 3변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherColor3(Gradient gradient1, Gradient gradient2, Gradient gradient3)
	{
		_furtherColor3_1 = gradient1;
		_furtherColor3_2 = gradient2;
		_furtherColor3_3 = gradient3;
		UpdateFurtherColor3();
	}
	/// <summary>
	/// 추가폭발 색상 4변경
	/// </summary>
	/// <param name="gradient"></param>
	public void ChangeFurtherColor4(Gradient gradient1, Gradient gradient2, Gradient gradient3)
	{
		_furtherColor4_1 = gradient1;
		_furtherColor4_2 = gradient2;
		_furtherColor4_3 = gradient3;
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

	/// <summary>
	/// 불꽃놀이1 크기 조절
	/// </summary>
	/// <param name="value"></param>
	public void ChangeSizeFurther1(float value)
	{
		VFXSetFloat(_visualEffect, "FurtherSize1", value);
	}
	/// <summary>
	/// 불꽃놀이2 크기 조절
	/// </summary>
	/// <param name="value"></param>
	public void ChangeSizeFurther2(float value)
	{
		VFXSetFloat(_visualEffect, "FurtherSize2", value);
	}
	/// <summary>
	/// 불꽃놀이3 크기 조절
	/// </summary>
	/// <param name="value"></param>
	public void ChangeSizeFurther3(float value)
	{
		VFXSetFloat(_visualEffect, "FurtherSize3", value);
	}
	/// <summary>
	/// 불꽃놀이4 크기 조절
	/// </summary>
	/// <param name="value"></param>
	public void ChangeSizeFurther4(float value)
	{
		VFXSetFloat(_visualEffect, "FurtherSize4", value);
	}
}
