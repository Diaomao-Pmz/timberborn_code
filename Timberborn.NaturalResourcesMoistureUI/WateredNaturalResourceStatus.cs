using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.NaturalResourcesMoisture;
using Timberborn.StatusSystem;

namespace Timberborn.NaturalResourcesMoistureUI
{
	// Token: 0x0200000A RID: 10
	public class WateredNaturalResourceStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002513 File Offset: 0x00000713
		public WateredNaturalResourceStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002522 File Offset: 0x00000722
		public void Awake()
		{
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._wateredNaturalResource = base.GetComponent<WateredNaturalResource>();
			this._dryingStatusToggle = StatusToggle.CreateNormalStatus("DryingNaturalResource", this._loc.T(WateredNaturalResourceStatus.DryingLocKey));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000255C File Offset: 0x0000075C
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._dryingStatusToggle);
			this._livingNaturalResource.Died += this.OnDied;
			this._wateredNaturalResource.StartedDying += this.OnStartedDying;
			this._wateredNaturalResource.StoppedDying += this.OnStoppedDying;
			if (!this._livingNaturalResource.IsDead && this._wateredNaturalResource.DyingProgress.IsDying)
			{
				this._dryingStatusToggle.Activate();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025EC File Offset: 0x000007EC
		public void OnDied(object sender, EventArgs e)
		{
			this._dryingStatusToggle.Deactivate();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025F9 File Offset: 0x000007F9
		public void OnStartedDying(object sender, EventArgs e)
		{
			this._dryingStatusToggle.Activate();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025EC File Offset: 0x000007EC
		public void OnStoppedDying(object sender, EventArgs e)
		{
			this._dryingStatusToggle.Deactivate();
		}

		// Token: 0x0400001B RID: 27
		public static readonly string DryingLocKey = "Status.NaturalResources.Drying";

		// Token: 0x0400001C RID: 28
		public readonly ILoc _loc;

		// Token: 0x0400001D RID: 29
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x0400001E RID: 30
		public WateredNaturalResource _wateredNaturalResource;

		// Token: 0x0400001F RID: 31
		public StatusToggle _dryingStatusToggle;
	}
}
