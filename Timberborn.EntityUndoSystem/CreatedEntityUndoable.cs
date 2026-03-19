using System;
using Timberborn.UndoSystem;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x02000005 RID: 5
	public class CreatedEntityUndoable : IUndoable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000212D File Offset: 0x0000032D
		public CreatedEntityUndoable(UndoableEntity undoableEntity)
		{
			this._undoableEntity = undoableEntity;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000213C File Offset: 0x0000033C
		public void Undo()
		{
			this._undoableEntity.InitializeUndoableState();
			this._undoableEntity.Delete();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
		public void Redo()
		{
			this._undoableEntity.Create();
		}

		// Token: 0x04000009 RID: 9
		public readonly UndoableEntity _undoableEntity;
	}
}
