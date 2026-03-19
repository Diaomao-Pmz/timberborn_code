using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.ZiplineSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.ZiplineSystemUI
{
	// Token: 0x0200000F RID: 15
	public class ZiplinePreviewTooltip : ILoadableSingleton
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002C34 File Offset: 0x00000E34
		public ZiplinePreviewTooltip(ZiplineConnectionService ziplineConnectionService, VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._ziplineConnectionService = ziplineConnectionService;
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc2;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D14 File Offset: 0x00000F14
		public void Load()
		{
			this._tooltipRoot = this._visualElementLoader.LoadVisualElement("Game/ZiplineConnectionTooltip");
			this._distanceLabel = UQueryExtensions.Q<Label>(this._tooltipRoot, "Distance", null);
			this._distanceWarning = UQueryExtensions.Q<VisualElement>(this._tooltipRoot, "DistanceWarning", null);
			this._distanceIcon = UQueryExtensions.Q<VisualElement>(this._tooltipRoot, "DistanceIcon", null);
			this._inclinationLabel = UQueryExtensions.Q<Label>(this._tooltipRoot, "Inclination", null);
			this._inclinationWarning = UQueryExtensions.Q<VisualElement>(this._tooltipRoot, "InclinationWarning", null);
			this._inclinationIcon = UQueryExtensions.Q<VisualElement>(this._tooltipRoot, "InclinationIcon", null);
			this._warnings = UQueryExtensions.Q<VisualElement>(this._tooltipRoot, "WarningsWrapper", null);
			this._districtsWarning = UQueryExtensions.Q<VisualElement>(this._tooltipRoot, "DistrictsWarning", null);
			this._ziplineBlockedWarning = UQueryExtensions.Q<VisualElement>(this._tooltipRoot, "BlockedWarning", null);
			this._tooManyConnectionsWarning = UQueryExtensions.Q<VisualElement>(this._tooltipRoot, "TooManyConnectionsWarning", null);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E1D File Offset: 0x0000101D
		public void ShowTooltip(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, bool isConnectable)
		{
			this._tooltipRegistrar.ShowPriority(this.GetTooltip(ziplineTower, otherZiplineTower, isConnectable));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E33 File Offset: 0x00001033
		public void HideTooltip()
		{
			this._tooltipRegistrar.HidePriority();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E40 File Offset: 0x00001040
		public VisualElement GetTooltip(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, bool isConnectable)
		{
			float param;
			float param2;
			bool flag = this._ziplineConnectionService.DistanceIsValid(ziplineTower, otherZiplineTower, out param, out param2);
			float param3;
			float param4;
			bool flag2 = this._ziplineConnectionService.InclinationIsValid(ziplineTower, otherZiplineTower, out param3, out param4);
			this._distanceLabel.text = this._loc.T<float, float>(this._distancePhrase, param, param2);
			this._distanceWarning.ToggleDisplayStyle(!flag);
			this._distanceIcon.EnableInClassList(ZiplinePreviewTooltip.CrossClass, !flag);
			this._inclinationLabel.text = this._loc.T<float, float>(this._inclinationPhrase, param3, param4);
			this._inclinationWarning.ToggleDisplayStyle(!flag2);
			this._inclinationIcon.EnableInClassList(ZiplinePreviewTooltip.CrossClass, !flag2);
			this.HideWarnings();
			if (!isConnectable && flag && flag2)
			{
				this.ShowWarning(ziplineTower, otherZiplineTower);
			}
			return this._tooltipRoot;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F14 File Offset: 0x00001114
		public void ShowWarning(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			this._warnings.ToggleDisplayStyle(true);
			if (!this._ziplineConnectionService.DistrictCentersAreCompatible(ziplineTower, otherZiplineTower))
			{
				this._districtsWarning.ToggleDisplayStyle(true);
				return;
			}
			if (!otherZiplineTower.HasFreeSlots)
			{
				this._tooManyConnectionsWarning.ToggleDisplayStyle(true);
				return;
			}
			this._ziplineBlockedWarning.ToggleDisplayStyle(true);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F6A File Offset: 0x0000116A
		public void HideWarnings()
		{
			this._warnings.ToggleDisplayStyle(false);
			this._districtsWarning.ToggleDisplayStyle(false);
			this._ziplineBlockedWarning.ToggleDisplayStyle(false);
			this._tooManyConnectionsWarning.ToggleDisplayStyle(false);
		}

		// Token: 0x04000045 RID: 69
		public static readonly string CrossClass = "cross-red";

		// Token: 0x04000046 RID: 70
		public readonly ZiplineConnectionService _ziplineConnectionService;

		// Token: 0x04000047 RID: 71
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000048 RID: 72
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000049 RID: 73
		public readonly ILoc _loc;

		// Token: 0x0400004A RID: 74
		public VisualElement _tooltipRoot;

		// Token: 0x0400004B RID: 75
		public Label _distanceLabel;

		// Token: 0x0400004C RID: 76
		public VisualElement _distanceWarning;

		// Token: 0x0400004D RID: 77
		public VisualElement _distanceIcon;

		// Token: 0x0400004E RID: 78
		public Label _inclinationLabel;

		// Token: 0x0400004F RID: 79
		public VisualElement _inclinationWarning;

		// Token: 0x04000050 RID: 80
		public VisualElement _inclinationIcon;

		// Token: 0x04000051 RID: 81
		public VisualElement _warnings;

		// Token: 0x04000052 RID: 82
		public VisualElement _districtsWarning;

		// Token: 0x04000053 RID: 83
		public VisualElement _ziplineBlockedWarning;

		// Token: 0x04000054 RID: 84
		public VisualElement _tooManyConnectionsWarning;

		// Token: 0x04000055 RID: 85
		public readonly Phrase _distancePhrase = Phrase.New("Zipline.Distance").Format<float>((float value, ILoc loc) => UnitFormatter.FormatDistance(Mathf.CeilToInt(value), loc)).Format<float>((float value, ILoc loc) => UnitFormatter.FormatDistance((int)value, loc));

		// Token: 0x04000056 RID: 86
		public readonly Phrase _inclinationPhrase = Phrase.New("Zipline.Inclination").Format<float>((float value, ILoc loc) => UnitFormatter.FormatAngle(Mathf.CeilToInt(value), loc)).Format<float>((float value, ILoc loc) => UnitFormatter.FormatAngle((int)value, loc));
	}
}
