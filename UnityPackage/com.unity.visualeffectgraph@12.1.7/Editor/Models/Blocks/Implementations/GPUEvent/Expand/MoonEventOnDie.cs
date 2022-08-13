using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.VFX.Block
{
    [VFXInfo(category = "MoonEvent", experimental = true)]
    class MoonEventOnDie : VFXBlock
    {
        [VFXSetting, SerializeField, Delayed]
        protected string eventName = "On Received Event";
        public override string name { get { return "Moon Event On Die"; } }
        public override VFXContextType compatibleContexts { get { return VFXContextType.Update; } }
        public override VFXDataType compatibleData { get { return VFXDataType.Particle; } }

        public override IEnumerable<VFXAttributeInfo> attributes
        {
            get
            {
                yield return new VFXAttributeInfo(VFXAttribute.Lifetime, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.Age, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.Alive, VFXAttributeMode.Read);
                yield return new VFXAttributeInfo(VFXAttribute.EventCount, VFXAttributeMode.Write);
            }
        }
        public override IEnumerable<VFXNamedExpression> parameters
        {
            get
            {
                foreach (var param in base.parameters)
                    yield return param;
                yield return new VFXNamedExpression(VFXBuiltInExpression.DeltaTime, "deltaTime");
            }
        }


        public class InputProperties
        {
            [Tooltip("문 카운트입니다.")]
            public uint count = 1u;
        }

        public class OutputProperties
        {
            [Tooltip("문 이벤트입니다")]
            public MoonEvent evt = new MoonEvent();
        }

        public override string source
        {
            get
            {
                
                string outSource = ""; 
                outSource += $@"eventCount = (age + deltaTime > lifetime || !alive) ? count : 0;
                //UnityEngine.Debug.Log(""asd"");";
                return outSource;
            }
        }
    }
}
