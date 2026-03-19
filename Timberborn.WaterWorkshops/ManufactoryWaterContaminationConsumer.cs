using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterBuildings;
using Timberborn.Workshops;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x02000009 RID: 9
	public class ManufactoryWaterContaminationConsumer : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002284 File Offset: 0x00000484
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000228C File Offset: 0x0000048C
		public float ConsumedWaterContamination { get; private set; }

		// Token: 0x0600001D RID: 29 RVA: 0x00002295 File Offset: 0x00000495
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			this._waterInput = base.GetComponent<WaterInput>();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022AF File Offset: 0x000004AF
		public void Start()
		{
			this.UpdateRecipe();
			this._manufactory.RecipeChanged += delegate(object _, EventArgs _)
			{
				this.UpdateRecipe();
			};
			this._manufactory.ProductionProgressed += this.OnProductionProgressed;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022E5 File Offset: 0x000004E5
		public void UpdateRecipe()
		{
			this.ConsumedWaterContamination = WaterContaminationGoodToWaterContaminationAmountConverter.GetWaterContaminationAmount(this._manufactory.CurrentRecipe.Products);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002307 File Offset: 0x00000507
		public void OnProductionProgressed(object sender, ProductionProgressedEventArgs e)
		{
			if (this.ConsumedWaterContamination > 0f)
			{
				this._waterInput.RemoveContaminatedWater(this.ConsumedWaterContamination * e.ProductionProgressChange);
			}
		}

		// Token: 0x0400000C RID: 12
		public Manufactory _manufactory;

		// Token: 0x0400000D RID: 13
		public WaterInput _waterInput;
	}
}
