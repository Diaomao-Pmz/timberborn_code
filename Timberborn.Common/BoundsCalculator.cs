using System;
using System.Linq;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x0200000B RID: 11
	public class BoundsCalculator
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000026DA File Offset: 0x000008DA
		public float GetRendererYMaxBound(Transform parent)
		{
			return BoundsCalculator.GetRendererYMaxBoundInternal(parent, true);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026E3 File Offset: 0x000008E3
		public float GetEnabledRendererYMaxBound(Transform parent)
		{
			return BoundsCalculator.GetRendererYMaxBoundInternal(parent, false);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026EC File Offset: 0x000008EC
		public static float GetRendererYMaxBoundInternal(Transform parent, bool includeInactive)
		{
			return (from c in parent.GetComponentsInChildren<Renderer>(includeInactive)
			select c.bounds.max.y).DefaultIfEmpty(0f).Max();
		}
	}
}
