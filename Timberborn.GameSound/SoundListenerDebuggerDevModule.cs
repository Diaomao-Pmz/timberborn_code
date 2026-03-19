using System;
using Timberborn.Debugging;

namespace Timberborn.GameSound
{
	// Token: 0x0200000F RID: 15
	public class SoundListenerDebuggerDevModule : IDevModule
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003011 File Offset: 0x00001211
		public SoundListenerDebuggerDevModule(SoundListenerDebugger soundListenerDebugger)
		{
			this._soundListenerDebugger = soundListenerDebugger;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003020 File Offset: 0x00001220
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle sound listener debugger", new Action(this.ToggleDebugger))).Build();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003047 File Offset: 0x00001247
		public void ToggleDebugger()
		{
			this._soundListenerDebugger.ToggleActive();
		}

		// Token: 0x0400002F RID: 47
		public readonly SoundListenerDebugger _soundListenerDebugger;
	}
}
