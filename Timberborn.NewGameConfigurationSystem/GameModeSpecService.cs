using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.NewGameConfigurationSystem
{
	// Token: 0x02000008 RID: 8
	public class GameModeSpecService : ILoadableSingleton
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002E6C File Offset: 0x0000106C
		public GameModeSpecService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002E7C File Offset: 0x0000107C
		public void Load()
		{
			this._gameModeSpecsOrdered = (from spec in this._specService.GetSpecs<GameModeSpec>()
			orderby spec.Order
			select spec).ToImmutableArray<GameModeSpec>();
			this._defaultGameModeSpec = this._gameModeSpecsOrdered.First((GameModeSpec spec) => spec.IsDefault);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002EF3 File Offset: 0x000010F3
		public ImmutableArray<GameModeSpec> GetSpecsOrdered()
		{
			return this._gameModeSpecsOrdered;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002EFB File Offset: 0x000010FB
		public GameModeSpec GetDefaultSpec()
		{
			return this._defaultGameModeSpec;
		}

		// Token: 0x0400001F RID: 31
		public readonly ISpecService _specService;

		// Token: 0x04000020 RID: 32
		public ImmutableArray<GameModeSpec> _gameModeSpecsOrdered;

		// Token: 0x04000021 RID: 33
		public GameModeSpec _defaultGameModeSpec;
	}
}
