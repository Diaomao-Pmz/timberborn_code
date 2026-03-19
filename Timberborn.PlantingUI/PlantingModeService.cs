using System;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Growing;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Planting;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using UnityEngine;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000017 RID: 23
	public class PlantingModeService : ILoadableSingleton
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003240 File Offset: 0x00001440
		public PlantingModeService(EventBus eventBus, PlantingService plantingService, PlantablePreviewService plantablePreviewService, ToolGroupService toolGroupService, ILevelVisibilityService levelVisibilityService)
		{
			this._eventBus = eventBus;
			this._plantingService = plantingService;
			this._plantablePreviewService = plantablePreviewService;
			this._toolGroupService = toolGroupService;
			this._levelVisibilityService = levelVisibilityService;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000326D File Offset: 0x0000146D
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000327B File Offset: 0x0000147B
		[OnEvent]
		public void OnToolGroupEntered(ToolGroupEnteredEvent toolGroupEnteredEvent)
		{
			if (PlantingModeService.IsPlantingToolGroup(toolGroupEnteredEvent.ToolGroup))
			{
				this.EnterPlantingMode();
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003290 File Offset: 0x00001490
		[OnEvent]
		public void OnToolGroupExited(ToolGroupExitedEvent toolGroupExitedEvent)
		{
			if (PlantingModeService.IsPlantingToolGroup(toolGroupExitedEvent.ToolGroup))
			{
				this.ExitPlantingMode();
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032A8 File Offset: 0x000014A8
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			BlockObject component = entityInitializedEvent.Entity.GetComponent<BlockObject>();
			if (component != null && !component.Overridable && component.GetComponent<Growable>())
			{
				foreach (Vector3Int coordinates in component.PositionedBlocks.GetFoundationCoordinates())
				{
					this._plantablePreviewService.HidePreview(coordinates);
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003324 File Offset: 0x00001524
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			if (toolEnteredEvent.Tool is PlantingTool)
			{
				this.EnterPlantingMode();
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003339 File Offset: 0x00001539
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			if (toolExitedEvent.Tool is PlantingTool && !PlantingModeService.IsPlantingToolGroup(this._toolGroupService.ActiveToolGroup))
			{
				this.ExitPlantingMode();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003360 File Offset: 0x00001560
		[OnEvent]
		public void OnMaxVisibleLevelChanged(MaxVisibleLevelChangedEvent maxVisibleLevelChangedEvent)
		{
			if (this._inPlantingMode)
			{
				foreach (Vector3Int coordinates in this._plantingService.PlantingCoordinates)
				{
					if (this._plantablePreviewService.ShouldShowPreview(coordinates) && coordinates.z <= this._levelVisibilityService.MaxVisibleLevel)
					{
						this._plantablePreviewService.ShowPreview(coordinates);
					}
					else
					{
						this._plantablePreviewService.HidePreview(coordinates);
					}
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000033F0 File Offset: 0x000015F0
		public void EnterPlantingMode()
		{
			if (!this._inPlantingMode)
			{
				foreach (Vector3Int coordinates in this._plantingService.PlantingCoordinates)
				{
					if (this._plantablePreviewService.ShouldShowPreview(coordinates) && coordinates.z <= this._levelVisibilityService.MaxVisibleLevel)
					{
						this._plantablePreviewService.ShowPreview(coordinates);
					}
				}
				this._inPlantingMode = true;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003478 File Offset: 0x00001678
		public void ExitPlantingMode()
		{
			if (this._inPlantingMode)
			{
				this._plantablePreviewService.HidePreviews();
				this._inPlantingMode = false;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003494 File Offset: 0x00001694
		public static bool IsPlantingToolGroup(ToolGroupSpec toolGroupSpec)
		{
			return toolGroupSpec != null && toolGroupSpec.HasSpec<PlantingToolGroupSpec>();
		}

		// Token: 0x0400004B RID: 75
		public readonly EventBus _eventBus;

		// Token: 0x0400004C RID: 76
		public readonly PlantingService _plantingService;

		// Token: 0x0400004D RID: 77
		public readonly PlantablePreviewService _plantablePreviewService;

		// Token: 0x0400004E RID: 78
		public readonly ToolGroupService _toolGroupService;

		// Token: 0x0400004F RID: 79
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000050 RID: 80
		public bool _inPlantingMode;
	}
}
