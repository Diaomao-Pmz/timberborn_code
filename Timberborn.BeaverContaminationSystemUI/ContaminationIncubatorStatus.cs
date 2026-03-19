using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.BeaverContaminationSystemUI
{
	// Token: 0x02000005 RID: 5
	public class ContaminationIncubatorStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002103 File Offset: 0x00000303
		public ContaminationIncubatorStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002114 File Offset: 0x00000314
		public void Awake()
		{
			this._statusToggle = StatusToggle.CreateNormalStatusWithAlert("Incubation", this._loc.T(ContaminationIncubatorStatus.IncubationLocKey), this._loc.T(ContaminationIncubatorStatus.IncubationShortLocKey), 0f);
			this._contaminationIncubator = base.GetComponent<ContaminationIncubator>();
			this._contaminationIncubator.IncubationStateChanged += this.OnIncubationStateChanged;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002179 File Offset: 0x00000379
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000218C File Offset: 0x0000038C
		public void OnIncubationStateChanged(object sender, EventArgs e)
		{
			if (this._contaminationIncubator.IsIncubating || this._contaminationIncubator.IncubationFinished)
			{
				this._statusToggle.Activate();
				return;
			}
			this._statusToggle.Deactivate();
		}

		// Token: 0x04000006 RID: 6
		public static readonly string IncubationLocKey = "Status.BadwaterContamination.Incubation";

		// Token: 0x04000007 RID: 7
		public static readonly string IncubationShortLocKey = "Status.BadwaterContamination.Incubation.Short";

		// Token: 0x04000008 RID: 8
		public readonly ILoc _loc;

		// Token: 0x04000009 RID: 9
		public StatusToggle _statusToggle;

		// Token: 0x0400000A RID: 10
		public ContaminationIncubator _contaminationIncubator;
	}
}
