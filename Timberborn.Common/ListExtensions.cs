using System;
using System.Collections.Generic;

namespace Timberborn.Common
{
	// Token: 0x02000023 RID: 35
	public static class ListExtensions
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003394 File Offset: 0x00001594
		public static ReadOnlyList<T> AsReadOnlyList<T>(this List<T> list)
		{
			return new ReadOnlyList<T>(list);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000339C File Offset: 0x0000159C
		public static void InsertSorted<T>(this List<T> list, T item, IComparer<T> comparer, out int index)
		{
			index = list.BinarySearch(item, comparer);
			if (index < 0)
			{
				index = ~index;
			}
			list.Insert(index, item);
		}
	}
}
