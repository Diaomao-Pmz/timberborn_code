using System;
using Timberborn.SingletonSystem;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000019 RID: 25
	public class TubeVisitorUpdater : IUpdatableSingleton
	{
		// Token: 0x06000094 RID: 148 RVA: 0x0000320C File Offset: 0x0000140C
		public TubeVisitorUpdater(TubeVisitorRegistry tubeVisitorRegistry, TubeMap tubeMap)
		{
			this._tubeVisitorRegistry = tubeVisitorRegistry;
			this._tubeMap = tubeMap;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003224 File Offset: 0x00001424
		public void UpdateSingleton()
		{
			if (this._tubeMap.AnyTubeBuilt)
			{
				foreach (TubeVisitor tubeVisitor in this._tubeVisitorRegistry.TubeVisitors)
				{
					tubeVisitor.UpdateVisit();
				}
			}
		}

		// Token: 0x0400003C RID: 60
		public readonly TubeVisitorRegistry _tubeVisitorRegistry;

		// Token: 0x0400003D RID: 61
		public readonly TubeMap _tubeMap;
	}
}
