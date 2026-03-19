using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.GameSound;
using Timberborn.Planting;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainQueryingSystem;
using UnityEngine;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000018 RID: 24
	public class PlantingSelectionService : ILoadableSingleton
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000034A4 File Offset: 0x000016A4
		public PlantingSelectionService(TerrainAreaService terrainAreaService, PlantingAreaValidator plantingAreaValidator, PlantingService plantingService, ISpecService specService, GameUISoundController gameUISoundController, AreaHighlightingService areaHighlightingService, IBlockService blockService, PlantablePreviewService plantablePreviewService, EventBus eventBus, MeasurableAreaDrawer measurableAreaDrawer)
		{
			this._terrainAreaService = terrainAreaService;
			this._plantingAreaValidator = plantingAreaValidator;
			this._plantingService = plantingService;
			this._specService = specService;
			this._gameUISoundController = gameUISoundController;
			this._areaHighlightingService = areaHighlightingService;
			this._blockService = blockService;
			this._plantablePreviewService = plantablePreviewService;
			this._eventBus = eventBus;
			this._measurableAreaDrawer = measurableAreaDrawer;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003504 File Offset: 0x00001704
		public void Load()
		{
			this._plantingSelectionServiceSpec = this._specService.GetSingleSpec<PlantingSelectionServiceSpec>();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003518 File Offset: 0x00001718
		public void HighlightMarkableArea(IEnumerable<Vector3Int> inputBlocks, Ray ray, string templateName)
		{
			foreach (Vector3Int coordinates in this._terrainAreaService.InMapLeveledCoordinates(inputBlocks, ray))
			{
				if (this._plantingAreaValidator.CanPlant(coordinates, templateName))
				{
					this._areaHighlightingService.DrawTile(coordinates, this._plantingSelectionServiceSpec.PlantingToolTile);
					this._measurableAreaDrawer.AddMeasurableCoordinates(coordinates);
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003598 File Offset: 0x00001798
		public void MarkArea(IEnumerable<Vector3Int> inputBlocks, Ray ray, string templateName)
		{
			if (this.ActInArea(inputBlocks, ray, (Vector3Int coords) => this._plantingAreaValidator.CanPlant(coords, templateName), delegate(Vector3Int coords)
			{
				this._plantingService.SetPlantingCoordinates(coords, templateName);
			}))
			{
				this._eventBus.Post(new PlantingAreaMarkedEvent());
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000035EC File Offset: 0x000017EC
		public void HighlightUnmarkableArea(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			foreach (Vector3Int vector3Int in this._terrainAreaService.InMapLeveledCoordinates(inputBlocks, ray))
			{
				this._measurableAreaDrawer.AddMeasurableCoordinates(vector3Int);
				if (this._plantingService.GetResourceAt(vector3Int) != null)
				{
					this._areaHighlightingService.DrawTile(vector3Int, this._plantingSelectionServiceSpec.ToolActionTile);
					this.Highlight(vector3Int);
				}
				else
				{
					this._areaHighlightingService.DrawTile(vector3Int, this._plantingSelectionServiceSpec.ToolNoActionTile);
				}
			}
			this._areaHighlightingService.Highlight();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003698 File Offset: 0x00001898
		public void UnmarkArea(IEnumerable<Vector3Int> inputBlocks, Ray ray)
		{
			this.UnhighlightAll();
			this.ActInArea(inputBlocks, ray, (Vector3Int coords) => this._plantingService.IsResourceAt(coords), delegate(Vector3Int coords)
			{
				this._plantingService.UnsetPlantingCoordinates(coords);
			});
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000036C1 File Offset: 0x000018C1
		public void UnhighlightAll()
		{
			this._areaHighlightingService.UnhighlightAll();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000036D0 File Offset: 0x000018D0
		public bool ActInArea(IEnumerable<Vector3Int> inputBlocks, Ray ray, Predicate<Vector3Int> predicate, Action<Vector3Int> action)
		{
			bool flag = false;
			foreach (Vector3Int obj in this._terrainAreaService.InMapLeveledCoordinates(inputBlocks, ray))
			{
				if (predicate(obj))
				{
					action(obj);
					flag = true;
				}
			}
			if (flag)
			{
				this._gameUISoundController.PlayFieldPlacedSound();
			}
			return flag;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003744 File Offset: 0x00001944
		public void Highlight(Vector3Int coords)
		{
			PlantablePreview preview = this._plantablePreviewService.GetPreview(coords);
			if (preview != null && preview.IsShown)
			{
				this._areaHighlightingService.AddForHighlight(preview);
				return;
			}
			Plantable bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<Plantable>(coords);
			if (bottomObjectComponentAt != null)
			{
				this._areaHighlightingService.AddForHighlight(bottomObjectComponentAt);
			}
		}

		// Token: 0x04000051 RID: 81
		public readonly TerrainAreaService _terrainAreaService;

		// Token: 0x04000052 RID: 82
		public readonly PlantingAreaValidator _plantingAreaValidator;

		// Token: 0x04000053 RID: 83
		public readonly PlantingService _plantingService;

		// Token: 0x04000054 RID: 84
		public readonly ISpecService _specService;

		// Token: 0x04000055 RID: 85
		public readonly GameUISoundController _gameUISoundController;

		// Token: 0x04000056 RID: 86
		public readonly AreaHighlightingService _areaHighlightingService;

		// Token: 0x04000057 RID: 87
		public readonly IBlockService _blockService;

		// Token: 0x04000058 RID: 88
		public readonly PlantablePreviewService _plantablePreviewService;

		// Token: 0x04000059 RID: 89
		public readonly EventBus _eventBus;

		// Token: 0x0400005A RID: 90
		public readonly MeasurableAreaDrawer _measurableAreaDrawer;

		// Token: 0x0400005B RID: 91
		public PlantingSelectionServiceSpec _plantingSelectionServiceSpec;
	}
}
