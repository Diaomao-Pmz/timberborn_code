using System;
using System.Collections.Generic;
using Timberborn.BlockObjectPickingSystem;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Brushes;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.GridTraversing;
using Timberborn.InputSystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Localization;
using Timberborn.MapEditorConstructionGuidelinesUI;
using Timberborn.MapStateSystem;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using Timberborn.TerrainQueryingSystem;
using Timberborn.TerrainSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UndoSystem;
using UnityEngine;

namespace Timberborn.MapEditorBrushesUI
{
	// Token: 0x0200000D RID: 13
	public class RelativeTerrainHeightBrushTool : ITool, IToolDescriptor, IInputProcessor, ILoadableSingleton, IBrushWithSize, IBrushWithShape, IBrushWithHeight, IBrushWithDirection, IBrushWithGuidelines
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002DFF File Offset: 0x00000FFF
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002E07 File Offset: 0x00001007
		public int BrushSize { get; set; } = 5;

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002E10 File Offset: 0x00001010
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002E18 File Offset: 0x00001018
		public int BrushHeight { get; set; } = 1;

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002E21 File Offset: 0x00001021
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002E29 File Offset: 0x00001029
		public BrushShape BrushShape { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002E32 File Offset: 0x00001032
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002E3A File Offset: 0x0000103A
		public bool Increase { get; set; } = true;

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002E43 File Offset: 0x00001043
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002E4B File Offset: 0x0000104B
		public bool Inverse { get; set; }

		// Token: 0x06000052 RID: 82 RVA: 0x00002E54 File Offset: 0x00001054
		public RelativeTerrainHeightBrushTool(InputService inputService, ITerrainService terrainService, BrushShapeIterator brushShapeIterator, TerrainPicker terrainPicker, CameraService cameraService, MarkerDrawerFactory markerDrawerFactory, ILevelVisibilityService levelVisibilityService, TerrainIntegrityService terrainIntegrityService, ILoc loc, BlockObjectRaycaster blockObjectRaycaster, StackableBlockService stackableBlockService, IUndoRegistry undoRegistry, MapSize mapSize, ISpecService specService)
		{
			this._inputService = inputService;
			this._terrainService = terrainService;
			this._brushShapeIterator = brushShapeIterator;
			this._terrainPicker = terrainPicker;
			this._cameraService = cameraService;
			this._markerDrawerFactory = markerDrawerFactory;
			this._levelVisibilityService = levelVisibilityService;
			this._terrainIntegrityService = terrainIntegrityService;
			this._loc = loc;
			this._blockObjectRaycaster = blockObjectRaycaster;
			this._stackableBlockService = stackableBlockService;
			this._undoRegistry = undoRegistry;
			this._mapSize = mapSize;
			this._specService = specService;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002EF4 File Offset: 0x000010F4
		public int MinimumBrushHeight
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002EF7 File Offset: 0x000010F7
		public bool IsIncreasing
		{
			get
			{
				return (this.Increase && !this.Inverse) || (!this.Increase && this.Inverse);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F1C File Offset: 0x0000111C
		public void Load()
		{
			this._brushColorSpec = this._specService.GetSingleSpec<BrushColorSpec>();
			this._toolDescription = new ToolDescription.Builder(this._loc.T(RelativeTerrainHeightBrushTool.TitleLocKey)).Build();
			this._markerDrawer = this._markerDrawerFactory.CreateTileDrawer();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F6B File Offset: 0x0000116B
		public bool ProcessInput()
		{
			this.ProcessBrush();
			return false;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F74 File Offset: 0x00001174
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F82 File Offset: 0x00001182
		public void Exit()
		{
			this._terrainIntegrityService.ClearHighlight();
			this._inputService.RemoveInputProcessor(this);
			this._undoRegistry.CommitStack();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FA6 File Offset: 0x000011A6
		public ToolDescription DescribeTool()
		{
			return this._toolDescription;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002FB0 File Offset: 0x000011B0
		public void ProcessBrush()
		{
			Ray ray = this._cameraService.ScreenPointToRayInGridSpace(this._inputService.MousePosition);
			bool justStartedDrawing = this.UpdateDrawingState(ray);
			if (this._isDrawing)
			{
				this.ApplyBrush(ray, justStartedDrawing);
				return;
			}
			this.PreviewBrush(ray);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FFC File Offset: 0x000011FC
		public bool UpdateDrawingState(Ray ray)
		{
			if (!this._isDrawing)
			{
				Vector3Int drawingOrigin;
				if (this.TryGetCursorCoordinates(ray, out drawingOrigin))
				{
					this._drawingOrigin = drawingOrigin;
					if (this._inputService.MainMouseButtonHeld && !this._inputService.MouseOverUI)
					{
						this._isDrawing = true;
						return true;
					}
				}
			}
			else if (!this._inputService.MainMouseButtonHeld)
			{
				this._isDrawing = false;
				this._undoRegistry.CommitStack();
			}
			return false;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003068 File Offset: 0x00001268
		public void ApplyBrush(Ray ray, bool justStartedDrawing)
		{
			Vector3Int center;
			if (this.TryPickCoordinates(ray, justStartedDrawing, out center))
			{
				this.UpdateBrushCoordinates(center);
				this._terrainIntegrityService.RemoveViolatingElements(this.GetCoordinatesToCleanupBlockObjects(), this.GetCoordinatesToValidateIntegrity());
				int num = this.IsIncreasing ? this.BrushHeight : (-this.BrushHeight);
				foreach (Vector3Int vector3Int in this._brushCoordinates)
				{
					if (vector3Int.z == this._drawingOrigin.z && this.InMapRange(vector3Int.z))
					{
						Vector3Int vector3Int2;
						vector3Int2..ctor(vector3Int.x, vector3Int.y, vector3Int.z);
						if (num > 0)
						{
							int heightChange = Math.Min(vector3Int.z + num, this._mapSize.MaxMapEditorTerrainHeight) - vector3Int.z;
							this._terrainService.SetTerrain(vector3Int2, heightChange);
						}
						else
						{
							this._terrainService.UnsetTerrain(vector3Int2.Below(), Math.Abs(num));
						}
					}
				}
				this._brushCoordinates.Clear();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000319C File Offset: 0x0000139C
		public void PreviewBrush(Ray ray)
		{
			this._terrainIntegrityService.ClearHighlight();
			Vector3Int center;
			if (this.TryGetCursorCoordinates(ray, out center))
			{
				this.UpdateBrushCoordinates(center);
				this.DrawTileMarkers();
				this._terrainIntegrityService.HighlightViolatingElements(this.GetCoordinatesToCleanupBlockObjects(), this.GetCoordinatesToValidateIntegrity());
				this._brushCoordinates.Clear();
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000031F0 File Offset: 0x000013F0
		public bool TryPickCoordinates(Ray ray, bool justStartedDrawing, out Vector3Int center)
		{
			if (justStartedDrawing && this.TryGetCursorCoordinates(ray, out center))
			{
				return true;
			}
			if (this.TryGetCursorForStackableBlockObject(ray, out center) && center.z == this._drawingOrigin.z)
			{
				return true;
			}
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.FindCoordinatesOnLevelInMap(ray, (float)this._drawingOrigin.z);
			if (traversedCoordinates != null)
			{
				center = traversedCoordinates.GetValueOrDefault().Coordinates;
				return true;
			}
			center = default(Vector3Int);
			return false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000326C File Offset: 0x0000146C
		public void DrawTileMarkers()
		{
			foreach (Vector3Int vector3Int in this._brushCoordinates)
			{
				Vector3Int coordinates;
				coordinates..ctor(vector3Int.x, vector3Int.y, this._drawingOrigin.z);
				int num;
				if (this.TryGetRelativeHeight(coordinates, out num))
				{
					int num2 = coordinates.z + num;
					Vector3Int coordinates2;
					coordinates2..ctor(vector3Int.x, vector3Int.y, num2);
					Color color = (num == 0 && this.InMapRange(coordinates2.z)) ? (this.IsIncreasing ? this._brushColorSpec.Positive : this._brushColorSpec.Negative) : this._brushColorSpec.Neutral;
					this._markerDrawer.DrawAtCoordinates(coordinates2, RelativeTerrainHeightBrushTool.MarkerYOffset, color);
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003360 File Offset: 0x00001560
		public bool TryGetCursorCoordinates(Ray ray, out Vector3Int coordinates)
		{
			bool flag = this.TryGetCursorForStackableBlockObject(ray, out coordinates);
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(ray);
			if (traversedCoordinates != null)
			{
				TraversedCoordinates valueOrDefault = traversedCoordinates.GetValueOrDefault();
				if (!flag || valueOrDefault.CoordinatesWithFaceOffset.z > coordinates.z)
				{
					coordinates = valueOrDefault.CoordinatesWithFaceOffset;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000033BE File Offset: 0x000015BE
		public bool TryGetRelativeHeight(Vector3Int coordinates, out int relativeHeight)
		{
			if (!this._terrainService.Underground(coordinates) && this._stackableBlockService.IsFinishedStackableBlockAt(coordinates.Below()))
			{
				relativeHeight = 0;
				return true;
			}
			return this._terrainService.TryGetRelativeHeight(coordinates, out relativeHeight);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000033F4 File Offset: 0x000015F4
		public void UpdateBrushCoordinates(Vector3Int center)
		{
			foreach (Vector3Int coordinates in this._brushShapeIterator.IterateShape(center, this.BrushSize, this.BrushShape))
			{
				int num;
				if (this.AreCoordinatesValid(coordinates, out num))
				{
					this._brushCoordinates.Add(new Vector3Int(coordinates.x, coordinates.y, num));
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003478 File Offset: 0x00001678
		public bool AreCoordinatesValid(Vector3Int coordinates, out int height)
		{
			height = this.GetHeight(coordinates);
			return height < this._levelVisibilityService.MaxVisibleLevel + 1;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003494 File Offset: 0x00001694
		public int GetHeight(Vector3Int coordinates)
		{
			if (!this._terrainService.Underground(coordinates) && this._stackableBlockService.IsFinishedStackableBlockAt(coordinates.Below()))
			{
				return coordinates.z;
			}
			int num;
			if (this._terrainService.TryGetRelativeHeight(coordinates, out num))
			{
				return coordinates.z + num;
			}
			return int.MaxValue;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000034E9 File Offset: 0x000016E9
		public bool InMapRange(int height)
		{
			if (!this.IsIncreasing)
			{
				return height > 0;
			}
			return height < this._mapSize.MaxMapEditorTerrainHeight;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003506 File Offset: 0x00001706
		public IEnumerable<Vector3Int> GetCoordinatesToCleanupBlockObjects()
		{
			foreach (Vector3Int coordinates in this._brushCoordinates)
			{
				int z2 = coordinates.z;
				if (z2 == this._drawingOrigin.z)
				{
					int num = this.IsIncreasing ? z2 : (z2 - this.BrushHeight);
					int endHeight = this.IsIncreasing ? (z2 + this.BrushHeight) : z2;
					int num2;
					for (int z = num; z < endHeight; z = num2 + 1)
					{
						if (z >= 0 && z < this._mapSize.MaxMapEditorTerrainHeight)
						{
							yield return new Vector3Int(coordinates.x, coordinates.y, z);
						}
						num2 = z;
					}
				}
				coordinates = default(Vector3Int);
			}
			List<Vector3Int>.Enumerator enumerator = default(List<Vector3Int>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003516 File Offset: 0x00001716
		public IEnumerable<Vector3Int> GetCoordinatesToValidateIntegrity()
		{
			if (!this.IsIncreasing)
			{
				foreach (Vector3Int coordinates in this._brushCoordinates)
				{
					if (coordinates.z == this._drawingOrigin.z)
					{
						int num;
						for (int z = 0; z <= this.BrushHeight; z = num + 1)
						{
							yield return new Vector3Int(coordinates.x, coordinates.y, coordinates.z - z);
							num = z;
						}
					}
					coordinates = default(Vector3Int);
				}
				List<Vector3Int>.Enumerator enumerator = default(List<Vector3Int>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003528 File Offset: 0x00001728
		public bool TryGetCursorForStackableBlockObject(Ray ray, out Vector3Int coordinates)
		{
			BlockObjectHit blockObjectHit;
			if (this._blockObjectRaycaster.TryHitBlockObject<BlockObject>(ray, out blockObjectHit) && blockObjectHit.HitBlock.Stackable == BlockStackable.BlockObject)
			{
				coordinates = blockObjectHit.HitBlock.Coordinates.Above();
				return true;
			}
			coordinates = default(Vector3Int);
			return false;
		}

		// Token: 0x04000035 RID: 53
		public static readonly string TitleLocKey = "MapEditor.Brush.RelativeTerrainHeight";

		// Token: 0x04000036 RID: 54
		public static readonly float MarkerYOffset = 0.02f;

		// Token: 0x0400003C RID: 60
		public readonly InputService _inputService;

		// Token: 0x0400003D RID: 61
		public readonly ITerrainService _terrainService;

		// Token: 0x0400003E RID: 62
		public readonly BrushShapeIterator _brushShapeIterator;

		// Token: 0x0400003F RID: 63
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x04000040 RID: 64
		public readonly CameraService _cameraService;

		// Token: 0x04000041 RID: 65
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x04000042 RID: 66
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000043 RID: 67
		public readonly TerrainIntegrityService _terrainIntegrityService;

		// Token: 0x04000044 RID: 68
		public readonly ILoc _loc;

		// Token: 0x04000045 RID: 69
		public readonly BlockObjectRaycaster _blockObjectRaycaster;

		// Token: 0x04000046 RID: 70
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x04000047 RID: 71
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000048 RID: 72
		public readonly MapSize _mapSize;

		// Token: 0x04000049 RID: 73
		public readonly ISpecService _specService;

		// Token: 0x0400004A RID: 74
		public BrushColorSpec _brushColorSpec;

		// Token: 0x0400004B RID: 75
		public readonly List<Vector3Int> _brushCoordinates = new List<Vector3Int>();

		// Token: 0x0400004C RID: 76
		public MeshDrawer _markerDrawer;

		// Token: 0x0400004D RID: 77
		public ToolDescription _toolDescription;

		// Token: 0x0400004E RID: 78
		public bool _isDrawing;

		// Token: 0x0400004F RID: 79
		public Vector3Int _drawingOrigin;
	}
}
