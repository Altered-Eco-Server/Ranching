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

    [RequiresSkill(typeof(RanchingSkill), 4)]
    public partial class ButterRecipe : RecipeFamily
    {
        public ButterRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "Butter",  //noloc
                Localizer.DoStr("Butter"),
                new List<IngredientElement>
                {
                    new IngredientElement(typeof(MilkItem), 6, typeof(RanchingSkill)),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<SunButterItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.5f;
            this.LaborInCalories = CreateLaborInCaloriesValue(15, typeof(RanchingSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButterRecipe), 2, typeof(RanchingSkill));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Butter"), typeof(ButterRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
