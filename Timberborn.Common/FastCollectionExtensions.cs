using System;
using System.Collections.Generic;

namespace Timberborn.Common
{
	// Token: 0x0200001B RID: 27
	public static class FastCollectionExtensions
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002E74 File Offset: 0x00001074
		public static int FastCount<T>(this IReadOnlyList<T> source, Predicate<T> predicate)
		{
			int num = 0;
			for (int i = 0; i < source.Count; i++)
			{
				if (predicate(source[i]))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002EA8 File Offset: 0x000010A8
		public static bool FastAll<T>(this IReadOnlyList<T> source, Predicate<T> predicate)
		{
			for (int i = 0; i < source.Count; i++)
			{
				if (!predicate(source[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002ED8 File Offset: 0x000010D8
		public static bool FastAny<T>(this IReadOnlyList<T> source, Predicate<T> predicate)
		{
			for (int i = 0; i < source.Count; i++)
			{
				if (predicate(source[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F08 File Offset: 0x00001108
		public static bool FastContains<T>(this IReadOnlyList<T> source, T element) where T : IEquatable<T>
		{
			for (int i = 0; i < source.Count; i++)
			{
				T t = source[i];
				if (t.Equals(element))
				{
					return true;
				}
			}
			return false;
		}
	}
}
