using System;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

namespace UnityEditor.VFX
{
    [VFXType(VFXTypeAttribute.Usage.ExcludeFromProperty)]
    struct MoonEvent
    {
        /* expected emptiness */
    };

    [VFXInfo(experimental = true)]
    class VFXOutputEventMoonEvent : VFXContext
    {
        [VFXSetting, SerializeField, Delayed]
        protected string eventName = "On Received Event";
        protected override int outputFlowCount => 0;

        public VFXOutputEventMoonEvent() : base(VFXContextType.OutputEvent, VFXDataType.None, VFXDataType.OutputEvent) { }
        public VFXOutputEventMoonEvent(VFXContextType ThisDataType, VFXDataType InputDataType, VFXDataType OutDataType) : base(ThisDataType, InputDataType, OutDataType) { }
        public override string name { get { return "MoonEvent"; } }

        public class InputProperties
        {
            public MoonEvent evt = new MoonEvent();
        }

        public override VFXExpressionMapper GetExpressionMapper(VFXDeviceTarget target)
        {
            return new VFXExpressionMapper();
        }

        public override bool CanBeCompiled()
        {
            var anyInputContextPlugged = inputContexts.Any();
            return anyInputContextPlugged;
        }

        public void adc()
		{
            VisualEffect vfx = null;
            vfx.SendEvent(eventName);
		}
    }
}
