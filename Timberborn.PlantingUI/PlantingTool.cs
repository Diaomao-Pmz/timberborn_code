using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Planting;
using Timberborn.SelectionToolSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using UnityEngine;

namespace Timberborn.PlantingUI
{
	// Token: 0x0200001B RID: 27
	public class PlantingTool : ITool, IToolDescriptor
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003A25 File Offset: 0x00001C25
		public PlantableSpec PlantableSpec { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003A2D File Offset: 0x00001C2D
		public string BuildingName { get; }

		// Token: 0x060000A0 RID: 160 RVA: 0x00003A38 File Offset: 0x00001C38
		public PlantingTool(PlantableDescriber plantableDescriber, PlantingSelectionService plantingSelectionService, DevModePlantableSpawner devModePlantableSpawner, ToolUnlockingService toolUnlockingService, SelectionToolProcessorFactory selectionToolProcessorFactory, PlantableSpec plantableSpec, string buildingName)
		{
			this._plantableDescriber = plantableDescriber;
			this._plantingSelectionService = plantingSelectionService;
			this._devModePlantableSpawner = devModePlantableSpawner;
			this._toolUnlockingService = toolUnlockingService;
			this.PlantableSpec = plantableSpec;
			this.BuildingName = buildingName;
			this._selectionToolProcessor = selectionToolProcessorFactory.Create(new Action<IEnumerable<Vector3Int>, Ray>(this.PreviewCallback), new Action<IEnumerable<Vector3Int>, Ray>(this.ActionCallback), new Action(PlantingTool.ShowNoneCallback), PlantingTool.CursorKey);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003AAE File Offset: 0x00001CAE
		public void Enter()
		{
			this._selectionToolProcessor.Enter();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003ABB File Offset: 0x00001CBB
		public void Exit()
		{
			this._selectionToolProcessor.Exit();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public ToolDescription DescribeTool()
		{
			return this._plantableDescriber.Describe(this.PlantableSpec, this.BuildingName);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003AE1 File Offset: 0x00001CE1
		public void PreviewCallback(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			this._plantingSelectionService.HighlightMarkableArea(inputBlocks, ray, this.PlantableSpec.TemplateName);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003AFC File Offset: 0x00001CFC
		public void ActionCallback(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			if (this._toolUnlockingService.IsLocked(this))
			{
				this._toolUnlockingService.TryToUnlock(this, delegate()
				{
					this.Plant(inputBlocks, ray);
				}, delegate()
				{
				});
				return;
			}
			this.Plant(inputBlocks, ray);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000030E3 File Offset: 0x000012E3
		public static void ShowNoneCallback()
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003B7C File Offset: 0x00001D7C
		public void Plant(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			List<Vector3Int> list = inputBlocks.ToList<Vector3Int>();
			string templateName = this.PlantableSpec.TemplateName;
			this._plantingSelectionService.MarkArea(list, ray, templateName);
			this._devModePlantableSpawner.SpawnPlantables(list, templateName);
		}

		// Token: 0x04000061 RID: 97
		public static readonly string CursorKey = "PlantingCursor";

		// Token: 0x04000064 RID: 100
		public readonly PlantableDescriber _plantableDescriber;

		// Token: 0x04000065 RID: 101
		public readonly PlantingSelectionService _plantingSelectionService;

		// Token: 0x04000066 RID: 102
		public readonly DevModePlantableSpawner _devModePlantableSpawner;

		// Token: 0x04000067 RID: 103
		public readonly ToolUnlockingService _toolUnlockingService;

		// Token: 0x04000068 RID: 104
		public readonly SelectionToolProcessor _selectionToolProcessor;
	}
}
