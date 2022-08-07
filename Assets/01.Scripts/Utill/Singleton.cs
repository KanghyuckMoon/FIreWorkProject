using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance; //�Ӽ����� ���� �ν��Ͻ�, �������� �����Ͽ� �������� �������� �����Ѵ�
	public static T Instance //�ν��Ͻ��� ��ȯ�ϴ� ������Ƽ
	{
		get
		{
			//�̹� ������� �ν��Ͻ��� ������
			if (_instance == null)
			{
				//�ν��Ͻ��� �ִ��� Ž��
				_instance = FindObjectOfType<T>();

				//�׷��� ������ ���� �����
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
		//���� �ν��Ͻ��� ������
		if (_instance == null)
		{
			//�ν��Ͻ��� �ش� ������Ʈ�� �ȴ�.
			_instance = this as T;

			//�׸��� �ش� ������Ʈ�� �������� �ʴ´�.
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			//�̹� �ν��Ͻ��� ������ ��� �� ������Ʈ�� �����ȴ�.
			Destroy(gameObject);
		}
	}

}
