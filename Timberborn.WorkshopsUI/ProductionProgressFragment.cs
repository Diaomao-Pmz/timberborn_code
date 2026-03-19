using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using Timberborn.Workshops;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x02000018 RID: 24
	public class ProductionProgressFragment : IEntityPanelFragment
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003371 File Offset: 0x00001571
		public ProductionProgressFragment(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003388 File Offset: 0x00001588
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ProductionProgressFragment");
			this._progressText = UQueryExtensions.Q<Label>(this._root, "ProgressText", null);
			this._craftingTime = UQueryExtensions.Q<Label>(this._root, "CraftingTime", null);
			this._fuelRemaining = UQueryExtensions.Q<Label>(this._root, "FuelRemaining", null);
			this._input = UQueryExtensions.Q<VisualElement>(this._root, "InputWrapper", null);
			this._output = UQueryExtensions.Q<VisualElement>(this._root, "OutputWrapper", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003430 File Offset: 0x00001630
		public void ShowFragment(BaseComponent entity)
		{
			this._manufactory = entity.GetComponent<Manufactory>();
			if (this._manufactory)
			{
				this._workplace = entity.GetComponent<Workplace>();
				this._manufactoryDescriber = entity.GetComponent<ManufactoryDescriber>();
				if (this._manufactory.HasCurrentRecipe)
				{
					this.AddProductionItem();
					this.UpdateCraftingTime();
				}
				this._enabled = true;
				this._manufactory.RecipeChanged += this.OnProductionRecipeChanged;
				if (this._workplace)
				{
					this._workplace.WorkerAssigned += this.OnWorkerChanged;
					this._workplace.WorkerUnassigned += this.OnWorkerChanged;
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000034E4 File Offset: 0x000016E4
		public void ClearFragment()
		{
			if (this._manufactory)
			{
				this._manufactory.RecipeChanged -= this.OnProductionRecipeChanged;
				if (this._workplace)
				{
					this._workplace.WorkerAssigned -= this.OnWorkerChanged;
					this._workplace.WorkerUnassigned -= this.OnWorkerChanged;
				}
			}
			this._input.Clear();
			this._output.Clear();
			this._manufactory = null;
			this._workplace = null;
			this._manufactoryDescriber = null;
			this._enabled = false;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003584 File Offset: 0x00001784
		public void UpdateFragment()
		{
			if (this._enabled && this._manufactory.Enabled)
			{
				if (this._manufactory.HasCurrentRecipe)
				{
					this.UpdateProductionRecipe(this._manufactory.CurrentRecipe.ConsumesFuel);
				}
				else
				{
					bool showFuel = this._manufactory.ProductionRecipes.Any((RecipeSpec recipe) => recipe.ConsumesFuel);
					this.UpdateProductionRecipe(showFuel);
				}
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000361C File Offset: 0x0000181C
		public void UpdateProductionRecipe(bool showFuel)
		{
			string text = NumberFormatter.FormatAsPercentFloored((double)this._manufactory.ProductionProgress);
			this._progressText.text = text;
			string param = string.Format("{0:0}", this._manufactory.FuelRemaining * 100f);
			this._fuelRemaining.text = this._loc.T<string>(ProductionProgressFragment.FuelRemainingLocKey, param);
			this._fuelRemaining.ToggleDisplayStyle(showFuel);
			this.UpdateCraftingTime();
			this._root.EnableInClassList(ProductionProgressFragment.NoRecipeClass, !this._manufactory.HasCurrentRecipe);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000036B4 File Offset: 0x000018B4
		public void OnProductionRecipeChanged(object sender, EventArgs args)
		{
			Manufactory manufactory = this._manufactory;
			this.ClearFragment();
			this.ShowFragment(manufactory);
			this.UpdateFragment();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000036DC File Offset: 0x000018DC
		public void AddProductionItem()
		{
			ValueTuple<VisualElement, VisualElement> valueTuple = this._manufactoryDescriber.DescribeRecipe(this._manufactory.CurrentRecipe);
			VisualElement item = valueTuple.Item1;
			VisualElement item2 = valueTuple.Item2;
			this._input.Add(item);
			this._output.Add(item2);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003724 File Offset: 0x00001924
		public void OnWorkerChanged(object sender, WorkerChangedEventArgs workerChangedEventArgs)
		{
			this.UpdateCraftingTime();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000372C File Offset: 0x0000192C
		public void UpdateCraftingTime()
		{
			bool flag = !this._workplace || this._workplace.DesiredWorkers > 0;
			if (this._manufactory.HasCurrentRecipe && flag)
			{
				this._craftingTime.text = this._manufactoryDescriber.GetCraftingTime(this._manufactory.CurrentRecipe, (float)(this._workplace ? this._workplace.DesiredWorkers : 1));
				return;
			}
			this._craftingTime.text = "-";
		}

		// Token: 0x04000064 RID: 100
		public static readonly string NoRecipeClass = "production-progress-fragment__no-recipe";

		// Token: 0x04000065 RID: 101
		public static readonly string FuelRemainingLocKey = "Work.FuelRemaining";

		// Token: 0x04000066 RID: 102
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000067 RID: 103
		public readonly ILoc _loc;

		// Token: 0x04000068 RID: 104
		public Manufactory _manufactory;

		// Token: 0x04000069 RID: 105
		public Workplace _workplace;

		// Token: 0x0400006A RID: 106
		public ManufactoryDescriber _manufactoryDescriber;

		// Token: 0x0400006B RID: 107
		public VisualElement _root;

		// Token: 0x0400006C RID: 108
		public Label _progressText;

		// Token: 0x0400006D RID: 109
		public Label _craftingTime;

		// Token: 0x0400006E RID: 110
		public Label _fuelRemaining;

		// Token: 0x0400006F RID: 111
		public VisualElement _input;

		// Token: 0x04000070 RID: 112
		public VisualElement _output;

		// Token: 0x04000071 RID: 113
		public bool _enabled;
	}
}
