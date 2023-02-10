namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation.WorldLayers;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Components;
    using System;
    using System.Collections.Generic;
    using Eco.Simulation.WorldLayers.Layers;
    using Eco.Shared.IoC;
    using Eco.Mods.Organisms;
    using Eco.Mods.TechTree;
    using Eco.AlteredCore;


    [RequireComponent(typeof(StatusCheckerComponent))]
    public partial class NewAnimalFeederObject : WorldObject
    {
        private bool animalsInPen;
        private List<WorldObject> pensInRange = new();
        protected override void PostInitialize()
        {
            base.PostInitialize();
            this.GetComponent<StatusCheckerComponent>().SuccessStatusMessage = Localizer.DoStr("Animal Feeder is Operational.");
            this.GetComponent<StatusCheckerComponent>().FailStatusMessage = Localizer.DoStr("Animal Feeder must be placed inside an animal pen.");
            this.GetComponent<StatusCheckerComponent>().EnabledTest = this.StatusTest;
            this.GetComponent<StatusCheckerComponent>().UpdateEnabled();
        }

        public override void Tick()
        {
            base.Tick();
            StatusTest(this.Position3i);
            this.GetComponent<StatusCheckerComponent>().UpdateEnabled();
            this.GetComponent<StatusCheckerComponent>().UpdateStatus();
            UpdateEnabledAndOperating();
            SetAnimatedState("Feeder_On", animalsInPen);
        }

        public bool StatusTest(Vector3i pos)
        {
            LayerPosition layerPos = LayerPosition.FromWorldPosition(pos.XZ, 1);
            WorldLayer fertile = WorldLayerManager.Obj.GetLayer(LayerNames.FertileGround);
            WorldLayer building = WorldLayerManager.Obj.GetLayer(LayerNames.ConstructedSpace);
            //if (fertile.GetValue(layerPos) >= 0.5f && building.GetValue(layerPos) <= 0.5f && PenDetected()) return true;
            if (PenDetected()) return true;
            return false;
        }

        public bool PenDetected()
        {
            pensInRange.Clear();
            bool is_Detected = new();
            foreach (WorldObject worldObject in ServiceHolder<IWorldObjectManager>.Obj.All)
            {
                var is_BigPen = worldObject.GetType() == typeof(BigPenObject);
                var distance = Vector3i.Distance(this.Position.XYZi(), worldObject.Position.XYZi());
                if (is_BigPen && distance < 4)
                {
                    pensInRange.Add(worldObject);
                    animalsInPen = !worldObject.GetComponent<PublicStorageComponent>().Inventory.IsEmpty;
                }
            }
            if (pensInRange.Count > 0)
            {
                is_Detected = true;
            }
            else is_Detected = false;
            return is_Detected;
        }
    }
}
