﻿// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// <auto-generated />

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
    [RequiresSkill(typeof(RanchingSkill), 6)]
    public partial class NewBreedBisonRecipe :
        RecipeFamily
    {
        public NewBreedBisonRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "Breed Bison",
                    Localizer.DoStr("BreedBison"),
                    new IngredientElement[]
                    {
               new IngredientElement(typeof(DomesticatedBisonItem), 2, true),
               new IngredientElement(typeof(NewHerbivoreRationItem), 4, typeof(RanchingSkill)),
                    },
                    new CraftingElement[]
                    {
               new CraftingElement<DomesticatedBisonItem>(3)

                    })
            };
            this.ExperienceOnCraft = 7;
            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(RanchingSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(NewBreedBisonRecipe), 5, typeof(RanchingSkill));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Breed Bison"), typeof(NewBreedBisonRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(NewAnimalFeederObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
