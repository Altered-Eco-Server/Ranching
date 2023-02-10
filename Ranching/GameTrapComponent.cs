using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Simulation;
using Eco.Simulation.Agents;
using Eco.Simulation.Types;
using Eco.Simulation.WorldLayers;
using Eco.Simulation.WorldLayers.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using Eco.Gameplay.Components;
using Eco.Mods.TechTree;

namespace Eco.AlteredCore
{
    [Serialized]
    [RequireComponent(typeof(PublicStorageComponent), null)]
    public class GameTrapComponent : WorldObjectComponent
    {
        //private const string AnimalCountState = "AnimalsInTrap";
        private const string HasAnimalsState = "OpenDoor";
        private PublicStorageComponent storage;
        private List<AnimalLayer> targetLayers;
        private StatusElement trappingStatus;
        private bool enabled = true;
        int i = 0;
        int ChanceMulti = 1;

        private int getRandomNumber(int min, int max)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(min, max);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.trappingStatus = this.Parent.GetComponent<StatusComponent>().CreateStatusElement();
            this.enabled = this.EnabledTest(this.Parent.Position.XYZi());
            this.UpdateTrappingStatus();
            AutoSingleton<WorldLayerSync>.Obj.PreTickActions.Add(new Action(this.LayerTick));
        }

        public void UpdateTrappingStatus() => this.trappingStatus.SetStatusMessage(this.Enabled, Localizer.DoStr("Currently trapping animals."), this.FailStatusMessage);

        public override bool Enabled => this.enabled;

        public Func<Vector3i, bool> EnabledTest { get; set; } = (Func<Vector3i, bool>)(x => true);

        public LocString FailStatusMessage { get; set; } = Localizer.DoStr("Trap is Full.");

        public void UpdateEnabled() => this.enabled = this.EnabledTest(this.Parent.Position.XYZi());

        internal void OnPickup(Player player) => AutoSingleton<WorldLayerSync>.Obj.PreTickActions.Remove(new Action(this.LayerTick));

        public void Initialize(List<string> layers)
        {
            base.Initialize();
            List<AnimalLayer> animalLayerList = new List<AnimalLayer>();
            foreach (string layer in layers)
            {
                string layerName = layer;
                animalLayerList.Add((AnimalLayer)Singleton<WorldLayerManager>.Obj.SpeciesToLayers[EcoSim.AllSpecies.First<Species>((Func<Species, bool>)(x => x.Name == layerName))]);
            }
            this.targetLayers = animalLayerList;
            this.storage = this.Parent.GetComponent<PublicStorageComponent>();
            this.storage.Initialize(8);
            this.storage.Inventory.OnChanged.Add(new Action<User>(this.UpdateAnimalCount));
            this.UpdateAnimalCount();
        }

        public void LayerTick()
        {
            this.enabled = this.EnabledTest(this.Parent.Position.XYZi());
            this.UpdateTrappingStatus();
            if (!this.enabled)
                return;
            foreach (AnimalLayer targetLayer in this.targetLayers)
            {
                for (i = 0; i < ChanceMulti; i++)
                {
                    Vector2i layerPos = targetLayer.WorldPosToLayerPos(this.Parent.Position.XZi());
                    var lureCount = storage.Inventory.TotalNumberOfItems<NewHerbivoreLureItem>();
                    if (getRandomNumber(1, 15) == 1 && lureCount > 0)
                    {
                        if (RandomUtil.Chance(targetLayer.SafeEntry(layerPos) * (targetLayer.Settings.RenderRange.Max) * 2f))
                        {
                            List<Animal> list = targetLayer.SafePopMapEntry(layerPos);
                            if (list == null)
                                break;

                            this.storage.Inventory.TryRemoveItem<NewHerbivoreLureItem>();
                            Animal animal = list.Random<Animal>();
                            InventoryChangeSet set = InventoryChangeSet.New(this.storage.Inventory);
                            if (animal.GetType() == typeof(Bison))
                                set.AddItem<DomesticatedBisonItem>();
                            else if (animal.GetType() == typeof(Turkey))
                                set.AddItem<DomesticatedTurkeyItem>();
                            else if (animal.GetType() == typeof(MountainGoat))
                                set.AddItem<DomesticatedSheepItem>();
                            else
                                OrganismItemManager.AddRandomResourcesToChangeSet(ref set, (Species)animal.Species);

                            if (!set.TryApply().Success) break;
                            animal.Kill();

                            break;
                        }
                    }
                }
            }
        }

        private void UpdateAnimalCount(User user = null)
        {
            var lureCount = storage.Inventory.TotalNumberOfItems<NewHerbivoreLureItem>();
            int num = this.storage.Inventory.Stacks.Sum<ItemStack>((Func<ItemStack, int>)(stack => stack.Quantity));
            this.Parent.SetAnimatedState("OpenDoor", num > 0 && lureCount <= 0);
        }
    }
}