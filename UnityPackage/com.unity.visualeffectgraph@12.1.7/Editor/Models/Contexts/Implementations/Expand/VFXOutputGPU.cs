using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

namespace UnityEditor.VFX
{

    [VFXInfo(experimental = true)]
    class VFXOutputGPU : VFXContext
    {
        [VFXSetting, SerializeField, Delayed]
        protected string eventName = "On Received Event";
        public VFXOutputGPU() : base(VFXContextType.OutputEvent, VFXDataType.SpawnEvent, VFXDataType.OutputEvent) { }
        public override string name { get { return "VFXOutputGPU"; } }

        public class InputProperties
        {
            public GPUEvent evt = new GPUEvent();
        }

        public override VFXExpressionMapper GetExpressionMapper(VFXDeviceTarget target)
        {
            return new VFXExpressionMapper();
        }

        public override bool CanBeCompiled()
        {
            return outputContexts.Any(c => c.CanBeCompiled());
        }
        private static void CollectParentsContextRecursively(VFXContext start, HashSet<VFXContext> parents)
        {
            if (parents.Contains(start))
                return;
            parents.Add(start);
            foreach (var parent in start.inputContexts)
                CollectParentsContextRecursively(parent, parents);
        }

        public override IEnumerable<VFXAttributeInfo> attributes
        {
            get
            {
                var parents = new HashSet<VFXContext>();
                CollectParentsContextRecursively(this, parents);
                Debug.Log("asd");

                //Detect all attribute used in source spawner & consider as read source from them
                //This can be done using VFXDataSpawner after read attribute from spawn feature merge (require to be sure that the order of compilation is respected)
                foreach (var block in parents.SelectMany(o => o.children).OfType<VFX.Block.VFXSpawnerSetAttribute>())
                {
                    var attributeName = block.GetSetting("attribute");
                    yield return new VFXAttributeInfo(VFXAttribute.Find((string)attributeName.value), VFXAttributeMode.ReadSource);
                }
            }
        }
    }
}
