using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;
using UnityEngine.Rendering;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x0200001B RID: 27
	public class MeshBuilder
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x0000445D File Offset: 0x0000265D
		public MeshBuilder()
		{
			this._name = "";
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004491 File Offset: 0x00002691
		public MeshBuilder(string name)
		{
			this._name = name;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000044C1 File Offset: 0x000026C1
		public bool IsEmpty
		{
			get
			{
				return this._vertexCount == 0;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000044CC File Offset: 0x000026CC
		public void Reset(string name)
		{
			this._name = name;
			this._vertexCount = 0;
			if (this._indices != null)
			{
				foreach (List<int> list in this._indices.Values)
				{
					list.Clear();
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004538 File Offset: 0x00002738
		public void AppendMesh<TTransform>(BuiltMesh meshAndMaterials, TTransform transform) where TTransform : ITransform
		{
			this.AppendMesh<TTransform>(meshAndMaterials.Mesh, meshAndMaterials.Materials, transform);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004550 File Offset: 0x00002750
		public void AppendMesh<TTransform>(Mesh sourceMesh, Material[] materials, TTransform transform) where TTransform : ITransform
		{
			int vertexCount = this._vertexCount;
			sourceMesh.GetUVs(0, this._uv0Cache);
			sourceMesh.GetUVs(1, this._uv1Cache);
			sourceMesh.GetUVs(2, this._uv2Cache);
			this.AppendVertexData<TTransform>(transform, sourceMesh.vertexCount, sourceMesh.vertices, sourceMesh.normals, sourceMesh.tangents, sourceMesh.colors32, this._uv0Cache.ToArray(), this._uv1Cache.ToArray(), this._uv2Cache.ToArray());
			this._uv0Cache.Clear();
			this._uv1Cache.Clear();
			this._uv2Cache.Clear();
			this.AppendIndices(sourceMesh, materials, vertexCount);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000045FC File Offset: 0x000027FC
		public void AppendIntermediateMesh<TTransform>(IntermediateMesh sourceMesh, TTransform transform) where TTransform : ITransform
		{
			int vertexCount = this._vertexCount;
			this.AppendVertexData<TTransform>(transform, sourceMesh.VertexCount, sourceMesh.Vertices, sourceMesh.Normals, sourceMesh.Tangents, sourceMesh.Colors, sourceMesh.UV0, sourceMesh.UV1, sourceMesh.UV2);
			this.AppendIndices(sourceMesh, vertexCount);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004650 File Offset: 0x00002850
		public BuiltMesh Build(IndexFormat indexFormat = 1)
		{
			Mesh mesh = new Mesh
			{
				name = this._name,
				indexFormat = indexFormat
			};
			Material[] materials;
			this.BuildInternal(out materials, mesh);
			return new BuiltMesh(mesh, materials);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004688 File Offset: 0x00002888
		public void Build(Mesh mesh)
		{
			mesh.Clear();
			Material[] array;
			this.BuildInternal(out array, mesh);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000046A4 File Offset: 0x000028A4
		public IntermediateMesh BuildIntermediateMesh()
		{
			ValueTuple<NullableKey<Material>, int[]>[] array = new ValueTuple<NullableKey<Material>, int[]>[this.CountNonEmptySubmeshes()];
			int num = 0;
			foreach (KeyValuePair<NullableKey<Material>, List<int>> keyValuePair in this._indices)
			{
				NullableKey<Material> key = keyValuePair.Key;
				List<int> value = keyValuePair.Value;
				if (!value.IsEmpty<int>())
				{
					array[num] = new ValueTuple<NullableKey<Material>, int[]>(key, value.ToArray());
					num++;
				}
			}
			return new IntermediateMesh
			{
				VertexCount = this._vertexCount,
				Vertices = MeshBuilder.CloneArrayOrEmpty<Vector3>(this._vertices, this._vertexCount),
				Normals = MeshBuilder.CloneArrayOrEmpty<Vector3>(this._normals, this._vertexCount),
				Tangents = MeshBuilder.CloneArrayOrEmpty<Vector4>(this._tangents, this._vertexCount),
				Colors = MeshBuilder.CloneArrayOrEmpty<Color32>(this._colors, this._vertexCount),
				UV0 = MeshBuilder.CloneArrayOrEmpty<Vector4>(this._uv0, this._vertexCount),
				UV1 = MeshBuilder.CloneArrayOrEmpty<Vector4>(this._uv1, this._vertexCount),
				UV2 = MeshBuilder.CloneArrayOrEmpty<Vector4>(this._uv2, this._vertexCount),
				Submeshes = array
			};
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000047EC File Offset: 0x000029EC
		public void AppendVertexData<TTransform>(TTransform transform, int sourceVertexCount, Vector3[] sourceVertices, Vector3[] sourceNormals, Vector4[] sourceTangents, Color32[] sourceColors, Vector4[] sourceUV0, Vector4[] sourceUV1, Vector4[] sourceUV2) where TTransform : ITransform
		{
			int num = this._vertexCount + sourceVertexCount;
			if (this._vertexCapacity < num)
			{
				this._vertexCapacity = ((this._vertexCapacity == 0) ? Math.Max(MeshBuilder.InitialCapacity, num) : Math.Max(this._vertexCapacity * MeshBuilder.CapacityGrowthRate, num));
				MeshBuilder.CreateOrResize<Vector3>(ref this._vertices, this._vertexCapacity);
			}
			if (!this._normals.IsNullOrEmpty<Vector3>() || !sourceNormals.IsNullOrEmpty<Vector3>())
			{
				MeshBuilder.CreateOrResize<Vector3>(ref this._normals, this._vertexCapacity, Vector3.up);
			}
			if (!this._tangents.IsNullOrEmpty<Vector4>() || !sourceTangents.IsNullOrEmpty<Vector4>())
			{
				MeshBuilder.CreateOrResize<Vector4>(ref this._tangents, this._vertexCapacity);
			}
			if (!this._colors.IsNullOrEmpty<Color32>() || !sourceColors.IsNullOrEmpty<Color32>())
			{
				MeshBuilder.CreateOrResize<Color32>(ref this._colors, this._vertexCapacity, MeshBuilder.White);
			}
			if (!this._uv0.IsNullOrEmpty<Vector4>() || !sourceUV0.IsNullOrEmpty<Vector4>())
			{
				MeshBuilder.CreateOrResize<Vector4>(ref this._uv0, this._vertexCapacity);
			}
			if (!this._uv1.IsNullOrEmpty<Vector4>() || !sourceUV1.IsNullOrEmpty<Vector4>())
			{
				MeshBuilder.CreateOrResize<Vector4>(ref this._uv1, this._vertexCapacity);
			}
			if (!this._uv2.IsNullOrEmpty<Vector4>() || !sourceUV2.IsNullOrEmpty<Vector4>())
			{
				MeshBuilder.CreateOrResize<Vector4>(ref this._uv2, this._vertexCapacity);
			}
			int vertexCount = this._vertexCount;
			transform.MultiplyPoints(sourceVertices, this._vertices, vertexCount, sourceVertexCount);
			if (!sourceNormals.IsNullOrEmpty<Vector3>())
			{
				transform.MultiplyNormals(sourceNormals, this._normals, vertexCount, sourceVertexCount);
			}
			if (!sourceTangents.IsNullOrEmpty<Vector4>())
			{
				transform.MultiplyTangents(sourceTangents, this._tangents, vertexCount, sourceVertexCount);
			}
			if (!sourceColors.IsNullOrEmpty<Color32>())
			{
				Array.Copy(sourceColors, 0, this._colors, vertexCount, sourceVertexCount);
			}
			if (!sourceUV0.IsNullOrEmpty<Vector4>())
			{
				Array.Copy(sourceUV0, 0, this._uv0, vertexCount, sourceVertexCount);
			}
			if (!sourceUV1.IsNullOrEmpty<Vector4>())
			{
				Array.Copy(sourceUV1, 0, this._uv1, vertexCount, sourceVertexCount);
			}
			if (!sourceUV2.IsNullOrEmpty<Vector4>())
			{
				Array.Copy(sourceUV2, 0, this._uv2, vertexCount, sourceVertexCount);
			}
			this._vertexCount += sourceVertexCount;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004A0C File Offset: 0x00002C0C
		public void AppendIndices(Mesh sourceMesh, Material[] materials, int baseVertexIndex)
		{
			int subMeshCount = sourceMesh.subMeshCount;
			if (subMeshCount > 0)
			{
				this.AllocateIndices();
				for (int i = 0; i < subMeshCount; i++)
				{
					int[] indices = sourceMesh.GetIndices(i);
					Material key = materials[i];
					List<int> orAdd = this._indices.GetOrAdd(new NullableKey<Material>(key));
					MeshBuilder.AppendSubmeshIndices(baseVertexIndex, indices, orAdd);
				}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004A60 File Offset: 0x00002C60
		public void AppendIndices(IntermediateMesh sourceMesh, int baseVertexIndex)
		{
			ValueTuple<NullableKey<Material>, int[]>[] submeshes = sourceMesh.Submeshes;
			int num = submeshes.Length;
			if (num > 0)
			{
				this.AllocateIndices();
				for (int i = 0; i < num; i++)
				{
					ValueTuple<NullableKey<Material>, int[]> valueTuple = submeshes[i];
					NullableKey<Material> item = valueTuple.Item1;
					int[] item2 = valueTuple.Item2;
					List<int> orAdd = this._indices.GetOrAdd(item);
					MeshBuilder.AppendSubmeshIndices(baseVertexIndex, item2, orAdd);
				}
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004ABB File Offset: 0x00002CBB
		public void AllocateIndices()
		{
			if (this._indices == null)
			{
				this._indices = new Dictionary<NullableKey<Material>, List<int>>();
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public static void AppendSubmeshIndices(int baseVertexIndex, int[] sourceIndices, ICollection<int> targetIndices)
		{
			for (int i = 0; i < sourceIndices.Length; i++)
			{
				targetIndices.Add(baseVertexIndex + sourceIndices[i]);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public void BuildInternal(out Material[] materials, Mesh mesh)
		{
			mesh.SetVertices(this._vertices, 0, this._vertexCount);
			if (!this._normals.IsNullOrEmpty<Vector3>())
			{
				mesh.SetNormals(this._normals, 0, this._vertexCount);
			}
			if (!this._tangents.IsNullOrEmpty<Vector4>())
			{
				mesh.SetTangents(this._tangents, 0, this._vertexCount);
			}
			if (!this._colors.IsNullOrEmpty<Color32>())
			{
				mesh.SetColors(this._colors, 0, this._vertexCount);
			}
			if (!this._uv0.IsNullOrEmpty<Vector4>())
			{
				mesh.SetUVs(0, this._uv0, 0, this._vertexCount);
			}
			if (!this._uv1.IsNullOrEmpty<Vector4>())
			{
				mesh.SetUVs(1, this._uv1, 0, this._vertexCount);
			}
			if (!this._uv2.IsNullOrEmpty<Vector4>())
			{
				mesh.SetUVs(2, this._uv2, 0, this._vertexCount);
			}
			List<Material> list = new List<Material>();
			if (this._indices != null)
			{
				int num = 0;
				mesh.subMeshCount = this.CountNonEmptySubmeshes();
				foreach (KeyValuePair<NullableKey<Material>, List<int>> keyValuePair in this._indices)
				{
					NullableKey<Material> key = keyValuePair.Key;
					List<int> value = keyValuePair.Value;
					if (!value.IsEmpty<int>())
					{
						list.Add(key.Key);
						mesh.SetIndices(value, 0, num, true, 0);
						num++;
					}
				}
			}
			mesh.RecalculateBounds();
			materials = list.ToArray();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004C7C File Offset: 0x00002E7C
		public int CountNonEmptySubmeshes()
		{
			int num = 0;
			using (Dictionary<NullableKey<Material>, List<int>>.ValueCollection.Enumerator enumerator = this._indices.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsEmpty<int>())
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004CDC File Offset: 0x00002EDC
		public static void CreateOrResize<T>(ref T[] array, int newSize)
		{
			if (array == null)
			{
				array = new T[newSize];
			}
			if (array.Length != newSize)
			{
				Array.Resize<T>(ref array, newSize);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public static void CreateOrResize<T>(ref T[] array, int newSize, T value)
		{
			T[] array2 = array;
			int num = (array2 != null) ? array2.Length : 0;
			MeshBuilder.CreateOrResize<T>(ref array, newSize);
			for (int i = num; i < newSize; i++)
			{
				array[i] = value;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004D2C File Offset: 0x00002F2C
		public static T[] CloneArrayOrEmpty<T>(T[] source, int length)
		{
			if (source == null)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[length];
			Array.Copy(source, array, length);
			return array;
		}

		// Token: 0x04000068 RID: 104
		public static readonly int InitialCapacity = 6000;

		// Token: 0x04000069 RID: 105
		public static readonly int CapacityGrowthRate = 2;

		// Token: 0x0400006A RID: 106
		public static readonly Color32 White = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x0400006B RID: 107
		public string _name;

		// Token: 0x0400006C RID: 108
		public int _vertexCount;

		// Token: 0x0400006D RID: 109
		public int _vertexCapacity;

		// Token: 0x0400006E RID: 110
		public Vector3[] _vertices;

		// Token: 0x0400006F RID: 111
		public Vector3[] _normals;

		// Token: 0x04000070 RID: 112
		public Vector4[] _tangents;

		// Token: 0x04000071 RID: 113
		public Color32[] _colors;

		// Token: 0x04000072 RID: 114
		public Vector4[] _uv0;

		// Token: 0x04000073 RID: 115
		public Vector4[] _uv1;

		// Token: 0x04000074 RID: 116
		public Vector4[] _uv2;

		// Token: 0x04000075 RID: 117
		public Dictionary<NullableKey<Material>, List<int>> _indices;

		// Token: 0x04000076 RID: 118
		public readonly List<Vector4> _uv0Cache = new List<Vector4>();

		// Token: 0x04000077 RID: 119
		public readonly List<Vector4> _uv1Cache = new List<Vector4>();

		// Token: 0x04000078 RID: 120
		public readonly List<Vector4> _uv2Cache = new List<Vector4>();
	}
}
