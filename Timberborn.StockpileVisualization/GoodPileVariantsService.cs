using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using Timberborn.Stockpiles;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x0200000B RID: 11
	public class GoodPileVariantsService : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000026D3 File Offset: 0x000008D3
		public GoodPileVariantsService(TemplateService templateService, GoodVisualizationSpecService goodVisualizationSpecService, IRandomNumberGenerator randomNumberGenerator)
		{
			this._templateService = templateService;
			this._goodVisualizationSpecService = goodVisualizationSpecService;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002714 File Offset: 0x00000914
		public void Load()
		{
			foreach (StockpileGoodPileVisualizerSpec visualizer in this._templateService.GetAll<StockpileGoodPileVisualizerSpec>())
			{
				this.LoadVisualizerVariants(visualizer);
			}
			this._meshBuilder.Reset("");
			this._primaryIntermediateMesh = null;
			this._secondaryIntermediateMesh = null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002784 File Offset: 0x00000984
		public void Unload()
		{
			foreach (Mesh mesh in this._variants.Values)
			{
				Object.Destroy(mesh);
			}
			foreach (Mesh mesh2 in this._rotatedVariants.Values)
			{
				Object.Destroy(mesh2);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002820 File Offset: 0x00000A20
		public Mesh GetVariant(StockpileGoodPileVisualizer visualizer, int amount, bool rotated)
		{
			string key = GoodPileVariantsService.GetKey(visualizer.GetComponent<TemplateSpec>(), visualizer.CurrentVisualization, amount);
			if (!rotated)
			{
				return this._variants[key];
			}
			return this._rotatedVariants[key];
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000285C File Offset: 0x00000A5C
		public void LoadVisualizerVariants(StockpileGoodPileVisualizerSpec visualizer)
		{
			TemplateSpec spec = visualizer.GetSpec<TemplateSpec>();
			foreach (string visualizationId in visualizer.GoodPileVisualizations)
			{
				GoodVisualizationSpec visualization = this._goodVisualizationSpecService.GetVisualization(visualizationId, "");
				this.BuildIntermediateMeshes(visualization);
				List<GoodPileVariantsService.Pile> list = new List<GoodPileVariantsService.Pile>(this.GetPiles(visualizer, visualization));
				float num = (float)list.Count * visualization.LimitingAmount;
				int num2 = 0;
				while ((float)num2 <= num)
				{
					if (num2 != 0)
					{
						this.GetRandomPile(list).AddItem();
					}
					string key = GoodPileVariantsService.GetKey(spec, visualization, num2);
					this._variants.Add(key, this.GetMesh(list, visualization, false));
					this._rotatedVariants.Add(key, this.GetMesh(list, visualization, true));
					num2++;
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002934 File Offset: 0x00000B34
		public void BuildIntermediateMeshes(GoodVisualizationSpec visualization)
		{
			Material asset = visualization.Material.Asset;
			this._primaryIntermediateMesh = this.BuildIntermediateMesh(visualization.PrimaryMesh.Asset, asset);
			this._secondaryIntermediateMesh = this.BuildIntermediateMesh(visualization.SecondaryMesh.Asset, asset);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000297D File Offset: 0x00000B7D
		public IntermediateMesh BuildIntermediateMesh(Mesh mesh, Material material)
		{
			this._meshBuilder.Reset("");
			this._meshBuilder.AppendMesh<TranslationTransform>(mesh, new Material[]
			{
				material
			}, new TranslationTransform(Vector3.zero));
			return this._meshBuilder.BuildIntermediateMesh();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000029BA File Offset: 0x00000BBA
		public IEnumerable<GoodPileVariantsService.Pile> GetPiles(StockpileGoodPileVisualizerSpec visualizer, GoodVisualizationSpec visualization)
		{
			BlockObjectSpec blockObjectSpec = visualizer.GetSpec<BlockObjectSpec>();
			List<Vector3Int> list = (from coords in blockObjectSpec.GetBlocks().GetOccupiedCoordinates()
			where coords.z == blockObjectSpec.BaseZ
			select coords).ToList<Vector3Int>();
			int num = Mathf.CeilToInt((float)visualizer.GetSpec<StockpileSpec>().MaxCapacity / (float)list.Count);
			float limitingAmount = visualization.LimitingAmount;
			int numberOfLevels = Mathf.CeilToInt((float)num / limitingAmount);
			foreach (Vector3Int vector3Int in list)
			{
				bool rotated = this._randomNumberGenerator.CheckProbability(0.5f);
				yield return new GoodPileVariantsService.Pile(numberOfLevels, rotated, vector3Int, visualization.LimitingAmountFlooredToInt);
			}
			List<Vector3Int>.Enumerator enumerator = default(List<Vector3Int>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029D8 File Offset: 0x00000BD8
		public GoodPileVariantsService.Pile GetRandomPile(IEnumerable<GoodPileVariantsService.Pile> piles)
		{
			IEnumerable<GoodPileVariantsService.Pile> source = from pile in piles
			where pile.IsNotFull
			select pile;
			return this._randomNumberGenerator.GetEnumerableElement<GoodPileVariantsService.Pile>(source);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A18 File Offset: 0x00000C18
		public Mesh GetMesh(List<GoodPileVariantsService.Pile> piles, GoodVisualizationSpec visualization, bool rotated)
		{
			this._meshBuilder.Reset("");
			foreach (GoodPileVariantsService.Pile pile in piles)
			{
				this.AddFullLevels(visualization, pile, rotated);
				if (pile.IsNotFull)
				{
					this.AddIndividualItems(visualization, pile, rotated);
				}
				else
				{
					this.AddFullLevel(visualization, pile, rotated, GoodPileVariantsService.IndividualItemsLevel);
				}
			}
			return this._meshBuilder.Build(1).Mesh;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public void AddFullLevels(GoodVisualizationSpec visualization, GoodPileVariantsService.Pile pile, bool rotatedVariant)
		{
			for (int i = 0; i < pile.MaxLevels; i++)
			{
				this.AddFullLevel(visualization, pile, rotatedVariant, i);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public void AddFullLevel(GoodVisualizationSpec visualization, GoodPileVariantsService.Pile pile, bool rotatedVariant, int index)
		{
			Vector3 position = GoodPileVariantsService.CalculatePosition(pile.Position, visualization.Offset, index);
			bool rotated = rotatedVariant ? (!pile.Rotated) : pile.Rotated;
			bool rotate = GoodPileVariantsService.ShouldRotate(index, rotated);
			this._meshBuilder.AppendIntermediateMesh<ITransform>(this._secondaryIntermediateMesh, GoodPileVariantsService.GetTransform(position, rotate));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B30 File Offset: 0x00000D30
		public void AddIndividualItems(GoodVisualizationSpec visualization, GoodPileVariantsService.Pile pile, bool rotatedVariant)
		{
			Vector3 offset = visualization.Offset;
			Vector3 vector = GoodPileVariantsService.CalculatePosition(pile.Position, offset, GoodPileVariantsService.IndividualItemsLevel);
			float num = (float)pile.MaxItemsPerLevel * offset.x / 2f - offset.x / 2f;
			bool rotated = rotatedVariant ? (!pile.Rotated) : pile.Rotated;
			bool flag = GoodPileVariantsService.ShouldRotate(pile.MaxLevels, rotated);
			Vector3 vector2 = flag ? Vector3.right : Vector3.forward;
			for (int i = 0; i < pile.Items; i++)
			{
				ITransform transform = GoodPileVariantsService.GetTransform(vector - vector2 * (num - offset.x * (float)i), flag);
				this._meshBuilder.AppendIntermediateMesh<ITransform>(this._primaryIntermediateMesh, transform);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public static ITransform GetTransform(Vector3 position, bool rotate)
		{
			if (rotate)
			{
				Quaternion quaternion = Quaternion.AngleAxis(90f, Vector3.up);
				return new Matrix4x4Transform(Matrix4x4.TRS(position, quaternion, Vector3.one));
			}
			return new TranslationTransform(position);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C3A File Offset: 0x00000E3A
		public static Vector3 CalculatePosition(Vector3 position, Vector3 offset, int level)
		{
			return CoordinateSystem.GridToWorld(position) - Vector3.up * (offset.z * (float)level);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C5C File Offset: 0x00000E5C
		public static bool ShouldRotate(int visibleLevels, bool rotated)
		{
			int num = (visibleLevels % 2 == 0) ? 0 : 90;
			return (rotated ? 90 : 0) + num == 90;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002404 File Offset: 0x00000604
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

		// Token: 0x0400001A RID: 26
		public static readonly int IndividualItemsLevel = -1;

		// Token: 0x0400001B RID: 27
		public readonly TemplateService _templateService;

		// Token: 0x0400001C RID: 28
		public readonly GoodVisualizationSpecService _goodVisualizationSpecService;

		// Token: 0x0400001D RID: 29
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400001E RID: 30
		public readonly Dictionary<string, Mesh> _variants = new Dictionary<string, Mesh>();

		// Token: 0x0400001F RID: 31
		public readonly Dictionary<string, Mesh> _rotatedVariants = new Dictionary<string, Mesh>();

		// Token: 0x04000020 RID: 32
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();

		// Token: 0x04000021 RID: 33
		public IntermediateMesh _primaryIntermediateMesh;

		// Token: 0x04000022 RID: 34
		public IntermediateMesh _secondaryIntermediateMesh;

		// Token: 0x0200000C RID: 12
		public class Pile
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000030 RID: 48 RVA: 0x00002C8A File Offset: 0x00000E8A
			public int MaxLevels { get; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000031 RID: 49 RVA: 0x00002C92 File Offset: 0x00000E92
			public bool Rotated { get; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000032 RID: 50 RVA: 0x00002C9A File Offset: 0x00000E9A
			public Vector3 Position { get; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000033 RID: 51 RVA: 0x00002CA2 File Offset: 0x00000EA2
			public int MaxItemsPerLevel { get; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000034 RID: 52 RVA: 0x00002CAA File Offset: 0x00000EAA
			// (set) Token: 0x06000035 RID: 53 RVA: 0x00002CB2 File Offset: 0x00000EB2
			public int Items { get; private set; }

			// Token: 0x06000036 RID: 54 RVA: 0x00002CBB File Offset: 0x00000EBB
			public Pile(int maxLevels, bool rotated, Vector3 position, int maxItemsPerLevel)
			{
				this.MaxLevels = maxLevels;
				this.Rotated = rotated;
				this.Position = position;
				this.MaxItemsPerLevel = maxItemsPerLevel;
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000037 RID: 55 RVA: 0x00002CE0 File Offset: 0x00000EE0
			public bool IsNotFull
			{
				get
				{
					return this.Items < this.MaxItemsPerLevel;
				}
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002CF0 File Offset: 0x00000EF0
			public void AddItem()
			{
				int items = this.Items;
				this.Items = items + 1;
			}
		}
	}
}
