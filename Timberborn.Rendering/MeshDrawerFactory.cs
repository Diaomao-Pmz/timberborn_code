using System;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x0200001E RID: 30
	public class MeshDrawerFactory
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00004249 File Offset: 0x00002449
		public MeshDrawer Create(AssetRef<Mesh> mesh, Material material, Color color)
		{
			return MeshDrawerFactory.Create(mesh.Asset, material, MeshDrawerFactory.CreateColorizedMaterialPropertyBlock(color));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000425D File Offset: 0x0000245D
		public MeshDrawer Create(Mesh mesh, Material material)
		{
			return MeshDrawerFactory.Create(mesh, material, MeshDrawerFactory.CreateBlankMaterialPropertyBlock());
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000426B File Offset: 0x0000246B
		public static MeshDrawer Create(Mesh mesh, Material material, MaterialPropertyBlock defaultMaterialPropertyBlock)
		{
			return new MeshDrawer(mesh, material, defaultMaterialPropertyBlock, MeshDrawerFactory.CreateBlankMaterialPropertyBlock());
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000427A File Offset: 0x0000247A
		public static MaterialPropertyBlock CreateColorizedMaterialPropertyBlock(Color tileColor)
		{
			MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
			materialPropertyBlock.SetColor(MeshDrawerFactory.ColorProperty, tileColor);
			return materialPropertyBlock;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000428D File Offset: 0x0000248D
		public static MaterialPropertyBlock CreateBlankMaterialPropertyBlock()
		{
			return new MaterialPropertyBlock();
		}

		// Token: 0x04000057 RID: 87
		public static readonly int ColorProperty = Shader.PropertyToID("_BaseColor");
	}
}
