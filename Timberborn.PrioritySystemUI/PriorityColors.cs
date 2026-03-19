using System;
using Timberborn.BlueprintSystem;
using Timberborn.PrioritySystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.PrioritySystemUI
{
	// Token: 0x02000008 RID: 8
	public class PriorityColors : ILoadableSingleton
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020FE File Offset: 0x000002FE
		public PriorityColors(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000210D File Offset: 0x0000030D
		public void Load()
		{
			this._priorityColorsSpec = this._specService.GetSingleSpec<PriorityColorsSpec>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002120 File Offset: 0x00000320
		public Color GetHighlightColor(Priority priority)
		{
			switch (priority)
			{
			case Priority.VeryLow:
				return this._priorityColorsSpec.HighlightVeryLow;
			case Priority.Low:
				return this._priorityColorsSpec.HighlightLow;
			case Priority.Normal:
				return this._priorityColorsSpec.HighlightNormal;
			case Priority.High:
				return this._priorityColorsSpec.HighlightHigh;
			case Priority.VeryHigh:
				return this._priorityColorsSpec.HighlightVeryHigh;
			default:
				throw new ArgumentOutOfRangeException("priority", priority, null);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002198 File Offset: 0x00000398
		public Color GetButtonColor(Priority priority)
		{
			switch (priority)
			{
			case Priority.VeryLow:
				return this._priorityColorsSpec.ButtonVeryLow;
			case Priority.Low:
				return this._priorityColorsSpec.ButtonLow;
			case Priority.Normal:
				return this._priorityColorsSpec.ButtonNormal;
			case Priority.High:
				return this._priorityColorsSpec.ButtonHigh;
			case Priority.VeryHigh:
				return this._priorityColorsSpec.ButtonVeryHigh;
			default:
				throw new ArgumentOutOfRangeException("priority", priority, null);
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly ISpecService _specService;

		// Token: 0x04000009 RID: 9
		public PriorityColorsSpec _priorityColorsSpec;
	}
}
