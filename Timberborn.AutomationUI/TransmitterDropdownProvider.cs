using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DropdownSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using UnityEngine;

namespace Timberborn.AutomationUI
{
	// Token: 0x02000015 RID: 21
	public class TransmitterDropdownProvider : BaseComponent, IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002BCB File Offset: 0x00000DCB
		public TransmitterDropdownProvider(AutomatorRegistry automatorRegistry, ILoc loc, Func<Automator> getter, Action<Automator> setter, string noneLocKey, string selectedNoneLocKey)
		{
			this._automatorRegistry = automatorRegistry;
			this._loc = loc;
			this._getter = getter;
			this._setter = setter;
			this._noneLocKey = noneLocKey;
			this._selectedNoneLocKey = selectedNoneLocKey;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C0C File Offset: 0x00000E0C
		public IReadOnlyList<string> Items
		{
			get
			{
				this._itemCache.Clear();
				this._itemCache.Add(string.Empty);
				this._itemCache.AddRange(this._automatorRegistry.SortedTransmitterIds);
				return this._itemCache.AsReadOnlyList<string>();
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C5C File Offset: 0x00000E5C
		public string GetValue()
		{
			Automator automator = this._getter();
			if (!automator)
			{
				return "";
			}
			return automator.GetComponent<EntityComponent>().EntityId.ToString();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C9C File Offset: 0x00000E9C
		public void SetValue(string value)
		{
			this._setter(string.IsNullOrEmpty(value) ? null : this._automatorRegistry.FindTransmitterById(Guid.Parse(value)));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002CC5 File Offset: 0x00000EC5
		public string FormatDisplayText(string value, bool selected)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return this._automatorRegistry.FindTransmitterById(Guid.Parse(value)).AutomatorName;
			}
			return this._loc.T(selected ? this._selectedNoneLocKey : this._noneLocKey);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D02 File Offset: 0x00000F02
		public Sprite GetIcon(string value)
		{
			return null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D05 File Offset: 0x00000F05
		public ImmutableArray<string> GetItemClasses(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return ImmutableArray<string>.Empty;
			}
			return TransmitterDropdownProvider.NoneDropdownItemClasses;
		}

		// Token: 0x04000037 RID: 55
		public static readonly ImmutableArray<string> NoneDropdownItemClasses = ImmutableArray.Create<string>("dropdown-item--none");

		// Token: 0x04000038 RID: 56
		public readonly AutomatorRegistry _automatorRegistry;

		// Token: 0x04000039 RID: 57
		public readonly ILoc _loc;

		// Token: 0x0400003A RID: 58
		public readonly Func<Automator> _getter;

		// Token: 0x0400003B RID: 59
		public readonly Action<Automator> _setter;

		// Token: 0x0400003C RID: 60
		public readonly string _noneLocKey;

		// Token: 0x0400003D RID: 61
		public readonly string _selectedNoneLocKey;

		// Token: 0x0400003E RID: 62
		public readonly List<string> _itemCache = new List<string>();
	}
}
