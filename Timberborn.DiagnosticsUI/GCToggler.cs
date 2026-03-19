using System;
using Timberborn.Debugging;
using Timberborn.QuickNotificationSystem;
using UnityEngine;
using UnityEngine.Scripting;

namespace Timberborn.DiagnosticsUI
{
	// Token: 0x02000008 RID: 8
	public class GCToggler : IDevModule
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022DD File Offset: 0x000004DD
		public GCToggler(QuickNotificationService quickNotificationService)
		{
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022EC File Offset: 0x000004EC
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle GC", new Action(this.ToggleGC))).Build();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002314 File Offset: 0x00000514
		public void ToggleGC()
		{
			if (Application.isEditor)
			{
				this._quickNotificationService.SendNotification("Can't toggle GC in editor");
				return;
			}
			GarbageCollector.Mode gcmode = GarbageCollector.GCMode;
			if (gcmode == null)
			{
				GarbageCollector.GCMode = 1;
				this._quickNotificationService.SendNotification("Enabled GC");
				return;
			}
			if (gcmode == 1)
			{
				GarbageCollector.GCMode = 0;
				this._quickNotificationService.SendNotification("Disabled GC");
				return;
			}
			throw new ArgumentOutOfRangeException(string.Format("Unexpected GCMode: {0}", GarbageCollector.GCMode));
		}

		// Token: 0x0400000F RID: 15
		public readonly QuickNotificationService _quickNotificationService;
	}
}
