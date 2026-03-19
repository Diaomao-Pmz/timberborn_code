using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000025 RID: 37
	public class StatusSubject : BaseComponent, IDeletableEntity
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000EC RID: 236 RVA: 0x00004634 File Offset: 0x00002834
		// (remove) Token: 0x060000ED RID: 237 RVA: 0x0000466C File Offset: 0x0000286C
		public event EventHandler<EventArgs> StatusToggled;

		// Token: 0x060000EE RID: 238 RVA: 0x000046A1 File Offset: 0x000028A1
		public StatusSubject(StatusInstanceFactory statusInstanceFactory, DynamicStatusAggregator dynamicStatusAggregator, StatusAggregator statusAggregator)
		{
			this._statusInstanceFactory = statusInstanceFactory;
			this._dynamicStatusAggregator = dynamicStatusAggregator;
			this._statusAggregator = statusAggregator;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000046D4 File Offset: 0x000028D4
		public ReadOnlyList<StatusInstance> ActiveStatuses
		{
			get
			{
				if (!this.InPriorityMode)
				{
					return this._activeNormalStatuses.AsReadOnlyList<StatusInstance>();
				}
				return this._activePriorityStatuses.AsReadOnlyList<StatusInstance>();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000046F5 File Offset: 0x000028F5
		public bool InPriorityMode
		{
			get
			{
				return !this._activePriorityStatuses.IsEmpty<StatusInstance>();
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004708 File Offset: 0x00002908
		public void RegisterStatuses(IEnumerable<StatusToggle> statusToggles)
		{
			foreach (StatusToggle statusToggle in statusToggles)
			{
				this.RegisterStatus(statusToggle);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004750 File Offset: 0x00002950
		public void RegisterStatus(StatusToggle statusToggle)
		{
			StatusInstance statusInstance = this._statusInstanceFactory.CreateStatus(this, statusToggle);
			this._statusAggregator.AddStatus(statusInstance);
			this.UpdateStatus(statusToggle, statusInstance);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004780 File Offset: 0x00002980
		public void RegisterDynamicStatus(StatusToggle statusToggle, Func<float> statusGroupOrderingGetter, Func<StatusWarningType> statusWarningTypeGetter, string warningSound)
		{
			StatusInstance statusInstance = this._statusInstanceFactory.CreateDynamicStatus(this, statusToggle, statusGroupOrderingGetter, statusWarningTypeGetter, warningSound);
			this._dynamicStatusAggregator.AddStatus(statusInstance);
			this.UpdateStatus(statusToggle, statusInstance);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000047B3 File Offset: 0x000029B3
		public void DeleteEntity()
		{
			this._statusAggregator.RemoveStatuses(this);
			this._dynamicStatusAggregator.RemoveStatuses(this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000047D0 File Offset: 0x000029D0
		public void UpdateStatus(StatusToggle statusToggle, StatusInstance statusInstance)
		{
			this.UpdateStatus(statusInstance, statusToggle.IsActive);
			statusToggle.StatusToggled += delegate(object _, EventArgs _)
			{
				this.UpdateStatus(statusInstance, statusToggle.IsActive);
			};
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004828 File Offset: 0x00002A28
		public void UpdateStatus(StatusInstance statusInstance, bool isActive)
		{
			List<StatusInstance> list = statusInstance.IsPriorityStatus ? this._activePriorityStatuses : this._activeNormalStatuses;
			if (isActive)
			{
				statusInstance.Activate();
				if (!list.Contains(statusInstance))
				{
					list.Add(statusInstance);
				}
			}
			else
			{
				statusInstance.Deactivate();
				list.Remove(statusInstance);
			}
			EventHandler<EventArgs> statusToggled = this.StatusToggled;
			if (statusToggled == null)
			{
				return;
			}
			statusToggled(this, EventArgs.Empty);
		}

		// Token: 0x04000087 RID: 135
		public readonly StatusInstanceFactory _statusInstanceFactory;

		// Token: 0x04000088 RID: 136
		public readonly DynamicStatusAggregator _dynamicStatusAggregator;

		// Token: 0x04000089 RID: 137
		public readonly StatusAggregator _statusAggregator;

		// Token: 0x0400008A RID: 138
		public readonly List<StatusInstance> _activePriorityStatuses = new List<StatusInstance>();

		// Token: 0x0400008B RID: 139
		public readonly List<StatusInstance> _activeNormalStatuses = new List<StatusInstance>();
	}
}
