using System;
using Timberborn.UndoSystem;

namespace Timberborn.TerrainUndoSystem
{
	// Token: 0x02000004 RID: 4
	public class CreatedTerrainUndoable : IUndoable
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public CreatedTerrainUndoable(UndoableTerrain undoableTerrain)
		{
			this._undoableTerrain = undoableTerrain;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public void Undo()
		{
			this._undoableTerrain.UnsetTerrain();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DA File Offset: 0x000002DA
		public void Redo()
		{
			this._undoableTerrain.SetTerrain();
		}

		// Token: 0x04000006 RID: 6
		public readonly UndoableTerrain _undoableTerrain;
	}
}
