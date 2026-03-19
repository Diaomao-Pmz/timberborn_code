using System;
using Timberborn.Debugging;

namespace Timberborn.SkySystem
{
	// Token: 0x02000012 RID: 18
	public class SkySystemDevModule : IDevModule
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000329A File Offset: 0x0000149A
		public SkySystemDevModule(Sun sun)
		{
			this._sun = sun;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000032A9 File Offset: 0x000014A9
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Sky: Toggle fog", new Action(this.ToggleFog))).Build();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000032D0 File Offset: 0x000014D0
		public void ToggleFog()
		{
			this._sun.Fog = !this._sun.Fog;
		}

		// Token: 0x04000033 RID: 51
		public readonly Sun _sun;
	}
}
