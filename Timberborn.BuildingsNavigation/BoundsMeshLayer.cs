using System;
using Timberborn.PrefabOptimization;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000008 RID: 8
	public class BoundsMeshLayer
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002266 File Offset: 0x00000466
		public BoundsMeshLayer(Material material, MeshBuilder meshBuilder, Mesh mesh)
		{
			this._material = material;
			this._meshBuilder = meshBuilder;
			this._mesh = mesh;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002284 File Offset: 0x00000484
		public static BoundsMeshLayer Create(Material baseMaterial, int layerIndex)
		{
			Material material = new Material(baseMaterial);
			material.renderQueue = baseMaterial.renderQueue + layerIndex;
			MeshBuilder meshBuilder = new MeshBuilder();
			Mesh mesh = meshBuilder.Build(1).Mesh;
			mesh.MarkDynamic();
			return new BoundsMeshLayer(material, meshBuilder, mesh);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C8 File Offset: 0x000004C8
		public void Reset()
		{
			this._meshBuilder.Reset(string.Empty);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022DA File Offset: 0x000004DA
		public void Build()
		{
			this._meshBuilder.Build(this._mesh);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022ED File Offset: 0x000004ED
		public void AppendMesh(IntermediateMesh mesh, TranslationTransform translation)
		{
			this._meshBuilder.AppendIntermediateMesh<TranslationTransform>(mesh, translation);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022FC File Offset: 0x000004FC
		public void Draw()
		{
			if (this._mesh.vertexCount > 0)
			{
				Graphics.DrawMesh(this._mesh, Vector3.zero, Quaternion.identity, this._material, Layers.UILayer, null, 0, null, false, false, false);
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly Material _material;

		// Token: 0x0400000B RID: 11
		public readonly MeshBuilder _meshBuilder;

		// Token: 0x0400000C RID: 12
		public readonly Mesh _mesh;
	}
}
