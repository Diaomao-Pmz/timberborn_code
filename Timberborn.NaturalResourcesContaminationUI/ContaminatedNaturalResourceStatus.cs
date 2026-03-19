using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.NaturalResourcesContamination;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.StatusSystem;

namespace Timberborn.NaturalResourcesContaminationUI
{
	// Token: 0x02000004 RID: 4
	public class ContaminatedNaturalResourceStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public ContaminatedNaturalResourceStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public void Awake()
		{
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._contaminatedNaturalResource = base.GetComponent<ContaminatedNaturalResource>();
			this._contaminatedStatusToggle = StatusToggle.CreateNormalStatus("ContaminatedNaturalResource", this._loc.T(ContaminatedNaturalResourceStatus.ContaminatedLocKey));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002108 File Offset: 0x00000308
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._contaminatedStatusToggle);
			this._livingNaturalResource.Died += this.OnDied;
			this._contaminatedNaturalResource.StartedDying += this.OnStartedDying;
			this._contaminatedNaturalResource.StoppedDying += this.OnStoppedDying;
			if (!this._livingNaturalResource.IsDead && this._contaminatedNaturalResource.DyingProgress.IsDying)
			{
				this._contaminatedStatusToggle.Activate();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002198 File Offset: 0x00000398
		public void OnDied(object sender, EventArgs e)
		{
			this._contaminatedStatusToggle.Deactivate();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021A5 File Offset: 0x000003A5
		public void OnStartedDying(object sender, EventArgs e)
		{
			this._contaminatedStatusToggle.Activate();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
		public void OnStoppedDying(object sender, EventArgs e)
		{
			this._contaminatedStatusToggle.Deactivate();
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ContaminatedLocKey = "Status.NaturalResources.Contaminated";

		// Token: 0x04000007 RID: 7
		public readonly ILoc _loc;

		// Token: 0x04000008 RID: 8
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x04000009 RID: 9
		public ContaminatedNaturalResource _contaminatedNaturalResource;

		// Token: 0x0400000A RID: 10
		public StatusToggle _contaminatedStatusToggle;
	}
}
