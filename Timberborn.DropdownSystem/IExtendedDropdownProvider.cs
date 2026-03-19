using System;
using System.Collections.Immutable;
using UnityEngine;

namespace Timberborn.DropdownSystem
{
	// Token: 0x02000014 RID: 20
	public interface IExtendedDropdownProvider : IDropdownProvider
	{
		// Token: 0x0600005E RID: 94
		string FormatDisplayText(string value, bool selected);

		// Token: 0x0600005F RID: 95
		Sprite GetIcon(string value);

		// Token: 0x06000060 RID: 96
		ImmutableArray<string> GetItemClasses(string value);
	}
}
