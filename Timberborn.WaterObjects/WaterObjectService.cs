using System;
using System.Collections.Generic;
using Timberborn.MapEditorTickSystem;
using Timberborn.TickSystem;

namespace Timberborn.WaterObjects
{
	// Token: 0x02000014 RID: 20
	[MapEditorTickable]
	public class WaterObjectService : ITickableSingleton
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002CE6 File Offset: 0x00000EE6
		public void RegisterWaterObject(WaterObject waterObject)
		{
			this._waterObjects.Add(waterObject);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public void UnregisterWaterObject(WaterObject waterObject)
		{
			this._waterObjects.Remove(waterObject);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002D04 File Offset: 0x00000F04
		public void Tick()
		{
			foreach (WaterObject waterObject in this._waterObjects)
			{
				waterObject.UpdateWaterAboveBase();
			}
		}

		// Token: 0x0400001F RID: 31
		public readonly List<WaterObject> _waterObjects = new List<WaterObject>();
	}
}
