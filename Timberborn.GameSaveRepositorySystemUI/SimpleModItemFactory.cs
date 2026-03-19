using System;
using Timberborn.CoreUI;
using Timberborn.Modding;
using Timberborn.SaveMetadataSystem;
using Timberborn.TooltipSystem;
using Timberborn.Versioning;
using UnityEngine.UIElements;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000015 RID: 21
	public class SimpleModItemFactory
	{
		// Token: 0x0600006F RID: 111 RVA: 0x000034AA File Offset: 0x000016AA
		public SimpleModItemFactory(VisualElementLoader visualElementLoader, ModRepository modRepository, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._modRepository = modRepository;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000034C8 File Offset: 0x000016C8
		public void FillActiveMods(VisualElement container)
		{
			foreach (Mod mod in this._modRepository.EnabledMods)
			{
				VisualElement visualElement = this.CreateModItem(mod.Manifest.Name, mod.Manifest.Version.Formatted);
				container.Add(visualElement);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003540 File Offset: 0x00001740
		public void FillSavedMods(VisualElement container, SaveMetadata metadata)
		{
			foreach (ModReference modReference in metadata.Mods)
			{
				VisualElement visualElement = this.CreateModItem(modReference.Name, Version.Create(modReference.Version).Formatted);
				if (this._modRepository.ModIsNotEnabled(modReference.Id))
				{
					this.SetErrorIcon(visualElement);
				}
				else if (this._modRepository.ModIsOnDifferentVersion(modReference.Id, modReference.Version))
				{
					this.SetWarningIcon(visualElement);
				}
				container.Add(visualElement);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035D3 File Offset: 0x000017D3
		public VisualElement CreateModItem(string modName, string modVersion)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Modding/SimpleModItem");
			UQueryExtensions.Q<Label>(visualElement, "ModName", null).text = modName;
			UQueryExtensions.Q<Label>(visualElement, "ModVersion", null).text = modVersion;
			return visualElement;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000360C File Offset: 0x0000180C
		public void SetErrorIcon(VisualElement modItem)
		{
			VisualElement incompatibilityIcon = SimpleModItemFactory.GetIncompatibilityIcon(modItem);
			incompatibilityIcon.AddToClassList(SimpleModItemFactory.ErrorIconClass);
			this._tooltipRegistrar.RegisterLocalizable(incompatibilityIcon, SimpleModItemFactory.MissingModLocKey);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000363C File Offset: 0x0000183C
		public void SetWarningIcon(VisualElement modItem)
		{
			VisualElement incompatibilityIcon = SimpleModItemFactory.GetIncompatibilityIcon(modItem);
			incompatibilityIcon.AddToClassList(SimpleModItemFactory.WarningIconClass);
			this._tooltipRegistrar.RegisterLocalizable(incompatibilityIcon, SimpleModItemFactory.VersionMismatchLocKey);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000366C File Offset: 0x0000186C
		public static VisualElement GetIncompatibilityIcon(VisualElement modItem)
		{
			return UQueryExtensions.Q<VisualElement>(modItem, "IncompatibilityIcon", null);
		}

		// Token: 0x0400005E RID: 94
		public static readonly string WarningIconClass = "warning-icon";

		// Token: 0x0400005F RID: 95
		public static readonly string ErrorIconClass = "error-icon";

		// Token: 0x04000060 RID: 96
		public static readonly string MissingModLocKey = "Modding.MissingMod";

		// Token: 0x04000061 RID: 97
		public static readonly string VersionMismatchLocKey = "Modding.VersionMismatch";

		// Token: 0x04000062 RID: 98
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000063 RID: 99
		public readonly ModRepository _modRepository;

		// Token: 0x04000064 RID: 100
		public readonly ITooltipRegistrar _tooltipRegistrar;
	}
}
