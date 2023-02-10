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
    using Eco.Gameplay.Players;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Simulation;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Types;
    using Eco.AlteredCore;

    [RequireComponent(typeof(GameTrapComponent))]
    public partial class GameTrapObject : WorldObject
    {
        protected override void PostInitialize()
        {
            base.PostInitialize();
            this.GetComponent<PublicStorageComponent>().Initialize(3, 25000);
            this.GetComponent<PublicStorageComponent>().Inventory.AddInvRestriction(new StackLimitRestriction(1));
            this.GetComponent<PublicStorageComponent>().Inventory.AddInvRestriction(new SpecificItemTypesRestriction(new System.Type[] { typeof(NewHerbivoreLureItem), typeof(DeerCarcassItem), typeof(AgoutiCarcassItem), typeof(CoyoteCarcassItem), typeof(FoxCarcassItem), typeof(HareCarcassItem), typeof(DomesticatedTurkeyItem), typeof(WolfCarcassItem), typeof(OtterCarcassItem), typeof(DomesticatedBisonItem), typeof(DomesticatedSheepItem)}));
            this.GetComponent<GameTrapComponent>().Initialize(new List<string>() { "Deer", "Agouti", "Coyote", "Fox", "Hare", "Turkey", "Wolf", "Otter", "Bison", "MountainGoat" });
            this.GetComponent<GameTrapComponent>().FailStatusMessage = Localizer.DoStr("Game trap not enabled.");
            this.GetComponent<GameTrapComponent>().UpdateEnabled();
        }

        public override void Tick()
        {
            GetComponent<GameTrapComponent>().UpdateEnabled();
            GetComponent<GameTrapComponent>().UpdateTrappingStatus();
            this.UpdateEnabledAndOperating();
        }
    }
}

