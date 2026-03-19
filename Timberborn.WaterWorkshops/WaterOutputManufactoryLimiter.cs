using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterBuildings;
using Timberborn.Workshops;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x02000013 RID: 19
	public class WaterOutputManufactoryLimiter : BaseComponent, IAwakableComponent, IManufactoryLimiter
	{
		// Token: 0x06000063 RID: 99 RVA: 0x000028BC File Offset: 0x00000ABC
		public void Awake()
		{
			this._waterOutput = base.GetComponent<WaterOutput>();
			this._workshop = base.GetComponent<Workshop>();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000028D8 File Offset: 0x00000AD8
		public float ProductionEfficiency()
		{
			float num = this._workshop.CurrentlyWorking ? WaterOutputManufactoryLimiter.DepthOffset : (-WaterOutputManufactoryLimiter.DepthOffset);
			if (this._waterOutput.AvailableSpace + num <= 0f)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000291F File Offset: 0x00000B1F
		public float MaxProductionProgressChange()
		{
			return 1f;
		}

		// Token: 0x04000020 RID: 32
		public static readonly float DepthOffset = 0.01f;

		// Token: 0x04000021 RID: 33
		public WaterOutput _waterOutput;

		// Token: 0x04000022 RID: 34
		public Workshop _workshop;
	}
}
