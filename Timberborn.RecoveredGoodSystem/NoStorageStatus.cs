using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000008 RID: 8
	public class NoStorageStatus : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002281 File Offset: 0x00000481
		public NoStorageStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002290 File Offset: 0x00000490
		public void Awake()
		{
			this._noStorageStatusToggle = StatusToggle.CreateNormalStatus("NoStorage", this._loc.T(NoStorageStatus.NoStorageLocKey));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022B2 File Offset: 0x000004B2
		public void InitializeEntity()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._noStorageStatusToggle);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C5 File Offset: 0x000004C5
		public void ActivateNoStorageStatus()
		{
			this._noStorageStatusToggle.Activate();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022D2 File Offset: 0x000004D2
		public void DeactivateNoStorageStatus()
		{
			this._noStorageStatusToggle.Deactivate();
		}

		// Token: 0x0400000E RID: 14
		public static readonly string NoStorageLocKey = "Status.RecoveredGoodStack.NoStorage";

		// Token: 0x0400000F RID: 15
		public readonly ILoc _loc;

		// Token: 0x04000010 RID: 16
		public StatusToggle _noStorageStatusToggle;
	}
}
