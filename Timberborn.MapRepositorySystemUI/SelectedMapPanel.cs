using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MapItemsUI;
using Timberborn.MapRepositorySystem;
using Timberborn.MapThumbnail;
using Timberborn.WonderCompletion;
using UnityEngine.UIElements;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x02000014 RID: 20
	public class SelectedMapPanel
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002F43 File Offset: 0x00001143
		public SelectedMapPanel(ILoc loc, MapThumbnailCache mapThumbnailCache, WonderCompletionService wonderCompletionService)
		{
			this._loc = loc;
			this._mapThumbnailCache = mapThumbnailCache;
			this._wonderCompletionService = wonderCompletionService;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F60 File Offset: 0x00001160
		public void InitializeWithFlexibleStartInfoShown(VisualElement root)
		{
			this.Initialize(root, true);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F6A File Offset: 0x0000116A
		public void InitializeWithFlexibleStartInfoHidden(VisualElement root)
		{
			this.Initialize(root, false);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F74 File Offset: 0x00001174
		public void Open()
		{
			this._mapThumbnailCache.Clear();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F84 File Offset: 0x00001184
		public void Update(MapItem mapItem)
		{
			this.SetDescription(mapItem);
			this._mapName.text = mapItem.DisplayName;
			this._thumbnail.image = this._mapThumbnailCache.GetThumbnail(mapItem.MapFileReference);
			this._recommendedMap.ToggleDisplayStyle(mapItem.IsRecommended);
			this._unconventionalMap.ToggleDisplayStyle(mapItem.IsUnconventional);
			this._flexibleStart.ToggleDisplayStyle(this._showFlexibleStartInfo && this.IsMapGoalUnlocked(mapItem));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003004 File Offset: 0x00001204
		public void ClearSelection()
		{
			this._mapName.text = string.Empty;
			this._description.text = string.Empty;
			this._thumbnail.image = null;
			this._recommendedMap.ToggleDisplayStyle(false);
			this._unconventionalMap.ToggleDisplayStyle(false);
			this._flexibleStart.ToggleDisplayStyle(false);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003064 File Offset: 0x00001264
		public void Initialize(VisualElement root, bool showFlexibleStartInfo)
		{
			this._showFlexibleStartInfo = showFlexibleStartInfo;
			this._mapName = UQueryExtensions.Q<Label>(root, "MapName", null);
			this._description = UQueryExtensions.Q<Label>(root, "DescriptionText", null);
			this._thumbnail = UQueryExtensions.Q<Image>(root, "ThumbnailImage", null);
			this._recommendedMap = UQueryExtensions.Q<VisualElement>(root, "RecommendedMap", null);
			this._unconventionalMap = UQueryExtensions.Q<VisualElement>(root, "UnconventionalMap", null);
			this._flexibleStart = UQueryExtensions.Q<VisualElement>(root, "FlexibleStart", null);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000030E4 File Offset: 0x000012E4
		public void SetDescription(MapItem mapItem)
		{
			bool flag = string.IsNullOrEmpty(mapItem.DisplayDescription);
			this._description.text = (flag ? this._loc.T(SelectedMapPanel.NoDescriptionLocKey) : mapItem.DisplayDescription);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003124 File Offset: 0x00001324
		public bool IsMapGoalUnlocked(MapItem mapItem)
		{
			MapFileReference mapFileReference = mapItem.MapFileReference;
			return this._wonderCompletionService.IsWonderCompletedWithAnyFaction(mapFileReference.Name, mapFileReference.Resource);
		}

		// Token: 0x04000047 RID: 71
		public static readonly string NoDescriptionLocKey = "MapSelection.NoDescription";

		// Token: 0x04000048 RID: 72
		public readonly ILoc _loc;

		// Token: 0x04000049 RID: 73
		public readonly MapThumbnailCache _mapThumbnailCache;

		// Token: 0x0400004A RID: 74
		public readonly WonderCompletionService _wonderCompletionService;

		// Token: 0x0400004B RID: 75
		public Label _mapName;

		// Token: 0x0400004C RID: 76
		public Label _description;

		// Token: 0x0400004D RID: 77
		public Image _thumbnail;

		// Token: 0x0400004E RID: 78
		public VisualElement _recommendedMap;

		// Token: 0x0400004F RID: 79
		public VisualElement _unconventionalMap;

		// Token: 0x04000050 RID: 80
		public VisualElement _flexibleStart;

		// Token: 0x04000051 RID: 81
		public bool _showFlexibleStartInfo;
	}
}
