namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;

    public partial class Arrow2Recipe : Recipe
    {
        public Arrow2Recipe()
        {
            this.Init(
                "Arrow",  //noloc
                Localizer.DoStr("Arrow"),
                new List<IngredientElement>
                {
                    new IngredientElement("Wood", 1),
					new IngredientElement(typeof(FeatherItem), 1),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<ArrowItem>(8)
                });
            this.ModsPostInitialize();
            CraftingComponent.AddTagProduct(typeof(ToolBenchObject), typeof(ArrowRecipe), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
