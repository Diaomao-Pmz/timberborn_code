using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Effects;
using Timberborn.GameFactionSystem;
using Timberborn.NeedSpecs;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.SoakedEffects
{
	// Token: 0x0200000A RID: 10
	public class SoakedEffectService : ILoadableSingleton
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000234C File Offset: 0x0000054C
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002354 File Offset: 0x00000554
		public ImmutableArray<InstantEffect> Effects { get; private set; }

		// Token: 0x0600001D RID: 29 RVA: 0x0000235D File Offset: 0x0000055D
		public SoakedEffectService(FactionNeedService factionNeedService, IDayNightCycle dayNightCycle)
		{
			this._factionNeedService = factionNeedService;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002373 File Offset: 0x00000573
		public void Load()
		{
			this.Effects = this.CreateEffects(this._factionNeedService.Needs).ToImmutableArray<InstantEffect>();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002396 File Offset: 0x00000596
		public IEnumerable<InstantEffect> CreateEffects(IEnumerable<NeedSpec> needSpecs)
		{
			foreach (NeedSpec needSpec in needSpecs)
			{
				NeedAffectedBySoakednessSpec spec = needSpec.GetSpec<NeedAffectedBySoakednessSpec>();
				if (spec != null)
				{
					float points = spec.PointsPerHour * this._dayNightCycle.FixedDeltaTimeInHours;
					yield return new InstantEffect(needSpec.Id, points, 1);
				}
			}
			IEnumerator<NeedSpec> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0400000E RID: 14
		public readonly FactionNeedService _factionNeedService;

		// Token: 0x0400000F RID: 15
		public readonly IDayNightCycle _dayNightCycle;
	}
}
