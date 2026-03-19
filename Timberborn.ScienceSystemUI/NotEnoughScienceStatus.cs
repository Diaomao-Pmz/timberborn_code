using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.ScienceSystem;
using Timberborn.StatusSystem;

namespace Timberborn.ScienceSystemUI
{
	// Token: 0x02000004 RID: 4
	public class NotEnoughScienceStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BF File Offset: 0x000002BF
		public NotEnoughScienceStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public void Awake()
		{
			this._scienceNeedingBuilding = base.GetComponent<ScienceNeedingBuilding>();
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("NotEnoughScience", this._loc.T(NotEnoughScienceStatus.NotEnoughScienceLocKey), this._loc.T(NotEnoughScienceStatus.NotEnoughScienceShortLocKey), 0.2f);
			this._scienceNeedingBuilding.NotEnoughScienceStateChanged += this.OnNotEnoughScienceStateChanged;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002135 File Offset: 0x00000335
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002148 File Offset: 0x00000348
		public void OnNotEnoughScienceStateChanged(object sender, NotEnoughScienceStateChangedEventArgs notEnoughScienceStateChangedEventArgs)
		{
			if (notEnoughScienceStateChangedEventArgs.NewState)
			{
				this._statusToggle.Activate();
				return;
			}
			this._statusToggle.Deactivate();
		}

		// Token: 0x04000006 RID: 6
		public static readonly string NotEnoughScienceLocKey = "Status.Science.NotEnoughScience";

		// Token: 0x04000007 RID: 7
		public static readonly string NotEnoughScienceShortLocKey = "Status.Science.NotEnoughScience.Short";

		// Token: 0x04000008 RID: 8
		public readonly ILoc _loc;

		// Token: 0x04000009 RID: 9
		public ScienceNeedingBuilding _scienceNeedingBuilding;

		// Token: 0x0400000A RID: 10
		public StatusToggle _statusToggle;
	}
}
