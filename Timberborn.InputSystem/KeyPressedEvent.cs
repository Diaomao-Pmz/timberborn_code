using System;

namespace Timberborn.InputSystem
{
	// Token: 0x02000013 RID: 19
	public readonly struct KeyPressedEvent
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000300F File Offset: 0x0000120F
		public string Key { get; }

		// Token: 0x0600008A RID: 138 RVA: 0x00003017 File Offset: 0x00001217
		public KeyPressedEvent(string key)
		{
			this.Key = key;
		}
	}
}
