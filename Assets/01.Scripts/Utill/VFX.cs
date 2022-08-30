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
		public static void VFXSetVector3(VisualEffect visualEffect, string name, Vector3 vector3)
		{
			visualEffect.SetVector3(name, vector3);
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

		/// <summary>
		/// VFX 프로퍼티 설정
		/// </summary>
		public static void VFXSetTexture(VisualEffect visualEffect, string name, Texture value)
		{
			visualEffect.SetTexture(name, value);
		}
	}

}