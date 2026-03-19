using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterBuildings;
using Timberborn.Workshops;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x02000007 RID: 7
	public class ManufactoryWaterConsumer : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public float ConsumedWater { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002111 File Offset: 0x00000311
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			this._waterInput = base.GetComponent<WaterInput>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000212B File Offset: 0x0000032B
		public void Start()
		{
			this.UpdateRecipe();
			this._manufactory.RecipeChanged += delegate(object _, EventArgs _)
			{
				this.UpdateRecipe();
			};
			this._manufactory.ProductionProgressed += this.OnProductionProgressed;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002161 File Offset: 0x00000361
		public void UpdateRecipe()
		{
			this.ConsumedWater = WaterGoodToWaterAmountConverter.GetWaterAmount(this._manufactory.CurrentRecipe.Products);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002183 File Offset: 0x00000383
		public void OnProductionProgressed(object sender, ProductionProgressedEventArgs e)
		{
			if (this.ConsumedWater > 0f)
			{
				this._waterInput.RemoveCleanWater(this.ConsumedWater * e.ProductionProgressChange);
			}
		}

		// Token: 0x04000009 RID: 9
		public Manufactory _manufactory;

		// Token: 0x0400000A RID: 10
		public WaterInput _waterInput;
	}
}
