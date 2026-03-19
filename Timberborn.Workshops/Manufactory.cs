using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.MechanicalSystem;
using Timberborn.Persistence;
using Timberborn.ResourceCountingSystem;
using Timberborn.ScienceSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Workshops
{
	// Token: 0x02000011 RID: 17
	public class Manufactory : BaseComponent, IAwakableComponent, IPersistentEntity, IFinishedStateListener, IRegisteredComponent, IRecipeSelector, IDuplicable<Manufactory>, IDuplicable, IGoodProcessor
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000039 RID: 57 RVA: 0x00002758 File Offset: 0x00000958
		// (remove) Token: 0x0600003A RID: 58 RVA: 0x00002790 File Offset: 0x00000990
		public event EventHandler<ProductionProgressedEventArgs> ProductionProgressed;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600003B RID: 59 RVA: 0x000027C8 File Offset: 0x000009C8
		// (remove) Token: 0x0600003C RID: 60 RVA: 0x00002800 File Offset: 0x00000A00
		public event EventHandler ProductionFinished;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600003D RID: 61 RVA: 0x00002838 File Offset: 0x00000A38
		// (remove) Token: 0x0600003E RID: 62 RVA: 0x00002870 File Offset: 0x00000A70
		public event EventHandler RecipeChanged;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000028A5 File Offset: 0x00000AA5
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000028AD File Offset: 0x00000AAD
		public Inventory Inventory { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028B6 File Offset: 0x00000AB6
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000028BE File Offset: 0x00000ABE
		public float FuelRemaining { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000028C7 File Offset: 0x00000AC7
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000028CF File Offset: 0x00000ACF
		public float ProductionProgress { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000028D8 File Offset: 0x00000AD8
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000028E0 File Offset: 0x00000AE0
		public RecipeSpec CurrentRecipe { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000028E9 File Offset: 0x00000AE9
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000028F1 File Offset: 0x00000AF1
		public ImmutableArray<RecipeSpec> ProductionRecipes { get; private set; }

		// Token: 0x06000049 RID: 73 RVA: 0x000028FA File Offset: 0x00000AFA
		public Manufactory(ScienceService scienceService, RecipeSpecService recipeSpecService)
		{
			this._scienceService = scienceService;
			this._recipeSpecService = recipeSpecService;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002926 File Offset: 0x00000B26
		public bool HasFuel
		{
			get
			{
				return !this.CurrentRecipe.ConsumesFuel || this.Inventory.UnreservedAmountInStock(this.CurrentRecipe.Fuel) > 0 || this.FuelRemaining > 0f;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000295D File Offset: 0x00000B5D
		public bool IsReadyToProduce
		{
			get
			{
				return this.HasCurrentRecipe && this.HasAllIngredients && this.HasUnreservedCapacityForCurrentProducts() && this.HasFuel && this.HasPower && this.ProductionEfficiency() > 0f;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002996 File Offset: 0x00000B96
		public bool HasCurrentRecipe
		{
			get
			{
				return this.CurrentRecipe != null;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000029A4 File Offset: 0x00000BA4
		public bool NeedsInventory
		{
			get
			{
				if (!this.ProductionRecipes.Any((RecipeSpec recipe) => recipe.ConsumesFuel))
				{
					if (!this.ProductionRecipes.Any((RecipeSpec recipe) => recipe.ConsumesIngredients))
					{
						return this.ProductionRecipes.Any((RecipeSpec recipe) => recipe.ProducesProducts);
					}
				}
				return true;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002A35 File Offset: 0x00000C35
		public bool HasAllIngredients
		{
			get
			{
				return this.HasAllIngredientsInternal() || this._ingredientsConsumed;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A47 File Offset: 0x00000C47
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._recipeGoodDisallower = base.GetComponent<RecipeGoodDisallower>();
			base.GetComponents<IManufactoryLimiter>(this._manufactoryLimiters);
			this._manufactorySpec = base.GetComponent<ManufactorySpec>();
			this.InitializeRecipes();
			base.DisableComponent();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A85 File Offset: 0x00000C85
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			if (this.NeedsInventory)
			{
				this.Inventory.Enable();
				this._recipeGoodDisallower.UpdateAllowedAmounts(this.CurrentRecipe);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002AB1 File Offset: 0x00000CB1
		public void OnExitFinishedState()
		{
			if (this.NeedsInventory)
			{
				this.Inventory.Disable();
			}
			base.DisableComponent();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002ACC File Offset: 0x00000CCC
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Manufactory.ManufactoryKey);
			if (this.ProductionRecipes.Length > 1 && this.HasCurrentRecipe)
			{
				component.Set(Manufactory.CurrentRecipeIdKey, this.CurrentRecipe.Id);
			}
			if (this.FuelRemaining > 0f)
			{
				component.Set(Manufactory.FuelRemainingKey, this.FuelRemaining);
			}
			component.Set(Manufactory.ProductionProgressKey, this.ProductionProgress);
			component.Set(Manufactory.IngredientsConsumedKey, this._ingredientsConsumed);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B54 File Offset: 0x00000D54
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Manufactory.ManufactoryKey);
			if (component.Has<string>(Manufactory.CurrentRecipeIdKey))
			{
				string recipeId = component.Get(Manufactory.CurrentRecipeIdKey);
				RecipeSpec recipeSpec = this.ProductionRecipes.SingleOrDefault((RecipeSpec recipe) => recipe.Id == recipeId) ?? this.ProductionRecipes.SingleOrDefault((RecipeSpec recipe) => recipe.BackwardCompatibleIds.Contains(recipeId));
				if (recipeSpec != null)
				{
					this.CurrentRecipe = recipeSpec;
				}
			}
			if (component.Has<float>(Manufactory.FuelRemainingKey))
			{
				this.FuelRemaining = component.Get(Manufactory.FuelRemainingKey);
			}
			this.ProductionProgress = component.Get(Manufactory.ProductionProgressKey);
			this._ingredientsConsumed = (component.Has<bool>(Manufactory.IngredientsConsumedKey) && component.Get(Manufactory.IngredientsConsumedKey));
			this.UpdateGoodRegistry();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C2C File Offset: 0x00000E2C
		public void DuplicateFrom(Manufactory source)
		{
			RecipeSpec currentRecipe = source.CurrentRecipe;
			if (this.CurrentRecipe != currentRecipe && ((currentRecipe == null && this.ProductionRecipes.Length > 1) || this.ProductionRecipes.Contains(currentRecipe)))
			{
				this.SetRecipe(currentRecipe);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C80 File Offset: 0x00000E80
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<Manufactory>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C9A File Offset: 0x00000E9A
		public void SetRecipe(RecipeSpec selectedRecipe)
		{
			this.CurrentRecipe = selectedRecipe;
			this.ResetProductionProgress();
			if (base.Enabled)
			{
				this._recipeGoodDisallower.UpdateAllowedAmounts(this.CurrentRecipe);
			}
			EventHandler recipeChanged = this.RecipeChanged;
			if (recipeChanged == null)
			{
				return;
			}
			recipeChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public void IncreaseProductionProgress(float workedHours)
		{
			if (this.IsReadyToProduce)
			{
				float num = Mathf.Min(workedHours / this.CurrentRecipe.CycleDurationInHours * this.ProductionEfficiency(), this.MaxProductionProgressChange());
				this.RefillFuelAndTakeIngredients(num);
				this.ProductionProgress += num;
				EventHandler<ProductionProgressedEventArgs> productionProgressed = this.ProductionProgressed;
				if (productionProgressed != null)
				{
					productionProgressed(this, new ProductionProgressedEventArgs(num));
				}
				if (this.ProductionProgress >= 1f)
				{
					this.FinishProduction();
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D4D File Offset: 0x00000F4D
		public void ResetProductionProgress()
		{
			this._ingredientsConsumed = false;
			this.ProductionProgress = 0f;
			this.UpdateGoodRegistry();
			EventHandler<ProductionProgressedEventArgs> productionProgressed = this.ProductionProgressed;
			if (productionProgressed == null)
			{
				return;
			}
			productionProgressed(this, new ProductionProgressedEventArgs(0f));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D84 File Offset: 0x00000F84
		public bool HasUnreservedCapacityForCurrentProducts()
		{
			for (int i = 0; i < this.CurrentRecipe.Products.Length; i++)
			{
				GoodAmountSpec goodAmountSpec = this.CurrentRecipe.Products[i];
				if (!this.Inventory.HasUnreservedCapacity(goodAmountSpec.ToGoodAmount()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DDA File Offset: 0x00000FDA
		public GoodRegistry GetProcessingGoods()
		{
			return this._processingGoodRegistry;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002DE2 File Offset: 0x00000FE2
		public bool HasPower
		{
			get
			{
				return !this.NeedsPower || this._mechanicalNode.ActiveAndPowered;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002DF9 File Offset: 0x00000FF9
		public bool NeedsPower
		{
			get
			{
				return this._mechanicalNode && this._mechanicalNode.IsConsumer;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E18 File Offset: 0x00001018
		public void UpdateGoodRegistry()
		{
			this._processingGoodRegistry.Clear();
			if (this.HasCurrentRecipe)
			{
				if (this._ingredientsConsumed)
				{
					foreach (GoodAmountSpec goodAmountSpec in this.CurrentRecipe.Ingredients)
					{
						this._processingGoodRegistry.Add(goodAmountSpec.ToGoodAmount());
					}
				}
				if (this.CurrentRecipe.ConsumesFuel && this.FuelRemaining > 0f)
				{
					this._processingGoodRegistry.Add(new GoodAmount(this.CurrentRecipe.Fuel, 1));
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002EB0 File Offset: 0x000010B0
		public void InitializeRecipes()
		{
			this.ProductionRecipes = (from id in this._manufactorySpec.ProductionRecipeIds
			select this._recipeSpecService.GetRecipe(id) into recipe
			where recipe.Blueprint.IsAllowedByFeatureToggles()
			select recipe).ToImmutableArray<RecipeSpec>();
			if (this.ProductionRecipes.Length == 1)
			{
				this.SetRecipe(this.ProductionRecipes[0]);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F30 File Offset: 0x00001130
		public void RefillFuelAndTakeIngredients(float productionProgressChange)
		{
			if (this.CurrentRecipe.ConsumesFuel && this.FuelRemaining <= 0f)
			{
				this.Inventory.Take(new GoodAmount(this.CurrentRecipe.Fuel, 1));
				this.FuelRemaining = 1f;
			}
			if (productionProgressChange > 0f && !this._ingredientsConsumed)
			{
				this._ingredientsConsumed = true;
				foreach (GoodAmountSpec goodAmountSpec in this.CurrentRecipe.Ingredients)
				{
					this.Inventory.Take(goodAmountSpec.ToGoodAmount());
				}
				this.UpdateGoodRegistry();
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FD4 File Offset: 0x000011D4
		public float MaxProductionProgressChange()
		{
			float num = 1f;
			for (int i = 0; i < this._manufactoryLimiters.Count; i++)
			{
				float val = this._manufactoryLimiters[i].MaxProductionProgressChange();
				num = Math.Min(num, val);
			}
			return num;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003018 File Offset: 0x00001218
		public float ProductionEfficiency()
		{
			float num = this.NeedsPower ? this._mechanicalNode.PowerEfficiency : 1f;
			for (int i = 0; i < this._manufactoryLimiters.Count; i++)
			{
				num = Math.Min(num, this._manufactoryLimiters[i].ProductionEfficiency());
			}
			return num;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003070 File Offset: 0x00001270
		public void FinishProduction()
		{
			foreach (GoodAmountSpec goodAmountSpec in this.CurrentRecipe.Products)
			{
				this.Inventory.Give(goodAmountSpec.ToGoodAmount());
			}
			this._scienceService.AddPoints(this.CurrentRecipe.ProducedSciencePoints);
			this.ConsumeFuel();
			this.ResetProductionProgress();
			EventHandler productionFinished = this.ProductionFinished;
			if (productionFinished == null)
			{
				return;
			}
			productionFinished(this, EventArgs.Empty);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000030F0 File Offset: 0x000012F0
		public void ConsumeFuel()
		{
			if (this.CurrentRecipe.ConsumesFuel)
			{
				float num = 1f / (float)this.CurrentRecipe.CyclesFuelLasts;
				this.FuelRemaining -= num;
				if (this.FuelRemaining < num * 0.5f)
				{
					this.FuelRemaining = 0f;
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003148 File Offset: 0x00001348
		public bool HasAllIngredientsInternal()
		{
			for (int i = 0; i < this.CurrentRecipe.Ingredients.Length; i++)
			{
				GoodAmountSpec goodAmountSpec = this.CurrentRecipe.Ingredients[i];
				if (!this.Inventory.HasUnreservedStock(goodAmountSpec.ToGoodAmount()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000020 RID: 32
		public static readonly ComponentKey ManufactoryKey = new ComponentKey("Manufactory");

		// Token: 0x04000021 RID: 33
		public static readonly PropertyKey<string> CurrentRecipeIdKey = new PropertyKey<string>("CurrentRecipe");

		// Token: 0x04000022 RID: 34
		public static readonly PropertyKey<float> ProductionProgressKey = new PropertyKey<float>("ProductionProgress");

		// Token: 0x04000023 RID: 35
		public static readonly PropertyKey<float> FuelRemainingKey = new PropertyKey<float>("FuelRemaining");

		// Token: 0x04000024 RID: 36
		public static readonly PropertyKey<bool> IngredientsConsumedKey = new PropertyKey<bool>("IngredientsConsumed");

		// Token: 0x0400002D RID: 45
		public readonly ScienceService _scienceService;

		// Token: 0x0400002E RID: 46
		public readonly RecipeSpecService _recipeSpecService;

		// Token: 0x0400002F RID: 47
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000030 RID: 48
		public RecipeGoodDisallower _recipeGoodDisallower;

		// Token: 0x04000031 RID: 49
		public readonly List<IManufactoryLimiter> _manufactoryLimiters = new List<IManufactoryLimiter>();

		// Token: 0x04000032 RID: 50
		public ManufactorySpec _manufactorySpec;

		// Token: 0x04000033 RID: 51
		public bool _ingredientsConsumed;

		// Token: 0x04000034 RID: 52
		public readonly GoodRegistry _processingGoodRegistry = new GoodRegistry();
	}
}
