using System;
using Timberborn.UndoSystem;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x02000006 RID: 6
	public class DeletedEntityUndoable : IUndoable
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002161 File Offset: 0x00000361
		public DeletedEntityUndoable(UndoableEntity undoableEntity)
		{
			this._undoableEntity = undoableEntity;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002170 File Offset: 0x00000370
		public void Undo()
		{
			this._undoableEntity.Create();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217D File Offset: 0x0000037D
		public void Redo()
		{
			this._undoableEntity.Delete();
		}

		// Token: 0x0400000A RID: 10
		public readonly UndoableEntity _undoableEntity;
	}
}
