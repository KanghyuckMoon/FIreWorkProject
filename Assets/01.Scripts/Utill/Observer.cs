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
	public void AddObserver();
}
