using System;

namespace Timberborn.DropdownSystem
{
	// Token: 0x02000015 RID: 21
	public interface IExtendedTooltipDropdownProvider : IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x06000061 RID: 97
		string GetDropdownTooltip(string value);
	}
}
