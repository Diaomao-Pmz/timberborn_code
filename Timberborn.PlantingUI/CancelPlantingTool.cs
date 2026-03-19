using System;
using System.Collections.Generic;
using Timberborn.Localization;
using Timberborn.SelectionToolSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using UnityEngine;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000007 RID: 7
	public class CancelPlantingTool : ITool, IToolDescriptor
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public CancelPlantingTool(PlantingSelectionService plantingSelectionService, ILoc loc, SelectionToolProcessorFactory selectionToolProcessorFactory)
		{
			this._plantingSelectionService = plantingSelectionService;
			this._loc = loc;
			this._selectionToolProcessor = selectionToolProcessorFactory.Create(new Action<IEnumerable<Vector3Int>, Ray>(this.PreviewCallback), new Action<IEnumerable<Vector3Int>, Ray>(this.ActionCallback), new Action(this.ShowNoneCallback), CancelPlantingTool.CursorKey);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002156 File Offset: 0x00000356
		public void Enter()
		{
			this._selectionToolProcessor.Enter();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002163 File Offset: 0x00000363
		public void Exit()
		{
			this._plantingSelectionService.UnhighlightAll();
			this._selectionToolProcessor.Exit();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000217B File Offset: 0x0000037B
		public ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder(this._loc.T(CancelPlantingTool.TitleLocKey)).AddSection(this._loc.T(CancelPlantingTool.DescriptionLocKey)).Build();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021AC File Offset: 0x000003AC
		public void PreviewCallback(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			this._plantingSelectionService.HighlightUnmarkableArea(inputBlocks, ray);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021BB File Offset: 0x000003BB
		public void ActionCallback(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			this._plantingSelectionService.UnmarkArea(inputBlocks, ray);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021CA File Offset: 0x000003CA
		public void ShowNoneCallback()
		{
			this._plantingSelectionService.UnhighlightAll();
		}

		// Token: 0x04000008 RID: 8
		public static readonly string CursorKey = "CancelCursor";

		// Token: 0x04000009 RID: 9
		public static readonly string TitleLocKey = "CancelPlantingTool.Title";

		// Token: 0x0400000A RID: 10
		public static readonly string DescriptionLocKey = "CancelPlantingTool.Description";

		// Token: 0x0400000B RID: 11
		public readonly PlantingSelectionService _plantingSelectionService;

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;

		// Token: 0x0400000D RID: 13
		public readonly SelectionToolProcessor _selectionToolProcessor;
	}
}
