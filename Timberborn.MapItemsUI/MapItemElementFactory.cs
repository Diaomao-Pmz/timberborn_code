using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.FactionSystem;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using Timberborn.WonderCompletion;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MapItemsUI
{
	// Token: 0x02000007 RID: 7
	public class MapItemElementFactory
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002188 File Offset: 0x00000388
		public MapItemElementFactory(ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader, WonderCompletionService wonderCompletionService, FactionSpecService factionSpecService, MapItemFactionIconFactory mapItemFactionIconFactory, ILoc loc)
		{
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
			this._wonderCompletionService = wonderCompletionService;
			this._factionSpecService = factionSpecService;
			this._mapItemFactionIconFactory = mapItemFactionIconFactory;
			this._loc = loc;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021E0 File Offset: 0x000003E0
		public VisualElement Create()
		{
			VisualElement item = this._visualElementLoader.LoadVisualElement("Common/MapItemElement");
			this._tooltipRegistrar.Register(UQueryExtensions.Q<VisualElement>(item, "Icon", null), () => this.GetIconTooltipLocKey(item));
			this._tooltipRegistrar.Register(UQueryExtensions.Q<VisualElement>(item, "Recommended", null), () => this._loc.T(MapItemElementFactory.RecommendedLocKey));
			this._tooltipRegistrar.Register(UQueryExtensions.Q<VisualElement>(item, "Unconventional", null), () => this._loc.T(MapItemElementFactory.UnconventionalLocKey));
			return item;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002290 File Offset: 0x00000490
		public void Bind(VisualElement item, MapItem mapItem, bool showMapGoals)
		{
			if (showMapGoals)
			{
				this.CreateFactionIcons(item, mapItem);
			}
			UQueryExtensions.Q<Label>(item, "MapName", null).text = mapItem.DisplayName;
			UQueryExtensions.Q<Label>(item, "MapSize", null).text = MapItemElementFactory.GetDisplaySize(mapItem.Size);
			UQueryExtensions.Q<Image>(item, "Recommended", null).ToggleDisplayStyle(mapItem.IsRecommended);
			UQueryExtensions.Q<Image>(item, "Unconventional", null).ToggleDisplayStyle(mapItem.IsUnconventional);
			Image image = UQueryExtensions.Q<Image>(item, "Icon", null);
			if (mapItem.MapIcon != null)
			{
				image.sprite = mapItem.MapIcon.Icon;
				image.ToggleDisplayStyle(true);
				this._iconTooltipLocKeys[item] = mapItem.MapIcon.TooltipLocKey;
				return;
			}
			image.ToggleDisplayStyle(false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002355 File Offset: 0x00000555
		public void Clear()
		{
			this._iconTooltipLocKeys.Clear();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002364 File Offset: 0x00000564
		public void CreateFactionIcons(VisualElement item, MapItem mapItem)
		{
			IEnumerable<string> completedWonderFactionIds = this._wonderCompletionService.GetWonderCompletionFactionIds(mapItem.MapFileReference.Name, mapItem.MapFileReference.Resource);
			List<FactionSpec> validFactions = (from faction in this._factionSpecService.Factions
			where completedWonderFactionIds.Contains(faction.Id)
			select faction).ToList<FactionSpec>();
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(item, "FactionsList", null);
			visualElement.Clear();
			foreach (FactionSpec factionSpec in from faction in validFactions
			orderby faction.Order descending
			select faction)
			{
				visualElement.Add(this._mapItemFactionIconFactory.Create(factionSpec));
			}
			this._tooltipRegistrar.Register(visualElement, () => this.GetFactionIconsTooltipText(validFactions));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002470 File Offset: 0x00000670
		public string GetFactionIconsTooltipText(IEnumerable<FactionSpec> factionSpecs)
		{
			this._tooltipBuilder.Clear();
			foreach (FactionSpec factionSpec in factionSpecs)
			{
				string str = this._loc.T<string>(MapItemElementFactory.WonderCompletedLocKey, factionSpec.DisplayName.Value);
				this._tooltipBuilder.AppendLine(SpecialStrings.RowStarter + str);
			}
			return this._tooltipBuilder.ToStringWithoutNewLineEnd();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024FC File Offset: 0x000006FC
		public static string GetDisplaySize(Vector2Int? size)
		{
			if (size == null)
			{
				return string.Empty;
			}
			return string.Format("{0}{1}{2}", size.Value.x, SpecialStrings.SizeSeparator, size.Value.y);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002550 File Offset: 0x00000750
		public string GetIconTooltipLocKey(VisualElement item)
		{
			string text = this._iconTooltipLocKeys[item];
			if (!string.IsNullOrEmpty(text))
			{
				return this._loc.T(text);
			}
			return null;
		}

		// Token: 0x04000011 RID: 17
		public static readonly string WonderCompletedLocKey = "WonderCompletion.WonderCompleted";

		// Token: 0x04000012 RID: 18
		public static readonly string RecommendedLocKey = "MapSelection.Recommended";

		// Token: 0x04000013 RID: 19
		public static readonly string UnconventionalLocKey = "MapSelection.Unconventional";

		// Token: 0x04000014 RID: 20
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000015 RID: 21
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000016 RID: 22
		public readonly WonderCompletionService _wonderCompletionService;

		// Token: 0x04000017 RID: 23
		public readonly FactionSpecService _factionSpecService;

		// Token: 0x04000018 RID: 24
		public readonly MapItemFactionIconFactory _mapItemFactionIconFactory;

		// Token: 0x04000019 RID: 25
		public readonly ILoc _loc;

		// Token: 0x0400001A RID: 26
		public readonly Dictionary<VisualElement, string> _iconTooltipLocKeys = new Dictionary<VisualElement, string>();

		// Token: 0x0400001B RID: 27
		public readonly StringBuilder _tooltipBuilder = new StringBuilder();
	}
}
