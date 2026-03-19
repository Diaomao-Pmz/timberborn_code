using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityNaming;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.EntityNamingUI
{
	// Token: 0x02000004 RID: 4
	public class DuplicateEntityNameStatus : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public DuplicateEntityNameStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public void Awake()
		{
			this._uniquelyNamedEntity = base.GetComponent<UniquelyNamedEntity>();
			this._statusToggle = StatusToggle.CreatePriorityStatusWithAlertAndFloatingIcon("GenericError", this._loc.T(DuplicateEntityNameStatus.DuplicateNameLocKey), this._loc.T(DuplicateEntityNameStatus.DuplicateNameShortLocKey), 0f);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000211E File Offset: 0x0000031E
		public void Start()
		{
			this.UpdateStatus();
			this._uniquelyNamedEntity.IsUniqueChanged += this.OnIsUniqueChanged;
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000214E File Offset: 0x0000034E
		public void OnIsUniqueChanged(object sender, EventArgs e)
		{
			this.UpdateStatus();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002156 File Offset: 0x00000356
		public void UpdateStatus()
		{
			this._statusToggle.Toggle(!this._uniquelyNamedEntity.IsUnique);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string DuplicateNameLocKey = "Status.Naming.DuplicateName";

		// Token: 0x04000007 RID: 7
		public static readonly string DuplicateNameShortLocKey = "Status.Naming.DuplicateName.Short";

		// Token: 0x04000008 RID: 8
		public readonly ILoc _loc;

		// Token: 0x04000009 RID: 9
		public UniquelyNamedEntity _uniquelyNamedEntity;

		// Token: 0x0400000A RID: 10
		public StatusToggle _statusToggle;
	}
}
