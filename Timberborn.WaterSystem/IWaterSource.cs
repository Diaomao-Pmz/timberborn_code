using System;
using System.Collections.Immutable;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000018 RID: 24
	public interface IWaterSource
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006E RID: 110
		ImmutableArray<Vector3Int> Coordinates { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006F RID: 111
		float SpecifiedStrength { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000070 RID: 112
		float CurrentStrength { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000071 RID: 113
		float Contamination { get; }

		// Token: 0x06000072 RID: 114
		void SetSpecifiedStrength(float strength);
	}
}
