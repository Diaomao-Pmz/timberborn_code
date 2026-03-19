using System;

namespace Timberborn.ToolSystem
{
	// Token: 0x0200000D RID: 13
	public interface IToolLocker
	{
		// Token: 0x0600000D RID: 13
		bool ShouldLock(ITool tool);

		// Token: 0x0600000E RID: 14
		void TryToUnlock(ITool tool, Action successCallback, Action failCallback);
	}
}
