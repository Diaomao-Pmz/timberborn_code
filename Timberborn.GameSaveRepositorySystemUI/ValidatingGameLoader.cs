using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.GameSceneLoading;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000016 RID: 22
	public class ValidatingGameLoader
	{
		// Token: 0x06000077 RID: 119 RVA: 0x000036A4 File Offset: 0x000018A4
		public ValidatingGameLoader(GameSceneLoader gameSceneLoader, IEnumerable<IGameLoadValidator> gameLoadValidators)
		{
			this._gameSceneLoader = gameSceneLoader;
			this._gameLoadValidators = (from validator in gameLoadValidators
			orderby validator.Priority
			select validator).ToImmutableArray<IGameLoadValidator>();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000036E3 File Offset: 0x000018E3
		public void LoadGame(SaveReference saveReference)
		{
			this.CheckNextValidator(saveReference, 0);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000036F0 File Offset: 0x000018F0
		public void CheckNextValidator(SaveReference saveReference, int index)
		{
			if (index >= this._gameLoadValidators.Length)
			{
				this._gameSceneLoader.StartSaveGame(saveReference);
				return;
			}
			this._gameLoadValidators[index].ValidateSave(saveReference, delegate
			{
				this.CheckNextValidator(saveReference, index + 1);
			});
		}

		// Token: 0x04000065 RID: 101
		public readonly GameSceneLoader _gameSceneLoader;

		// Token: 0x04000066 RID: 102
		public readonly ImmutableArray<IGameLoadValidator> _gameLoadValidators;
	}
}
