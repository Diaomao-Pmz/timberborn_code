using System;
using Timberborn.SingletonSystem;
using Timberborn.UndoSystem;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x02000004 RID: 4
	public class ChangedEntityUndoable : IUndoable
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public ChangedEntityUndoable(EventBus eventBus, UndoableEntity preChangeUndoableEntity, UndoableEntity postChangeUndoableEntity)
		{
			this._eventBus = eventBus;
			this._preChangeUndoableEntity = preChangeUndoableEntity;
			this._postChangeUndoableEntity = postChangeUndoableEntity;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DD File Offset: 0x000002DD
		public void Undo()
		{
			this._preChangeUndoableEntity.Reload();
			this._eventBus.Post(new UndoableEntityChangedEvent(this._preChangeUndoableEntity.GetEntity()));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002105 File Offset: 0x00000305
		public void Redo()
		{
			this._postChangeUndoableEntity.Reload();
			this._eventBus.Post(new UndoableEntityChangedEvent(this._postChangeUndoableEntity.GetEntity()));
		}

		// Token: 0x04000006 RID: 6
		public readonly EventBus _eventBus;

		// Token: 0x04000007 RID: 7
		public readonly UndoableEntity _preChangeUndoableEntity;

		// Token: 0x04000008 RID: 8
		public readonly UndoableEntity _postChangeUndoableEntity;
	}
}
