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
	/// 관찰대상 추가
	/// </summary>
	public void AddObservation();
	/// <summary>
	/// 관찰대상으로부터 메세지 받기
	/// </summary>
	public void ReceiveMessage();
}
