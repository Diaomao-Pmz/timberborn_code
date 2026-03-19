using System;
using System.Collections.Generic;

namespace Timberborn.BottomBarSystem
{
	// Token: 0x0200000A RID: 10
	public interface IBottomBarElementsProvider
	{
		// Token: 0x06000021 RID: 33
		IEnumerable<BottomBarElement> GetElements();
	}
}
