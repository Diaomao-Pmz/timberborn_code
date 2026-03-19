using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Effects;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x0200000C RID: 12
	public class RangedEffect
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002452 File Offset: 0x00000652
		public ContinuousEffect BaseEffect { get; }

		// Token: 0x06000028 RID: 40 RVA: 0x0000245A File Offset: 0x0000065A
		public RangedEffect(ContinuousEffect baseEffect)
		{
			this.BaseEffect = baseEffect;
			this._appliers = new HashSet<RangedEffectApplier>();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002474 File Offset: 0x00000674
		public bool IsActive
		{
			get
			{
				return this._appliers.Any((RangedEffectApplier applier) => applier.Active);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000024A0 File Offset: 0x000006A0
		public IEnumerable<RangedEffectApplier> Appliers
		{
			get
			{
				return this._appliers;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024A8 File Offset: 0x000006A8
		public void Add(RangedEffectApplier rangedEffectApplier)
		{
			this._appliers.Add(rangedEffectApplier);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024B7 File Offset: 0x000006B7
		public void Remove(RangedEffectApplier rangedEffectApplier)
		{
			this._appliers.Remove(rangedEffectApplier);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024C8 File Offset: 0x000006C8
		public ContinuousEffect ToContinuousEffect()
		{
			return new ContinuousEffect(this.BaseEffect.NeedId, this.BaseEffect.PointsPerHour * this.GetEfficiency());
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002500 File Offset: 0x00000700
		public float GetEfficiency()
		{
			float num = 0f;
			foreach (RangedEffectApplier rangedEffectApplier in this._appliers)
			{
				if (rangedEffectApplier.Active)
				{
					if (rangedEffectApplier.Efficiency >= 1f)
					{
						return 1f;
					}
					if (rangedEffectApplier.Efficiency > num)
					{
						num = rangedEffectApplier.Efficiency;
					}
				}
			}
			return num;
		}

		// Token: 0x04000014 RID: 20
		public readonly HashSet<RangedEffectApplier> _appliers;
	}
}
