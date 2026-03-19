using System;

namespace Timberborn.UndoSystem
{
	// Token: 0x02000006 RID: 6
	public interface IUndoRegistry
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6
		bool UndoAllowed { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7
		bool IsProcessingStack { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8
		bool CanUndo { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9
		bool CanRedo { get; }

		// Token: 0x0600000A RID: 10
		void RegisterSingleUndoable(IUndoable undoable);

		// Token: 0x0600000B RID: 11
		void RegisterStackedUndoable(IUndoable undoable);

		// Token: 0x0600000C RID: 12
		void CommitStack();

		// Token: 0x0600000D RID: 13
		void Undo();

		// Token: 0x0600000E RID: 14
		void Redo();
	}
}
