using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.UndoSystem;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x02000008 RID: 8
	public class EntityChangeRecorderFactory
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002210 File Offset: 0x00000410
		public EntityChangeRecorderFactory(EventBus eventBus, IUndoRegistry undoRegistry, UndoableEntityFactory undoableEntityFactory)
		{
			this._eventBus = eventBus;
			this._undoRegistry = undoRegistry;
			this._undoableEntityFactory = undoableEntityFactory;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002230 File Offset: 0x00000430
		public EntityChangeRecorder CreateChangeRecorder(BaseComponent baseComponent)
		{
			if (this._undoRegistry.UndoAllowed)
			{
				EntityComponent component = baseComponent.GetComponent<EntityComponent>();
				UndoableEntity preChangeUndoableEntity = this._undoableEntityFactory.CreateInitialized(component);
				return new EntityChangeRecorder(this._eventBus, this._undoRegistry, this._undoableEntityFactory, preChangeUndoableEntity);
			}
			return new EntityChangeRecorder(this._eventBus, this._undoRegistry, this._undoableEntityFactory, null);
		}

		// Token: 0x0400000F RID: 15
		public readonly EventBus _eventBus;

		// Token: 0x04000010 RID: 16
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000011 RID: 17
		public readonly UndoableEntityFactory _undoableEntityFactory;
	}
}
