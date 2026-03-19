using System;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000024 RID: 36
	public class KeyReboundEvent
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004601 File Offset: 0x00002801
		public string KeyBindingId { get; }

		// Token: 0x06000103 RID: 259 RVA: 0x00004609 File Offset: 0x00002809
		public KeyReboundEvent(string keyBindingId)
		{
			this.KeyBindingId = keyBindingId;
		}
	}
}
