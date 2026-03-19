using System;
using UnityEngine;

namespace Timberborn.Debugging
{
	// Token: 0x02000008 RID: 8
	public class DevMethod
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002223 File Offset: 0x00000423
		public string Name { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000222B File Offset: 0x0000042B
		public string KeyBindingId { get; }

		// Token: 0x06000013 RID: 19 RVA: 0x00002233 File Offset: 0x00000433
		public DevMethod(string name, string keyBindingId, Action action)
		{
			this.Name = name;
			this.KeyBindingId = keyBindingId;
			this._action = action;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002250 File Offset: 0x00000450
		public static DevMethod Create(string name, Action action)
		{
			return new DevMethod(name, null, action);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000225A File Offset: 0x0000045A
		public static DevMethod CreateBindable(string name, string keyBindingId, Action action)
		{
			return new DevMethod(name, keyBindingId, action);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002264 File Offset: 0x00000464
		public void Invoke()
		{
			Debug.Log("Dev mode: " + this.Name);
			this._action();
		}

		// Token: 0x0400000E RID: 14
		public readonly Action _action;
	}
}
