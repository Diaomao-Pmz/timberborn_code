using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Buildings;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.PlayerDataSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.ScienceSystem
{
	// Token: 0x02000008 RID: 8
	public class BuildingUnlockingService : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002118 File Offset: 0x00000318
		public BuildingUnlockingService(ISingletonLoader singletonLoader, ScienceService scienceService, BuildingService buildingService, MapEditorMode mapEditorMode, EventBus eventBus, TemplateNameMapper templateNameMapper, IPlayerDataService playerDataService)
		{
			this._singletonLoader = singletonLoader;
			this._scienceService = scienceService;
			this._buildingService = buildingService;
			this._mapEditorMode = mapEditorMode;
			this._eventBus = eventBus;
			this._templateNameMapper = templateNameMapper;
			this._playerDataService = playerDataService;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216B File Offset: 0x0000036B
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				singletonSaver.GetSingleton(BuildingUnlockingService.BuildingUnlockingServiceKey).Set(BuildingUnlockingService.UnlockedBuildingsKey, this._unlockedBuildings);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002198 File Offset: 0x00000398
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(BuildingUnlockingService.BuildingUnlockingServiceKey, out objectLoader))
			{
				foreach (string templateName in objectLoader.Get(BuildingUnlockingService.UnlockedBuildingsKey))
				{
					TemplateSpec templateSpec;
					if (this._templateNameMapper.TryGetTemplate(templateName, out templateSpec))
					{
						this._unlockedBuildings.Add(templateSpec.TemplateName);
					}
				}
			}
			this.LoadUnlockableOnce();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002228 File Offset: 0x00000428
		public bool Unlocked(BuildingSpec buildingSpec)
		{
			return buildingSpec.ScienceCost == 0 || this._unlockedBuildings.Contains(this._buildingService.GetTemplateName(buildingSpec));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000224C File Offset: 0x0000044C
		public void Unlock(BuildingSpec buildingSpec)
		{
			if (!this.Unlockable(buildingSpec))
			{
				throw new ArgumentException("Can't unlock " + this._buildingService.GetTemplateName(buildingSpec) + ", not enough science points!");
			}
			this._scienceService.SubtractPoints(buildingSpec.ScienceCost);
			this.UnlockIgnoringCost(buildingSpec);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000229C File Offset: 0x0000049C
		public void UnlockIgnoringCost(BuildingSpec buildingSpec)
		{
			this._unlockedBuildings.Add(this._buildingService.GetTemplateName(buildingSpec));
			if (buildingSpec.HasSpec<UnlockableOnceSpec>())
			{
				this._playerDataService.SetBool(BuildingUnlockingService.UnlockedOnceKey(buildingSpec), true);
			}
			this._eventBus.Post(new BuildingUnlockedEvent(buildingSpec));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022EC File Offset: 0x000004EC
		public bool Unlockable(BuildingSpec buildingSpec)
		{
			return this._scienceService.SciencePoints >= buildingSpec.ScienceCost;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002304 File Offset: 0x00000504
		public void LoadUnlockableOnce()
		{
			foreach (BuildingSpec buildingSpec in from building in this._buildingService.Buildings
			where building.HasSpec<UnlockableOnceSpec>()
			select building)
			{
				if (this._playerDataService.GetBool(BuildingUnlockingService.UnlockedOnceKey(buildingSpec), false))
				{
					this._unlockedBuildings.Add(buildingSpec.GetSpec<TemplateSpec>().TemplateName);
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023A4 File Offset: 0x000005A4
		public static string UnlockedOnceKey(BuildingSpec buildingSpec)
		{
			return BuildingUnlockingService.UnlockedOncePlayerDataKey + buildingSpec.GetSpec<TemplateSpec>().TemplateName;
		}

		// Token: 0x04000009 RID: 9
		public static readonly SingletonKey BuildingUnlockingServiceKey = new SingletonKey("BuildingUnlockingService");

		// Token: 0x0400000A RID: 10
		public static readonly ListKey<string> UnlockedBuildingsKey = new ListKey<string>("UnlockedBuildings");

		// Token: 0x0400000B RID: 11
		public static readonly string UnlockedOncePlayerDataKey = "UnlockedOnce_";

		// Token: 0x0400000C RID: 12
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400000D RID: 13
		public readonly ScienceService _scienceService;

		// Token: 0x0400000E RID: 14
		public readonly BuildingService _buildingService;

		// Token: 0x0400000F RID: 15
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000010 RID: 16
		public readonly EventBus _eventBus;

		// Token: 0x04000011 RID: 17
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x04000012 RID: 18
		public readonly IPlayerDataService _playerDataService;

		// Token: 0x04000013 RID: 19
		public readonly HashSet<string> _unlockedBuildings = new HashSet<string>();
	}
}
