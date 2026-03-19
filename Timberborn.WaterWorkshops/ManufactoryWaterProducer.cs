using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterBuildings;
using Timberborn.Workshops;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x0200000D RID: 13
	public class ManufactoryWaterProducer : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002542 File Offset: 0x00000742
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			this._waterOutput = base.GetComponent<WaterOutput>();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000255C File Offset: 0x0000075C
		public void Start()
		{
			this.UpdateRecipe();
			this._manufactory.RecipeChanged += delegate(object _, EventArgs _)
			{
				this.UpdateRecipe();
			};
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000257C File Offset: 0x0000077C
		public void UpdateRecipe()
		{
			this._manufactory.ProductionProgressed -= this.OnProductionProgressed;
			this._producedWater = 0f;
			RecipeSpec currentRecipe = this._manufactory.CurrentRecipe;
			if (currentRecipe != null && currentRecipe.Id == ManufactoryWaterProducer.WaterId)
			{
				this._manufactory.ProductionProgressed += this.OnProductionProgressed;
				this._producedWater = WaterGoodToWaterAmountConverter.GetWaterAmount(currentRecipe.Ingredients);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000025FF File Offset: 0x000007FF
		public void OnProductionProgressed(object sender, ProductionProgressedEventArgs e)
		{
			this._waterOutput.AddCleanWater(this._producedWater * e.ProductionProgressChange);
		}

		// Token: 0x04000012 RID: 18
		public static readonly string WaterId = "FlowingWater";

		// Token: 0x04000013 RID: 19
		public Manufactory _manufactory;

		// Token: 0x04000014 RID: 20
		public WaterOutput _waterOutput;

		// Token: 0x04000015 RID: 21
		public float _producedWater;
	}
}
