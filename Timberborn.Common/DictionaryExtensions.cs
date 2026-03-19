using System;
using System.Collections.Generic;

namespace Timberborn.Common
{
	// Token: 0x02000015 RID: 21
	public static class DictionaryExtensions
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002C8C File Offset: 0x00000E8C
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> supplier)
		{
			TValue tvalue;
			if (dictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = supplier();
			dictionary.Add(key, tvalue);
			return tvalue;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CB6 File Offset: 0x00000EB6
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
		{
			return dictionary.GetOrAdd(key, () => Activator.CreateInstance<TValue>());
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue result;
			if (!dictionary.TryGetValue(key, out result))
			{
				return default(TValue);
			}
			return result;
		}
	}
}
