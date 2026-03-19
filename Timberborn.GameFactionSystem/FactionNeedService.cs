using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.LocalizationSerialization;
using Timberborn.NeedCollectionSystem;
using Timberborn.NeedSpecs;
using Timberborn.SingletonSystem;

namespace Timberborn.GameFactionSystem
{
	// Token: 0x0200000C RID: 12
	public class FactionNeedService : ILoadableSingleton
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000257F File Offset: 0x0000077F
		public FactionNeedService(NeedModificationService needModificationService, ISpecService specService, IEnumerable<INeedCollectionIdsProvider> needCollectionIdsProviders)
		{
			this._needModificationService = needModificationService;
			this._specService = specService;
			this._needCollectionIdsProviders = needCollectionIdsProviders.ToImmutableArray<INeedCollectionIdsProvider>();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025AC File Offset: 0x000007AC
		public ReadOnlyList<NeedSpec> Needs
		{
			get
			{
				return this._needs.AsReadOnlyList<NeedSpec>();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000025B9 File Offset: 0x000007B9
		public NeedSpec NullNeed
		{
			get
			{
				return FactionNeedService.NullNeedSpec;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000025C0 File Offset: 0x000007C0
		public void Load()
		{
			HashSet<string> hashSet = this._needCollectionIdsProviders.SelectMany((INeedCollectionIdsProvider provider) => provider.GetNeedCollectionIds()).ToHashSet<string>();
			HashSet<string> hashSet2 = new HashSet<string>();
			foreach (NeedCollectionSpec needCollectionSpec in this._specService.GetSpecs<NeedCollectionSpec>())
			{
				if (hashSet.Contains(needCollectionSpec.CollectionId))
				{
					hashSet2.AddRange(needCollectionSpec.Needs);
				}
			}
			foreach (NeedSpec needSpec2 in from needSpec in this._specService.GetSpecs<NeedSpec>()
			orderby needSpec.Order
			select needSpec)
			{
				if (hashSet2.Contains(needSpec2.Id) && needSpec2.Blueprint.IsAllowedByFeatureToggles())
				{
					this._needs.Add(this._needModificationService.ModifyIfEligible(needSpec2));
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002700 File Offset: 0x00000900
		public bool IsCurrentFactionNeed(string id)
		{
			return this._needs.Any((NeedSpec need) => need.GetSpec<NeedSpec>().Id == id);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002734 File Offset: 0x00000934
		public NeedSpec GetBeaverOrBotNeedById(string id)
		{
			NeedSpec needSpec = this.GetBeaverNeeds().SingleOrDefault((NeedSpec need) => need.Id == id) ?? this.GetBotNeeds().SingleOrDefault((NeedSpec need) => need.Id == id);
			if (needSpec != null)
			{
				return needSpec;
			}
			throw new InvalidOperationException("Need with id " + id + " not found or multiple needs found");
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027A6 File Offset: 0x000009A6
		public IEnumerable<NeedSpec> GetBeaverNeeds()
		{
			return from need in this._needs
			where need.CharacterType == "Beaver"
			select need;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027D2 File Offset: 0x000009D2
		public IEnumerable<NeedSpec> GetBotNeeds()
		{
			return from need in this._needs
			where need.CharacterType == "Bot"
			select need;
		}

		// Token: 0x04000020 RID: 32
		public static readonly NeedSpec NullNeedSpec = new NeedSpec
		{
			DisplayName = new LocalizedText("NullNeed"),
			MinimumValue = 0f,
			MaximumValue = 1f
		};

		// Token: 0x04000021 RID: 33
		public readonly NeedModificationService _needModificationService;

		// Token: 0x04000022 RID: 34
		public readonly ISpecService _specService;

		// Token: 0x04000023 RID: 35
		public readonly ImmutableArray<INeedCollectionIdsProvider> _needCollectionIdsProviders;

		// Token: 0x04000024 RID: 36
		public readonly List<NeedSpec> _needs = new List<NeedSpec>();
	}
}
