using System;

namespace Timberborn.UndoSystem
{
	// Token: 0x02000004 RID: 4
	public interface IUndoable
	{
		// Token: 0x06000003 RID: 3
		void Undo();

		// Token: 0x06000004 RID: 4
		void Redo();
	}
}
