using System;
using Timberborn.Debugging;
using Timberborn.ScienceSystem;

namespace Timberborn.ScienceSystemUI
{
	// Token: 0x02000005 RID: 5
	public class ScienceAdder : IDevModule
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000217F File Offset: 0x0000037F
		public ScienceAdder(ScienceService scienceService)
		{
			this._scienceService = scienceService;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000218E File Offset: 0x0000038E
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Add 1000 Science", new Action(this.AddScience))).Build();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021B5 File Offset: 0x000003B5
		public void AddScience()
		{
			this._scienceService.AddPoints(1000);
		}

		// Token: 0x0400000B RID: 11
		public readonly ScienceService _scienceService;
	}
}
