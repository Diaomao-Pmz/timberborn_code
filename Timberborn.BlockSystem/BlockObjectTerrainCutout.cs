using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200001D RID: 29
	public class BlockObjectTerrainCutout : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity, ICutoutTilesProvider
	{
		// Token: 0x060000DE RID: 222 RVA: 0x000043EE File Offset: 0x000025EE
		public BlockObjectTerrainCutout(TerrainCutout terrainCutout)
		{
			this._terrainCutout = terrainCutout;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000043FD File Offset: 0x000025FD
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectTerrainCutoutSpec = base.GetComponent<BlockObjectTerrainCutoutSpec>();
			Asserts.CollectionIsNotEmpty<Vector3Int>(this._blockObjectTerrainCutoutSpec.CutoutTiles, "CutoutTiles");
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004431 File Offset: 0x00002631
		public void InitializeEntity()
		{
			this._terrainCutout.SetCutout(this.GetPositionedCutoutTiles());
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004444 File Offset: 0x00002644
		public void DeleteEntity()
		{
			this._terrainCutout.UnsetCutout(this.GetPositionedCutoutTiles());
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004457 File Offset: 0x00002657
		public IEnumerable<Vector3Int> GetPositionedCutoutTiles()
		{
			return from tile in this._blockObjectTerrainCutoutSpec.CutoutTiles
			select this._blockObject.TransformCoordinates(tile);
		}

		// Token: 0x04000087 RID: 135
		public readonly TerrainCutout _terrainCutout;

		// Token: 0x04000088 RID: 136
		public BlockObject _blockObject;

		// Token: 0x04000089 RID: 137
		public BlockObjectTerrainCutoutSpec _blockObjectTerrainCutoutSpec;
	}
}
