using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Forestry;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.SelectionToolSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainQueryingSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using UnityEngine;

namespace Timberborn.ForestryUI
{
	// Token: 0x02000012 RID: 18
	public class TreeCuttingAreaSelectionTool : ITool, IToolDescriptor, ILoadableSingleton
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000284C File Offset: 0x00000A4C
		public TreeCuttingAreaSelectionTool(TreeCuttingArea treeCuttingArea, TerrainAreaService terrainAreaService, AreaHighlightingService areaHighlightingService, IBlockService blockService, SelectionToolProcessorFactory selectionToolProcessorFactory, ILoc loc, ISpecService specService, MeasurableAreaDrawer measurableAreaDrawer)
		{
			this._treeCuttingArea = treeCuttingArea;
			this._terrainAreaService = terrainAreaService;
			this._areaHighlightingService = areaHighlightingService;
			this._blockService = blockService;
			this._selectionToolProcessorFactory = selectionToolProcessorFactory;
			this._loc = loc;
			this._specService = specService;
			this._measurableAreaDrawer = measurableAreaDrawer;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000289C File Offset: 0x00000A9C
		public void Load()
		{
			this._selectionToolProcessor = this._selectionToolProcessorFactory.Create(new Action<IEnumerable<Vector3Int>, Ray>(this.PreviewCallback), new Action<IEnumerable<Vector3Int>, Ray>(this.ActionCallback), new Action(this.ShowNoneCallback), TreeCuttingAreaSelectionTool.CursorKey);
			this._toolActionTileColor = this._specService.GetSingleSpec<TreeCuttingColorsSpec>().ToolActionTile;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028F9 File Offset: 0x00000AF9
		public ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder(this._loc.T(TreeCuttingAreaSelectionTool.TitleLocKey)).AddSection(this._loc.T(TreeCuttingAreaSelectionTool.DescriptionLocKey)).Build();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000292A File Offset: 0x00000B2A
		public void Enter()
		{
			this._selectionToolProcessor.Enter();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002937 File Offset: 0x00000B37
		public void Exit()
		{
			this._areaHighlightingService.UnhighlightAll();
			this._selectionToolProcessor.Exit();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002950 File Offset: 0x00000B50
		public void PreviewCallback(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			foreach (Vector3Int coordinates in this._terrainAreaService.InMapLeveledCoordinates(inputBlocks, ray))
			{
				if (!this._treeCuttingArea.IsInCuttingArea(coordinates))
				{
					this._areaHighlightingService.DrawTile(coordinates, this._toolActionTileColor);
					this._measurableAreaDrawer.AddMeasurableCoordinates(coordinates);
					TreeComponent bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<TreeComponent>(coordinates);
					if (bottomObjectComponentAt != null)
					{
						this._areaHighlightingService.AddForHighlight(bottomObjectComponentAt);
					}
				}
			}
			this._areaHighlightingService.Highlight();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029F0 File Offset: 0x00000BF0
		public void ActionCallback(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			this._areaHighlightingService.UnhighlightAll();
			IEnumerable<Vector3Int> coordinates = this._terrainAreaService.InMapLeveledCoordinates(inputBlocks, ray);
			this._treeCuttingArea.AddCoordinates(coordinates);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A22 File Offset: 0x00000C22
		public void ShowNoneCallback()
		{
			this._areaHighlightingService.UnhighlightAll();
		}

		// Token: 0x0400002D RID: 45
		public static readonly string CursorKey = "CutTreeCursor";

		// Token: 0x0400002E RID: 46
		public static readonly string DescriptionLocKey = "TreeCuttingSelectionTool.Description";

		// Token: 0x0400002F RID: 47
		public static readonly string TitleLocKey = "TreeCuttingSelectionTool.Title";

		// Token: 0x04000030 RID: 48
		public readonly TreeCuttingArea _treeCuttingArea;

		// Token: 0x04000031 RID: 49
		public readonly TerrainAreaService _terrainAreaService;

		// Token: 0x04000032 RID: 50
		public readonly AreaHighlightingService _areaHighlightingService;

		// Token: 0x04000033 RID: 51
		public readonly IBlockService _blockService;

		// Token: 0x04000034 RID: 52
		public readonly SelectionToolProcessorFactory _selectionToolProcessorFactory;

		// Token: 0x04000035 RID: 53
		public readonly ILoc _loc;

		// Token: 0x04000036 RID: 54
		public readonly ISpecService _specService;

		// Token: 0x04000037 RID: 55
		public readonly MeasurableAreaDrawer _measurableAreaDrawer;

		// Token: 0x04000038 RID: 56
		public SelectionToolProcessor _selectionToolProcessor;

		// Token: 0x04000039 RID: 57
		public Color _toolActionTileColor;
	}
}
