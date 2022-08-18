using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class VFXTestEvent : VFXEventBinderBase
{
	protected override void SetEventAttribute(object[] parameters = null)
	{

	}

	public void Update()
	{
		SendEventToVisualEffect();
	}

}
