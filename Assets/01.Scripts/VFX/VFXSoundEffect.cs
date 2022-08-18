using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;


[RequireComponent(typeof(VisualEffect))]
public class VFXSoundEffect : VFXOutputEventAbstractHandler
{
	[HideInInspector] public override bool canExecuteInEditor => false;

    public AudioEFFType AudioEFFType;

    public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
    {
        SoundManager.Instance.PlayEFF(AudioEFFType);
    }
}

