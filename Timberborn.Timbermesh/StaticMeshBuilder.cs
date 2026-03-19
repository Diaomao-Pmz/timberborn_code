using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.TimbermeshDTO;
using UnityEngine;

namespace Timberborn.Timbermesh
{
	// Token: 0x0200000A RID: 10
	public class StaticMeshBuilder
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000213C File Offset: 0x0000033C
		public StaticMeshBuilder(IMaterialRepository materialRepository)
		{
			this._materialRepository = materialRepository;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002190 File Offset: 0x00000390
		public void BuildMesh(GameObject meshContainer, Node node)
		{
			Mesh mesh = this.BuildMesh(node);
			if (mesh)
			{
				this.SetMaterials(node);
				this.CreateMeshComponents(mesh, meshContainer);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021BC File Offset: 0x000003BC
		public Mesh BuildMesh(Node node)
		{
			int vertexCount = node.VertexCount;
			if (vertexCount > 0)
			{
				Mesh mesh = new Mesh
				{
					name = node.Name,
					indexFormat = ((vertexCount > StaticMeshBuilder.VertexLimitFor16BitIndexBuffer) ? 1 : 0)
				};
				this.SetVertices(mesh, node);
				this.SetNormals(mesh, node);
				this.SetTangents(mesh, node);
				this.SetColors(mesh, node);
				this.SetUV(mesh, node, 0);
				this.SetUV(mesh, node, 1);
				this.SetUV(mesh, node, 2);
				StaticMeshBuilder.SetSubmeshes(mesh, node);
				StaticMeshBuilder.OptimizeMesh(mesh, node);
				return mesh;
			}
			return null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002244 File Offset: 0x00000444
		public void SetVertices(Mesh mesh, Node node)
		{
			node.ReadProperties("position", this._vector3ImportCache);
			if (this._vector3ImportCache.Any<Vector3>())
			{
				mesh.SetVertices(this._vector3ImportCache);
			}
			this._vector3ImportCache.Clear();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000227B File Offset: 0x0000047B
		public void SetNormals(Mesh mesh, Node node)
		{
			node.ReadProperties("normal", this._vector3ImportCache);
			if (this._vector3ImportCache.Any<Vector3>())
			{
				mesh.SetNormals(this._vector3ImportCache);
			}
			this._vector3ImportCache.Clear();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022B2 File Offset: 0x000004B2
		public void SetTangents(Mesh mesh, Node node)
		{
			node.ReadProperties("tangent", this._vector4ImportCache);
			if (this._vector4ImportCache.Any<Vector4>())
			{
				mesh.SetTangents(this._vector4ImportCache);
			}
			this._vector4ImportCache.Clear();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022E9 File Offset: 0x000004E9
		public void SetColors(Mesh mesh, Node node)
		{
			node.ReadProperties("color", this._colorImportCache);
			if (this._colorImportCache.Any<Color>())
			{
				mesh.SetColors(this._colorImportCache);
			}
			this._colorImportCache.Clear();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002320 File Offset: 0x00000520
		public void SetUV(Mesh mesh, Node node, int channel)
		{
			node.ReadProperties(string.Format("uv{0}", channel), this._vector2ImportCache);
			if (this._vector2ImportCache.Any<Vector2>())
			{
				mesh.SetUVs(channel, this._vector2ImportCache);
			}
			this._vector2ImportCache.Clear();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002370 File Offset: 0x00000570
		public static void SetSubmeshes(Mesh mesh, Node node)
		{
			mesh.subMeshCount = node.Meshes.Count;
			for (int i = 0; i < node.Meshes.Count; i++)
			{
				Mesh mesh2 = node.Meshes[i];
				mesh.SetIndices(mesh2.Indices, 0, i, true, 0);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023C4 File Offset: 0x000005C4
		public void SetMaterials(Node node)
		{
			for (int i = 0; i < node.Meshes.Count; i++)
			{
				Mesh mesh = node.Meshes[i];
				Material material = this._materialRepository.GetMaterial(mesh.Material);
				this._materialsToSetCache.Add(material);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002412 File Offset: 0x00000612
		public static void OptimizeMesh(Mesh mesh, Node node)
		{
			if (!node.VertexAnimations.Any<VertexAnimation>())
			{
				mesh.Optimize();
			}
			mesh.RecalculateBounds();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000242D File Offset: 0x0000062D
		public void CreateMeshComponents(Mesh mesh, GameObject meshContainer)
		{
			meshContainer.AddComponent<MeshFilter>().sharedMesh = mesh;
			meshContainer.AddComponent<MeshRenderer>().sharedMaterials = this._materialsToSetCache.ToArray();
			this._materialsToSetCache.Clear();
		}

		// Token: 0x0400000A RID: 10
		public static readonly int VertexLimitFor16BitIndexBuffer = 65535;

		// Token: 0x0400000B RID: 11
		public readonly IMaterialRepository _materialRepository;

		// Token: 0x0400000C RID: 12
		public readonly List<Vector2> _vector2ImportCache = new List<Vector2>();

		// Token: 0x0400000D RID: 13
		public readonly List<Vector3> _vector3ImportCache = new List<Vector3>();

		// Token: 0x0400000E RID: 14
		public readonly List<Vector4> _vector4ImportCache = new List<Vector4>();

		// Token: 0x0400000F RID: 15
		public readonly List<Color> _colorImportCache = new List<Color>();

		// Token: 0x04000010 RID: 16
		public readonly List<Material> _materialsToSetCache = new List<Material>();
	}
}
