using System;
using Timberborn.Localization;
using Timberborn.ModdingUI;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuModdingUI
{
	// Token: 0x0200000B RID: 11
	public class ModManagerBoxTooltipRegistrar : IModManagerTooltipRegistrar
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002BDF File Offset: 0x00000DDF
		public ModManagerBoxTooltipRegistrar(ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public void RegisterModWarning(VisualElement element, ModItem modItem)
		{
			this._tooltipRegistrar.Register(element, () => this.GetWarningText(modItem));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C31 File Offset: 0x00000E31
		public void RegisterModIcon(VisualElement element, ModItem modItem)
		{
			this._tooltipRegistrar.Register(element, this.GetModSourceText(modItem));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C46 File Offset: 0x00000E46
		public void RegisterIncreaseButton(VisualElement element)
		{
			this._tooltipRegistrar.Register(element, this._loc.T(ModManagerBoxTooltipRegistrar.IncreasePriorityLocKey));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C64 File Offset: 0x00000E64
		public void RegisterDecreaseButton(VisualElement element)
		{
			this._tooltipRegistrar.Register(element, this._loc.T(ModManagerBoxTooltipRegistrar.DecreasePriorityLocKey));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C84 File Offset: 0x00000E84
		public string GetWarningText(ModItem modItem)
		{
			string result;
			switch (modItem.WarningReason)
			{
			case ModWarningReason.None:
				throw new ArgumentException("GetWarningText called with None warning reason");
			case ModWarningReason.MissingRequiredMod:
				result = this._loc.T<string>(ModManagerBoxTooltipRegistrar.MissingRequiredModLocKey, modItem.WarningInfo);
				break;
			case ModWarningReason.RequiredModNotEnabled:
				result = this._loc.T<string>(ModManagerBoxTooltipRegistrar.RequiredModNotEnabledLocKey, modItem.WarningInfo);
				break;
			case ModWarningReason.RequiredModInvalidVersion:
				result = this._loc.T<string>(ModManagerBoxTooltipRegistrar.RequiredModInvalidVersionLocKey, modItem.WarningInfo);
				break;
			case ModWarningReason.RequiredModInvalidOrder:
				result = this._loc.T<string>(ModManagerBoxTooltipRegistrar.RequiredModInvalidOrderLocKey, modItem.WarningInfo);
				break;
			case ModWarningReason.InvalidGameVersion:
				result = this._loc.T<string>(ModManagerBoxTooltipRegistrar.InvalidGameVersionLocKey, modItem.WarningInfo);
				break;
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unknown {0}: {1}", "ModWarningReason", modItem.WarningReason));
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D68 File Offset: 0x00000F68
		public string GetModSourceText(ModItem modItem)
		{
			if (!modItem.Mod.ModDirectory.IsUserMod)
			{
				return this._loc.T<string>(ModManagerBoxTooltipRegistrar.CloudModLocKey, modItem.Mod.ModDirectory.DisplaySource);
			}
			return this._loc.T(ModManagerBoxTooltipRegistrar.LocalModLocKey);
		}

		// Token: 0x04000038 RID: 56
		public static readonly string DecreasePriorityLocKey = "Modding.DecreasePriority";

		// Token: 0x04000039 RID: 57
		public static readonly string IncreasePriorityLocKey = "Modding.IncreasePriority";

		// Token: 0x0400003A RID: 58
		public static readonly string InvalidGameVersionLocKey = "Modding.ModWarning.InvalidGameVersion";

		// Token: 0x0400003B RID: 59
		public static readonly string MissingRequiredModLocKey = "Modding.ModWarning.MissingRequiredMod";

		// Token: 0x0400003C RID: 60
		public static readonly string RequiredModInvalidOrderLocKey = "Modding.ModWarning.RequiredModInvalidOrder";

		// Token: 0x0400003D RID: 61
		public static readonly string RequiredModInvalidVersionLocKey = "Modding.ModWarning.RequiredModInvalidVersion";

		// Token: 0x0400003E RID: 62
		public static readonly string RequiredModNotEnabledLocKey = "Modding.ModWarning.RequiredModNotEnabled";

		// Token: 0x0400003F RID: 63
		public static readonly string CloudModLocKey = "Modding.CloudSource";

		// Token: 0x04000040 RID: 64
		public static readonly string LocalModLocKey = "Modding.LocalSource";

		// Token: 0x04000041 RID: 65
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000042 RID: 66
		public readonly ILoc _loc;
	}
}
