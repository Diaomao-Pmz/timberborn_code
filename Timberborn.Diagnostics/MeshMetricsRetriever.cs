using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Diagnostics
{
	// Token: 0x0200000A RID: 10
	public class MeshMetricsRetriever
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000025E4 File Offset: 0x000007E4
		public MeshMetrics GetMeshMetrics(GameObject root)
		{
			MeshMetricsRetriever.MutableMetrics mutableMetrics = new MeshMetricsRetriever.MutableMetrics();
			MeshMetricsRetriever.Visit(root, mutableMetrics);
			BlockObject componentSlow = root.GetComponentSlow<BlockObject>();
			int? numberOfTrianglesPerTile = componentSlow ? new int?(mutableMetrics.NumberOfTriangles / MeshMetricsRetriever.NumberOfTiles(componentSlow)) : null;
			string name = root.name;
			int numberOfVertices = mutableMetrics.NumberOfVertices;
			int numberOfTriangles = mutableMetrics.NumberOfTriangles;
			int numberOfSubmeshes = mutableMetrics.NumberOfSubmeshes;
			return new MeshMetrics(name, numberOfVertices, numberOfTriangles, numberOfTrianglesPerTile, numberOfSubmeshes);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002650 File Offset: 0x00000850
		public static void Visit(GameObject gameObject, MeshMetricsRetriever.MutableMetrics mutableMetrics)
		{
			if (gameObject.activeSelf && !MeshMetricsRetriever.NameIsIgnored(gameObject.name))
			{
				MeshFilter meshFilter;
				SkinnedMeshRenderer skinnedMeshRenderer;
				if (gameObject.TryGetComponent<MeshFilter>(ref meshFilter))
				{
					if (meshFilter.GetComponent<MeshRenderer>().enabled)
					{
						MeshMetricsRetriever.CountMesh(meshFilter.sharedMesh, mutableMetrics);
					}
				}
				else if (gameObject.TryGetComponent<SkinnedMeshRenderer>(ref skinnedMeshRenderer) && skinnedMeshRenderer.enabled)
				{
					MeshMetricsRetriever.CountMesh(skinnedMeshRenderer.sharedMesh, mutableMetrics);
				}
				MeshMetricsRetriever.VisitChildren(gameObject, mutableMetrics);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026BC File Offset: 0x000008BC
		public static void VisitChildren(GameObject gameObject, MeshMetricsRetriever.MutableMetrics mutableMetrics)
		{
			int childCount = gameObject.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				MeshMetricsRetriever.Visit(gameObject.transform.GetChild(i).gameObject, mutableMetrics);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026F8 File Offset: 0x000008F8
		public static void CountMesh(Mesh mesh, MeshMetricsRetriever.MutableMetrics mutableMetrics)
		{
			mutableMetrics.NumberOfVertices += mesh.vertexCount;
			mutableMetrics.NumberOfTriangles += mesh.triangles.Length / 3;
			mutableMetrics.NumberOfSubmeshes += mesh.subMeshCount;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002738 File Offset: 0x00000938
		public static int NumberOfTiles(BlockObject blockObject)
		{
			Vector3Int[] array = blockObject.Blocks.GetOccupiedCoordinates().ToArray<Vector3Int>();
			return (from coords in array.Any<Vector3Int>() ? array : blockObject.Blocks.GetAllCoordinates().ToArray<Vector3Int>()
			select coords.XY()).Distinct<Vector2Int>().Count<Vector2Int>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000279F File Offset: 0x0000099F
		public static bool NameIsIgnored(string name)
		{
			return name.Contains("Marker") || name.Contains("StatusIcon") || name.Contains("#Unfinished");
		}

		// Token: 0x0200000B RID: 11
		public class MutableMetrics
		{
			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000028 RID: 40 RVA: 0x000027C8 File Offset: 0x000009C8
			// (set) Token: 0x06000029 RID: 41 RVA: 0x000027D0 File Offset: 0x000009D0
			public int NumberOfVertices { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600002A RID: 42 RVA: 0x000027D9 File Offset: 0x000009D9
			// (set) Token: 0x0600002B RID: 43 RVA: 0x000027E1 File Offset: 0x000009E1
			public int NumberOfTriangles { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x0600002C RID: 44 RVA: 0x000027EA File Offset: 0x000009EA
			// (set) Token: 0x0600002D RID: 45 RVA: 0x000027F2 File Offset: 0x000009F2
			public int NumberOfSubmeshes { get; set; }
		}
	}
}
