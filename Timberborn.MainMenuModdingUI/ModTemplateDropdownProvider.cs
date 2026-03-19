using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.DropdownSystem;

namespace Timberborn.MainMenuModdingUI
{
	// Token: 0x0200000D RID: 13
	public class ModTemplateDropdownProvider : IDropdownProvider
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002E3A File Offset: 0x0000103A
		public IReadOnlyList<string> Items { get; } = (from template in ModTemplateDropdownProvider.ModTemplates
		select template.Name).ToList<string>();

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002E42 File Offset: 0x00001042
		public bool LocalizationTemplateChosen
		{
			get
			{
				return this._selectedIndex == 1;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002E4D File Offset: 0x0000104D
		public string GetValue()
		{
			return this.Items[this._selectedIndex];
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002E60 File Offset: 0x00001060
		public void SetValue(string value)
		{
			int num = this.Items.IndexOf(value);
			if (num >= 0)
			{
				this._selectedIndex = num;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E88 File Offset: 0x00001088
		public string GetDirectory()
		{
			return ModTemplateDropdownProvider.ModTemplates[this._selectedIndex].Directory;
		}

		// Token: 0x04000045 RID: 69
		public static readonly List<ModTemplateDropdownProvider.ModTemplate> ModTemplates = new List<ModTemplateDropdownProvider.ModTemplate>
		{
			new ModTemplateDropdownProvider.ModTemplate("Example building", "BerryJam"),
			new ModTemplateDropdownProvider.ModTemplate("Translation", "Empty"),
			new ModTemplateDropdownProvider.ModTemplate("Tails and banners", "TailsAndBanners"),
			new ModTemplateDropdownProvider.ModTemplate("Empty", "Empty")
		};

		// Token: 0x04000047 RID: 71
		public int _selectedIndex;

		// Token: 0x0200000E RID: 14
		public readonly struct ModTemplate
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000044 RID: 68 RVA: 0x00002F57 File Offset: 0x00001157
			public string Name { get; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000045 RID: 69 RVA: 0x00002F5F File Offset: 0x0000115F
			public string Directory { get; }

			// Token: 0x06000046 RID: 70 RVA: 0x00002F67 File Offset: 0x00001167
			public ModTemplate(string name, string directory)
			{
				this.Name = name;
				this.Directory = directory;
			}
		}
	}
}
