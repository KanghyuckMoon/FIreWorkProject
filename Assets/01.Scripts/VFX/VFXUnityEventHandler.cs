using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;


[RequireComponent(typeof(VisualEffect))]
public class VFXUnityEventHandler : VFXOutputEventAbstractHandler
{
    [HideInInspector] public override bool canExecuteInEditor => false;

    [SerializeField] private UnityEvent _unityEvent;

    public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
    {
        _unityEvent?.Invoke();
    }
}

