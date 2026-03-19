using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.SelectionSystem;
using Timberborn.TerrainSystem;

namespace Timberborn.Explosions
{
	// Token: 0x02000020 RID: 32
	public class UnstableCoreVisualisation : BaseComponent, IDeletableEntity, ISelectionListener, IPostPlacementChangeListener, IAwakableComponent
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00004BAD File Offset: 0x00002DAD
		public UnstableCoreVisualisation(ExplosionVisualizerService explosionVisualizerService, ITerrainService terrainService)
		{
			this._explosionVisualizerService = explosionVisualizerService;
			this._terrainService = terrainService;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004BC3 File Offset: 0x00002DC3
		public void Awake()
		{
			this._unstableCore = base.GetComponent<UnstableCore>();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004BD1 File Offset: 0x00002DD1
		public void DeleteEntity()
		{
			this._explosionVisualizerService.ClearSelected(this._unstableCore);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004BE4 File Offset: 0x00002DE4
		public void OnPostPlacementChanged()
		{
			this._explosionVisualizerService.UpdateHighlight(this._unstableCore);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004BF7 File Offset: 0x00002DF7
		public void OnSelect()
		{
			this._explosionVisualizerService.UpdateHighlight(this._unstableCore);
			this._terrainService.TerrainHeightChanged += this.OnTerrainChanged;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004C21 File Offset: 0x00002E21
		public void OnUnselect()
		{
			this._terrainService.TerrainHeightChanged -= this.OnTerrainChanged;
			this._explosionVisualizerService.ClearSelected(this._unstableCore);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004BE4 File Offset: 0x00002DE4
		public void OnTerrainChanged(object sender, TerrainHeightChangeEventArgs terrainChangedEvent)
		{
			this._explosionVisualizerService.UpdateHighlight(this._unstableCore);
		}

		// Token: 0x04000091 RID: 145
		public readonly ExplosionVisualizerService _explosionVisualizerService;

		// Token: 0x04000092 RID: 146
		public readonly ITerrainService _terrainService;

		// Token: 0x04000093 RID: 147
		public UnstableCore _unstableCore;
	}
}
