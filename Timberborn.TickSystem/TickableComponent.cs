using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.TickSystem
{
	// Token: 0x02000010 RID: 16
	public abstract class TickableComponent : BaseComponent, IStartableComponent
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000022ED File Offset: 0x000004ED
		public virtual void StartTickable()
		{
		}

		// Token: 0x06000029 RID: 41
		public abstract void Tick();

		// Token: 0x0600002A RID: 42 RVA: 0x000022EF File Offset: 0x000004EF
		public void Start()
		{
			if (!this._started)
			{
				this.StartTickable();
				this._started = true;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002306 File Offset: 0x00000506
		public void StartAndTick()
		{
			this.Start();
			this.Tick();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002314 File Offset: 0x00000514
		public TickableComponent()
		{
		}

		// Token: 0x04000010 RID: 16
		public bool _started;
	}
}
