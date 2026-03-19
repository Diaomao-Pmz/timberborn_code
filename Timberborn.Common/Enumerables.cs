using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.Common
{
	// Token: 0x02000017 RID: 23
	public static class Enumerables
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002D16 File Offset: 0x00000F16
		public static IEnumerable<T> One<T>(T item)
		{
			return Enumerable.Repeat<T>(item, 1);
		}
	}
}
