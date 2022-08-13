using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;
using UnityEditor.VFX.UI;

namespace UnityEditor.VFX
{
    [VFXInfo]
    class VFXOutputEventEX : VFXContext
    {
        [VFXSetting, SerializeField, Delayed]
        protected string eventName = "On Received Event";
        public VFXOutputEventEX() : base(VFXContextType.Init, VFXDataType.SpawnEvent, VFXDataType.OutputEvent) { }
        public override string name { get { return "OutputEventEX "; } }
        public override VFXTaskType taskType { get { return VFXTaskType.Initialize; } }
        public override VFXDataType outputType { get { return GetData() == null ? VFXDataType.Particle : GetData().type; } }

        private bool hasGPUSpawner => inputContexts.Any(o => o.contextType == VFXContextType.SpawnerGPU);

        private bool hasDynamicSourceCount => GetData() != null ? ((VFXDataParticle)GetData()).hasDynamicSourceCount : false;

        protected override void OnInvalidate(VFXModel model, InvalidationCause cause)
        {
            if (cause == InvalidationCause.kConnectionChanged)
            {
                if (model == this)
                    ResyncSlots(false); // To add/remove stripIndex
                RefreshErrors(GetGraph());
            }

            base.OnInvalidate(model, cause);
        }

        protected override void GenerateErrors(VFXInvalidateErrorReporter manager)
        {
            VFXSetting capacitySetting = GetSetting("capacity");
            if ((uint)capacitySetting.value > 1000000)
                manager.RegisterError("CapacityOver1M", VFXErrorType.PerfWarning, "Systems with large capacities can be slow to simulate");
            var data = GetData() as VFXDataParticle;
            if (data != null && CanBeCompiled())
            {
                if (data.boundsMode == BoundsSettingMode.Recorded)
                {
                    if (VFXViewWindow.currentWindow?.graphView?.attachedComponent == null ||
                        !BoardPreferenceHelper.IsVisible(BoardPreferenceHelper.Board.componentBoard, false))
                    {
                        manager.RegisterError("NeedsRecording", VFXErrorType.Warning,
                            "In order to record the bounds, the current graph needs to be attached to a scene instance via the Target Game Object panel");
                    }
                    var boundsSlot = inputSlots.FirstOrDefault(s => s.name == "bounds");
                    if (boundsSlot != null && boundsSlot.HasLink(true))
                    {
                        manager.RegisterError("OverriddenRecording", VFXErrorType.Warning,
                            "This system bounds will not be recorded because they are set from operators.");
                    }
                }

                if (data.boundsMode == BoundsSettingMode.Automatic)
                {
                    manager.RegisterError("CullingFlagAlwaysSimulate", VFXErrorType.Warning,
                        "Setting the system Bounds Mode to Automatic will switch the culling flags of the Visual Effect asset" +
                        " to 'Always recompute bounds and simulate'.");
                }
            }
        }

        protected override IEnumerable<VFXPropertyWithValue> inputProperties
        {
            get
            {
                var particleData = GetData() as VFXDataParticle;

                var prop = Enumerable.Empty<VFXPropertyWithValue>();
                if (particleData)
                {
                    if (particleData.boundsMode == BoundsSettingMode.Manual)
                    {
                        prop = prop.Concat(PropertiesFromType("InputPropertiesBounds"));
                    }
                    if (particleData.boundsMode == BoundsSettingMode.Recorded)
                    {
                        prop = prop.Concat(PropertiesFromType("InputPropertiesBounds"));
                        prop = prop.Concat(PropertiesFromType("InputPropertiesPadding"));
                    }
                    if (particleData.boundsMode == BoundsSettingMode.Automatic)
                    {
                        prop = prop.Concat(PropertiesFromType("InputPropertiesPadding"));
                    }
                }

                if (ownedType == VFXDataType.ParticleStrip && !hasGPUSpawner)
                    prop = prop.Concat(PropertiesFromType("StripInputProperties"));
                return prop;
            }
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

                //Detect all attribute used in source spawner & consider as read source from them
                //This can be done using VFXDataSpawner after read attribute from spawn feature merge (require to be sure that the order of compilation is respected)
                foreach (var block in parents.SelectMany(o => o.children).OfType<VFX.Block.VFXSpawnerSetAttribute>())
                {
                    var attributeName = block.GetSetting("attribute");
                    yield return new VFXAttributeInfo(VFXAttribute.Find((string)attributeName.value), VFXAttributeMode.ReadSource);
                }
            }
        }

        public sealed override VFXCoordinateSpace GetOutputSpaceFromSlot(VFXSlot slot)
        {
            if (slot.name == "bounds")
                return VFXCoordinateSpace.Local;
            return base.GetOutputSpaceFromSlot(slot);
        }

        public override VFXExpressionMapper GetExpressionMapper(VFXDeviceTarget target)
        {
            var particleData = GetData() as VFXDataParticle;
            bool isRecordedBounds = particleData && particleData.boundsMode == BoundsSettingMode.Recorded;
            // GPU
            if (target == VFXDeviceTarget.GPU)
            {
                var gpuMapper = VFXExpressionMapper.FromBlocks(activeFlattenedChildrenWithImplicit);
                if (ownedType == VFXDataType.ParticleStrip && !hasGPUSpawner)
                    gpuMapper.AddExpressionsFromSlot(inputSlots[(isRecordedBounds ? 2 : 1)], -1); // strip index
                return gpuMapper;
            }

            // CPU
            var cpuMapper = new VFXExpressionMapper();
            if (particleData)
            {
                switch (particleData.boundsMode)
                {
                    case BoundsSettingMode.Manual:
                        cpuMapper.AddExpressionsFromSlot(inputSlots[0], -1); // bounds
                        break;
                    case BoundsSettingMode.Recorded:
                        cpuMapper.AddExpressionsFromSlot(inputSlots[0], -1); // bounds
                        cpuMapper.AddExpressionsFromSlot(inputSlots[1], -1); //bounds padding
                        break;
                    case BoundsSettingMode.Automatic:
                        cpuMapper.AddExpressionsFromSlot(inputSlots[0], -1); //bounds padding
                        break;
                }
            }

            return cpuMapper;
        }

        public override VFXSetting GetSetting(string name)
        {
            return GetData().GetSetting(name); // Just a bridge on data
        }

        public override IEnumerable<VFXSetting> GetSettings(bool listHidden, VFXSettingAttribute.VisibleFlags flags)
        {
            return GetData().GetSettings(listHidden, flags); // Just a bridge on data
        }
    }
}
