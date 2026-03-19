using System;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.BonusSystem;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Yielding
{
	// Token: 0x02000018 RID: 24
	public class YieldRemovalChanceBonusService : ILoadableSingleton
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000038D1 File Offset: 0x00001AD1
		public YieldRemovalChanceBonusService(ISpecService specService, IRandomNumberGenerator randomNumberGenerator)
		{
			this._specService = specService;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000038E8 File Offset: 0x00001AE8
		public void Load()
		{
			this._yieldRemovalChanceBonusTypes = (from spec in this._specService.GetSpecs<YieldRemovalChanceBonusSpec>()
			group spec by spec.GoodId).ToFrozenDictionary((IGrouping<string, YieldRemovalChanceBonusSpec> spec) => spec.Key, (IGrouping<string, YieldRemovalChanceBonusSpec> spec) => (from s in spec
			select s.BonusId).ToImmutableList<string>(), null);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003970 File Offset: 0x00001B70
		public bool CheckYieldRemovalSuccess(BonusManager bonusManager, string goodId)
		{
			ImmutableList<string> immutableList;
			return !this._yieldRemovalChanceBonusTypes.TryGetValue(goodId, out immutableList) || immutableList.Count == 0 || this.CheckBonusProbability(bonusManager, immutableList);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000039A4 File Offset: 0x00001BA4
		public bool CheckBonusProbability(BonusManager bonusManager, ImmutableList<string> bonusTypes)
		{
			float num = 1f;
			foreach (string bonusType in bonusTypes)
			{
				num *= bonusManager.Multiplier(bonusType);
			}
			return this._randomNumberGenerator.CheckProbability(num);
		}

		// Token: 0x04000044 RID: 68
		public readonly ISpecService _specService;

		// Token: 0x04000045 RID: 69
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000046 RID: 70
		public FrozenDictionary<string, ImmutableList<string>> _yieldRemovalChanceBonusTypes;
	}
}
