using System;
using System.Collections.Generic;

namespace Timberborn.MapItemsUI
{
	// Token: 0x02000004 RID: 4
	public interface ICustomMapItemFactory
	{
		// Token: 0x06000003 RID: 3
		IEnumerable<MapItem> Create();
	}
}
