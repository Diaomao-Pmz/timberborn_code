using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.MapRepositorySystem;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x0200000E RID: 14
	public class MapValidator
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002A22 File Offset: 0x00000C22
		public MapValidator(IEnumerable<IMapLoadValidator> mapLoadValidators)
		{
			this._mapLoadValidators = (from validator in mapLoadValidators
			orderby validator.Priority
			select validator).ToImmutableArray<IMapLoadValidator>();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A5A File Offset: 0x00000C5A
		public void ValidateForNewGame(MapFileReference mapFileReference, Action continueCallback)
		{
			this.CheckNextValidator(mapFileReference, continueCallback, 0, true);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A66 File Offset: 0x00000C66
		public void ValidateForMapEditor(MapFileReference mapFileReference, Action continueCallback)
		{
			this.CheckNextValidator(mapFileReference, continueCallback, 0, false);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A74 File Offset: 0x00000C74
		public void CheckNextValidator(MapFileReference mapFileReference, Action continueCallback, int index, bool isForNewGame)
		{
			if (index >= this._mapLoadValidators.Length)
			{
				continueCallback();
				return;
			}
			IMapLoadValidator mapLoadValidator = this._mapLoadValidators[index];
			if (isForNewGame)
			{
				mapLoadValidator.ValidateForNewGame(mapFileReference, delegate
				{
					this.CheckNextValidator(mapFileReference, continueCallback, index + 1, true);
				});
				return;
			}
			mapLoadValidator.ValidateForMapEditor(mapFileReference, delegate
			{
				this.CheckNextValidator(mapFileReference, continueCallback, index + 1, false);
			});
		}

		// Token: 0x0400002D RID: 45
		public readonly ImmutableArray<IMapLoadValidator> _mapLoadValidators;
	}
}
