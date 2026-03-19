using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000007 RID: 7
	public class GoodColumnVariantsService : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public GoodColumnVariantsService(TemplateService templateService, GoodVisualizationSpecService goodVisualizationSpecService, IRandomNumberGenerator randomNumberGenerator)
		{
			this._templateService = templateService;
			this._goodVisualizationSpecService = goodVisualizationSpecService;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public void Load()
		{
			foreach (StockpileGoodColumnVisualizerSpec visualizer in this._templateService.GetAll<StockpileGoodColumnVisualizerSpec>())
			{
				this.LoadVisualizerVariants(visualizer);
			}
			this._meshBuilder.Reset("");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002198 File Offset: 0x00000398
		public void Unload()
		{
			foreach (Mesh mesh in this._variants.Values)
			{
				Object.Destroy(mesh);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F0 File Offset: 0x000003F0
		public Mesh GetVariant(StockpileGoodColumnVisualizer visualizer, int amount)
		{
			return this._variants[GoodColumnVariantsService.GetKey(visualizer.GetComponent<TemplateSpec>(), visualizer.CurrentVisualization, amount)];
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002210 File Offset: 0x00000410
		public void LoadVisualizerVariants(StockpileGoodColumnVisualizerSpec visualizer)
		{
			GoodVisualizationSpec visualization = this._goodVisualizationSpecService.GetVisualization(visualizer.GoodVisualizationId, visualizer.GoodVisualizationVariant);
			IntermediateMesh mesh = this.BuildIntermediateMesh(visualization);
			TemplateSpec spec = visualizer.GetSpec<TemplateSpec>();
			BlockObjectSpec spec2 = visualizer.GetSpec<BlockObjectSpec>();
			List<Vector3> list = new List<Vector3>(GoodColumnVariantsService.GetColumnPositions(visualization, spec2));
			for (int i = 0; i < list.Count; i++)
			{
				if (i != 0)
				{
					this.RiseRandomColumn(list, visualization, spec2);
				}
				this.AddVariant(list, mesh, GoodColumnVariantsService.GetKey(spec, visualization, i));
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002290 File Offset: 0x00000490
		public IntermediateMesh BuildIntermediateMesh(GoodVisualizationSpec visualization)
		{
			this._meshBuilder.Reset("");
			AssetRef<Mesh> primaryMesh = visualization.PrimaryMesh;
			AssetRef<Material> material = visualization.Material;
			this._meshBuilder.AppendMesh<TranslationTransform>(primaryMesh.Asset, new Material[]
			{
				material.Asset
			}, new TranslationTransform(Vector3.zero));
			return this._meshBuilder.BuildIntermediateMesh();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022F0 File Offset: 0x000004F0
		public static IEnumerable<Vector3> GetColumnPositions(GoodVisualizationSpec visualization, BlockObjectSpec blockObjectSpec)
		{
			IEnumerable<Vector3Int> enumerable = from coords in blockObjectSpec.GetBlocks().GetOccupiedCoordinates()
			where coords.z == blockObjectSpec.BaseZ
			select coords;
			foreach (Vector3Int baseLevelCoord in enumerable)
			{
				int num;
				for (int x = -1; x <= 1; x = num)
				{
					for (int y = -1; y <= 1; y = num)
					{
						Vector3 offset = visualization.Offset;
						yield return baseLevelCoord + new Vector3((float)x * offset.x, (float)y * offset.y, 0f);
						num = y + 1;
					}
					num = x + 1;
				}
				baseLevelCoord = default(Vector3Int);
			}
			IEnumerator<Vector3Int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002308 File Offset: 0x00000508
		public void RiseRandomColumn(ICollection<Vector3> positions, GoodVisualizationSpec visualization, BlockObjectSpec blockObjectSpec)
		{
			IEnumerable<Vector3> source = from position in positions
			where Mathf.Approximately(position.z, (float)blockObjectSpec.BaseZ)
			select position;
			Vector3 enumerableElement = this._randomNumberGenerator.GetEnumerableElement<Vector3>(source);
			positions.Remove(enumerableElement);
			positions.Add(enumerableElement + new Vector3(0f, 0f, visualization.Offset.z));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002370 File Offset: 0x00000570
		public void AddVariant(List<Vector3> positions, IntermediateMesh mesh, string key)
		{
			this._meshBuilder.Reset("");
			foreach (Vector3 coordinates in positions)
			{
				Vector3 translation = CoordinateSystem.GridToWorld(coordinates);
				TranslationTransform transform = new TranslationTransform(translation);
				this._meshBuilder.AppendIntermediateMesh<TranslationTransform>(mesh, transform);
			}
			this._variants.Add(key, this._meshBuilder.Build(1).Mesh);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002404 File Offset: 0x00000604
		public static string GetKey(TemplateSpec templateSpec, GoodVisualizationSpec visualization, int amount)
		{
			return string.Format("{0}{1}{2}{3}", new object[]
			{
				templateSpec.TemplateName,
				visualization.Id,
				visualization.Variant,
				amount
			});
		}

		// Token: 0x04000008 RID: 8
		public readonly TemplateService _templateService;

		// Token: 0x04000009 RID: 9
		public readonly GoodVisualizationSpecService _goodVisualizationSpecService;

		// Token: 0x0400000A RID: 10
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000B RID: 11
		public readonly Dictionary<string, Mesh> _variants = new Dictionary<string, Mesh>();

		// Token: 0x0400000C RID: 12
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();
	}
}
