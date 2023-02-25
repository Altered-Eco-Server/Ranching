﻿// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// <auto-generated />
namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    [Serialized]
    [LocDisplayName("Feather")]
    [Tag("Feather", 1)]
    [Weight(50)]
    [Ecopedia("Natural Resources", "Animal", createAsSubPage: true)]
    public partial class FeatherItem : Item
    {
        public override LocString DisplayDescription => Localizer.DoStr("tickle tickle");
    }
}