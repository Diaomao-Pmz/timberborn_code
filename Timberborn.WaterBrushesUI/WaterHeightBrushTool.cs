using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Brushes;
using Timberborn.Common;
using Timberborn.CursorToolSystem;
using Timberborn.InputSystem;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterBrushesUI
{
	// Token: 0x0200000C RID: 12
	public class WaterHeightBrushTool : IDevModeTool, ITool, IWaterIgnoringTool, IInputProcessor, ITickableSingleton, ILoadableSingleton, IBrushWithSize, IBrushWithShape
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000024C9 File Offset: 0x000006C9
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000024D1 File Offset: 0x000006D1
		public int BrushSize { get; set; } = 1;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000024DA File Offset: 0x000006DA
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000024E2 File Offset: 0x000006E2
		public BrushShape BrushShape { get; set; }

		// Token: 0x0600002C RID: 44 RVA: 0x000024EC File Offset: 0x000006EC
		public WaterHeightBrushTool(InputService inputService, IWaterService waterService, BrushShapeIterator brushShapeIterator, MarkerDrawerFactory markerDrawerFactory, IThreadSafeWaterMap threadSafeWaterMap, CursorCoordinatesPicker cursorCoordinatesPicker, ISpecService specService)
		{
			this._inputService = inputService;
			this._waterService = waterService;
			this._brushShapeIterator = brushShapeIterator;
			this._markerDrawerFactory = markerDrawerFactory;
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._cursorCoordinatesPicker = cursorCoordinatesPicker;
			this._specService = specService;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002546 File Offset: 0x00000746
		public bool IsDevMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002549 File Offset: 0x00000749
		public void Load()
		{
			this._waterHeightBrushSpec = this._specService.GetSingleSpec<WaterHeightBrushSpec>();
			this._meshDrawer = this._markerDrawerFactory.CreateTileDrawer();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002570 File Offset: 0x00000770
		public void Tick()
		{
			while (!this._waterChanges.IsEmpty<WaterHeightBrushTool.BrushWaterChange>())
			{
				WaterHeightBrushTool.BrushWaterChange brushWaterChange = this._waterChanges.Dequeue();
				Vector3Int coordinates = brushWaterChange.Coordinates;
				int num;
				if (this._threadSafeWaterMap.TryGetColumnFloor(coordinates, out num))
				{
					Vector3Int coordinates2;
					coordinates2..ctor(coordinates.x, coordinates.y, num);
					if (brushWaterChange.IsContaminated)
					{
						this._waterService.AddContaminatedWater(coordinates2, brushWaterChange.WaterChange);
					}
					else
					{
						this._waterService.AddCleanWater(coordinates2, brushWaterChange.WaterChange);
					}
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025F8 File Offset: 0x000007F8
		public bool ProcessInput()
		{
			CursorCoordinates? cursorCoordinates = this._cursorCoordinatesPicker.Pick();
			if (cursorCoordinates != null)
			{
				CursorCoordinates valueOrDefault = cursorCoordinates.GetValueOrDefault();
				bool isRemoving = this._inputService.IsKeyHeld(WaterHeightBrushTool.RemoveWaterModifierKey);
				bool isContaminating = this._inputService.IsKeyHeld(WaterHeightBrushTool.UseContaminationModifierKey);
				if (this._inputService.MainMouseButtonDown && !this._inputService.MouseOverUI)
				{
					this.ApplyBrush(valueOrDefault.TileCoordinates, isRemoving, isContaminating);
				}
				this.DrawTileMarkers(valueOrDefault.TileCoordinates, isRemoving, isContaminating);
			}
			return false;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000267D File Offset: 0x0000087D
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000268B File Offset: 0x0000088B
		public void Exit()
		{
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000269C File Offset: 0x0000089C
		public void ApplyBrush(Vector3Int tileCoordinates, bool isRemoving, bool isContaminating)
		{
			foreach (Vector3Int coordinates in this.GetAffectedCoordinates(tileCoordinates))
			{
				float num = this._threadSafeWaterMap.WaterHeightOrFloor(coordinates) % 1f;
				if (!isRemoving && num > 0.99f)
				{
					num = 0f;
				}
				float waterChange = isRemoving ? (-num) : (1f - num);
				this._waterChanges.Enqueue(new WaterHeightBrushTool.BrushWaterChange(coordinates, waterChange, isContaminating));
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000272C File Offset: 0x0000092C
		public IEnumerable<Vector3Int> GetAffectedCoordinates(Vector3Int center)
		{
			IEnumerable<Vector3Int> enumerable = this._brushShapeIterator.IterateShape(center, this.BrushSize, this.BrushShape);
			foreach (Vector3Int vector3Int in enumerable)
			{
				yield return new Vector3Int(vector3Int.x, vector3Int.y, center.z);
			}
			IEnumerator<Vector3Int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002744 File Offset: 0x00000944
		public void DrawTileMarkers(Vector3Int center, bool isRemoving, bool isContaminating)
		{
			foreach (Vector3Int coordinates in this.GetAffectedCoordinates(center))
			{
				Color color = this.GetColor(isRemoving, isContaminating);
				this._meshDrawer.DrawAtCoordinates(coordinates, WaterHeightBrushTool.MarkerYOffset, color);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027A8 File Offset: 0x000009A8
		public Color GetColor(bool isRemoving, bool isContaminating)
		{
			if (isRemoving)
			{
				return this._waterHeightBrushSpec.RemovingTileColor;
			}
			if (isContaminating)
			{
				return this._waterHeightBrushSpec.ContaminatedTileColor;
			}
			return this._waterHeightBrushSpec.AddingTileColor;
		}

		// Token: 0x04000013 RID: 19
		public static readonly float MarkerYOffset = 0.02f;

		// Token: 0x04000014 RID: 20
		public static readonly string RemoveWaterModifierKey = "RemoveWaterModifier";

		// Token: 0x04000015 RID: 21
		public static readonly string UseContaminationModifierKey = "UseContaminationModifier";

		// Token: 0x04000018 RID: 24
		public readonly InputService _inputService;

		// Token: 0x04000019 RID: 25
		public readonly IWaterService _waterService;

		// Token: 0x0400001A RID: 26
		public readonly BrushShapeIterator _brushShapeIterator;

		// Token: 0x0400001B RID: 27
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x0400001C RID: 28
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400001D RID: 29
		public readonly CursorCoordinatesPicker _cursorCoordinatesPicker;

		// Token: 0x0400001E RID: 30
		public readonly ISpecService _specService;

		// Token: 0x0400001F RID: 31
		public WaterHeightBrushSpec _waterHeightBrushSpec;

		// Token: 0x04000020 RID: 32
		public MeshDrawer _meshDrawer;

		// Token: 0x04000021 RID: 33
		public readonly Queue<WaterHeightBrushTool.BrushWaterChange> _waterChanges = new Queue<WaterHeightBrushTool.BrushWaterChange>();

		// Token: 0x0200000D RID: 13
		public struct BrushWaterChange
		{
			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000038 RID: 56 RVA: 0x000027F3 File Offset: 0x000009F3
			public readonly Vector3Int Coordinates { get; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000039 RID: 57 RVA: 0x000027FB File Offset: 0x000009FB
			public readonly float WaterChange { get; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x0600003A RID: 58 RVA: 0x00002803 File Offset: 0x00000A03
			public readonly bool IsContaminated { get; }

			// Token: 0x0600003B RID: 59 RVA: 0x0000280B File Offset: 0x00000A0B
			public BrushWaterChange(Vector3Int coordinates, float waterChange, bool isContaminated)
			{
				this.Coordinates = coordinates;
				this.WaterChange = waterChange;
				this.IsContaminated = isContaminated;
			}
		}
	}
}
