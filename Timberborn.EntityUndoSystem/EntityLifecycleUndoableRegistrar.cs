using System;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.UndoSystem;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x02000009 RID: 9
	public class EntityLifecycleUndoableRegistrar : IPostLoadableSingleton
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000228F File Offset: 0x0000048F
		public EntityLifecycleUndoableRegistrar(UndoableEntityFactory undoableEntityFactory, IUndoRegistry undoRegistry, EventBus eventBus)
		{
			this._undoableEntityFactory = undoableEntityFactory;
			this._undoRegistry = undoRegistry;
			this._eventBus = eventBus;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022AC File Offset: 0x000004AC
		public void PostLoad()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022BA File Offset: 0x000004BA
		[OnEvent]
		public void OnEntityCreated(EntityCreatedEvent entityCreatedEvent)
		{
			if (this._undoRegistry.UndoAllowed && !this._undoRegistry.IsProcessingStack)
			{
				this.RegisterCreatedEntity(entityCreatedEvent.Entity);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022E2 File Offset: 0x000004E2
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			if (this._undoRegistry.UndoAllowed && !this._undoRegistry.IsProcessingStack)
			{
				this.RegisterDeletedEntity(entityDeletedEvent.Entity);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000230C File Offset: 0x0000050C
		public void RegisterCreatedEntity(EntityComponent entity)
		{
			CreatedEntityUndoable undoable = new CreatedEntityUndoable(this._undoableEntityFactory.CreateUninitialized(entity));
			this._undoRegistry.RegisterStackedUndoable(undoable);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002338 File Offset: 0x00000538
		public void RegisterDeletedEntity(EntityComponent entity)
		{
			DeletedEntityUndoable undoable = new DeletedEntityUndoable(this._undoableEntityFactory.CreateInitialized(entity));
			this._undoRegistry.RegisterStackedUndoable(undoable);
		}

		// Token: 0x04000012 RID: 18
		public readonly UndoableEntityFactory _undoableEntityFactory;

		// Token: 0x04000013 RID: 19
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000014 RID: 20
		public readonly EventBus _eventBus;
	}
}
