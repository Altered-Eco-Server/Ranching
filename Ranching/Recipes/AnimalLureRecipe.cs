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

    /// <summary>Auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization.</summary>
    [RequiresSkill(typeof(FarmingSkill), 4)]
    public partial class AnimalLureRecipe :
        RecipeFamily
    {
        public AnimalLureRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "AnimalLure",
                    Localizer.DoStr("Animal Lure"),
                    new IngredientElement[]
                    {
               new IngredientElement("Vegetable", 5, typeof(FarmingSkill)),
               new IngredientElement("Greens", 6, typeof(FarmingSkill)),
               new IngredientElement("Root", 5, typeof(FarmingSkill)),
                    },
                    new CraftingElement[]
                    {
               new CraftingElement<NewHerbivoreLureItem>(1),

                    })
            };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(40, typeof(FarmingSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(AnimalLureRecipe), 4, typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Animal Lure"), typeof(AnimalLureRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
