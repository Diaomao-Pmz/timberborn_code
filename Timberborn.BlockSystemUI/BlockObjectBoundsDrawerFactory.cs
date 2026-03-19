using System;
using Timberborn.BlueprintSystem;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BlockSystemUI
{
	// Token: 0x02000009 RID: 9
	public class BlockObjectBoundsDrawerFactory : ILoadableSingleton
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000022BF File Offset: 0x000004BF
		public BlockObjectBoundsDrawerFactory(MeshDrawerFactory meshDrawerFactory, ISpecService specService)
		{
			this._meshDrawerFactory = meshDrawerFactory;
			this._specService = specService;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022D5 File Offset: 0x000004D5
		public void Load()
		{
			this._blockObjectBoundsDrawerFactorySpec = this._specService.GetSingleSpec<BlockObjectBoundsDrawerFactorySpec>();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022E8 File Offset: 0x000004E8
		public BlockObjectBoundsDrawer Create(Color color)
		{
			Material asset = this._blockObjectBoundsDrawerFactorySpec.Material.Asset;
			MeshDrawer blockSideMeshDrawer = this._meshDrawerFactory.Create(this._blockObjectBoundsDrawerFactorySpec.BlockSideMesh0010, asset, color);
			MeshDrawer blockSideMeshDrawer2 = this._meshDrawerFactory.Create(this._blockObjectBoundsDrawerFactorySpec.BlockSideMesh0011, asset, color);
			MeshDrawer blockSideMeshDrawer3 = this._meshDrawerFactory.Create(this._blockObjectBoundsDrawerFactorySpec.BlockSideMesh0111, asset, color);
			MeshDrawer blockSideMeshDrawer4 = this._meshDrawerFactory.Create(this._blockObjectBoundsDrawerFactorySpec.BlockSideMesh1010, asset, color);
			MeshDrawer blockSideMeshDrawer5 = this._meshDrawerFactory.Create(this._blockObjectBoundsDrawerFactorySpec.BlockSideMesh1111, asset, color);
			return new BlockObjectBoundsDrawer(blockSideMeshDrawer, blockSideMeshDrawer2, blockSideMeshDrawer3, blockSideMeshDrawer4, blockSideMeshDrawer5);
		}

		// Token: 0x0400000A RID: 10
		public readonly MeshDrawerFactory _meshDrawerFactory;

		// Token: 0x0400000B RID: 11
		public readonly ISpecService _specService;

		// Token: 0x0400000C RID: 12
		public BlockObjectBoundsDrawerFactorySpec _blockObjectBoundsDrawerFactorySpec;
	}
}
