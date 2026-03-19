using System;
using Timberborn.Debugging;
using Timberborn.QuickNotificationSystem;

namespace Timberborn.MortalSystem
{
	// Token: 0x0200000D RID: 13
	public class LongLastingCorpsesDevModule : IDevModule
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000028ED File Offset: 0x00000AED
		public LongLastingCorpsesDevModule(LongLastingCorpsesService longLastingCorpsesService, QuickNotificationService quickNotificationService)
		{
			this._longLastingCorpsesService = longLastingCorpsesService;
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002903 File Offset: 0x00000B03
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle long lasting corpses", new Action(this.ToggleLongLastingCorpses))).Build();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000292A File Offset: 0x00000B2A
		public void ToggleLongLastingCorpses()
		{
			this._longLastingCorpsesService.Toggle();
			this._quickNotificationService.SendNotification("Long lasting corpses " + (this._longLastingCorpsesService.Enabled ? "enabled" : "disabled"));
		}

		// Token: 0x0400001D RID: 29
		public readonly LongLastingCorpsesService _longLastingCorpsesService;

		// Token: 0x0400001E RID: 30
		public readonly QuickNotificationService _quickNotificationService;
	}
}
