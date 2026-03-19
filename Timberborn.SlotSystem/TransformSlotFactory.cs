using System;
using Timberborn.Common;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.SlotSystem
{
	// Token: 0x0200001E RID: 30
	public class TransformSlotFactory
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00003B99 File Offset: 0x00001D99
		public TransformSlotFactory(IRandomNumberGenerator randomNumberGenerator, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003BAF File Offset: 0x00001DAF
		public TransformSlot Create(Transform followedTransform, TransformSlotSpec transformSlotSpec)
		{
			return new TransformSlot(this._randomNumberGenerator, this._threadSafeWaterMap, followedTransform, transformSlotSpec);
		}

		// Token: 0x0400003D RID: 61
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400003E RID: 62
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;
	}
}
