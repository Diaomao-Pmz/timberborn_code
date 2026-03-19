using System;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.UndoSystem;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x02000007 RID: 7
	public class EntityChangeRecorder : IDisposable
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000218A File Offset: 0x0000038A
		public EntityChangeRecorder(EventBus eventBus, IUndoRegistry undoRegistry, UndoableEntityFactory undoableEntityFactory, UndoableEntity preChangeUndoableEntity)
		{
			this._eventBus = eventBus;
			this._undoRegistry = undoRegistry;
			this._undoableEntityFactory = undoableEntityFactory;
			this._preChangeUndoableEntity = preChangeUndoableEntity;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B0 File Offset: 0x000003B0
		public void Dispose()
		{
			if (this._undoRegistry.UndoAllowed)
			{
				EntityComponent entity = this._preChangeUndoableEntity.GetEntity();
				UndoableEntity undoableEntity = this._undoableEntityFactory.CreateInitialized(entity);
				if (!this._preChangeUndoableEntity.Equals(undoableEntity))
				{
					ChangedEntityUndoable undoable = new ChangedEntityUndoable(this._eventBus, this._preChangeUndoableEntity, undoableEntity);
					this._undoRegistry.RegisterSingleUndoable(undoable);
				}
			}
		}

		// Token: 0x0400000B RID: 11
		public readonly EventBus _eventBus;

		// Token: 0x0400000C RID: 12
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x0400000D RID: 13
		public readonly UndoableEntityFactory _undoableEntityFactory;

		// Token: 0x0400000E RID: 14
		public readonly UndoableEntity _preChangeUndoableEntity;
	}
}
