using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000012 RID: 18
	public class RecoveredGoodStackDisintegration : BaseComponent, IAwakableComponent, IInitializableEntity, IPersistentEntity, IDeletableEntity
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002FF1 File Offset: 0x000011F1
		public RecoveredGoodStackDisintegration(EntityService entityService, ITimeTriggerFactory timeTriggerFactory)
		{
			this._entityService = entityService;
			this._timeTriggerFactory = timeTriggerFactory;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003007 File Offset: 0x00001207
		public float DaysToDisintegration
		{
			get
			{
				return this._timeTrigger.DaysLeft;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003014 File Offset: 0x00001214
		public float Progress
		{
			get
			{
				return this._timeTrigger.Progress;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003021 File Offset: 0x00001221
		public void Awake()
		{
			this._spec = base.GetComponent<RecoveredGoodStackDisintegrationSpec>();
			this._timeTrigger = this._timeTriggerFactory.Create(new Action(this.Disintegrate), this._spec.DaysToDisintegrate);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003057 File Offset: 0x00001257
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(RecoveredGoodStackDisintegration.RecoveredGoodStackDisintegrationKey).Set(RecoveredGoodStackDisintegration.DisintegrationTimeKey, this._timeTrigger.Progress);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000307C File Offset: 0x0000127C
		[BackwardCompatible(2025, 11, 20, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(RecoveredGoodStackDisintegration.RecoveredGoodStackDisintegrationKey, out objectLoader))
			{
				this._timeTrigger.FastForwardProgress(objectLoader.Get(RecoveredGoodStackDisintegration.DisintegrationTimeKey));
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000030AE File Offset: 0x000012AE
		public void DeleteEntity()
		{
			this._timeTrigger.Reset();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000030BB File Offset: 0x000012BB
		public void InitializeEntity()
		{
			this._timeTrigger.Resume();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000030C8 File Offset: 0x000012C8
		public void Disintegrate()
		{
			this._entityService.Delete(this);
		}

		// Token: 0x0400003C RID: 60
		public static readonly ComponentKey RecoveredGoodStackDisintegrationKey = new ComponentKey("RecoveredGoodStackDisintegration");

		// Token: 0x0400003D RID: 61
		public static readonly PropertyKey<float> DisintegrationTimeKey = new PropertyKey<float>("DisintegrationTime");

		// Token: 0x0400003E RID: 62
		public readonly EntityService _entityService;

		// Token: 0x0400003F RID: 63
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x04000040 RID: 64
		public RecoveredGoodStackDisintegrationSpec _spec;

		// Token: 0x04000041 RID: 65
		public ITimeTrigger _timeTrigger;
	}
}
