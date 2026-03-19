using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.TickSystem;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200003C RID: 60
	[MapEditorTickable]
	public class WaterSourceRegistry : ITickableSingleton
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00007C90 File Offset: 0x00005E90
		public ReadOnlyList<ThreadSafeWaterSource> ThreadSafeWaterSources
		{
			get
			{
				return this._threadSafeWaterSources.AsReadOnlyList<ThreadSafeWaterSource>();
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007C9D File Offset: 0x00005E9D
		public void Tick()
		{
			this.UpdateThreadSafeRegistry();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007CA5 File Offset: 0x00005EA5
		public void RegisterWaterSource(IWaterSource waterSource)
		{
			this._waterSources.Add(waterSource);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007CB3 File Offset: 0x00005EB3
		public void UnregisterWaterSource(IWaterSource waterSource)
		{
			this._waterSources.Remove(waterSource);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public void UpdateThreadSafeRegistry()
		{
			this._threadSafeWaterSources.Clear();
			foreach (IWaterSource waterSource in this._waterSources)
			{
				this._threadSafeWaterSources.Add(new ThreadSafeWaterSource(waterSource));
			}
		}

		// Token: 0x04000144 RID: 324
		public readonly List<IWaterSource> _waterSources = new List<IWaterSource>();

		// Token: 0x04000145 RID: 325
		public readonly List<ThreadSafeWaterSource> _threadSafeWaterSources = new List<ThreadSafeWaterSource>();
	}
}
