using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterBuildings;
using Timberborn.Workshops;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x02000011 RID: 17
	public class WaterInputContaminationManufactoryLimiter : BaseComponent, IAwakableComponent, IManufactoryLimiter
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002785 File Offset: 0x00000985
		public void Awake()
		{
			this._waterInput = base.GetComponent<WaterInput>();
			this._manufactoryWaterContaminationConsumer = base.GetComponent<ManufactoryWaterContaminationConsumer>();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000027A0 File Offset: 0x000009A0
		public float ProductionEfficiency()
		{
			if (!this._waterInput.IsUnderwater)
			{
				return 0f;
			}
			float contaminationPercentage = this._waterInput.ContaminationPercentage;
			if (contaminationPercentage < WaterInputContaminationManufactoryLimiter.EfficiencyThreshold)
			{
				return 0f;
			}
			return contaminationPercentage;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000027DC File Offset: 0x000009DC
		public float MaxProductionProgressChange()
		{
			float consumedWaterContamination = this._manufactoryWaterContaminationConsumer.ConsumedWaterContamination;
			if (consumedWaterContamination > 0f)
			{
				return this._waterInput.ContaminatedWaterAmount / consumedWaterContamination;
			}
			return 0f;
		}

		// Token: 0x0400001A RID: 26
		public static readonly float EfficiencyThreshold = 0.0001f;

		// Token: 0x0400001B RID: 27
		public WaterInput _waterInput;

		// Token: 0x0400001C RID: 28
		public ManufactoryWaterContaminationConsumer _manufactoryWaterContaminationConsumer;
	}
}
