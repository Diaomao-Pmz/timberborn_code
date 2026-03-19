using System;
using System.Collections.Immutable;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000022 RID: 34
	public readonly struct ThreadSafeWaterSource
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003DDC File Offset: 0x00001FDC
		public float CurrentStrength { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public float Contamination { get; }

		// Token: 0x060000AF RID: 175 RVA: 0x00003DEC File Offset: 0x00001FEC
		public ThreadSafeWaterSource(IWaterSource waterSource)
		{
			this._waterSource = waterSource;
			this.CurrentStrength = waterSource.CurrentStrength;
			this.Contamination = waterSource.Contamination;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003E0D File Offset: 0x0000200D
		public ImmutableArray<Vector3Int> Coordinates
		{
			get
			{
				return this._waterSource.Coordinates;
			}
		}

		// Token: 0x04000078 RID: 120
		public readonly IWaterSource _waterSource;
	}
}
