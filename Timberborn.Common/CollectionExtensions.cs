using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timberborn.Common
{
	// Token: 0x0200000E RID: 14
	public static class CollectionExtensions
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002842 File Offset: 0x00000A42
		public static bool IsEmpty<T>(this T[] collection)
		{
			return collection.Length == 0;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002849 File Offset: 0x00000A49
		public static bool IsNullOrEmpty<T>(this T[] collection)
		{
			return collection == null || collection.Length == 0;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002858 File Offset: 0x00000A58
		public static void Fill<T>(this T[] collection, Func<T> valueGetter)
		{
			for (int i = 0; i < collection.Length; i++)
			{
				collection[i] = valueGetter();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002880 File Offset: 0x00000A80
		public static bool IsEmpty<T>(this IReadOnlyCollection<T> collection)
		{
			return collection.Count == 0;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000288B File Offset: 0x00000A8B
		public static IEnumerable<T> AsReadOnlyEnumerable<T>(this IEnumerable<T> source)
		{
			return from x in source
			select x;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028B4 File Offset: 0x00000AB4
		public static bool AllAreEqual<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
		{
			comparer = (comparer ?? EqualityComparer<T>.Default);
			bool result;
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					T y = enumerator.Current;
					while (enumerator.MoveNext())
					{
						if (!comparer.Equals(enumerator.Current, y))
						{
							return false;
						}
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002920 File Offset: 0x00000B20
		public static string CollectionToString<T>(this IEnumerable<T> source, string collectionName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(collectionName + ":");
			foreach (T t in source)
			{
				stringBuilder.AppendLine(string.Format("  {0}", t));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002998 File Offset: 0x00000B98
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> values)
		{
			List<T> list = collection as List<T>;
			if (list != null)
			{
				list.AddRange(values);
				return;
			}
			foreach (T item in values)
			{
				collection.Add(item);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029F4 File Offset: 0x00000BF4
		public static void AddRange<T>(this ICollection<T> collection, IReadOnlyList<T> values)
		{
			List<T> list = collection as List<T>;
			if (list != null)
			{
				list.AddRange(values);
				return;
			}
			for (int i = 0; i < values.Count; i++)
			{
				collection.Add(values[i]);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A31 File Offset: 0x00000C31
		public static T? MinOrNullable<T>(this IEnumerable<T> source) where T : struct, IComparable<T>
		{
			return (from element in source
			select new T?(element)).DefaultIfEmpty<T?>().Min<T?>();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A62 File Offset: 0x00000C62
		public static void RemoveLast(this IList list)
		{
			list.RemoveAt(list.Count - 1);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A74 File Offset: 0x00000C74
		public static int IndexOf<T>(this IReadOnlyList<T> source, T obj)
		{
			for (int i = 0; i < source.Count; i++)
			{
				T t = source[i];
				if (t.Equals(obj))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public static int IndexOf<T>(this ICollection<T> source, T obj)
		{
			int num = 0;
			foreach (T t in source)
			{
				if (t.Equals(obj))
				{
					return num;
				}
				num++;
			}
			return -1;
		}
	}
}
