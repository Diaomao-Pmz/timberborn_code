using System;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x0200000C RID: 12
	public interface IKeyBindingBlocker
	{
		// Token: 0x0600002A RID: 42
		bool IsKeyBlocked(KeyBinding keyBinding);
	}
}
