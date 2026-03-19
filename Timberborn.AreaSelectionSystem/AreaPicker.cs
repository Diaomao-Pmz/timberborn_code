using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockObjectPickingSystem;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.GridTraversing;
using Timberborn.SingletonSystem;
using Timberborn.TerrainQueryingSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x02000012 RID: 18
	public class AreaPicker : ILoadableSingleton
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000026D0 File Offset: 0x000008D0
		public AreaPicker(TerrainPicker terrainPicker, BlockObjectPreviewPicker blockObjectPreviewPicker, AreaSelectionController areaSelectionController, StackableBlockService stackableBlockService, ITerrainService terrainService, AreaIterator areaIterator, ISpecService specService)
		{
			this._terrainPicker = terrainPicker;
			this._blockObjectPreviewPicker = blockObjectPreviewPicker;
			this._areaSelectionController = areaSelectionController;
			this._stackableBlockService = stackableBlockService;
			this._terrainService = terrainService;
			this._areaIterator = areaIterator;
			this._specService = specService;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000270D File Offset: 0x0000090D
		public void Load()
		{
			this._maxBlocks = this._specService.GetSingleSpec<AreaPickersSpec>().AreaMaxBlocks;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002728 File Offset: 0x00000928
		public bool PickTerrainIntArea(AreaPicker.IntAreaCallback previewCallback, AreaPicker.IntAreaCallback actionCallback, Action showNoneCallback)
		{
			return this._areaSelectionController.ProcessInput(delegate(Ray start, Ray end, bool _)
			{
				previewCallback(this.GetTerrainBlocks(start, end), start);
			}, delegate(Ray start, Ray end, bool _)
			{
				actionCallback(this.GetTerrainBlocks(start, end), start);
			}, showNoneCallback);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002774 File Offset: 0x00000974
		public bool PickBlockObjectArea(PlaceableBlockObjectSpec blockObjectSpec, Orientation orientation, FlipMode flipMode, AreaPicker.BlockObjectAreaCallback previewCallback, AreaPicker.BlockObjectAreaCallback actionCallback)
		{
			return this._areaSelectionController.ProcessInput(delegate(Ray start, Ray end, bool _)
			{
				previewCallback(this.GetBlocks(start, end, blockObjectSpec, orientation, flipMode));
			}, delegate(Ray start, Ray end, bool _)
			{
				actionCallback(this.GetBlocks(start, end, blockObjectSpec, orientation, flipMode));
			}, delegate
			{
				previewCallback(Enumerable.Empty<Placement>());
			});
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027E2 File Offset: 0x000009E2
		public void Reset()
		{
			this._areaSelectionController.Reset();
			this._segmentedLineDirection = LineDirection.SinglePoint;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027F8 File Offset: 0x000009F8
		public IEnumerable<Vector3Int> GetTerrainBlocks(Ray startRay, Ray endRay)
		{
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(startRay);
			if (traversedCoordinates == null)
			{
				return Enumerable.Empty<Vector3Int>();
			}
			Vector3Int coordinates = traversedCoordinates.Value.Coordinates;
			TraversedCoordinates? traversedCoordinates2;
			Vector3Int end = (this._terrainPicker.FindCoordinatesOnLevelInMap(endRay, (float)(coordinates.z + 1)) != null) ? traversedCoordinates2.GetValueOrDefault().Coordinates : coordinates;
			return this._areaIterator.GetRectangle(coordinates, end, this._maxBlocks);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002878 File Offset: 0x00000A78
		public IEnumerable<Placement> GetBlocks(Ray startRay, Ray endRay, PlaceableBlockObjectSpec blockObjectSpec, Orientation orientation, FlipMode flipMode)
		{
			PickedCoordinates? pickedCoordinates = this._blockObjectPreviewPicker.CenteredPreviewCoordinates(blockObjectSpec, orientation, startRay);
			if (pickedCoordinates != null)
			{
				Vector3Int endCoords = this.GetEndCoords(startRay, endRay, pickedCoordinates.Value);
				return this.GetBlocksForLayout(blockObjectSpec, orientation, flipMode, pickedCoordinates.Value, endCoords);
			}
			return Enumerable.Empty<Placement>();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028C8 File Offset: 0x00000AC8
		public Vector3Int GetEndCoords(Ray startRay, Ray endRay, PickedCoordinates startPlacement)
		{
			if (!startRay.Equals(endRay))
			{
				TraversedCoordinates? traversedCoordinates = this._terrainPicker.FindCoordinatesOnLevelInMap(endRay, (float)startPlacement.ReferenceTerrainLevel);
				if (traversedCoordinates != null)
				{
					return traversedCoordinates.GetValueOrDefault().Coordinates + new Vector3Int(0, 0, startPlacement.VerticalOffset);
				}
			}
			return startPlacement.Coordinates;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002934 File Offset: 0x00000B34
		public IEnumerable<Placement> GetBlocksForLayout(PlaceableBlockObjectSpec blockObjectSpec, Orientation orientation, FlipMode flipMode, PickedCoordinates pickedStartCoordinates, Vector3Int endCoords)
		{
			Vector3Int coordinates = pickedStartCoordinates.Coordinates;
			IEnumerable<Placement> placements = this.GetPlacements(blockObjectSpec, orientation, flipMode, coordinates, endCoords);
			return this.FilterPlacements(placements, pickedStartCoordinates.FilterOverhangingCoordinates);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002964 File Offset: 0x00000B64
		public IEnumerable<Placement> GetPlacements(PlaceableBlockObjectSpec blockObjectSpec, Orientation orientation, FlipMode flipMode, Vector3Int startCoords, Vector3Int endCoords)
		{
			BlockObjectLayout layout = blockObjectSpec.Layout;
			int previewCount = layout.GetPreviewCount();
			switch (layout)
			{
			case BlockObjectLayout.Single:
				return Enumerables.One<Placement>(new Placement(startCoords, orientation, flipMode));
			case BlockObjectLayout.Rectangle:
				return this.RectangleCoordinates(previewCount, orientation, flipMode, startCoords, endCoords);
			case BlockObjectLayout.Line:
				return this.LineCoordinates(previewCount, orientation, flipMode, startCoords, endCoords);
			case BlockObjectLayout.Half:
				return AreaPicker.HalvesCoordinates(blockObjectSpec, startCoords, orientation, flipMode);
			case BlockObjectLayout.SideLine:
				return this.SideLineCoordinates(previewCount, orientation, flipMode, startCoords, endCoords);
			case BlockObjectLayout.TwoSegmentLine:
				return this.TwoSegmentLineCoordinates(previewCount, orientation, flipMode, startCoords, endCoords);
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unknown {0}: {1}", "BlockObjectLayout", layout));
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A0B File Offset: 0x00000C0B
		public IEnumerable<Placement> FilterPlacements(IEnumerable<Placement> placements, bool filterOverhangingCoordinates)
		{
			if (!filterOverhangingCoordinates)
			{
				return placements;
			}
			return placements.Where(new Func<Placement, bool>(this.PlacementHasStackableBelow));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A24 File Offset: 0x00000C24
		public bool PlacementHasStackableBelow(Placement placement)
		{
			Vector3Int coords = placement.Coordinates.Below();
			return this._terrainService.Underground(coords) || this._stackableBlockService.IsStackableBlockAt(coords, false);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A5C File Offset: 0x00000C5C
		public IEnumerable<Placement> RectangleCoordinates(int maxPoints, Orientation orientation, FlipMode flipMode, Vector3Int startCoords, Vector3Int endCoords)
		{
			return from coordinates in this._areaIterator.GetRectangle(startCoords, endCoords, maxPoints)
			select new Placement(coordinates, orientation, flipMode);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public IEnumerable<Placement> LineCoordinates(int maxPoints, Orientation orientation, FlipMode flipMode, Vector3Int startCoords, Vector3Int endCoords)
		{
			LineDirection lineDirection;
			IEnumerable<Vector3Int> line = this._areaIterator.GetLine(startCoords, endCoords, maxPoints, out lineDirection);
			Orientation lineOrientation = AreaPicker.ConvertOrientation(orientation, lineDirection);
			return from coordinates in line
			select new Placement(coordinates, lineOrientation, flipMode);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AEA File Offset: 0x00000CEA
		public static IEnumerable<Placement> HalvesCoordinates(PlaceableBlockObjectSpec blockObjectSpec, Vector3Int startCoords, Orientation orientation, FlipMode flipMode)
		{
			yield return new Placement(startCoords, orientation, flipMode);
			Vector3Int size = blockObjectSpec.GetSpec<BlockObjectSpec>().Size;
			int num = size.x - 1;
			int num2 = size.y * 2 - 1;
			Vector3Int coordinates = startCoords + orientation.Transform(new Vector3Int(num, num2, 0));
			yield return new Placement(coordinates, orientation.Flip(), flipMode);
			yield break;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B10 File Offset: 0x00000D10
		public IEnumerable<Placement> SideLineCoordinates(int maxPoints, Orientation orientation, FlipMode flipMode, Vector3Int startCoords, Vector3Int endCoords)
		{
			LineDirection lineDirection;
			IEnumerable<Vector3Int> line = this._areaIterator.GetLine(startCoords, endCoords, maxPoints, out lineDirection);
			Orientation orientation2 = AreaPicker.ConvertOrientation(orientation, lineDirection);
			if (orientation2.RotateClockwise() == orientation || orientation2.RotateCounterclockwise() == orientation)
			{
				return from coordinates in line
				select new Placement(coordinates, orientation, flipMode);
			}
			return Enumerables.One<Placement>(new Placement(startCoords, orientation, flipMode));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B98 File Offset: 0x00000D98
		public IEnumerable<Placement> TwoSegmentLineCoordinates(int maxPoints, Orientation orientation, FlipMode flipMode, Vector3Int startCoords, Vector3Int endCoords)
		{
			LineDirection lineDirection;
			IEnumerable<Vector3Int> enumerable = this.FirstSegmentLineCoordinates(maxPoints, startCoords, endCoords, out lineDirection);
			this._segmentedLineDirection = lineDirection;
			Orientation lineOrientation = AreaPicker.ConvertOrientation(orientation, lineDirection);
			Vector3Int startCoords2 = Vector3Int.zero;
			foreach (Vector3Int coordinates in enumerable)
			{
				yield return new Placement(coordinates, lineOrientation, flipMode);
				startCoords2 = coordinates;
				int num = maxPoints;
				maxPoints = num - 1;
				coordinates = default(Vector3Int);
			}
			IEnumerator<Vector3Int> enumerator = null;
			IEnumerable<Placement> enumerable2 = this.SecondSegmentLineCoordinates(maxPoints, orientation, flipMode, startCoords2, endCoords);
			foreach (Placement placement in enumerable2)
			{
				yield return placement;
			}
			IEnumerator<Placement> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BCD File Offset: 0x00000DCD
		public IEnumerable<Vector3Int> FirstSegmentLineCoordinates(int maxPoints, Vector3Int startCoords, Vector3Int endCoords, out LineDirection lineDirection)
		{
			if (this._segmentedLineDirection == LineDirection.SinglePoint)
			{
				return this._areaIterator.GetLine(startCoords, endCoords, maxPoints, out lineDirection);
			}
			return this._areaIterator.GetLine(startCoords, endCoords, this._segmentedLineDirection, maxPoints, out lineDirection);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C00 File Offset: 0x00000E00
		public IEnumerable<Placement> SecondSegmentLineCoordinates(int pointsLeft, Orientation orientation, FlipMode flipMode, Vector3Int startCoords, Vector3Int endCoords)
		{
			if (pointsLeft > 0)
			{
				LineDirection lineDirection;
				IEnumerable<Vector3Int> source = this._areaIterator.GetLine(startCoords, endCoords, pointsLeft + 1, out lineDirection).Skip(1);
				Orientation lineOrientation = AreaPicker.ConvertOrientation(orientation, lineDirection);
				return from coordinates in source
				select new Placement(coordinates, lineOrientation, flipMode);
			}
			return Enumerable.Empty<Placement>();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C5C File Offset: 0x00000E5C
		public static Orientation ConvertOrientation(Orientation blockObjectOrientation, LineDirection lineDirection)
		{
			switch (lineDirection)
			{
			case LineDirection.SinglePoint:
				return blockObjectOrientation;
			case LineDirection.Down:
				return Orientation.Cw0;
			case LineDirection.Left:
				return Orientation.Cw90;
			case LineDirection.Up:
				return Orientation.Cw180;
			case LineDirection.Right:
				return Orientation.Cw270;
			default:
				throw new ArgumentOutOfRangeException("lineDirection", lineDirection, null);
			}
		}

		// Token: 0x04000028 RID: 40
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x04000029 RID: 41
		public readonly BlockObjectPreviewPicker _blockObjectPreviewPicker;

		// Token: 0x0400002A RID: 42
		public readonly AreaSelectionController _areaSelectionController;

		// Token: 0x0400002B RID: 43
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x0400002C RID: 44
		public readonly ITerrainService _terrainService;

		// Token: 0x0400002D RID: 45
		public readonly AreaIterator _areaIterator;

		// Token: 0x0400002E RID: 46
		public readonly ISpecService _specService;

		// Token: 0x0400002F RID: 47
		public int _maxBlocks;

		// Token: 0x04000030 RID: 48
		public LineDirection _segmentedLineDirection;

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x06000047 RID: 71
		public delegate void IntAreaCallback(IEnumerable<Vector3Int> blocks, Ray ray);

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x0600004B RID: 75
		public delegate void BlockObjectAreaCallback(IEnumerable<Placement> placements);
	}
}
