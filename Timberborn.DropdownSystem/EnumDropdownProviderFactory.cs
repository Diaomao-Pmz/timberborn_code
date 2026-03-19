using System;
using Timberborn.Localization;
using UnityEngine;

namespace Timberborn.DropdownSystem
{
	// Token: 0x02000011 RID: 17
	public class EnumDropdownProviderFactory
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002F9F File Offset: 0x0000119F
		public EnumDropdownProviderFactory(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002FAE File Offset: 0x000011AE
		public EnumDropdownProvider<TEnum> Create<TEnum>(Func<TEnum> getter, Action<TEnum> setter, Func<string, string> displayText) where TEnum : struct
		{
			return new EnumDropdownProvider<TEnum>(getter, setter, displayText, null);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002FB9 File Offset: 0x000011B9
		public EnumDropdownProvider<TEnum> CreateWithIcon<TEnum>(Func<TEnum> getter, Action<TEnum> setter, Func<string, string> displayText, Func<string, Sprite> iconGetter) where TEnum : struct
		{
			return new EnumDropdownProvider<TEnum>(getter, setter, displayText, iconGetter);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FC8 File Offset: 0x000011C8
		public EnumDropdownProvider<TEnum> CreateLocalized<TEnum>(Func<TEnum> getter, Action<TEnum> setter, string valueLocKeyPrefix) where TEnum : struct
		{
			return new EnumDropdownProvider<TEnum>(getter, setter, (string value) => this.GetLocalizedValue(valueLocKeyPrefix, value), null);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FFD File Offset: 0x000011FD
		public string GetLocalizedValue(string prefix, string value)
		{
			return this._loc.T(prefix + value);
		}

		// Token: 0x04000038 RID: 56
		public readonly ILoc _loc;
	}
}
