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
            if (PenDetected()) return true;
            return false;
        }

        public bool PenDetected()
        {
            bool pen_Detected = false;
            var pensInRange = ServiceHolder<IWorldObjectManager>.Obj.All.Where(w => w.GetType() == typeof(BigPenObject) && Vector3i.Distance(this.Position.XYZi(), w.Position.XYZi()) < 4).ToList();

            if (pensInRange.Count > 0)
            {
                animalsInPen = !pensInRange[0].GetComponent<PublicStorageComponent>().Inventory.IsEmpty;
                pen_Detected = true;
            }
            return pen_Detected;
        }
    }
}
