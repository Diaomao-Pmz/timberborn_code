using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.InventorySystem;
using Timberborn.Workshops;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000008 RID: 8
	public class ManufactoryProgressVisualizer : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			this._manufactoryProgressVisualizerSpec = base.GetComponent<ManufactoryProgressVisualizerSpec>();
			this._manufactory.ProductionProgressed += this.OnProductionProgressed;
			this._manufactory.ProductionFinished += this.OnProductionFinished;
			this._manufactory.RecipeChanged += this.OnProductionRecipeChanged;
			this._manufactory.Inventory.InventoryStockChanged += this.OnInventoryStockChanged;
			base.DisableComponent();
			this.InitializeProgressSteps();
			this.UpdateVisualization();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002198 File Offset: 0x00000398
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateVisualization();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A6 File Offset: 0x000003A6
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this.UpdateVisualization();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B4 File Offset: 0x000003B4
		public void OnProductionProgressed(object sender, ProductionProgressedEventArgs e)
		{
			this.UpdateVisualization();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B4 File Offset: 0x000003B4
		public void OnProductionFinished(object sender, EventArgs e)
		{
			this.UpdateVisualization();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021B4 File Offset: 0x000003B4
		public void OnProductionRecipeChanged(object sender, EventArgs e)
		{
			this.UpdateVisualization();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021B4 File Offset: 0x000003B4
		public void OnInventoryStockChanged(object sender, InventoryAmountChangedEventArgs e)
		{
			this.UpdateVisualization();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021BC File Offset: 0x000003BC
		public void InitializeProgressSteps()
		{
			foreach (ProgressStepSpec spec in this._manufactoryProgressVisualizerSpec.ProgressSteps)
			{
				this._progressSteps.Add(ProgressStep.Create(spec, base.GameObject));
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002208 File Offset: 0x00000408
		public void UpdateVisualization()
		{
			float productionProgress = this.GetProductionProgress();
			bool flag = false;
			for (int i = this._progressSteps.Count - 1; i >= 0; i--)
			{
				ProgressStep progressStep = this._progressSteps[i];
				bool flag2 = productionProgress >= progressStep.Threshold && !flag;
				if (flag2)
				{
					progressStep.ShowStep();
				}
				else
				{
					progressStep.HideStep();
				}
				flag = (flag || flag2);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000226C File Offset: 0x0000046C
		public float GetProductionProgress()
		{
			float num = base.Enabled ? this._manufactory.ProductionProgress : 0f;
			if (num == 0f && this._manufactory.HasCurrentRecipe && !this._manufactory.HasUnreservedCapacityForCurrentProducts())
			{
				return 1f;
			}
			return num;
		}

		// Token: 0x04000008 RID: 8
		public Manufactory _manufactory;

		// Token: 0x04000009 RID: 9
		public ManufactoryProgressVisualizerSpec _manufactoryProgressVisualizerSpec;

		// Token: 0x0400000A RID: 10
		public readonly List<ProgressStep> _progressSteps = new List<ProgressStep>();
	}
}
