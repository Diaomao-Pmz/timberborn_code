using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000009 RID: 9
	public class DecalService : IDecalService, ILoadableSingleton
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002249 File Offset: 0x00000449
		public DecalService(FactionService factionService, ISpecService specService, UserDecalService userDecalService, EventBus eventBus)
		{
			this._factionService = factionService;
			this._specService = specService;
			this._userDecalService = userDecalService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000227C File Offset: 0x0000047C
		public void Load()
		{
			this._decalCategories = (from decalSpec in this._specService.GetSpecs<DecalSpec>()
			where string.IsNullOrWhiteSpace(decalSpec.FactionId) || decalSpec.FactionId == this._factionService.Current.Id
			group decalSpec by decalSpec.Category).ToDictionary((IGrouping<string, DecalSpec> group) => group.Key, (IGrouping<string, DecalSpec> group) => new DecalCategory(group));
			foreach (string category in this._decalCategories.Keys)
			{
				this.LoadCustomDecals(category);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002360 File Offset: 0x00000560
		public IEnumerable<Decal> GetDecals(string category)
		{
			DecalService.<>c__DisplayClass8_0 CS$<>8__locals1 = new DecalService.<>c__DisplayClass8_0();
			DecalService.<>c__DisplayClass8_0 CS$<>8__locals2 = CS$<>8__locals1;
			List<string> list;
			IEnumerable<string> customDecals;
			if (!this._customDecalIds.TryGetValue(category, out list))
			{
				customDecals = Enumerable.Empty<string>();
			}
			else
			{
				IEnumerable<string> enumerable = list;
				customDecals = enumerable;
			}
			CS$<>8__locals2.customDecals = customDecals;
			return from spec in this._decalCategories[category].CategorySpecs
			orderby CS$<>8__locals1.customDecals.Contains(spec.Id), spec.Id
			select new Decal(spec.Id, spec.Category);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002400 File Offset: 0x00000600
		public Decal GetValidatedDecal(Decal decal)
		{
			if (decal.Category == null)
			{
				throw new ArgumentException("Decal category cannot be null.", "decal");
			}
			DecalCategory decalCategory;
			DecalSpec decalSpec;
			if (!decal.IsEmpty && this._decalCategories.TryGetValue(decal.Category, out decalCategory) && decalCategory.TryGet(decal.Id, out decalSpec))
			{
				return new Decal(decalSpec.Id, decalSpec.Category);
			}
			return new Decal(this._decalCategories[decal.Category].CategorySpecs.First<DecalSpec>().Id, decal.Category);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002495 File Offset: 0x00000695
		public Texture2D GetDecalTexture(Decal decal)
		{
			return this._decalCategories[decal.Category].GetDecalTexture(decal);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024B0 File Offset: 0x000006B0
		public void ReloadCustomDecals(string category)
		{
			DecalCategory decalCategory = this._decalCategories[category];
			List<string> list = this._customDecalIds[category];
			foreach (string decalId in list)
			{
				decalCategory.Remove(decalId);
			}
			list.Clear();
			this.LoadCustomDecals(category);
			this._eventBus.Post(new DecalsReloadedEvent());
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002538 File Offset: 0x00000738
		public void LoadCustomDecals(string category)
		{
			DecalCategory decalCategory = this._decalCategories[category];
			List<string> orAdd = this._customDecalIds.GetOrAdd(category, () => new List<string>());
			foreach (DecalSpec decalSpec in this._userDecalService.GetCustomDecals(category))
			{
				if (decalCategory.TryAdd(decalSpec))
				{
					orAdd.Add(decalSpec.Id);
				}
			}
		}

		// Token: 0x0400000B RID: 11
		public readonly FactionService _factionService;

		// Token: 0x0400000C RID: 12
		public readonly ISpecService _specService;

		// Token: 0x0400000D RID: 13
		public readonly UserDecalService _userDecalService;

		// Token: 0x0400000E RID: 14
		public readonly EventBus _eventBus;

		// Token: 0x0400000F RID: 15
		public Dictionary<string, DecalCategory> _decalCategories;

		// Token: 0x04000010 RID: 16
		public readonly Dictionary<string, List<string>> _customDecalIds = new Dictionary<string, List<string>>();
	}
}
