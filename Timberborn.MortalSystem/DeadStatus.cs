using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.MortalComponents;
using Timberborn.StatusSystem;

namespace Timberborn.MortalSystem
{
	// Token: 0x0200000A RID: 10
	public class DeadStatus : BaseComponent, IAwakableComponent, IStartableComponent, IDeadNeededComponent
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000024C3 File Offset: 0x000006C3
		public DeadStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024D4 File Offset: 0x000006D4
		public void Awake()
		{
			string description = this._loc.T(base.GetComponent<DeadStatusSpec>().DeadStatusLocKey);
			string spriteName = "Death";
			this._toggleWithIcon = StatusToggle.CreatePriorityStatusWithFloatingIcon(spriteName, description, 0f);
			this._toggleWithoutIcon = StatusToggle.CreateNormalStatus(spriteName, description);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000251D File Offset: 0x0000071D
		public void Start()
		{
			StatusSubject component = base.GetComponent<StatusSubject>();
			component.RegisterStatus(this._toggleWithIcon);
			component.RegisterStatus(this._toggleWithoutIcon);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000253C File Offset: 0x0000073C
		public void Activate(bool diedPublicly)
		{
			if (diedPublicly)
			{
				this._toggleWithIcon.Activate();
				return;
			}
			this._toggleWithoutIcon.Activate();
		}

		// Token: 0x04000015 RID: 21
		public readonly ILoc _loc;

		// Token: 0x04000016 RID: 22
		public StatusToggle _toggleWithIcon;

		// Token: 0x04000017 RID: 23
		public StatusToggle _toggleWithoutIcon;
	}
}
