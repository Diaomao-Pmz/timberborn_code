using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingRange;
using Timberborn.ConstructionMode;
using Timberborn.EntitySystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.RangedEffectBuildingUI
{
	// Token: 0x02000008 RID: 8
	public class BuildingWithRangeUpdateService : ILoadableSingleton
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000021F9 File Offset: 0x000003F9
		public BuildingWithRangeUpdateService(RangeTileMarkerService rangeTileMarkerService, RangeObjectHighlighterService rangeObjectHighlighterService, EventBus eventBus)
		{
			this._rangeTileMarkerService = rangeTileMarkerService;
			this._rangeObjectHighlighterService = rangeObjectHighlighterService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002216 File Offset: 0x00000416
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002224 File Offset: 0x00000424
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			IBuildingWithRange buildingWithRange;
			if (BuildingWithRangeUpdateService.TryGetBuildingWithRange(entityInitializedEvent.Entity, out buildingWithRange))
			{
				this._rangeTileMarkerService.AddBuildingWithRange(buildingWithRange);
				this._rangeObjectHighlighterService.AddBuildingWithObjectRange(buildingWithRange);
				if (this.IsSameRangeActive(buildingWithRange))
				{
					this.RecalculateArea(buildingWithRange);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002268 File Offset: 0x00000468
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			IBuildingWithRange buildingWithRange;
			if (BuildingWithRangeUpdateService.TryGetBuildingWithRange(entityDeletedEvent.Entity, out buildingWithRange))
			{
				this._rangeTileMarkerService.RemoveBuildingWithRange(buildingWithRange);
				this._rangeObjectHighlighterService.RemoveBuildingWithObjectRange(buildingWithRange);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000229C File Offset: 0x0000049C
		[OnEvent]
		public void OnSelectableObjectSelected(SelectableObjectSelectedEvent selectableObjectSelectedEvent)
		{
			IBuildingWithRange buildingWithRange;
			if (BuildingWithRangeUpdateService.TryGetBuildingWithRange(selectableObjectSelectedEvent.SelectableObject, out buildingWithRange))
			{
				this._selectedBuilding = buildingWithRange;
				this.Show(buildingWithRange);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022C6 File Offset: 0x000004C6
		[OnEvent]
		public void OnSelectableObjectUnselected(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			if (this._selectedBuilding != null)
			{
				this._rangeObjectHighlighterService.ClearHighlights();
				this._rangeTileMarkerService.HideArea();
				this._selectedBuilding = null;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022ED File Offset: 0x000004ED
		[OnEvent]
		public void OnMaxVisibleLevelChanged(MaxVisibleLevelChangedEvent maxVisibleLevelChangedEvent)
		{
			this.RecalculateArea();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022ED File Offset: 0x000004ED
		[OnEvent]
		public void OnConstructionModeChanged(ConstructionModeChangedEvent constructionModeChangedEvent)
		{
			this.RecalculateArea();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022F5 File Offset: 0x000004F5
		public void AddPreview(IBuildingWithRange buildingWithRange, Preview preview)
		{
			this._rangeTileMarkerService.AddPreviewBuildingWithRange(buildingWithRange, preview);
			this._rangeObjectHighlighterService.AddPreviewBuildingWithObjectRange(buildingWithRange);
			this._previewBuilding = buildingWithRange;
			this.Show(buildingWithRange);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000231E File Offset: 0x0000051E
		public void RemovePreview()
		{
			if (this._previewBuilding != null)
			{
				this._rangeObjectHighlighterService.RemovePreviewBuildingWithObjectRange();
				this._rangeObjectHighlighterService.ClearHighlights();
				this._rangeTileMarkerService.RemovePreviewBuildingWithRange();
				this._rangeTileMarkerService.HideArea();
				this._previewBuilding = null;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000235B File Offset: 0x0000055B
		public void DrawArea()
		{
			this._rangeTileMarkerService.DrawArea();
			this._rangeObjectHighlighterService.HighlightObjects();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002373 File Offset: 0x00000573
		public static bool TryGetBuildingWithRange(BaseComponent component, out IBuildingWithRange buildingWithRange)
		{
			buildingWithRange = component.GetComponent<IBuildingWithRange>();
			return buildingWithRange != null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002382 File Offset: 0x00000582
		public void Show(IBuildingWithRange buildingWithRange)
		{
			this._rangeTileMarkerService.ShowArea();
			this.RecalculateArea(buildingWithRange);
			this.DrawArea();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000239C File Offset: 0x0000059C
		public bool IsSameRangeActive(IBuildingWithRange buildingWithRange)
		{
			return (this._previewBuilding != null && this._previewBuilding.RangeName == buildingWithRange.RangeName) || (this._selectedBuilding != null && this._selectedBuilding.RangeName == buildingWithRange.RangeName);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023EB File Offset: 0x000005EB
		public void RecalculateArea()
		{
			if (this._previewBuilding != null || this._selectedBuilding != null)
			{
				this.RecalculateArea(this._previewBuilding ?? this._selectedBuilding);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002414 File Offset: 0x00000614
		public void RecalculateArea(IBuildingWithRange buildingWithRange)
		{
			string rangeName = buildingWithRange.RangeName;
			this._rangeTileMarkerService.RecalculateArea(rangeName);
			this._rangeObjectHighlighterService.RecalculateAreaAndHighlightObjects(rangeName);
		}

		// Token: 0x0400000F RID: 15
		public readonly RangeTileMarkerService _rangeTileMarkerService;

		// Token: 0x04000010 RID: 16
		public readonly RangeObjectHighlighterService _rangeObjectHighlighterService;

		// Token: 0x04000011 RID: 17
		public readonly EventBus _eventBus;

		// Token: 0x04000012 RID: 18
		public IBuildingWithRange _selectedBuilding;

		// Token: 0x04000013 RID: 19
		public IBuildingWithRange _previewBuilding;
	}
}
