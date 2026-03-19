using System;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Debugging
{
	// Token: 0x02000006 RID: 6
	public class DebugModeManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002197 File Offset: 0x00000397
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000219F File Offset: 0x0000039F
		public bool Enabled { get; private set; }

		// Token: 0x0600000C RID: 12 RVA: 0x000021A8 File Offset: 0x000003A8
		public DebugModeManager(EventBus eventBus)
		{
			this._eventBus = eventBus;
			this.Enabled = false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021BE File Offset: 0x000003BE
		public void Enable()
		{
			if (!this.Enabled)
			{
				Debug.Log("Debug mode enabled");
				this.Enabled = true;
				this._eventBus.Post(new DebugModeToggledEvent(true));
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021EA File Offset: 0x000003EA
		public void Disable()
		{
			if (this.Enabled)
			{
				this.Enabled = false;
				this._eventBus.Post(new DebugModeToggledEvent(false));
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly EventBus _eventBus;
	}
}
