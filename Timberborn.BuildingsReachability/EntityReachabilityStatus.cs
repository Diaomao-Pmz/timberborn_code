using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.StatusSystem;
using Timberborn.TickSystem;

namespace Timberborn.BuildingsReachability
{
	// Token: 0x0200000B RID: 11
	public class EntityReachabilityStatus : TickableComponent, IAwakableComponent, ISelectionListener
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000024FC File Offset: 0x000006FC
		public EntityReachabilityStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002516 File Offset: 0x00000716
		public void Awake()
		{
			base.GetComponents<IUnreachableEntity>(this._unreachableEntities);
			this._unreachableStatus = StatusToggle.CreateNormalStatus("UnreachableObject", this._loc.T(EntityReachabilityStatus.UnreachableObjectLocKey));
			base.DisableComponent();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000254A File Offset: 0x0000074A
		public override void StartTickable()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._unreachableStatus);
			this.UpdateStatus();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002563 File Offset: 0x00000763
		public override void Tick()
		{
			this.UpdateStatus();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000256B File Offset: 0x0000076B
		public void OnSelect()
		{
			this.Enable();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002573 File Offset: 0x00000773
		public void OnUnselect()
		{
			this.Disable();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000257B File Offset: 0x0000077B
		public void UpdateStatus()
		{
			if (this.IsAnyUnreachable())
			{
				this._unreachableStatus.Activate();
				return;
			}
			this._unreachableStatus.Deactivate();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000259C File Offset: 0x0000079C
		public bool IsAnyUnreachable()
		{
			using (List<IUnreachableEntity>.Enumerator enumerator = this._unreachableEntities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsUnreachable())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025F8 File Offset: 0x000007F8
		public void Enable()
		{
			base.EnableComponent();
			this.UpdateStatus();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002606 File Offset: 0x00000806
		public void Disable()
		{
			base.DisableComponent();
			this._unreachableStatus.Deactivate();
		}

		// Token: 0x04000018 RID: 24
		public static readonly string UnreachableObjectLocKey = "Status.Object.UnreachableObject";

		// Token: 0x04000019 RID: 25
		public readonly ILoc _loc;

		// Token: 0x0400001A RID: 26
		public readonly List<IUnreachableEntity> _unreachableEntities = new List<IUnreachableEntity>();

		// Token: 0x0400001B RID: 27
		public StatusToggle _unreachableStatus;
	}
}
