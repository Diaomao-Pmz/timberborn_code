using System;
using System.Collections.Generic;
using System.Linq;
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
	// Token: 0x02000007 RID: 7
	public class AbsoluteTerrainHeightBrushTool : ITool, IToolDescriptor, IInputProcessor, ILoadableSingleton, IBrushWithSize, IBrushWithShape, IBrushWithHeight, IBrushWithGuidelines
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public int BrushSize { get; set; } = 3;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000210F File Offset: 0x0000030F
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002117 File Offset: 0x00000317
		public int BrushHeight { get; set; } = 1;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002120 File Offset: 0x00000320
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002128 File Offset: 0x00000328
		public BrushShape BrushShape { get; set; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		public AbsoluteTerrainHeightBrushTool(InputService inputService, ITerrainService terrainService, BrushShapeIterator brushShapeIterator, TerrainPicker terrainPicker, CameraService cameraService, MarkerDrawerFactory markerDrawerFactory, ILevelVisibilityService levelVisibilityService, TerrainIntegrityService terrainIntegrityService, ILoc loc, IUndoRegistry undoRegistry, MapSize mapSize, ISpecService specService)
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
			this._undoRegistry = undoRegistry;
			this._mapSize = mapSize;
			this._specService = specService;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021BD File Offset: 0x000003BD
		public int MinimumBrushHeight
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021C0 File Offset: 0x000003C0
		public void Load()
		{
			this._brushColorSpec = this._specService.GetSingleSpec<BrushColorSpec>();
			this._toolDescription = new ToolDescription.Builder(this._loc.T(AbsoluteTerrainHeightBrushTool.TitleLocKey)).Build();
			this._markerDrawer = this._markerDrawerFactory.CreateTileDrawer();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000220F File Offset: 0x0000040F
		public bool ProcessInput()
		{
			this.ProcessBrush();
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002218 File Offset: 0x00000418
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002226 File Offset: 0x00000426
		public void Exit()
		{
			this._terrainIntegrityService.ClearHighlight();
			this._inputService.RemoveInputProcessor(this);
			this._undoRegistry.CommitStack();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000224A File Offset: 0x0000044A
		public ToolDescription DescribeTool()
		{
			return this._toolDescription;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002254 File Offset: 0x00000454
		public void ProcessBrush()
		{
			Ray ray = this._cameraService.ScreenPointToRayInGridSpace(this._inputService.MousePosition);
			this.UpdateDrawingState(ray);
			if (this._isDrawing)
			{
				this.ApplyBrush(ray);
				return;
			}
			this.PreviewBrush(ray);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000229C File Offset: 0x0000049C
		public void UpdateDrawingState(Ray ray)
		{
			if (!this._isDrawing)
			{
				TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(ray);
				if (traversedCoordinates != null)
				{
					TraversedCoordinates valueOrDefault = traversedCoordinates.GetValueOrDefault();
					this._drawingOrigin = valueOrDefault.Coordinates + valueOrDefault.Face;
					if (this._inputService.MainMouseButtonDown && !this._inputService.MouseOverUI)
					{
						this._isDrawing = true;
						return;
					}
				}
			}
			else if (!this._inputService.MainMouseButtonHeld)
			{
				this._isDrawing = false;
				this._undoRegistry.CommitStack();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000232C File Offset: 0x0000052C
		public void ApplyBrush(Ray ray)
		{
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.FindCoordinatesOnLevelInMap(ray, (float)this._drawingOrigin.z);
			if (traversedCoordinates != null)
			{
				this.UpdateBrushCoordinates(traversedCoordinates.GetValueOrDefault().Coordinates);
				this._terrainIntegrityService.RemoveViolatingElements(this.GetCoordinatesToCleanupBlockObjects(), this.GetCoordinatesToValidateIntegrity());
				foreach (Vector3Int coordinates in this._brushCoordinates)
				{
					int terrainHeight = this._terrainService.GetTerrainHeight(coordinates);
					Vector3Int vector3Int;
					vector3Int..ctor(coordinates.x, coordinates.y, terrainHeight);
					if (terrainHeight > this.BrushHeight)
					{
						this._terrainService.UnsetTerrain(vector3Int.Below(), terrainHeight - this.BrushHeight);
					}
					else if (terrainHeight < this.BrushHeight)
					{
						this._terrainService.SetTerrain(vector3Int, this.BrushHeight - terrainHeight);
					}
				}
				this.DrawTileMarkers();
				this._brushCoordinates.Clear();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002448 File Offset: 0x00000648
		public void PreviewBrush(Ray ray)
		{
			this._terrainIntegrityService.ClearHighlight();
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(ray);
			if (traversedCoordinates != null)
			{
				TraversedCoordinates valueOrDefault = traversedCoordinates.GetValueOrDefault();
				Vector3Int center = valueOrDefault.Coordinates + valueOrDefault.Face;
				this.UpdateBrushCoordinates(center);
				this.DrawTileMarkers();
				this._terrainIntegrityService.HighlightViolatingElements(this.GetCoordinatesToCleanupBlockObjects(), this.GetCoordinatesToValidateIntegrity());
				this._brushCoordinates.Clear();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024C4 File Offset: 0x000006C4
		public void DrawTileMarkers()
		{
			foreach (Vector3Int coordinates in this._brushCoordinates)
			{
				int terrainHeight = this._terrainService.GetTerrainHeight(coordinates);
				Vector3Int coordinates2;
				coordinates2..ctor(coordinates.x, coordinates.y, terrainHeight);
				int num = this.BrushHeight - terrainHeight;
				Color color = (num == 0) ? this._brushColorSpec.Neutral : ((num > 0) ? this._brushColorSpec.Positive : this._brushColorSpec.Negative);
				this._markerDrawer.DrawAtCoordinates(coordinates2, AbsoluteTerrainHeightBrushTool.MarkerYOffset, color);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002584 File Offset: 0x00000784
		public void UpdateBrushCoordinates(Vector3Int center)
		{
			this._brushCoordinates.AddRange(this._brushShapeIterator.IterateShape(center, this.BrushSize, this.BrushShape).Where(new Func<Vector3Int, bool>(this.AreCoordinatesValid)));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025BC File Offset: 0x000007BC
		public bool AreCoordinatesValid(Vector3Int coordinates)
		{
			int num;
			return this._terrainService.TryGetRelativeHeight(coordinates, out num) && coordinates.z + num < this._levelVisibilityService.MaxVisibleLevel + 1;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025F3 File Offset: 0x000007F3
		public IEnumerable<Vector3Int> GetCoordinatesToCleanupBlockObjects()
		{
			foreach (Vector3Int coordinates in this._brushCoordinates)
			{
				int num;
				if (this._terrainService.TryGetRelativeHeight(coordinates, out num))
				{
					int num2 = coordinates.z + num;
					int num3 = (this.BrushHeight > num2) ? num2 : this.BrushHeight;
					int endHeight = (this.BrushHeight > num2) ? this.BrushHeight : num2;
					int num4;
					for (int z = num3; z < endHeight; z = num4 + 1)
					{
						if (z >= 0 && z < this._mapSize.MaxMapEditorTerrainHeight)
						{
							yield return new Vector3Int(coordinates.x, coordinates.y, z);
						}
						num4 = z;
					}
				}
				coordinates = default(Vector3Int);
			}
			List<Vector3Int>.Enumerator enumerator = default(List<Vector3Int>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002603 File Offset: 0x00000803
		public IEnumerable<Vector3Int> GetCoordinatesToValidateIntegrity()
		{
			foreach (Vector3Int coordinates in this._brushCoordinates)
			{
				int num;
				if (this._terrainService.TryGetRelativeHeight(coordinates, out num))
				{
					int terrainHeight = coordinates.z + num;
					int num2;
					for (int i = this.BrushHeight; i <= terrainHeight; i = num2 + 1)
					{
						yield return new Vector3Int(coordinates.x, coordinates.y, i);
						num2 = i;
					}
				}
				coordinates = default(Vector3Int);
			}
			List<Vector3Int>.Enumerator enumerator = default(List<Vector3Int>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string TitleLocKey = "MapEditor.Brush.AbsoluteTerrainHeight";

		// Token: 0x04000009 RID: 9
		public static readonly float MarkerYOffset = 0.02f;

		// Token: 0x0400000D RID: 13
		public readonly InputService _inputService;

		// Token: 0x0400000E RID: 14
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000F RID: 15
		public readonly BrushShapeIterator _brushShapeIterator;

		// Token: 0x04000010 RID: 16
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x04000011 RID: 17
		public readonly CameraService _cameraService;

		// Token: 0x04000012 RID: 18
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x04000013 RID: 19
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000014 RID: 20
		public readonly TerrainIntegrityService _terrainIntegrityService;

		// Token: 0x04000015 RID: 21
		public readonly ILoc _loc;

		// Token: 0x04000016 RID: 22
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000017 RID: 23
		public readonly MapSize _mapSize;

		// Token: 0x04000018 RID: 24
		public readonly ISpecService _specService;

		// Token: 0x04000019 RID: 25
		public BrushColorSpec _brushColorSpec;

		// Token: 0x0400001A RID: 26
		public readonly List<Vector3Int> _brushCoordinates = new List<Vector3Int>();

		// Token: 0x0400001B RID: 27
		public MeshDrawer _markerDrawer;

		// Token: 0x0400001C RID: 28
		public ToolDescription _toolDescription;

		// Token: 0x0400001D RID: 29
		public bool _isDrawing;

		// Token: 0x0400001E RID: 30
		public Vector3Int _drawingOrigin;

		// Token: 0x0400001F RID: 31
		public bool _isRegisteringUndo;
	}
}
