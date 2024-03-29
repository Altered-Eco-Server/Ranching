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
    using Eco.Core.Controller;

    [RequiresSkill(typeof(RanchingSkill), 5)]
    public partial class NewCheeseRecipe : RecipeFamily
    {
        public NewCheeseRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "Cheese",  //noloc
                Localizer.DoStr("Cheese"),
                new List<IngredientElement>
                {
                    new IngredientElement(typeof(MilkItem), 4, typeof(RanchingSkill)),
                    new IngredientElement(typeof(ClothItem), 1, true),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<SunCheeseItem>(1),
                    new CraftingElement<ClothItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.5f;
            this.LaborInCalories = CreateLaborInCaloriesValue(15, typeof(RanchingSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(NewCheeseRecipe), 2, typeof(RanchingSkill));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Cheese"), typeof(NewCheeseRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}