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
    [LocDisplayName("Egg")]
    [Tag("Egg", 1)]
    [Weight(50)]
    [Ecopedia("Food", "Ingredients", createAsSubPage: true)]
    public partial class EggItem : FoodItem
    {
        public override LocString DisplayDescription => Localizer.DoStr("Egg straight from the farm.");

        public override float Calories => 80;
        public override Nutrients Nutrition => new Nutrients() { Carbs = 0, Fat = 8, Protein = 5, Vitamins = 2 };
        protected override int BaseShelfLife => (int)TimeUtil.HoursToSeconds(96);
    }
}