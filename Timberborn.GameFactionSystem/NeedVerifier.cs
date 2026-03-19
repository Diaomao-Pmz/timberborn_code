using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.FactionSystem;
using Timberborn.NeedCollectionSystem;
using Timberborn.NeedSpecs;
using Timberborn.SingletonSystem;

namespace Timberborn.GameFactionSystem
{
	// Token: 0x02000014 RID: 20
	public class NeedVerifier : ILoadableSingleton
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002C2C File Offset: 0x00000E2C
		public NeedVerifier(FactionSpecService factionSpecService, NeedGroupSpecService needGroupSpecService, ISpecService specService, CommonNeedCollectionIdsProvider commonNeedCollectionIdsProvider)
		{
			this._factionSpecService = factionSpecService;
			this._needGroupSpecService = needGroupSpecService;
			this._specService = specService;
			this._commonNeedCollectionIdsProvider = commonNeedCollectionIdsProvider;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C54 File Offset: 0x00000E54
		public void Load()
		{
			List<NeedSpec> needSpecs = this._specService.GetSpecs<NeedSpec>().ToList<NeedSpec>();
			ImmutableArray<FactionSpec> factions = this._factionSpecService.Factions;
			List<NeedCollectionSpec> needCollections = this._specService.GetSpecs<NeedCollectionSpec>().ToList<NeedCollectionSpec>();
			NeedVerifier.VerifyAllNeedsAreUsed(needSpecs, needCollections);
			this.VerifyAllCollectionsAreUsed(needCollections, factions);
			NeedVerifier.VerifyAllNeedsExist(needSpecs, needCollections);
			this.VerifyGroupOfNeeds(needSpecs);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public static void VerifyAllNeedsAreUsed(IEnumerable<NeedSpec> needSpecs, ICollection<NeedCollectionSpec> needCollections)
		{
			using (IEnumerator<NeedSpec> enumerator = needSpecs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NeedSpec needSpec = enumerator.Current;
					if (!needCollections.Any((NeedCollectionSpec needCollection) => needCollection.Needs.Contains(needSpec.Id)))
					{
						throw new Exception("NeedSpec with id " + needSpec.Id + " is not used!");
					}
				}
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D34 File Offset: 0x00000F34
		public void VerifyAllCollectionsAreUsed(ICollection<NeedCollectionSpec> needCollections, ICollection<FactionSpec> allFactionSpecs)
		{
			string b = this._commonNeedCollectionIdsProvider.GetNeedCollectionIds().Single<string>();
			using (IEnumerator<string> enumerator = (from needCollection in needCollections
			select needCollection.CollectionId).Distinct<string>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string needCollectionId = enumerator.Current;
					if (needCollectionId != b && !allFactionSpecs.Any((FactionSpec faction) => faction.NeedCollectionIds.Contains(needCollectionId)))
					{
						throw new Exception("NeedCollectionSpec with id " + needCollectionId + " is not used!");
					}
				}
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public static void VerifyAllNeedsExist(IReadOnlyCollection<NeedSpec> needSpecs, IEnumerable<NeedCollectionSpec> needCollections)
		{
			foreach (NeedCollectionSpec needCollectionSpec in needCollections)
			{
				ImmutableArray<string>.Enumerator enumerator2 = needCollectionSpec.Needs.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					string need = enumerator2.Current;
					if (needSpecs.All((NeedSpec needSpec) => needSpec.Id != need))
					{
						throw new Exception("There is no NeedSpec with id " + need);
					}
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E90 File Offset: 0x00001090
		public void VerifyGroupOfNeeds(IEnumerable<NeedSpec> needSpecs)
		{
			foreach (NeedSpec needSpec in needSpecs)
			{
				this.VerifyGroupOfNeed(needSpec);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002ED8 File Offset: 0x000010D8
		public void VerifyGroupOfNeed(NeedSpec needSpec)
		{
			string needGroupId = needSpec.NeedGroupId;
			if (!this._needGroupSpecService.IsValidGroup(needGroupId))
			{
				throw new Exception("There is no NeedGroupSpec with id " + needGroupId);
			}
		}

		// Token: 0x0400003F RID: 63
		public readonly FactionSpecService _factionSpecService;

		// Token: 0x04000040 RID: 64
		public readonly NeedGroupSpecService _needGroupSpecService;

		// Token: 0x04000041 RID: 65
		public readonly ISpecService _specService;

		// Token: 0x04000042 RID: 66
		public readonly CommonNeedCollectionIdsProvider _commonNeedCollectionIdsProvider;
	}
}
