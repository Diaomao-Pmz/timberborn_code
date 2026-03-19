using System;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000015 RID: 21
	public class MarkerDrawerFactory : ILoadableSingleton
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002ED5 File Offset: 0x000010D5
		public MarkerDrawerFactory(MeshDrawerFactory meshDrawerFactory, ISpecService specService)
		{
			this._meshDrawerFactory = meshDrawerFactory;
			this._specService = specService;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002EEB File Offset: 0x000010EB
		public void Load()
		{
			this._markerDrawerFactorySpec = this._specService.GetSingleSpec<MarkerDrawerFactorySpec>();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002EFE File Offset: 0x000010FE
		public MeshDrawer CreateTileDrawer(Color tileColor)
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.TileMesh, this._markerDrawerFactorySpec.TileMaterial.Asset, tileColor);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F27 File Offset: 0x00001127
		public MeshDrawer CreatePrioritizedTileDrawer(Color tileColor)
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.TileMesh, this._markerDrawerFactorySpec.PrioritizedTileMaterial.Asset, tileColor);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F50 File Offset: 0x00001150
		public MeshDrawer CreateTileDrawer()
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.TileMesh.Asset, this._markerDrawerFactorySpec.TileMaterial.Asset);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F7D File Offset: 0x0000117D
		public MeshDrawer CreateSmallBlockTileDrawer()
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.SmallBlockMesh.Asset, this._markerDrawerFactorySpec.TileMaterial.Asset);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FAA File Offset: 0x000011AA
		public MeshDrawer CreateLargeBlockTileDrawer()
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.LargeBlockMesh.Asset, this._markerDrawerFactorySpec.TileMaterial.Asset);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002FD7 File Offset: 0x000011D7
		public MeshDrawer CreateTerrainTileDrawer()
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.TerrainBlockMesh.Asset, this._markerDrawerFactorySpec.TerrainTileMaterial.Asset);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003004 File Offset: 0x00001204
		public MeshDrawer CreateTopTerrainTileDrawer()
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.TopTerrainTileMesh.Asset, this._markerDrawerFactorySpec.TopTerrainTileMaterial.Asset);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003031 File Offset: 0x00001231
		public MeshDrawer CreateEntranceMarkerDrawer()
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.EntranceMesh.Asset, this._markerDrawerFactorySpec.EntranceMarkerMaterial.Asset);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000305E File Offset: 0x0000125E
		public MeshDrawer CreateMechanicalInputMarkerDrawer(Color markerColor)
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.MechanicalInputMesh, this._markerDrawerFactorySpec.MechanicalMarkerMaterial.Asset, markerColor);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003087 File Offset: 0x00001287
		public MeshDrawer CreateMechanicalOutputMarkerDrawer(Color markerColor)
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.MechanicalOutputMesh, this._markerDrawerFactorySpec.MechanicalMarkerMaterial.Asset, markerColor);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030B0 File Offset: 0x000012B0
		public MeshDrawer CreateArrowMarkerDrawer()
		{
			return this._meshDrawerFactory.Create(this._markerDrawerFactorySpec.ArrowMesh.Asset, this._markerDrawerFactorySpec.ArrowMaterial.Asset);
		}

		// Token: 0x04000026 RID: 38
		public readonly MeshDrawerFactory _meshDrawerFactory;

		// Token: 0x04000027 RID: 39
		public readonly ISpecService _specService;

		// Token: 0x04000028 RID: 40
		public MarkerDrawerFactorySpec _markerDrawerFactorySpec;
	}
}
