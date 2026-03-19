using System;
using Timberborn.MapRepositorySystem;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x02000005 RID: 5
	public interface IMapLoadValidator
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6
		int Priority { get; }

		// Token: 0x06000007 RID: 7
		void ValidateForNewGame(MapFileReference mapFileReference, Action continueCallback);

		// Token: 0x06000008 RID: 8
		void ValidateForMapEditor(MapFileReference mapFileReference, Action continueCallback);
	}
}
