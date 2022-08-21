using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFireSound : MonoBehaviour
{
	/// <summary>
	/// �������� ���� �Ҹ��� �����
	/// </summary>
	public void PlayRandomFireSound()
	{
		int random = Random.Range((int)AudioEFFType.Fire1, (int)AudioEFFType.Fire4);

		SoundManager.Instance.PlayEFF((AudioEFFType)random, 2f);
	}
}
