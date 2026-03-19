using System;
using Timberborn.ScienceSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000028 RID: 40
	public class UnlockedPlantableService : IPostLoadableSingleton
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00004139 File Offset: 0x00002339
		public UnlockedPlantableService(EventBus eventBus, ToolButtonService toolButtonService, ToolUnlockingService toolUnlockingService, UnlockedPlantableGroupsRegistry unlockedPlantableGroupsRegistry)
		{
			this._eventBus = eventBus;
			this._toolButtonService = toolButtonService;
			this._toolUnlockingService = toolUnlockingService;
			this._unlockedPlantableGroupsRegistry = unlockedPlantableGroupsRegistry;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000415E File Offset: 0x0000235E
		public void PostLoad()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000416C File Offset: 0x0000236C
		[OnEvent]
		public void OnBuildingUnlocked(BuildingUnlockedEvent buildingUnlockedEvent)
		{
			this._unlockedPlantableGroupsRegistry.AddUnlockedPlantableGroups(buildingUnlockedEvent.BuildingSpec);
			this.UnlockPlantables();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004188 File Offset: 0x00002388
		public void UnlockPlantables()
		{
			foreach (ToolButton toolButton in this._toolButtonService.ToolButtons)
			{
				PlantingTool plantingTool = toolButton.Tool as PlantingTool;
				if (plantingTool != null && this._toolUnlockingService.IsLocked(toolButton.Tool) && !this._unlockedPlantableGroupsRegistry.IsLocked(plantingTool.PlantableSpec))
				{
					this._toolUnlockingService.Unlock(toolButton.Tool);
				}
			}
		}

		// Token: 0x04000084 RID: 132
		public readonly EventBus _eventBus;

		// Token: 0x04000085 RID: 133
		public readonly ToolButtonService _toolButtonService;

		// Token: 0x04000086 RID: 134
		public readonly ToolUnlockingService _toolUnlockingService;

		// Token: 0x04000087 RID: 135
		public readonly UnlockedPlantableGroupsRegistry _unlockedPlantableGroupsRegistry;
	}
}
