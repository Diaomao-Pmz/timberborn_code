using System;
using System.Collections.Generic;

namespace Timberborn.Modding
{
	// Token: 0x0200000A RID: 10
	public interface IModsProvider
	{
		// Token: 0x06000023 RID: 35
		IEnumerable<ModDirectory> GetModDirectories();
	}
}
