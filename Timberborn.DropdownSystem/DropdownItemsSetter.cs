using System;
using System.Collections.Immutable;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.DropdownSystem
{
	// Token: 0x0200000A RID: 10
	public class DropdownItemsSetter
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000283F File Offset: 0x00000A3F
		public DropdownItemsSetter(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000285C File Offset: 0x00000A5C
		public void SetItems(Dropdown dropdown, IDropdownProvider dropdownProvider)
		{
			this.SetItems(dropdown, dropdownProvider, new Func<string, bool, DropdownElement>(this.Create));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002874 File Offset: 0x00000A74
		public void SetItems(Dropdown dropdown, IExtendedDropdownProvider dropdownProvider)
		{
			this.SetItems(dropdown, dropdownProvider, (string value, bool selected) => this.Create(dropdownProvider.FormatDisplayText(value, selected), dropdownProvider.GetIcon(value), dropdownProvider.GetItemClasses(value), null));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028B0 File Offset: 0x00000AB0
		public void SetItems(Dropdown dropdown, IExtendedTooltipDropdownProvider dropdownProvider)
		{
			this.SetItems(dropdown, dropdownProvider, (string value, bool selected) => this.Create(dropdownProvider.FormatDisplayText(value, selected), dropdownProvider.GetIcon(value), dropdownProvider.GetItemClasses(value), dropdownProvider.GetDropdownTooltip(value)));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028EC File Offset: 0x00000AEC
		public void SetLocalizableItems(Dropdown dropdown, IExtendedDropdownProvider dropdownProvider)
		{
			this.SetItems(dropdown, dropdownProvider, (string value, bool selected) => this.CreateLocalizable(dropdownProvider.FormatDisplayText(value, selected), dropdownProvider.GetIcon(value), dropdownProvider.GetItemClasses(value)));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002926 File Offset: 0x00000B26
		public void SetItems(Dropdown dropdown, IDropdownProvider dropdownProvider, Func<string, bool, DropdownElement> visualElementGetter)
		{
			dropdown.ClearItems();
			dropdown.SetItems(dropdownProvider, this._tooltipRegistrar, visualElementGetter);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000293C File Offset: 0x00000B3C
		public DropdownElement CreateLocalizable(string value, Sprite icon, ImmutableArray<string> itemClasses)
		{
			return this.Create(this._loc.T(value), icon, itemClasses, null);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002954 File Offset: 0x00000B54
		public DropdownElement Create(string text, Sprite icon, ImmutableArray<string> itemClasses, string tooltip = null)
		{
			VisualElement visualElement = this.CreateContent(text);
			foreach (string text2 in itemClasses)
			{
				visualElement.AddToClassList(text2);
			}
			Image image = UQueryExtensions.Q<Image>(visualElement, "Icon", null);
			image.sprite = icon;
			image.ToggleDisplayStyle(icon);
			return new DropdownElement(visualElement, tooltip);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029B1 File Offset: 0x00000BB1
		public DropdownElement Create(string text, bool selected)
		{
			return new DropdownElement(this.CreateContent(text), null);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029C0 File Offset: 0x00000BC0
		public VisualElement CreateContent(string text)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Core/DropdownItem");
			UQueryExtensions.Q<Label>(visualElement, "Text", null).text = text;
			return visualElement;
		}

		// Token: 0x04000020 RID: 32
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000021 RID: 33
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000022 RID: 34
		public readonly ILoc _loc;
	}
}
