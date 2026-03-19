using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystem;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Demolishing;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.Planting;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using UnityEngine;

namespace Timberborn.DemolishingUI
{
	// Token: 0x0200000C RID: 12
	public class DemolishableSelectionTool : ITool, IToolDescriptor, ILoadableSingleton, IInputProcessor
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000028F4 File Offset: 0x00000AF4
		public DemolishableSelectionTool(ILoc loc, ISpecService specService, InputService inputService, CursorService cursorService, PlantingService plantingService, ITerrainService terrainService, AreaBlockObjectPickerFactory areaBlockObjectPickerFactory, BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory)
		{
			this._loc = loc;
			this._specService = specService;
			this._inputService = inputService;
			this._cursorService = cursorService;
			this._plantingService = plantingService;
			this._terrainService = terrainService;
			this._areaBlockObjectPickerFactory = areaBlockObjectPickerFactory;
			this._blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002944 File Offset: 0x00000B44
		public void Load()
		{
			DemolishingColorsSpec singleSpec = this._specService.GetSingleSpec<DemolishingColorsSpec>();
			this._blockObjectSelectionDrawer = this._blockObjectSelectionDrawerFactory.Create(singleSpec.DeletedObjectHighlightColor, singleSpec.DeletedAreaTileColor, singleSpec.DeletedAreaSideColor);
			this._areaBlockObjectPicker = this._areaBlockObjectPickerFactory.CreatePickingUpwards();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002991 File Offset: 0x00000B91
		public ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder(this._loc.T(DemolishableSelectionTool.TitleLocKey)).AddSection(this._loc.T(DemolishableSelectionTool.DescriptionLocKey)).Build();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029C2 File Offset: 0x00000BC2
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
			this._cursorService.SetCursor(DemolishableSelectionTool.CursorKey);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029E0 File Offset: 0x00000BE0
		public void Exit()
		{
			this._blockObjectSelectionDrawer.StopDrawing();
			this._areaBlockObjectPicker.Reset();
			this._inputService.RemoveInputProcessor(this);
			this._cursorService.ResetCursor();
			this.ShowNoneCallback();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A15 File Offset: 0x00000C15
		public bool ProcessInput()
		{
			return this._areaBlockObjectPicker.PickBlockObjects<Demolishable>(new AreaBlockObjectPicker.Callback(this.PreviewCallback), new AreaBlockObjectPicker.Callback(this.ActionCallback), new Action(this.ShowNoneCallback), null);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A47 File Offset: 0x00000C47
		public void PreviewCallback(IEnumerable<BlockObject> blockObjects, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea)
		{
			this._blockObjectSelectionDrawer.Draw(blockObjects, start, end, selectingArea);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A5C File Offset: 0x00000C5C
		public void ActionCallback(IEnumerable<BlockObject> blockObjects, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea)
		{
			foreach (BlockObject blockObject in blockObjects)
			{
				blockObject.GetComponent<Demolishable>().Mark();
			}
			this.UnsetPlantingCoordinates(start, end);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public void ShowNoneCallback()
		{
			this._blockObjectSelectionDrawer.StopDrawing();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public void UnsetPlantingCoordinates(Vector3Int start, Vector3Int end)
		{
			ValueTuple<Vector3Int, Vector3Int> valueTuple = Vectors.MinMax(start, end);
			Vector3Int item = valueTuple.Item1;
			Vector3Int item2 = valueTuple.Item2;
			int z = start.z;
			for (int i = item.x; i <= item2.x; i++)
			{
				for (int j = item.y; j <= item2.y; j++)
				{
					Vector3Int coordinates;
					coordinates..ctor(i, j, z);
					if (this._terrainService.Contains(coordinates) && this._terrainService.GetTerrainHeightBelow(coordinates) == z)
					{
						this._plantingService.UnsetPlantingCoordinates(coordinates);
					}
				}
			}
		}

		// Token: 0x04000028 RID: 40
		public static readonly string CursorKey = "DemolishResourcesCursor";

		// Token: 0x04000029 RID: 41
		public static readonly string TitleLocKey = "DemolishSelectionTool.Title";

		// Token: 0x0400002A RID: 42
		public static readonly string DescriptionLocKey = "DemolishSelectionTool.Description";

		// Token: 0x0400002B RID: 43
		public readonly ILoc _loc;

		// Token: 0x0400002C RID: 44
		public readonly ISpecService _specService;

		// Token: 0x0400002D RID: 45
		public readonly InputService _inputService;

		// Token: 0x0400002E RID: 46
		public readonly CursorService _cursorService;

		// Token: 0x0400002F RID: 47
		public readonly PlantingService _plantingService;

		// Token: 0x04000030 RID: 48
		public readonly ITerrainService _terrainService;

		// Token: 0x04000031 RID: 49
		public readonly AreaBlockObjectPickerFactory _areaBlockObjectPickerFactory;

		// Token: 0x04000032 RID: 50
		public readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;

		// Token: 0x04000033 RID: 51
		public BlockObjectSelectionDrawer _blockObjectSelectionDrawer;

		// Token: 0x04000034 RID: 52
		public AreaBlockObjectPicker _areaBlockObjectPicker;
	}
}
