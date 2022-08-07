using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance; //속성으로 가질 인스턴스, 전역으로 선언하여 전역적인 접근점을 제공한다
	public static T Instance //인스턴스를 반환하는 프로퍼티
	{
		get
		{
			//이미 만들어진 인스턴스가 없으면
			if (_instance == null)
			{
				//인스턴스가 있는지 탐색
				_instance = FindObjectOfType<T>();

				//그래도 없으면 새로 만든다
				if (_instance == null)
				{
					_instance = new GameObject(typeof(T).Name).AddComponent<T>();
				}
			}

			return _instance;
		}
	}

	public void Awake()
	{
		//만약 인스턴스가 없을시
		if (_instance == null)
		{
			//인스턴스는 해당 오브젝트가 된다.
			_instance = this as T;

			//그리고 해당 오브젝트는 삭제되지 않는다.
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			//이미 인스턴스가 존재할 경우 이 오브젝트는 삭제된다.
			Destroy(gameObject);
		}
	}

}
