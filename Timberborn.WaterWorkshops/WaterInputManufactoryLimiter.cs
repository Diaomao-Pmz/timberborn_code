using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterBuildings;
using Timberborn.Workshops;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x02000012 RID: 18
	public class WaterInputManufactoryLimiter : BaseComponent, IAwakableComponent, IManufactoryLimiter
	{
		// Token: 0x0600005E RID: 94 RVA: 0x0000281C File Offset: 0x00000A1C
		public void Awake()
		{
			this._waterInput = base.GetComponent<WaterInput>();
			this._manufactoryWaterConsumer = base.GetComponent<ManufactoryWaterConsumer>();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002838 File Offset: 0x00000A38
		public float ProductionEfficiency()
		{
			if (!this._waterInput.IsUnderwater)
			{
				return 0f;
			}
			float num = 1f - this._waterInput.ContaminationPercentage;
			if (num < WaterInputManufactoryLimiter.EfficiencyThreshold)
			{
				return 0f;
			}
			return num;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000287C File Offset: 0x00000A7C
		public float MaxProductionProgressChange()
		{
			float consumedWater = this._manufactoryWaterConsumer.ConsumedWater;
			if (consumedWater > 0f)
			{
				return this._waterInput.CleanWaterAmount / consumedWater;
			}
			return 0f;
		}

		// Token: 0x0400001D RID: 29
		public static readonly float EfficiencyThreshold = 0.0001f;

		// Token: 0x0400001E RID: 30
		public WaterInput _waterInput;

		// Token: 0x0400001F RID: 31
		public ManufactoryWaterConsumer _manufactoryWaterConsumer;
	}
}
