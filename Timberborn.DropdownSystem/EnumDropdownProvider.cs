using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;

namespace Timberborn.DropdownSystem
{
	// Token: 0x02000010 RID: 16
	public class EnumDropdownProvider<TEnum> : IExtendedDropdownProvider, IDropdownProvider where TEnum : struct
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002EE7 File Offset: 0x000010E7
		public EnumDropdownProvider(Func<TEnum> getter, Action<TEnum> setter, Func<string, string> displayTextGetter, Func<string, Sprite> iconGetter = null)
		{
			this._getter = getter;
			this._setter = setter;
			this._displayTextGetter = displayTextGetter;
			this._iconGetter = iconGetter;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002F21 File Offset: 0x00001121
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F2C File Offset: 0x0000112C
		public string GetValue()
		{
			TEnum tenum = this._getter();
			return tenum.ToString();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F54 File Offset: 0x00001154
		public void SetValue(string value)
		{
			TEnum obj;
			Enum.TryParse<TEnum>(value, out obj);
			this._setter(obj);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F76 File Offset: 0x00001176
		public string FormatDisplayText(string value, bool selected)
		{
			return this._displayTextGetter(value);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F84 File Offset: 0x00001184
		public Sprite GetIcon(string value)
		{
			Func<string, Sprite> iconGetter = this._iconGetter;
			if (iconGetter == null)
			{
				return null;
			}
			return iconGetter(value);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F98 File Offset: 0x00001198
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x04000033 RID: 51
		public readonly Func<TEnum> _getter;

		// Token: 0x04000034 RID: 52
		public readonly Action<TEnum> _setter;

		// Token: 0x04000035 RID: 53
		public readonly Func<string, string> _displayTextGetter;

		// Token: 0x04000036 RID: 54
		public readonly Func<string, Sprite> _iconGetter;

		// Token: 0x04000037 RID: 55
		public readonly string[] _values = Enum.GetNames(typeof(TEnum));
	}
}
