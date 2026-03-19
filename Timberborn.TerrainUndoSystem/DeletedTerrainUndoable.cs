using System;
using Timberborn.UndoSystem;

namespace Timberborn.TerrainUndoSystem
{
	// Token: 0x02000005 RID: 5
	public class DeletedTerrainUndoable : IUndoable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020E7 File Offset: 0x000002E7
		public DeletedTerrainUndoable(UndoableTerrain undoableTerrain)
		{
			this._undoableTerrain = undoableTerrain;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F6 File Offset: 0x000002F6
		public void Undo()
		{
			this._undoableTerrain.SetTerrain();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002103 File Offset: 0x00000303
		public void Redo()
		{
			this._undoableTerrain.UnsetTerrain();
		}

		// Token: 0x04000007 RID: 7
		public readonly UndoableTerrain _undoableTerrain;
	}
}
