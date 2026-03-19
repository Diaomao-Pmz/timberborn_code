using System;
using Timberborn.UndoSystem;

namespace Timberborn.GameScene
{
	// Token: 0x02000005 RID: 5
	public class DummyUndoRegistry : IUndoRegistry
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002210 File Offset: 0x00000410
		public bool UndoAllowed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002210 File Offset: 0x00000410
		public bool IsProcessingStack
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002210 File Offset: 0x00000410
		public bool CanUndo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002210 File Offset: 0x00000410
		public bool CanRedo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002213 File Offset: 0x00000413
		public void RegisterSingleUndoable(IUndoable undoable)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002213 File Offset: 0x00000413
		public void RegisterStackedUndoable(IUndoable undoable)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002213 File Offset: 0x00000413
		public void CommitStack()
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002215 File Offset: 0x00000415
		public void Undo()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002215 File Offset: 0x00000415
		public void Redo()
		{
			throw new NotSupportedException();
		}
	}
}
