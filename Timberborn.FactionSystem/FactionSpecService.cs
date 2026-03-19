using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.FactionSystem
{
	// Token: 0x02000008 RID: 8
	public class FactionSpecService : ILoadableSingleton
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002D68 File Offset: 0x00000F68
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002D70 File Offset: 0x00000F70
		public ImmutableArray<FactionSpec> Factions { get; private set; }

		// Token: 0x0600004D RID: 77 RVA: 0x00002D79 File Offset: 0x00000F79
		public FactionSpecService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D88 File Offset: 0x00000F88
		public IEnumerable<UnlockableFactionSpec> UnlockableFactions
		{
			get
			{
				return from faction in this.Factions
				select faction.GetSpec<UnlockableFactionSpec>() into unlockableFactionSpec
				where unlockableFactionSpec != null
				select unlockableFactionSpec;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002DE3 File Offset: 0x00000FE3
		public void Load()
		{
			this.Factions = this._specService.GetSpecs<FactionSpec>().ToImmutableArray<FactionSpec>();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002DFC File Offset: 0x00000FFC
		public FactionSpec GetFaction(string id)
		{
			FactionSpec factionSpec = this.Factions.SingleOrDefault((FactionSpec faction) => faction.Id == id);
			if (factionSpec == null)
			{
				throw new ArgumentException("Faction with id " + id + " not found.");
			}
			return factionSpec;
		}

		// Token: 0x04000025 RID: 37
		public readonly ISpecService _specService;
	}
}
