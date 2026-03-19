using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.DropdownSystem;
using Timberborn.SteamWorkshop;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SteamWorkshopUI
{
	// Token: 0x0200000C RID: 12
	public class VisibilityDropdownProvider : IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002F90 File Offset: 0x00001190
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002F98 File Offset: 0x00001198
		public SteamWorkshopVisibility CurrentValue { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002FA1 File Offset: 0x000011A1
		public IReadOnlyList<string> Items
		{
			get
			{
				return VisibilityDropdownProvider.VisibilityNames;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002FA8 File Offset: 0x000011A8
		public void Initialize(Toggle updateVisibilityToggle)
		{
			Asserts.FieldIsNull<VisibilityDropdownProvider>(this, this._updateVisibilityToggle, "_updateVisibilityToggle");
			this._updateVisibilityToggle = updateVisibilityToggle;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002FC4 File Offset: 0x000011C4
		public string GetValue()
		{
			return this.CurrentValue.ToString();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002FE5 File Offset: 0x000011E5
		public void SetInitialValue(SteamWorkshopVisibility value)
		{
			this.CurrentValue = value;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002FEE File Offset: 0x000011EE
		public void SetValue(string value)
		{
			SteamWorkshopVisibility currentValue = this.CurrentValue;
			this.CurrentValue = Enum.Parse<SteamWorkshopVisibility>(value);
			if (currentValue != this.CurrentValue)
			{
				this._updateVisibilityToggle.value = true;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003016 File Offset: 0x00001216
		public string FormatDisplayText(string value, bool selected)
		{
			return "SteamWorkshop.Visibility." + value;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003023 File Offset: 0x00001223
		public Sprite GetIcon(string value)
		{
			return null;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003026 File Offset: 0x00001226
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x0400003E RID: 62
		public static readonly string[] VisibilityNames = Enum.GetNames(typeof(SteamWorkshopVisibility));

		// Token: 0x04000040 RID: 64
		public Toggle _updateVisibilityToggle;
	}
}
