using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
	public ObservationObject ObservationObject
	{
		get;
		set;
	}
	/// <summary>
	/// ������� �߰�
	/// </summary>
	public void AddObservation();
	/// <summary>
	/// ����������κ��� �޼��� �ޱ�
	/// </summary>
	public void ReceiveMessage();
}
