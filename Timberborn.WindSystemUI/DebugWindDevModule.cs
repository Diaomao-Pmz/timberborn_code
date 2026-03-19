using System;
using Timberborn.Debugging;
using Timberborn.QuickNotificationSystem;
using Timberborn.WindSystem;
using UnityEngine;

namespace Timberborn.WindSystemUI
{
	// Token: 0x02000004 RID: 4
	public class DebugWindDevModule : IDevModule
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public DebugWindDevModule(WindService windService, QuickNotificationService quickNotificationService)
		{
			this._windService = windService;
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle forced wind", new Action(this.ToggleForcedWind))).Build();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020FC File Offset: 0x000002FC
		public void ToggleForcedWind()
		{
			this._windService.ToggleForcedWind();
			Debug.Log(string.Format("Direction: ({0:F2}, {1:F2})", this._windService.WindDirection.x, this._windService.WindDirection.y) + string.Format(", Strength: {0:F3}", this._windService.WindStrength));
			this._quickNotificationService.SendNotification("Forced wind: " + (this._windService.IsForcedWind ? "ENABLED" : "DISABLED"));
		}

		// Token: 0x04000006 RID: 6
		public readonly WindService _windService;

		// Token: 0x04000007 RID: 7
		public readonly QuickNotificationService _quickNotificationService;
	}
}
