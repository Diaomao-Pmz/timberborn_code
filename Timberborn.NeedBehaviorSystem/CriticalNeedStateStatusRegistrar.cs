using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.StatusSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x02000011 RID: 17
	public class CriticalNeedStateStatusRegistrar : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002A9D File Offset: 0x00000C9D
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this.InitializeStatusToggles();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002AB1 File Offset: 0x00000CB1
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatuses(this._statusToggles.AsReadOnlyEnumerable<StatusToggle>());
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002ACC File Offset: 0x00000CCC
		public void InitializeStatusToggles()
		{
			foreach (NeedSpec needSpec in this._needManager.NeedSpecs)
			{
				CriticalNeedSpec spec = needSpec.GetSpec<CriticalNeedSpec>();
				if (spec != null)
				{
					if (spec.CriticalNeedType == CriticalNeedType.State)
					{
						StatusToggle statusToggle = StatusToggle.CreateNormalStatusWithFloatingIcon(spec.SpriteName, spec.Description.Value, 0f);
						this.InitializeStatusToggle(needSpec, statusToggle);
					}
					if (spec.CriticalNeedType == CriticalNeedType.Alert)
					{
						StatusToggle statusToggle2 = StatusToggle.CreateNormalStatusWithAlert(spec.SpriteName, spec.Description.Value, spec.DescriptionShort.Value, 0f);
						this.InitializeStatusToggle(needSpec, statusToggle2);
					}
					if (spec.CriticalNeedType == CriticalNeedType.StateWithAlert)
					{
						StatusToggle statusToggle3 = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon(spec.SpriteName, spec.Description.Value, spec.DescriptionShort.Value, 0f);
						this.InitializeStatusToggle(needSpec, statusToggle3);
					}
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public void InitializeStatusToggle(NeedSpec needSpec, StatusToggle statusToggle)
		{
			CriticalNeedStateStatusRegistrar.<>c__DisplayClass5_0 CS$<>8__locals1 = new CriticalNeedStateStatusRegistrar.<>c__DisplayClass5_0();
			CS$<>8__locals1.needSpec = needSpec;
			CS$<>8__locals1.statusToggle = statusToggle;
			this._needManager.NeedChangedCriticalState += CS$<>8__locals1.<InitializeStatusToggle>g__OnNeedChangedCriticalState|0;
			this._statusToggles.Add(CS$<>8__locals1.statusToggle);
		}

		// Token: 0x0400002F RID: 47
		public NeedManager _needManager;

		// Token: 0x04000030 RID: 48
		public readonly List<StatusToggle> _statusToggles = new List<StatusToggle>();
	}
}
