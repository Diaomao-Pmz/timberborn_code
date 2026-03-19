using System;
using Timberborn.Debugging;
using Timberborn.SimulationSystem;

namespace Timberborn.WaterSystemUI
{
	// Token: 0x02000006 RID: 6
	public class WaterSystemDevModule : IDevModule
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000266C File Offset: 0x0000086C
		public WaterSystemDevModule(SimulationController simulationController)
		{
			this._simulationController = simulationController;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000267B File Offset: 0x0000087B
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Water simulation: Reset all", new Action(this.Reset))).Build();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026A2 File Offset: 0x000008A2
		public void Reset()
		{
			this._simulationController.ResetSimulation();
		}

		// Token: 0x0400001C RID: 28
		public readonly SimulationController _simulationController;
	}
}
