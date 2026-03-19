using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.MapStateSystem;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200000A RID: 10
	public class BoundsNavRangeDrawer : ILoadableSingleton
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002518 File Offset: 0x00000718
		public BoundsNavRangeDrawer(BoundsNavRangeCalculator boundsNavRangeCalculator, MapSize mapSize, ISpecService specService)
		{
			this._boundsNavRangeCalculator = boundsNavRangeCalculator;
			this._specService = specService;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002550 File Offset: 0x00000750
		public void Load()
		{
			BoundsNavRangeDrawerSpec singleSpec = this._specService.GetSingleSpec<BoundsNavRangeDrawerSpec>();
			this._materials = new Material[]
			{
				singleSpec.Material.Asset
			};
			foreach (AssetRef<Mesh> assetRef in singleSpec.TileMeshes)
			{
				string name = assetRef.Asset.name.Replace("NavRangeTile", "");
				bool down = BoundsNavRangeDrawer.NameToKey(name, 0);
				bool downLeft = BoundsNavRangeDrawer.NameToKey(name, 1);
				bool left = BoundsNavRangeDrawer.NameToKey(name, 2);
				bool upLeft = BoundsNavRangeDrawer.NameToKey(name, 3);
				bool up = BoundsNavRangeDrawer.NameToKey(name, 4);
				bool upRight = BoundsNavRangeDrawer.NameToKey(name, 5);
				bool right = BoundsNavRangeDrawer.NameToKey(name, 6);
				bool downRight = BoundsNavRangeDrawer.NameToKey(name, 7);
				this.AddVariants(assetRef.Asset, down, downLeft, left, upLeft, up, upRight, right, downRight);
			}
			this._boundsMesh.Initialize(singleSpec.Material.Asset);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000263E File Offset: 0x0000083E
		public void UpdateArea(IReadOnlyCollection<Vector3Int> area)
		{
			this._boundsMesh.Reset();
			this._boundsNavRangeCalculator.Recalculate(area, this._neighboredMeshes, this._boundsMesh);
			this._boundsMesh.Build();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000266E File Offset: 0x0000086E
		public void UpdateAreaPreview(IReadOnlyCollection<Vector3Int> area)
		{
			this.UpdateArea(area);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002677 File Offset: 0x00000877
		public void Draw()
		{
			this._boundsMesh.Draw();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002684 File Offset: 0x00000884
		public static bool NameToKey(string name, int index)
		{
			return int.Parse(name[index].ToString()) == 1;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026A8 File Offset: 0x000008A8
		public void AddVariants(Mesh mesh, bool down, bool downLeft, bool left, bool upLeft, bool up, bool upRight, bool right, bool downRight)
		{
			this.AddVariant(mesh, Orientation.Cw0, down, downLeft, left, upLeft, up, upRight, right, downRight);
			this.AddVariant(mesh, Orientation.Cw90, right, downRight, down, downLeft, left, upLeft, up, upRight);
			this.AddVariant(mesh, Orientation.Cw180, up, upRight, right, downRight, down, downLeft, left, upLeft);
			this.AddVariant(mesh, Orientation.Cw270, left, upLeft, up, upRight, right, downRight, down, downLeft);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002710 File Offset: 0x00000910
		public void AddVariant(Mesh mesh, Orientation orientation, bool down, bool downLeft, bool left, bool upLeft, bool up, bool upRight, bool right, bool downRight)
		{
			this._meshBuilder.Reset("");
			this._meshBuilder.AppendMesh<OrientationTransform>(mesh, this._materials, new OrientationTransform(orientation));
			this._neighboredMeshes.AddExact(this._meshBuilder.BuildIntermediateMesh(), down, downLeft, left, upLeft, up, upRight, right, downRight);
		}

		// Token: 0x04000011 RID: 17
		public readonly BoundsNavRangeCalculator _boundsNavRangeCalculator;

		// Token: 0x04000012 RID: 18
		public readonly ISpecService _specService;

		// Token: 0x04000013 RID: 19
		public readonly NeighboredValues8<IntermediateMesh> _neighboredMeshes = new NeighboredValues8<IntermediateMesh>();

		// Token: 0x04000014 RID: 20
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();

		// Token: 0x04000015 RID: 21
		public readonly BoundsMesh _boundsMesh = new BoundsMesh();

		// Token: 0x04000016 RID: 22
		public Material[] _materials;
	}
}
