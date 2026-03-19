using System;
using Timberborn.BlockSystem;
using Timberborn.BuildingsReachability;
using Timberborn.Localization;
using Timberborn.StartingLocationSystem;

namespace Timberborn.GameStartup
{
	// Token: 0x02000010 RID: 16
	public class StartingBuildingPlacementValidator : IBlockObjectValidator
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002671 File Offset: 0x00000871
		public StartingBuildingPlacementValidator(ILoc loc, GameInitializer gameInitializer)
		{
			this._loc = loc;
			this._gameInitializer = gameInitializer;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002687 File Offset: 0x00000887
		public bool IsValid(BlockObject blockObject, out string errorMessage)
		{
			if (this.IsNotValid(blockObject))
			{
				errorMessage = this._loc.T(StartingBuildingPlacementValidator.EntranceBlockedLocKey);
				return false;
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026AC File Offset: 0x000008AC
		public bool IsNotValid(BlockObject blockObject)
		{
			if (!this._gameInitializer.IsGameInitialized && blockObject.HasComponent<StartingLocationSpec>())
			{
				BlockableEntranceBuilding component = blockObject.GetComponent<BlockableEntranceBuilding>();
				if (component != null)
				{
					return component.IsEntranceInaccessible();
				}
			}
			return false;
		}

		// Token: 0x0400002A RID: 42
		public static readonly string EntranceBlockedLocKey = "Buildings.EntranceBlocked";

		// Token: 0x0400002B RID: 43
		public readonly ILoc _loc;

		// Token: 0x0400002C RID: 44
		public readonly GameInitializer _gameInitializer;
	}
}
