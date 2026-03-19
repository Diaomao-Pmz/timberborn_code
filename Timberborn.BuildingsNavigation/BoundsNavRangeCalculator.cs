using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.LevelVisibilitySystem;
using Timberborn.PrefabOptimization;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000009 RID: 9
	public class BoundsNavRangeCalculator
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000233D File Offset: 0x0000053D
		public BoundsNavRangeCalculator(IBlockService blockService, PreviewBlockService previewBlockService, ILevelVisibilityService levelVisibilityService)
		{
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
			this._levelVisibilityService = levelVisibilityService;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002368 File Offset: 0x00000568
		public void Recalculate(IReadOnlyCollection<Vector3Int> area, NeighboredValues8<IntermediateMesh> meshes, BoundsMesh boundsMesh)
		{
			this._area.AddRange(area);
			foreach (Vector3Int coordinates in area)
			{
				if (!this.AnyBlockingObjectAt(coordinates) && this._levelVisibilityService.BlockIsVisible(coordinates))
				{
					this.AddToBounds(coordinates, meshes, boundsMesh);
				}
			}
			this._area.Clear();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023E0 File Offset: 0x000005E0
		public bool AnyBlockingObjectAt(Vector3Int coordinates)
		{
			return BoundsNavRangeCalculator.IsBlockingObject(this._blockService.GetBottomObjectAt(coordinates)) || BoundsNavRangeCalculator.IsBlockingObject(this._previewBlockService.GetBottomPreviewAt(coordinates));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002408 File Offset: 0x00000608
		public static bool IsBlockingObject(BlockObject blockObject)
		{
			return blockObject && blockObject.GetEnabledComponent<AreaBoundsDrawingBlocker>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002420 File Offset: 0x00000620
		public void AddToBounds(Vector3Int coordinates, NeighboredValues8<IntermediateMesh> meshes, BoundsMesh boundsMesh)
		{
			bool flag = this.IsVisibleSide(coordinates, new Vector3Int(0, -1, 0));
			bool flag2 = this.IsVisibleSide(coordinates, new Vector3Int(-1, -1, 0));
			bool flag3 = this.IsVisibleSide(coordinates, new Vector3Int(-1, 0, 0));
			bool flag4 = this.IsVisibleSide(coordinates, new Vector3Int(-1, 1, 0));
			bool flag5 = this.IsVisibleSide(coordinates, new Vector3Int(0, 1, 0));
			bool flag6 = this.IsVisibleSide(coordinates, new Vector3Int(1, 1, 0));
			bool flag7 = this.IsVisibleSide(coordinates, new Vector3Int(1, 0, 0));
			bool flag8 = this.IsVisibleSide(coordinates, new Vector3Int(1, -1, 0));
			if (flag || flag2 || flag3 || flag4 || flag5 || flag6 || flag7 || flag8)
			{
				IntermediateMesh value = meshes.GetMatch(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8).Value;
				Vector3 translation = CoordinateSystem.GridToWorldCentered(coordinates);
				boundsMesh.Append(coordinates.z, value, new TranslationTransform(translation));
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002501 File Offset: 0x00000701
		public bool IsVisibleSide(Vector3Int coordinates, Vector3Int neighborDelta)
		{
			return !this._area.Contains(coordinates + neighborDelta);
		}

		// Token: 0x0400000D RID: 13
		public readonly IBlockService _blockService;

		// Token: 0x0400000E RID: 14
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x0400000F RID: 15
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000010 RID: 16
		public readonly HashSet<Vector3Int> _area = new HashSet<Vector3Int>();
	}
}
