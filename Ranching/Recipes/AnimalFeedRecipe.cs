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

    [RequiresSkill(typeof(RanchingSkill), 1)]
    public partial class AnimalFeedsRecipe :
        RecipeFamily
    {
        public AnimalFeedsRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "AnimalRation",
                    Localizer.DoStr("Animal Feed"),
                    new IngredientElement[]
                    {
               new IngredientElement(typeof(WheatItem), 20, typeof(RanchingSkill)),
                    },
                    new CraftingElement[]
                    {
               new CraftingElement<NewHerbivoreRationItem>(1),

                    })
            };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(40, typeof(RanchingSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(AnimalFeedsRecipe), 4, typeof(RanchingSkill));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Animal Feed"), typeof(AnimalFeedsRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(NewAnimalFeederObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
