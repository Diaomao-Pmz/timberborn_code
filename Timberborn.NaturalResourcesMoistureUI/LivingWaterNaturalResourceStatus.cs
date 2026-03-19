using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.NaturalResourcesMoisture;
using Timberborn.StatusSystem;

namespace Timberborn.NaturalResourcesMoistureUI
{
	// Token: 0x02000006 RID: 6
	public class LivingWaterNaturalResourceStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021E7 File Offset: 0x000003E7
		public LivingWaterNaturalResourceStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021F8 File Offset: 0x000003F8
		public void Awake()
		{
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._livingWaterNaturalResource = base.GetComponent<LivingWaterNaturalResource>();
			this._tooMuchWaterStatusToggle = StatusToggle.CreateNormalStatus("TooMuchWater", this._loc.T(LivingWaterNaturalResourceStatus.TooMuchWaterLocKey));
			this._notEnoughWaterStatusToggle = StatusToggle.CreateNormalStatus("NotEnoughWater", this._loc.T(LivingWaterNaturalResourceStatus.NotEnoughWaterLocKey));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002260 File Offset: 0x00000460
		public void Start()
		{
			StatusSubject component = base.GetComponent<StatusSubject>();
			component.RegisterStatus(this._tooMuchWaterStatusToggle);
			component.RegisterStatus(this._notEnoughWaterStatusToggle);
			this._livingNaturalResource.Died += this.OnDied;
			this._livingWaterNaturalResource.StartedDying += this.OnStartedDying;
			this._livingWaterNaturalResource.StoppedDying += this.OnStoppedDying;
			if (!this._livingNaturalResource.IsDead && this._livingWaterNaturalResource.DyingProgress.IsDying)
			{
				this.ActivateStatus();
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F7 File Offset: 0x000004F7
		public void OnDied(object sender, EventArgs e)
		{
			this.DeactivateStatuses();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022FF File Offset: 0x000004FF
		public void OnStartedDying(object sender, EventArgs e)
		{
			this.ActivateStatus();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022F7 File Offset: 0x000004F7
		public void OnStoppedDying(object sender, EventArgs e)
		{
			this.DeactivateStatuses();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002307 File Offset: 0x00000507
		public void DeactivateStatuses()
		{
			this._tooMuchWaterStatusToggle.Deactivate();
			this._notEnoughWaterStatusToggle.Deactivate();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000231F File Offset: 0x0000051F
		public void ActivateStatus()
		{
			if (this._livingWaterNaturalResource.DeathByFlooding)
			{
				this._tooMuchWaterStatusToggle.Activate();
				return;
			}
			this._notEnoughWaterStatusToggle.Activate();
		}

		// Token: 0x0400000D RID: 13
		public static readonly string TooMuchWaterLocKey = "Status.NaturalResources.TooMuchWater";

		// Token: 0x0400000E RID: 14
		public static readonly string NotEnoughWaterLocKey = "Status.NaturalResources.NotEnoughWater";

		// Token: 0x0400000F RID: 15
		public readonly ILoc _loc;

		// Token: 0x04000010 RID: 16
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x04000011 RID: 17
		public LivingWaterNaturalResource _livingWaterNaturalResource;

		// Token: 0x04000012 RID: 18
		public StatusToggle _tooMuchWaterStatusToggle;

		// Token: 0x04000013 RID: 19
		public StatusToggle _notEnoughWaterStatusToggle;
	}
}
