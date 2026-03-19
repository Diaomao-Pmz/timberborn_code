using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuilderPrioritySystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000018 RID: 24
	public class ConstructionSitePrioritizableEnabler : BaseComponent, IAwakableComponent, IUnfinishedStateListener
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000038DF File Offset: 0x00001ADF
		public void Awake()
		{
			this._builderPrioritizable = base.GetComponent<BuilderPrioritizable>();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000038ED File Offset: 0x00001AED
		public void OnEnterUnfinishedState()
		{
			this._builderPrioritizable.Enable();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000038FA File Offset: 0x00001AFA
		public void OnExitUnfinishedState()
		{
			this._builderPrioritizable.Disable();
		}

		// Token: 0x04000054 RID: 84
		public BuilderPrioritizable _builderPrioritizable;
	}
}
