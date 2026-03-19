using System;
using Timberborn.SimulationSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.MapEditorSimulationSystem
{
	// Token: 0x02000003 RID: 3
	public class MapEditorSimulation : ILoadableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public int SimulationSpeed { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public MapEditorSimulation(SimulationController simulationController, SpeedManager speedManager)
		{
			this._simulationController = simulationController;
			this._speedManager = speedManager;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E5 File Offset: 0x000002E5
		public void Load()
		{
			this.SetSimulationSpeed(0);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EE File Offset: 0x000002EE
		public void SetSimulationSpeed(int simulationSpeed)
		{
			this.SimulationSpeed = simulationSpeed;
			this._speedManager.ChangeSpeed((float)simulationSpeed);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002104 File Offset: 0x00000304
		public void ResetSimulation()
		{
			this._simulationController.ResetSimulation();
		}

		// Token: 0x04000002 RID: 2
		private readonly SimulationController _simulationController;

		// Token: 0x04000003 RID: 3
		private readonly SpeedManager _speedManager;
	}
}
