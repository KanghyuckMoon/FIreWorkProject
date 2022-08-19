using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Utill
{
    public static class VFX
	{
		/// <summary>
		/// VFX 프로퍼티 설정
		/// </summary>
		public static void VFXSetFloat(VisualEffect visualEffect, string name, float value)
		{
			visualEffect.SetFloat(name, value);
		}

		/// <summary>
		/// VFX 프로퍼티 설정
		/// </summary>
		public static void VFXSetInt(VisualEffect visualEffect, string name, int value)
		{
			visualEffect.SetInt(name, value);
		}

		/// <summary>
		/// VFX 프로퍼티 설정
		/// </summary>
		public static void VFXSetGradient(VisualEffect visualEffect, string name, Gradient value)
		{
			visualEffect.SetGradient(name, value);
		}
	}

}