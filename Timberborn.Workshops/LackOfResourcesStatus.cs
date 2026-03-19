using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.StatusSystem;
using Timberborn.TickSystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000010 RID: 16
	public class LackOfResourcesStatus : TickableComponent, IAwakableComponent
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002686 File Offset: 0x00000886
		public LackOfResourcesStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002695 File Offset: 0x00000895
		public void Awake()
		{
			this._lackOfResourcesStatusToggle = StatusToggle.CreateNormalStatusWithAlert("LackOfResources", this._loc.T(LackOfResourcesStatus.LackOfResourcesLocKey), this._loc.T(LackOfResourcesStatus.LackOfResourcesShortLocKey), 3f);
			base.DisableComponent();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026D2 File Offset: 0x000008D2
		public override void Tick()
		{
			this.UpdateStatusToggle();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026DA File Offset: 0x000008DA
		public void Initialize(Func<bool> activePredicate)
		{
			Asserts.FieldIsNull<LackOfResourcesStatus>(this, this._activePredicate, "_activePredicate");
			base.GetComponent<StatusSubject>().RegisterStatus(this._lackOfResourcesStatusToggle);
			this._activePredicate = activePredicate;
			this.UpdateStatusToggle();
			base.EnableComponent();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002711 File Offset: 0x00000911
		public void Disable()
		{
			base.DisableComponent();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002719 File Offset: 0x00000919
		public void UpdateStatusToggle()
		{
			if (this._activePredicate())
			{
				this._lackOfResourcesStatusToggle.Activate();
				return;
			}
			this._lackOfResourcesStatusToggle.Deactivate();
		}

		// Token: 0x0400001B RID: 27
		public static readonly string LackOfResourcesLocKey = "Status.Work.LackOfResources";

		// Token: 0x0400001C RID: 28
		public static readonly string LackOfResourcesShortLocKey = "Status.Work.LackOfResources.Short";

		// Token: 0x0400001D RID: 29
		public readonly ILoc _loc;

		// Token: 0x0400001E RID: 30
		public StatusToggle _lackOfResourcesStatusToggle;

		// Token: 0x0400001F RID: 31
		public Func<bool> _activePredicate;
	}
}
