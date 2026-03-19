using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Forestry;
using Timberborn.Localization;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using Timberborn.SelectionToolSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainQueryingSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using UnityEngine;

namespace Timberborn.ForestryUI
{
	// Token: 0x02000013 RID: 19
	public class TreeCuttingAreaUnselectionTool : ITool, IToolDescriptor, ILoadableSingleton
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002A50 File Offset: 0x00000C50
		public TreeCuttingAreaUnselectionTool(TreeCuttingArea treeCuttingArea, TerrainAreaService terrainAreaService, AreaHighlightingService areaHighlightingService, IBlockService blockService, SelectionToolProcessorFactory selectionToolProcessorFactory, ILoc loc, MarkerDrawerFactory markerDrawerFactory, ISpecService specService, MeasurableAreaDrawer measurableAreaDrawer)
		{
			this._treeCuttingArea = treeCuttingArea;
			this._terrainAreaService = terrainAreaService;
			this._areaHighlightingService = areaHighlightingService;
			this._blockService = blockService;
			this._selectionToolProcessorFactory = selectionToolProcessorFactory;
			this._loc = loc;
			this._markerDrawerFactory = markerDrawerFactory;
			this._specService = specService;
			this._measurableAreaDrawer = measurableAreaDrawer;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public void Load()
		{
			TreeCuttingColorsSpec singleSpec = this._specService.GetSingleSpec<TreeCuttingColorsSpec>();
			this._actionMeshDrawer = this._markerDrawerFactory.CreatePrioritizedTileDrawer(singleSpec.ToolActionTile);
			this._noActionMeshDrawer = this._markerDrawerFactory.CreateTileDrawer(singleSpec.ToolNoActionTile);
			this._selectionToolProcessor = this._selectionToolProcessorFactory.Create(new Action<IEnumerable<Vector3Int>, Ray>(this.PreviewCallback), new Action<IEnumerable<Vector3Int>, Ray>(this.ActionCallback), new Action(this.ShowNoneCallback), TreeCuttingAreaUnselectionTool.CursorKey);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B29 File Offset: 0x00000D29
		public ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder(this._loc.T(TreeCuttingAreaUnselectionTool.TitleLocKey)).AddSection(this._loc.T(TreeCuttingAreaUnselectionTool.DescriptionLocKey)).Build();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B5A File Offset: 0x00000D5A
		public void Enter()
		{
			this._selectionToolProcessor.Enter();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B67 File Offset: 0x00000D67
		public void Exit()
		{
			this._areaHighlightingService.UnhighlightAll();
			this._selectionToolProcessor.Exit();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B80 File Offset: 0x00000D80
		public void PreviewCallback(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			foreach (Vector3Int coordinates in this._terrainAreaService.InMapLeveledCoordinates(inputBlocks, ray))
			{
				this._measurableAreaDrawer.AddMeasurableCoordinates(coordinates);
				if (this._treeCuttingArea.IsInCuttingArea(coordinates))
				{
					this._actionMeshDrawer.DrawAtCoordinates(coordinates, 0.03f);
					TreeComponent bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<TreeComponent>(coordinates);
					if (bottomObjectComponentAt != null)
					{
						this._areaHighlightingService.AddForHighlight(bottomObjectComponentAt);
					}
				}
				else
				{
					this._noActionMeshDrawer.DrawAtCoordinates(coordinates, 0.02f);
				}
			}
			this._areaHighlightingService.Highlight();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C34 File Offset: 0x00000E34
		public void ActionCallback(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			this._areaHighlightingService.UnhighlightAll();
			IEnumerable<Vector3Int> coordinates = this._terrainAreaService.InMapLeveledCoordinates(inputBlocks, ray);
			this._treeCuttingArea.RemoveCoordinates(coordinates);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C66 File Offset: 0x00000E66
		public void ShowNoneCallback()
		{
			this._areaHighlightingService.UnhighlightAll();
		}

		// Token: 0x0400003A RID: 58
		public static readonly string CursorKey = "CancelCursor";

		// Token: 0x0400003B RID: 59
		public static readonly string DescriptionLocKey = "TreeCuttingUnselectionTool.Description";

		// Token: 0x0400003C RID: 60
		public static readonly string TitleLocKey = "TreeCuttingUnselectionTool.Title";

		// Token: 0x0400003D RID: 61
		public readonly TreeCuttingArea _treeCuttingArea;

		// Token: 0x0400003E RID: 62
		public readonly TerrainAreaService _terrainAreaService;

		// Token: 0x0400003F RID: 63
		public readonly AreaHighlightingService _areaHighlightingService;

		// Token: 0x04000040 RID: 64
		public readonly IBlockService _blockService;

		// Token: 0x04000041 RID: 65
		public readonly SelectionToolProcessorFactory _selectionToolProcessorFactory;

		// Token: 0x04000042 RID: 66
		public readonly ILoc _loc;

		// Token: 0x04000043 RID: 67
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x04000044 RID: 68
		public readonly ISpecService _specService;

		// Token: 0x04000045 RID: 69
		public readonly MeasurableAreaDrawer _measurableAreaDrawer;

		// Token: 0x04000046 RID: 70
		public SelectionToolProcessor _selectionToolProcessor;

		// Token: 0x04000047 RID: 71
		public MeshDrawer _noActionMeshDrawer;

		// Token: 0x04000048 RID: 72
		public MeshDrawer _actionMeshDrawer;
	}
}
