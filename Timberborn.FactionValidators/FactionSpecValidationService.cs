using System;
using System.Collections.Generic;
using Timberborn.FactionSystem;
using Timberborn.Modding;
using Timberborn.SingletonSystem;

namespace Timberborn.FactionValidators
{
	// Token: 0x0200000C RID: 12
	public class FactionSpecValidationService : ILoadableSingleton
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002361 File Offset: 0x00000561
		public FactionSpecValidationService(FactionSpecService factionSpecService, IEnumerable<IFactionSpecValidator> factionSpecValidators)
		{
			this._factionSpecService = factionSpecService;
			this._factionSpecValidators = factionSpecValidators;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002378 File Offset: 0x00000578
		public void Load()
		{
			if (!ModdedState.IsModded)
			{
				foreach (FactionSpec factionSpec in this._factionSpecService.Factions)
				{
					this.ValidateFaction(factionSpec);
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023BC File Offset: 0x000005BC
		public void ValidateFaction(FactionSpec factionSpec)
		{
			using (IEnumerator<IFactionSpecValidator> enumerator = this._factionSpecValidators.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string str;
					if (!enumerator.Current.IsValid(factionSpec, out str))
					{
						throw new Exception("Faction " + factionSpec.Id + " load error: " + str);
					}
				}
			}
		}

		// Token: 0x04000012 RID: 18
		public readonly FactionSpecService _factionSpecService;

		// Token: 0x04000013 RID: 19
		public readonly IEnumerable<IFactionSpecValidator> _factionSpecValidators;
	}
}
