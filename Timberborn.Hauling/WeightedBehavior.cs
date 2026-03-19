using System;
using Timberborn.WorkSystem;

namespace Timberborn.Hauling
{
	// Token: 0x02000013 RID: 19
	public readonly struct WeightedBehavior
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002C00 File Offset: 0x00000E00
		public float Weight { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002C08 File Offset: 0x00000E08
		public WorkplaceBehavior WorkplaceBehavior { get; }

		// Token: 0x06000057 RID: 87 RVA: 0x00002C10 File Offset: 0x00000E10
		public WeightedBehavior(float weight, WorkplaceBehavior workplaceBehavior)
		{
			this.Weight = weight;
			this.WorkplaceBehavior = workplaceBehavior;
		}
	}
}
