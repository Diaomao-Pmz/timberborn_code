using System;
using Timberborn.MapEditorTickSystem;
using Timberborn.TickSystem;

namespace Timberborn.SimulationSystem
{
	// Token: 0x02000004 RID: 4
	[MapEditorTickable]
	public class SimulationController : ITickableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C3 File Offset: 0x000002C3
		public bool ShouldResetSimulation { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CC File Offset: 0x000002CC
		public void Tick()
		{
			if (this._clearNextTick)
			{
				this.ShouldResetSimulation = false;
				this._clearNextTick = false;
				return;
			}
			if (this.ShouldResetSimulation)
			{
				this._clearNextTick = true;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F4 File Offset: 0x000002F4
		public void ResetSimulation()
		{
			this.ShouldResetSimulation = true;
			this._clearNextTick = false;
		}

		// Token: 0x04000007 RID: 7
		public bool _clearNextTick;
	}
}
