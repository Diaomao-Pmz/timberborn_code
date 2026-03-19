using System;
using System.Collections.Generic;

namespace Timberborn.Automation
{
	// Token: 0x0200000D RID: 13
	public class AutomationResetter
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002F74 File Offset: 0x00001174
		public void ResetPartition(Automator seedAutomator)
		{
			AutomatorPartition partition = seedAutomator.Partition;
			if (partition != null)
			{
				foreach (Automator automator in partition.Automators)
				{
					automator.GetComponents<ISequentialTransmitter>(this._reusableSequentialTransmitters);
					foreach (ISequentialTransmitter sequentialTransmitter in this._reusableSequentialTransmitters)
					{
						sequentialTransmitter.Reset();
					}
					this._reusableSequentialTransmitters.Clear();
				}
			}
		}

		// Token: 0x04000023 RID: 35
		public readonly List<ISequentialTransmitter> _reusableSequentialTransmitters = new List<ISequentialTransmitter>();
	}
}
