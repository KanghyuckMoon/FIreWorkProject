using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObservationObject
{
	public List<Observer> Obsevers
	{
		get;
		set;
	}

	public void AddObserver(Observer observer);
	public void SendMessageToObsevers();
}
