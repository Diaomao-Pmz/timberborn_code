using System;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.UndoSystem;

namespace Timberborn.TerrainUndoSystem
{
	// Token: 0x02000006 RID: 6
	public class TerrainUndoableRegistrar : ILoadableSingleton
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public TerrainUndoableRegistrar(IUndoRegistry undoRegistry, ITerrainService terrainService)
		{
			this._undoRegistry = undoRegistry;
			this._terrainService = terrainService;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002126 File Offset: 0x00000326
		public void Load()
		{
			this._terrainService.TerrainHeightChanged += this.OnTerrainChanged;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002140 File Offset: 0x00000340
		public void OnTerrainChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			if (this._undoRegistry.UndoAllowed && !this._undoRegistry.IsProcessingStack)
			{
				TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
				UndoableTerrain undoableTerrain = new UndoableTerrain(this._terrainService, change);
				IUndoable undoable2;
				if (!change.SetTerrain)
				{
					IUndoable undoable = new DeletedTerrainUndoable(undoableTerrain);
					undoable2 = undoable;
				}
				else
				{
					IUndoable undoable = new CreatedTerrainUndoable(undoableTerrain);
					undoable2 = undoable;
				}
				IUndoable undoable3 = undoable2;
				this._undoRegistry.RegisterStackedUndoable(undoable3);
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000009 RID: 9
		public readonly ITerrainService _terrainService;
	}
}
