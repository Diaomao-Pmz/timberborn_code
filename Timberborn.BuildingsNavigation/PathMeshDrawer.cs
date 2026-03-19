using System;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using Timberborn.PrefabOptimization;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200001C RID: 28
	public class PathMeshDrawer
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public PathMeshDrawer(DistanceToColorConverter distanceToColorConverter, PathMeshDrawer.ConnectionKey connectionKey, NeighboredValues4<IntermediateMesh> meshes, Material material)
		{
			this._distanceToColorConverter = distanceToColorConverter;
			this._connectionKey = connectionKey;
			this._meshes = meshes;
			this._material = material;
			this._builtMesh = this._meshBuilder.Build(1).Mesh;
			this._builtMesh.MarkDynamic();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003E54 File Offset: 0x00002054
		public void Reset()
		{
			this._meshBuilder.Reset("");
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003E68 File Offset: 0x00002068
		public void Draw()
		{
			if (this._builtMesh.vertexCount > 0)
			{
				Graphics.DrawMesh(this._builtMesh, Vector3.zero, Quaternion.identity, this._material, Layers.UILayer, null, 0, null, false, false, false);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003EA9 File Offset: 0x000020A9
		public void Build()
		{
			this._meshBuilder.Build(this._builtMesh);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003EBC File Offset: 0x000020BC
		public void Add(WeightedCoordinates node)
		{
			Vector3Int coordinates = node.Coordinates;
			byte down = this._connectionKey(coordinates, Vector3Int.down);
			byte left = this._connectionKey(coordinates, Vector3Int.left);
			byte up = this._connectionKey(coordinates, Vector3Int.up);
			byte right = this._connectionKey(coordinates, Vector3Int.right);
			OrientedValue<IntermediateMesh> orientedValue;
			if (this._meshes.TryGetMatch(down, left, up, right, out orientedValue))
			{
				IntermediateMesh value = orientedValue.Value;
				Vector3 translation = CoordinateSystem.GridToWorldCentered(coordinates) + new Vector3(0f, PathMeshDrawer.VerticalOffset, 0f);
				Array.Fill<Color32>(value.Colors, this._distanceToColorConverter.DistanceToColor(node.Distance));
				this._meshBuilder.AppendIntermediateMesh<TranslationTransform>(value, new TranslationTransform(translation));
				return;
			}
			Debug.LogWarning(string.Format("Couldn't find an appropriate path marker mesh at {0}.", node.Coordinates) + "Please report this.");
		}

		// Token: 0x04000065 RID: 101
		public static readonly float VerticalOffset = 0.03f;

		// Token: 0x04000066 RID: 102
		public readonly DistanceToColorConverter _distanceToColorConverter;

		// Token: 0x04000067 RID: 103
		public readonly PathMeshDrawer.ConnectionKey _connectionKey;

		// Token: 0x04000068 RID: 104
		public readonly NeighboredValues4<IntermediateMesh> _meshes;

		// Token: 0x04000069 RID: 105
		public readonly Material _material;

		// Token: 0x0400006A RID: 106
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();

		// Token: 0x0400006B RID: 107
		public readonly Mesh _builtMesh;

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x060000B6 RID: 182
		public delegate byte ConnectionKey(Vector3Int coordinates, Vector3Int direction);
	}
}
