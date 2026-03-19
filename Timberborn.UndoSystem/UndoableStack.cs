using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Timberborn.UndoSystem
{
	// Token: 0x02000007 RID: 7
	public class UndoableStack : IUndoable
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000020BE File Offset: 0x000002BE
		public UndoableStack(List<IUndoable> stack)
		{
			this._stack = stack.ToImmutableArray<IUndoable>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Undo()
		{
			for (int i = this._stack.Length - 1; i >= 0; i--)
			{
				this._stack[i].Undo();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000210C File Offset: 0x0000030C
		public void Redo()
		{
			for (int i = 0; i < this._stack.Length; i++)
			{
				this._stack[i].Redo();
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly ImmutableArray<IUndoable> _stack;
	}
}
