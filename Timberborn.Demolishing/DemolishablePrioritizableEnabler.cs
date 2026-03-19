using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BuilderPrioritySystem;

namespace Timberborn.Demolishing
{
	// Token: 0x0200000F RID: 15
	public class DemolishablePrioritizableEnabler : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002AD9 File Offset: 0x00000CD9
		public void Awake()
		{
			this._builderPrioritizable = base.GetComponent<BuilderPrioritizable>();
			Demolishable component = base.GetComponent<Demolishable>();
			component.Marked += delegate(object _, EventArgs _)
			{
				this.OnMarked();
			};
			component.Unmarked += delegate(object _, EventArgs _)
			{
				this.OnUnmarked();
			};
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B10 File Offset: 0x00000D10
		public void OnMarked()
		{
			this._builderPrioritizable.Enable();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B1D File Offset: 0x00000D1D
		public void OnUnmarked()
		{
			this._builderPrioritizable.Disable();
		}

		// Token: 0x04000021 RID: 33
		public BuilderPrioritizable _builderPrioritizable;
	}
}
