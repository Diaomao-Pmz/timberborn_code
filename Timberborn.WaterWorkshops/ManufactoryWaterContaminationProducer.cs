using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterBuildings;
using Timberborn.Workshops;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x0200000B RID: 11
	public class ManufactoryWaterContaminationProducer : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000023C6 File Offset: 0x000005C6
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			this._waterOutput = base.GetComponent<WaterOutput>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000023E0 File Offset: 0x000005E0
		public void Start()
		{
			this.UpdateRecipe();
			this._manufactory.RecipeChanged += delegate(object _, EventArgs _)
			{
				this.UpdateRecipe();
			};
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002400 File Offset: 0x00000600
		public void UpdateRecipe()
		{
			this._manufactory.ProductionProgressed -= this.OnProductionProgressed;
			this._producedWaterContamination = 0f;
			RecipeSpec currentRecipe = this._manufactory.CurrentRecipe;
			if (currentRecipe != null && currentRecipe.Id == ManufactoryWaterContaminationProducer.WaterContaminationId)
			{
				this._manufactory.ProductionProgressed += this.OnProductionProgressed;
				this._producedWaterContamination = WaterContaminationGoodToWaterContaminationAmountConverter.GetWaterContaminationAmount(currentRecipe.Ingredients);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002483 File Offset: 0x00000683
		public void OnProductionProgressed(object sender, ProductionProgressedEventArgs e)
		{
			this._waterOutput.AddContaminatedWater(this._producedWaterContamination * e.ProductionProgressChange);
		}

		// Token: 0x0400000E RID: 14
		public static readonly string WaterContaminationId = "FlowingBadwater";

		// Token: 0x0400000F RID: 15
		public Manufactory _manufactory;

		// Token: 0x04000010 RID: 16
		public WaterOutput _waterOutput;

		// Token: 0x04000011 RID: 17
		public float _producedWaterContamination;
	}
}
