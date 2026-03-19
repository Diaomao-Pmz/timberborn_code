using System;
using System.Collections.Generic;
using System.Linq;

namespace Bindito.Core.Internal
{
	// Token: 0x0200008C RID: 140
	public static class EnumerableExtensions
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00003B87 File Offset: 0x00001D87
		public static IEnumerable<T> AsReadOnlyEnumerable<T>(this IEnumerable<T> source)
		{
			return from x in source
			select x;
		}
	}
}
