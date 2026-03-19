using System;
using Timberborn.BlueprintSystem;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x02000023 RID: 35
	public class RectangleBoundsDrawerFactory : ILoadableSingleton
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003B16 File Offset: 0x00001D16
		public RectangleBoundsDrawerFactory(MeshDrawerFactory meshDrawerFactory, ISpecService specService)
		{
			this._meshDrawerFactory = meshDrawerFactory;
			this._specService = specService;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003B2C File Offset: 0x00001D2C
		public void Load()
		{
			this._rectangleBoundsDrawerFactorySpec = this._specService.GetSingleSpec<RectangleBoundsDrawerFactorySpec>();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003B40 File Offset: 0x00001D40
		public RectangleBoundsDrawer Create(Color tileColor, Color blockSideColor)
		{
			return new RectangleBoundsDrawer(this.CreateSide(this._rectangleBoundsDrawerFactorySpec.BlockSideMesh0010, blockSideColor), this.CreateSide(this._rectangleBoundsDrawerFactorySpec.BlockSideMesh0011, blockSideColor), this.CreateSide(this._rectangleBoundsDrawerFactorySpec.BlockSideMesh0111, blockSideColor), this.CreateSide(this._rectangleBoundsDrawerFactorySpec.BlockSideMesh1010, blockSideColor), this.CreateSide(this._rectangleBoundsDrawerFactorySpec.BlockSideMesh1111, blockSideColor), this._meshDrawerFactory.Create(this._rectangleBoundsDrawerFactorySpec.BlockBottomMesh, this._rectangleBoundsDrawerFactorySpec.BlockBottomMaterial.Asset, tileColor));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003BD3 File Offset: 0x00001DD3
		public MeshDrawer CreateSide(AssetRef<Mesh> blockSideMesh0010, Color blockSideColor)
		{
			return this._meshDrawerFactory.Create(blockSideMesh0010, this._rectangleBoundsDrawerFactorySpec.BlockSideMaterial.Asset, blockSideColor);
		}

		// Token: 0x0400006E RID: 110
		public readonly MeshDrawerFactory _meshDrawerFactory;

		// Token: 0x0400006F RID: 111
		public readonly ISpecService _specService;

		// Token: 0x04000070 RID: 112
		public RectangleBoundsDrawerFactorySpec _rectangleBoundsDrawerFactorySpec;
	}
}
