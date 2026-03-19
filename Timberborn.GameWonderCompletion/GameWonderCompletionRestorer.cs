using System;
using Timberborn.SingletonSystem;

namespace Timberborn.GameWonderCompletion
{
	// Token: 0x02000009 RID: 9
	public class GameWonderCompletionRestorer : IPostLoadableSingleton
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002472 File Offset: 0x00000672
		public GameWonderCompletionRestorer(GameWonderCompletionService gameWonderCompletionService, WonderCompletionCountdownStarter wonderCompletionCountdownStarter)
		{
			this._gameWonderCompletionService = gameWonderCompletionService;
			this._wonderCompletionCountdownStarter = wonderCompletionCountdownStarter;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002488 File Offset: 0x00000688
		public void PostLoad()
		{
			this.CompleteWonderIfDataIsLost();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002490 File Offset: 0x00000690
		public void CompleteWonderIfDataIsLost()
		{
			if (this._wonderCompletionCountdownStarter.CountdownFinished && !this._gameWonderCompletionService.IsWonderCompletedWithAnyFaction())
			{
				this._gameWonderCompletionService.CompleteWonder();
			}
		}

		// Token: 0x0400000E RID: 14
		public readonly GameWonderCompletionService _gameWonderCompletionService;

		// Token: 0x0400000F RID: 15
		public readonly WonderCompletionCountdownStarter _wonderCompletionCountdownStarter;
	}
}
