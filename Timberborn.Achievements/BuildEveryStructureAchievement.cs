using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingAvailability;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000015 RID: 21
	public abstract class BuildEveryStructureAchievement : Achievement, IPostLoadableSingleton
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002C98 File Offset: 0x00000E98
		public BuildEveryStructureAchievement(EventBus eventBus, FactionService factionService, TemplateService templateService, EntityComponentRegistry entityComponentRegistry, BuildingAvailabilityValidator buildingAvailabilityValidator, string faction)
		{
			this._eventBus = eventBus;
			this._factionService = factionService;
			this._templateService = templateService;
			this._entityComponentRegistry = entityComponentRegistry;
			this._buildingAvailabilityValidator = buildingAvailabilityValidator;
			this._faction = faction;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002CCD File Offset: 0x00000ECD
		public override string Id
		{
			get
			{
				return "BUILD_EVERY_STRUCTURE_" + this._faction.ToUpperInvariant();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public void PostLoad()
		{
			this._structuresToBuild = (from spec in this._templateService.GetAll<BuildingSpec>().Where(new Func<BuildingSpec, bool>(this.IsValidStructure))
			select spec.GetSpec<TemplateSpec>().TemplateName).ToHashSet<string>();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D3C File Offset: 0x00000F3C
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			BuildingSpec component = enteredFinishedStateEvent.BlockObject.GetComponent<BuildingSpec>();
			TemplateSpec templateSpec = (component != null) ? component.GetSpec<TemplateSpec>() : null;
			if (templateSpec != null)
			{
				this.TryUnlock(templateSpec);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D6B File Offset: 0x00000F6B
		public override void EnableInternal()
		{
			if (this._factionService.Current.Id == this._faction)
			{
				this._eventBus.Register(this);
				this.TryUnlockFromExisting();
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D9C File Offset: 0x00000F9C
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DAC File Offset: 0x00000FAC
		public bool IsValidStructure(BuildingSpec buildingSpec)
		{
			TemplateSpec spec = buildingSpec.GetSpec<TemplateSpec>();
			if (spec.UsableWithCurrentFeatureToggles)
			{
				foreach (string value in BuildEveryStructureAchievement.BlacklistedPrefixes)
				{
					if (spec.TemplateName.StartsWith(value))
					{
						return false;
					}
				}
				return this._buildingAvailabilityValidator.IsAvailableForPlacement(buildingSpec) && !buildingSpec.GetSpec<PlaceableBlockObjectSpec>().DevModeTool;
			}
			return false;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E3C File Offset: 0x0000103C
		public void TryUnlock(TemplateSpec template)
		{
			this._structuresToBuild.Remove(template.TemplateName);
			if (this._structuresToBuild.Count == 0)
			{
				base.Unlock();
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E64 File Offset: 0x00001064
		public void TryUnlockFromExisting()
		{
			foreach (Building building in this._entityComponentRegistry.GetEnabled<Building>())
			{
				BlockObject component = building.GetComponent<BlockObject>();
				if (component != null && component.IsFinished)
				{
					this.TryUnlock(building.GetComponent<TemplateSpec>());
				}
			}
		}

		// Token: 0x0400002A RID: 42
		public static readonly HashSet<string> BlacklistedPrefixes = new HashSet<string>
		{
			"Dynamite.",
			"DoubleDynamite.",
			"TripleDynamite.",
			"Tunnel.",
			"TerrainBlock."
		};

		// Token: 0x0400002B RID: 43
		public readonly EventBus _eventBus;

		// Token: 0x0400002C RID: 44
		public readonly FactionService _factionService;

		// Token: 0x0400002D RID: 45
		public readonly TemplateService _templateService;

		// Token: 0x0400002E RID: 46
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x0400002F RID: 47
		public readonly BuildingAvailabilityValidator _buildingAvailabilityValidator;

		// Token: 0x04000030 RID: 48
		public readonly string _faction;

		// Token: 0x04000031 RID: 49
		public HashSet<string> _structuresToBuild;
	}
}
