using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.StatusSystem;
using Timberborn.TickSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x0200000B RID: 11
	public class CriticalNeedActionStatusRegistrar : TickableComponent, IAwakableComponent
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002611 File Offset: 0x00000811
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._needBehaviorPicker = base.GetComponent<INeedBehaviorPicker>();
			this._criticalNeederRootBehavior = base.GetComponent<CriticalNeederRootBehavior>();
			this.InitializeNeedStatusToggles();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000263D File Offset: 0x0000083D
		public override void StartTickable()
		{
			this.UpdateNeedStatuses();
			base.GetComponent<StatusSubject>().RegisterStatuses(from needStatusToggle in this._needStatusToggles
			select needStatusToggle.StatusToggle);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000267A File Offset: 0x0000087A
		public override void Tick()
		{
			this.UpdateNeedStatuses();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002684 File Offset: 0x00000884
		public void InitializeNeedStatusToggles()
		{
			foreach (NeedSpec needSpec in this._needManager.NeedSpecs)
			{
				CriticalNeedSpec spec = needSpec.GetSpec<CriticalNeedSpec>();
				if (spec != null && spec.CriticalNeedType == CriticalNeedType.Action)
				{
					this.InitializeNeedStatusToggle(needSpec, spec);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026D4 File Offset: 0x000008D4
		public void InitializeNeedStatusToggle(NeedSpec needSpec, CriticalNeedSpec criticalNeedSpec)
		{
			StatusToggle statusToggle = StatusToggle.CreateNormalStatusWithFloatingIcon(criticalNeedSpec.SpriteName, criticalNeedSpec.Description.Value, 0f);
			CriticalNeedActionStatusRegistrar.NeedStatusToggle item = new CriticalNeedActionStatusRegistrar.NeedStatusToggle(needSpec.Id, statusToggle);
			this._needStatusToggles.Add(item);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002718 File Offset: 0x00000918
		public void UpdateNeedStatuses()
		{
			for (int i = 0; i < this._needStatusToggles.Count; i++)
			{
				this.UpdateNeedStatus(this._needStatusToggles[i]);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002750 File Offset: 0x00000950
		public void UpdateNeedStatus(CriticalNeedActionStatusRegistrar.NeedStatusToggle needStatusToggle)
		{
			StatusToggle statusToggle = needStatusToggle.StatusToggle;
			if (this.NeedIsBeingCriticallySatisfied(needStatusToggle.NeedId))
			{
				statusToggle.Activate();
				return;
			}
			statusToggle.Deactivate();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000277F File Offset: 0x0000097F
		public bool NeedIsBeingCriticallySatisfied(string needId)
		{
			return this._criticalNeederRootBehavior.NeedRunning && this._needBehaviorPicker.NeedIsBeingCriticallySatisfied(needId);
		}

		// Token: 0x0400001F RID: 31
		public readonly List<CriticalNeedActionStatusRegistrar.NeedStatusToggle> _needStatusToggles = new List<CriticalNeedActionStatusRegistrar.NeedStatusToggle>();

		// Token: 0x04000020 RID: 32
		public NeedManager _needManager;

		// Token: 0x04000021 RID: 33
		public INeedBehaviorPicker _needBehaviorPicker;

		// Token: 0x04000022 RID: 34
		public CriticalNeederRootBehavior _criticalNeederRootBehavior;

		// Token: 0x0200000C RID: 12
		public class NeedStatusToggle
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000029 RID: 41 RVA: 0x000027AF File Offset: 0x000009AF
			public string NeedId { get; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600002A RID: 42 RVA: 0x000027B7 File Offset: 0x000009B7
			public StatusToggle StatusToggle { get; }

			// Token: 0x0600002B RID: 43 RVA: 0x000027BF File Offset: 0x000009BF
			public NeedStatusToggle(string needId, StatusToggle statusToggle)
			{
				this.NeedId = needId;
				this.StatusToggle = statusToggle;
			}
		}
	}
}
