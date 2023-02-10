﻿namespace Eco.Mods.TechTree
{
    using Eco.Core.Items;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    [Serialized]
    [LocDisplayName("Animal Lure")]
    [MaxStackSize(50)]
    [Weight(300)]
    [Ecopedia("Food", "Cooking", createAsSubPage: true)]
    public partial class NewHerbivoreLureItem : FoodItem
    {
        public override LocString DisplayDescription => Localizer.DoStr("Lures wild animals.");

        public override float Calories => 400;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 12, Fat = 4, Protein = 6, Vitamins = 7 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(336);
    }

}
