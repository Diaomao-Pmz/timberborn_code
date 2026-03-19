using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x0200001A RID: 26
	public class BuildingTerrainCutout : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity, ICutoutTilesProvider
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00003BB3 File Offset: 0x00001DB3
		public BuildingTerrainCutout(TerrainCutout terrainCutout)
		{
			this._terrainCutout = terrainCutout;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003BC2 File Offset: 0x00001DC2
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectModelController = base.GetComponent<BlockObjectModelController>();
			this._buildingTerrainCutoutSpec = base.GetComponent<BuildingTerrainCutoutSpec>();
			Asserts.CollectionIsNotEmpty<Vector3Int>(this._buildingTerrainCutoutSpec.CutoutTiles, "CutoutTiles");
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003C02 File Offset: 0x00001E02
		public void InitializeEntity()
		{
			this._blockObjectModelController.ModelsUpdated += this.OnModelsUpdated;
			this.UpdateCutout();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003C21 File Offset: 0x00001E21
		public void DeleteEntity()
		{
			this._blockObjectModelController.ModelsUpdated -= this.OnModelsUpdated;
			this._terrainCutout.UnsetCutout(this.GetPositionedCutoutTiles());
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003C4B File Offset: 0x00001E4B
		public IEnumerable<Vector3Int> GetPositionedCutoutTiles()
		{
			return from tile in this._buildingTerrainCutoutSpec.CutoutTiles
			select this._blockObject.TransformCoordinates(tile);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003C69 File Offset: 0x00001E69
		public void OnModelsUpdated(object sender, EventArgs e)
		{
			this.UpdateCutout();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003C74 File Offset: 0x00001E74
		public void UpdateCutout()
		{
			if (!this._blockObject.IsPreview)
			{
				if (this._blockObjectModelController.IsFinishedModelShown && !this._isCutoutSet)
				{
					this._terrainCutout.SetCutout(this.GetPositionedCutoutTiles());
					this._isCutoutSet = true;
					return;
				}
				if (!this._blockObjectModelController.IsFinishedModelShown && this._isCutoutSet)
				{
					this._terrainCutout.UnsetCutout(this.GetPositionedCutoutTiles());
					this._isCutoutSet = false;
				}
			}
		}

		// Token: 0x04000042 RID: 66
		public readonly TerrainCutout _terrainCutout;

		// Token: 0x04000043 RID: 67
		public BlockObject _blockObject;

		// Token: 0x04000044 RID: 68
		public BlockObjectModelController _blockObjectModelController;

		// Token: 0x04000045 RID: 69
		public BuildingTerrainCutoutSpec _buildingTerrainCutoutSpec;

		// Token: 0x04000046 RID: 70
		public bool _isCutoutSet;
	}
}
