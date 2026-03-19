using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.GameWonderCompletion;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Wonders
{
	// Token: 0x0200000D RID: 13
	public class Wonder : BaseComponent, IAwakableComponent, IPersistentEntity, IFinishedStateListener, IRegisteredComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001C RID: 28 RVA: 0x000022BC File Offset: 0x000004BC
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x000022F4 File Offset: 0x000004F4
		public event EventHandler WonderActivated;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001E RID: 30 RVA: 0x0000232C File Offset: 0x0000052C
		// (remove) Token: 0x0600001F RID: 31 RVA: 0x00002364 File Offset: 0x00000564
		public event EventHandler WonderDeactivated;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002399 File Offset: 0x00000599
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000023A1 File Offset: 0x000005A1
		public bool IsActive { get; private set; }

		// Token: 0x06000022 RID: 34 RVA: 0x000023AA File Offset: 0x000005AA
		public Wonder(WonderCompletionCountdownStarter wonderCompletionCountdownStarter, EventBus eventBus)
		{
			this._wonderCompletionCountdownStarter = wonderCompletionCountdownStarter;
			this._eventBus = eventBus;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023CB File Offset: 0x000005CB
		public void Awake()
		{
			base.GetComponents<IWonderBlocker>(this._wonderBlockers);
			base.DisableComponent();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023DF File Offset: 0x000005DF
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(Wonder.WonderKey).Set(Wonder.IsActiveKey, this.IsActive);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023FC File Offset: 0x000005FC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Wonder.WonderKey);
			this.IsActive = component.Get(Wonder.IsActiveKey);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002426 File Offset: 0x00000626
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this._eventBus.Register(this);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000243A File Offset: 0x0000063A
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000244E File Offset: 0x0000064E
		public void Activate()
		{
			if (this.CanBeActivated())
			{
				this.IsActive = true;
				EventHandler wonderActivated = this.WonderActivated;
				if (wonderActivated != null)
				{
					wonderActivated(this, EventArgs.Empty);
				}
				this._eventBus.Post(new WonderActivatedEvent());
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002486 File Offset: 0x00000686
		public void Deactivate()
		{
			this.IsActive = false;
			EventHandler wonderDeactivated = this.WonderDeactivated;
			if (wonderDeactivated != null)
			{
				wonderDeactivated(this, EventArgs.Empty);
			}
			this._wonderCompletionCountdownStarter.BeginUnlockCountdown();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024B4 File Offset: 0x000006B4
		public bool CanBeActivated()
		{
			for (int i = 0; i < this._wonderBlockers.Count; i++)
			{
				if (this._wonderBlockers[i].IsWonderBlocked())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000012 RID: 18
		public static readonly ComponentKey WonderKey = new ComponentKey("Wonder");

		// Token: 0x04000013 RID: 19
		public static readonly PropertyKey<bool> IsActiveKey = new PropertyKey<bool>("IsActive");

		// Token: 0x04000017 RID: 23
		public readonly WonderCompletionCountdownStarter _wonderCompletionCountdownStarter;

		// Token: 0x04000018 RID: 24
		public readonly EventBus _eventBus;

		// Token: 0x04000019 RID: 25
		public readonly List<IWonderBlocker> _wonderBlockers = new List<IWonderBlocker>();
	}
}
