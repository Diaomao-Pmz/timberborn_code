using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.EntitySystem
{
	// Token: 0x02000007 RID: 7
	public class EntityComponent : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public Guid EntityId { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002111 File Offset: 0x00000311
		public List<IRegisteredComponent> RegisteredComponents { get; } = new List<IRegisteredComponent>();

		// Token: 0x0600000A RID: 10 RVA: 0x00002119 File Offset: 0x00000319
		public EntityComponent(EventBus eventBus, EntityComponentRegistry entityComponentRegistry)
		{
			this._eventBus = eventBus;
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000213A File Offset: 0x0000033A
		public bool Initialized
		{
			get
			{
				return this._entityState == EntityComponent.EntityState.Initialized;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002145 File Offset: 0x00000345
		public bool Deleted
		{
			get
			{
				return this._entityState == EntityComponent.EntityState.Deleted;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002150 File Offset: 0x00000350
		public void Awake()
		{
			base.GetComponents<IRegisteredComponent>(this.RegisteredComponents);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000215E File Offset: 0x0000035E
		public void Start()
		{
			this.InitializeIfUninitialized();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002168 File Offset: 0x00000368
		public void PreInitialize()
		{
			if (!this.Deleted)
			{
				foreach (IPreInitializableEntity preInitializableEntity in base.GetComponentsAllocating<IPreInitializableEntity>())
				{
					preInitializableEntity.PreInitializeEntity();
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021C0 File Offset: 0x000003C0
		public void Initialize()
		{
			if (!this.Deleted)
			{
				this._entityState = EntityComponent.EntityState.Initializing;
				foreach (IInitializableEntity initializableEntity in base.GetComponentsAllocating<IInitializableEntity>())
				{
					initializableEntity.InitializeEntity();
				}
				this._entityComponentRegistry.Register(this);
				this._eventBus.Post(new EntityInitializedEvent(this));
				this._entityState = EntityComponent.EntityState.Initialized;
				if (this._deleteAfterInitialization)
				{
					this.InternalDelete();
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002254 File Offset: 0x00000454
		public void PostInitialize()
		{
			if (!this.Deleted)
			{
				foreach (IPostInitializableEntity postInitializableEntity in base.GetComponentsAllocating<IPostInitializableEntity>())
				{
					postInitializableEntity.PostInitializeEntity();
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022AC File Offset: 0x000004AC
		public void PostLoad()
		{
			if (!this.Deleted)
			{
				foreach (IPostLoadableEntity postLoadableEntity in base.GetComponentsAllocating<IPostLoadableEntity>())
				{
					postLoadableEntity.PostLoadEntity();
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002304 File Offset: 0x00000504
		public void Delete()
		{
			if (this._entityState == EntityComponent.EntityState.Initialized)
			{
				this.InternalDelete();
				return;
			}
			this._deleteAfterInitialization = true;
			this.InitializeIfUninitialized();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002323 File Offset: 0x00000523
		public void SetEntityId(Guid id)
		{
			if (this.EntityId != Guid.Empty)
			{
				throw new InvalidOperationException(base.Name + " has already initialized EntityId!");
			}
			this.EntityId = id;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002354 File Offset: 0x00000554
		public void InitializeIfUninitialized()
		{
			if (this._entityState == EntityComponent.EntityState.Uninitialized)
			{
				this.PreInitialize();
				this.Initialize();
				this.PostInitialize();
				this.PostLoad();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002378 File Offset: 0x00000578
		public void InternalDelete()
		{
			if (!this.Deleted)
			{
				this._entityState = EntityComponent.EntityState.Deleted;
				foreach (IDeletableEntity deletableEntity in base.GetComponentsAllocating<IDeletableEntity>())
				{
					deletableEntity.DeleteEntity();
				}
				this._entityComponentRegistry.Unregister(this);
				this._eventBus.Post(new EntityDeletedEvent(this));
				this._eventBus.Unregister(base.AllComponents);
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly EventBus _eventBus;

		// Token: 0x0400000B RID: 11
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x0400000C RID: 12
		public EntityComponent.EntityState _entityState;

		// Token: 0x0400000D RID: 13
		public bool _deleteAfterInitialization;

		// Token: 0x02000008 RID: 8
		public enum EntityState
		{
			// Token: 0x0400000F RID: 15
			Uninitialized,
			// Token: 0x04000010 RID: 16
			Initializing,
			// Token: 0x04000011 RID: 17
			Initialized,
			// Token: 0x04000012 RID: 18
			Deleted
		}
	}
}
