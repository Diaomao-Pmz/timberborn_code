using System;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Debugging;
using Timberborn.EntitySystem;

namespace Timberborn.BuildingsUI
{
	// Token: 0x0200000A RID: 10
	public class BuildingsModelToggler : IDevModule
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002754 File Offset: 0x00000954
		public BuildingsModelToggler(EntityComponentRegistry entityComponentRegistry)
		{
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002763 File Offset: 0x00000963
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle models: Buildings", new Action(this.ToggleBuildingModels))).Build();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000278C File Offset: 0x0000098C
		public void ToggleBuildingModels()
		{
			this._buildingsHidden = !this._buildingsHidden;
			foreach (Building building in this._entityComponentRegistry.GetEnabled<Building>())
			{
				this.ToggleBuildingModel(building);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027F0 File Offset: 0x000009F0
		public void ToggleBuildingModel(Building building)
		{
			bool active = !this._buildingsHidden;
			BuildingModel component = building.GetComponent<BuildingModel>();
			if (component != null)
			{
				BlockObject component2 = component.GetComponent<BlockObject>();
				if (component2.IsFinished)
				{
					component.FinishedModel.SetActive(active);
					return;
				}
				if (component2.IsUnfinished)
				{
					component.UnfinishedModel.SetActive(active);
				}
			}
		}

		// Token: 0x04000026 RID: 38
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x04000027 RID: 39
		public bool _buildingsHidden;
	}
}
