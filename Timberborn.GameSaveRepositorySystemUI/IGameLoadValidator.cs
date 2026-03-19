using System;
using Timberborn.GameSaveRepositorySystem;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x0200000A RID: 10
	public interface IGameLoadValidator
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33
		int Priority { get; }

		// Token: 0x06000022 RID: 34
		void ValidateSave(SaveReference saveReference, Action continueCallback);
	}
}
