using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000027 RID: 39
	public class WaterChangeService : ITickableSingleton, ILateTickable
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000044B4 File Offset: 0x000026B4
		public ReadOnlyList<WaterChange> ThreadSafeWaterChanges
		{
			get
			{
				return this._threadSafeWaterChanges.AsReadOnlyList<WaterChange>();
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000044C1 File Offset: 0x000026C1
		public void Tick()
		{
			this._threadSafeWaterChanges.Clear();
			this._threadSafeWaterChanges.AddRange(this._waterChanges);
			this._waterChanges.Clear();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000044EA File Offset: 0x000026EA
		public void EnqueueWaterChange(Vector3Int coordinates, float depthChange, float contaminationChange)
		{
			this._waterChanges.Add(new WaterChange(coordinates, depthChange, contaminationChange));
		}

		// Token: 0x0400009A RID: 154
		public readonly List<WaterChange> _threadSafeWaterChanges = new List<WaterChange>();

		// Token: 0x0400009B RID: 155
		public readonly List<WaterChange> _waterChanges = new List<WaterChange>();
	}
}
