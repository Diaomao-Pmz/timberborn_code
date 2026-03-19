using System;
using Timberborn.KeyBindingSystem;

namespace Timberborn.Debugging
{
	// Token: 0x0200000A RID: 10
	public class DevModeKeyBindingBlocker : IKeyBindingBlocker
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000022F6 File Offset: 0x000004F6
		public DevModeKeyBindingBlocker(DevModeManager devModeManager)
		{
			this._devModeManager = devModeManager;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002305 File Offset: 0x00000505
		public bool IsKeyBlocked(KeyBinding keyBinding)
		{
			return keyBinding.DevModeOnly && !this._devModeManager.Enabled;
		}

		// Token: 0x04000012 RID: 18
		public readonly DevModeManager _devModeManager;
	}
}
