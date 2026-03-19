using System;
using System.Collections.Generic;

namespace Timberborn.Common
{
	// Token: 0x0200001F RID: 31
	public static class HashSetExtensions
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000333C File Offset: 0x0000153C
		public static void CopyTo<T>(this HashSet<T> source, List<T> target)
		{
			foreach (T item in source)
			{
				target.Add(item);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000338C File Offset: 0x0000158C
		public static ReadOnlyHashSet<T> AsReadOnlyHashSet<T>(this HashSet<T> set)
		{
			return new ReadOnlyHashSet<T>(set);
		}
	}
}
