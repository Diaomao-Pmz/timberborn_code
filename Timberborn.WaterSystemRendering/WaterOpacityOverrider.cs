using System;
using Timberborn.Debugging;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x02000017 RID: 23
	public class WaterOpacityOverrider : IDevModule
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00004CB8 File Offset: 0x00002EB8
		public WaterOpacityOverrider(WaterOpacityService waterOpacityService)
		{
			this._waterOpacityService = waterOpacityService;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004CC7 File Offset: 0x00002EC7
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Force water on", new Action(this.ToggleOpacityOverride))).Build();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004CEE File Offset: 0x00002EEE
		public void ToggleOpacityOverride()
		{
			this._waterOpacityService.ToggleOpacityOverride();
		}

		// Token: 0x0400009C RID: 156
		public readonly WaterOpacityService _waterOpacityService;
	}
}
