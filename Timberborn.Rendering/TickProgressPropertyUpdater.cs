using System;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000023 RID: 35
	public class TickProgressPropertyUpdater : ILateUpdatableSingleton
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00004636 File Offset: 0x00002836
		public TickProgressPropertyUpdater(ITickProgressService tickProgressService)
		{
			this._tickProgressService = tickProgressService;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004645 File Offset: 0x00002845
		public void LateUpdateSingleton()
		{
			Shader.SetGlobalFloat(TickProgressPropertyUpdater.TickProgressProperty, this._tickProgressService.Progress);
		}

		// Token: 0x04000062 RID: 98
		public static readonly int TickProgressProperty = Shader.PropertyToID("_TickProgress");

		// Token: 0x04000063 RID: 99
		public readonly ITickProgressService _tickProgressService;
	}
}
