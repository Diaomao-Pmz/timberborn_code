using System;
using System.Collections.Generic;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x0200001D RID: 29
	public class MeshDrawer
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000040D5 File Offset: 0x000022D5
		public MeshDrawer(Mesh mesh, Material material, MaterialPropertyBlock defaultMaterialPropertyBlock, MaterialPropertyBlock modifiableMaterialPropertyBlock)
		{
			this._mesh = mesh;
			this._material = material;
			this._defaultMaterialPropertyBlock = defaultMaterialPropertyBlock;
			this._modifiableMaterialPropertyBlock = modifiableMaterialPropertyBlock;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000040FA File Offset: 0x000022FA
		public void DrawAtCoordinates(Vector3Int coordinates, float verticalOffset)
		{
			this.DrawAtCoordinates(coordinates, verticalOffset, Quaternion.identity);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000410C File Offset: 0x0000230C
		public void DrawAtCoordinates(Vector3Int coordinates, float verticalOffset, Color color)
		{
			Vector3 position = MeshDrawer.GetPosition(coordinates, verticalOffset);
			this.DrawAtPosition(position, Quaternion.identity, color);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004130 File Offset: 0x00002330
		public void DrawAtCoordinates(Vector3Int coordinates, float verticalOffset, Quaternion rotation)
		{
			Vector3 position = MeshDrawer.GetPosition(coordinates, verticalOffset);
			this.DrawAtPosition(position, rotation, this._defaultMaterialPropertyBlock);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004153 File Offset: 0x00002353
		public void DrawAtPosition(Vector3 position, Quaternion rotation, Color color)
		{
			this._modifiableMaterialPropertyBlock.SetColor(MeshDrawer.ColorProperty, color);
			this.DrawAtPosition(position, rotation, this._modifiableMaterialPropertyBlock);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004174 File Offset: 0x00002374
		public void DrawMultiple(List<Matrix4x4> matrices)
		{
			for (int i = 0; i < matrices.Count; i++)
			{
				this.Draw(matrices[i]);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000419F File Offset: 0x0000239F
		public void DrawMultipleInstanced(List<Matrix4x4> matrices)
		{
			Graphics.DrawMeshInstanced(this._mesh, 0, this._material, matrices);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000041B4 File Offset: 0x000023B4
		public void Draw(Matrix4x4 matrix)
		{
			this.Draw(matrix, this._defaultMaterialPropertyBlock);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000041C4 File Offset: 0x000023C4
		public void Draw(Matrix4x4 matrix, MaterialPropertyBlock materialPropertyBlock)
		{
			Graphics.DrawMesh(this._mesh, matrix, this._material, Layers.UILayer, null, 0, materialPropertyBlock, false, false, false);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000041F0 File Offset: 0x000023F0
		public void DrawAtPosition(Vector3 position, Quaternion rotation, MaterialPropertyBlock materialPropertyBlock)
		{
			Graphics.DrawMesh(this._mesh, position, rotation, this._material, Layers.UILayer, null, 0, materialPropertyBlock, false, false, false);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000421B File Offset: 0x0000241B
		public static Vector3 GetPosition(Vector3Int coordinates, float verticalOffset)
		{
			return CoordinateSystem.GridToWorldCentered(coordinates) + new Vector3(0f, verticalOffset, 0f);
		}

		// Token: 0x04000052 RID: 82
		public static readonly int ColorProperty = Shader.PropertyToID("_BaseColor");

		// Token: 0x04000053 RID: 83
		public readonly Mesh _mesh;

		// Token: 0x04000054 RID: 84
		public readonly Material _material;

		// Token: 0x04000055 RID: 85
		public readonly MaterialPropertyBlock _defaultMaterialPropertyBlock;

		// Token: 0x04000056 RID: 86
		public readonly MaterialPropertyBlock _modifiableMaterialPropertyBlock;
	}
}
