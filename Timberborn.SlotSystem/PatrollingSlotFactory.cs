using System;
using Timberborn.Common;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.SlotSystem
{
	// Token: 0x0200000D RID: 13
	public class PatrollingSlotFactory
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002797 File Offset: 0x00000997
		public PatrollingSlotFactory(IRandomNumberGenerator randomNumberGenerator, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027AD File Offset: 0x000009AD
		public PatrollingSlot Create(Transform slotTransform, Transform start, Transform end, PatrollingSlotSpec patrollingSlotSpec)
		{
			return new PatrollingSlot(this._randomNumberGenerator, slotTransform, start, end, patrollingSlotSpec, this._threadSafeWaterMap);
		}

		// Token: 0x04000013 RID: 19
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000014 RID: 20
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;
	}
}
