using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;


[RequireComponent(typeof(VisualEffect))]
public class VFXSoundEffect : VFXOutputEventAbstractHandler
{
    public override bool canExecuteInEditor => true;
    public VisualEffect effect;
    public string vfxname;

    public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
    {
        var info = effect.GetSpawnSystemInfo(vfxname);
        Invoke("Explosion", info.loopDuration);
        SoundManager.Instance.PlayEFF(AudioEFFType.Shot);
    }

    private void Explosion()
    {
        SoundManager.Instance.PlayEFF(AudioEFFType.Fire);
    }
}

