#if VFX_OUTPUTEVENT_AUDIO
using UnityEngine.Events;

namespace UnityEngine.VFX.Utility
{
    [ExecuteAlways]
    [RequireComponent(typeof(VisualEffect))]
    class VFXOutputEventPlayAudio : VFXOutputEventAbstractHandler
    {
        new public ExposedProperty outputEvent = "On Received Event";
        public override bool canExecuteInEditor => true;

        public AudioSource audioSource;

        public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
        {
            if (audioSource != null)
			{
                audioSource.Play();
			}
        }
    }
}
#endif
