using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.FireworkSystem
{
	// Token: 0x02000012 RID: 18
	public class FireworkSpecService : ILoadableSingleton
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000034F0 File Offset: 0x000016F0
		public FireworkSpecService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003500 File Offset: 0x00001700
		public void Load()
		{
			this._fireworkSpecsById = this._specService.GetSpecs<FireworkSpec>().ToFrozenDictionary((FireworkSpec spec) => spec.FireworkId, null);
			this._fireworkIds = (from spec in this._fireworkSpecsById.Values
			orderby spec.DisplayName.Value
			select spec.FireworkId).ToImmutableArray<string>();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000035A6 File Offset: 0x000017A6
		public ImmutableArray<string> GetFireworkIds()
		{
			return this._fireworkIds;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000035B0 File Offset: 0x000017B0
		public FireworkSpec GetFireworkSpec(string id)
		{
			FireworkSpec result;
			if (!this._fireworkSpecsById.TryGetValue(id, out result))
			{
				throw new KeyNotFoundException("Firework spec for id '" + id + "' not found.");
			}
			return result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000035E4 File Offset: 0x000017E4
		public bool HasSpec(string id)
		{
			return this._fireworkSpecsById.ContainsKey(id);
		}

		// Token: 0x04000059 RID: 89
		public readonly ISpecService _specService;

		// Token: 0x0400005A RID: 90
		public ImmutableArray<string> _fireworkIds;

		// Token: 0x0400005B RID: 91
		public FrozenDictionary<string, FireworkSpec> _fireworkSpecsById;
	}
}
