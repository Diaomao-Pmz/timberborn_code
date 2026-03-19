using System;
using Timberborn.Debugging;

namespace Timberborn.DiagnosticsUI
{
	// Token: 0x02000009 RID: 9
	public class GCTrigger : IDevModule
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000238D File Offset: 0x0000058D
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Trigger GC", new Action(GC.Collect))).Build();
		}
	}
}
