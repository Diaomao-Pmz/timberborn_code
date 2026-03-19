using System;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000019 RID: 25
	public interface IWaterNeedingBuilding
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000E2 RID: 226
		// (remove) Token: 0x060000E3 RID: 227
		event EventHandler StartedNeedingWater;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000E4 RID: 228
		// (remove) Token: 0x060000E5 RID: 229
		event EventHandler StoppedNeedingWater;

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E6 RID: 230
		Vector3Int WaterCoordinatesTransformed { get; }
	}
}
