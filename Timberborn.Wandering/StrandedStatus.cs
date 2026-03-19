using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.GameDistricts;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.Wandering
{
	// Token: 0x0200000B RID: 11
	public class StrandedStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002485 File Offset: 0x00000685
		public StrandedStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002494 File Offset: 0x00000694
		public void Awake()
		{
			this._citizen = base.GetComponent<Citizen>();
			base.GetComponent<Character>().Died += delegate(object _, EventArgs _)
			{
				this._statusToggle.Deactivate();
			};
			this._citizen.ChangedAssignedDistrict += delegate(object _, ChangeAssignedDistrictEventArgs _)
			{
				this.UpdateStatus();
			};
			this.InitializeStatus();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024E1 File Offset: 0x000006E1
		public void Start()
		{
			this.UpdateStatus();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024E9 File Offset: 0x000006E9
		public void Disable()
		{
			this._isDisabled = true;
			this.UpdateStatus();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024F8 File Offset: 0x000006F8
		public void InitializeStatus()
		{
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("Stranded", this._loc.T(StrandedStatus.StrandedLocKey), this._loc.T(StrandedStatus.StrandedShortLocKey), 0.1f);
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000254B File Offset: 0x0000074B
		public void UpdateStatus()
		{
			if (this._citizen.HasAssignedDistrict || this._isDisabled)
			{
				this._statusToggle.Deactivate();
				return;
			}
			this._statusToggle.Activate();
		}

		// Token: 0x04000010 RID: 16
		public static readonly string StrandedLocKey = "Status.Homelessness.Stranded";

		// Token: 0x04000011 RID: 17
		public static readonly string StrandedShortLocKey = "Status.Homelessness.Stranded.Short";

		// Token: 0x04000012 RID: 18
		public readonly ILoc _loc;

		// Token: 0x04000013 RID: 19
		public Citizen _citizen;

		// Token: 0x04000014 RID: 20
		public StatusToggle _statusToggle;

		// Token: 0x04000015 RID: 21
		public bool _isDisabled;
	}
}
