using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000087 RID: 135
	internal static class KeyValuePairExtensions
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x0000E36B File Offset: 0x0000C56B
		[NullableContext(1)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void Deconstruct<[Nullable(2)] TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			0,
			1,
			1
		})] this KeyValuePair<TKey, TValue> source, out TKey key, out TValue value)
		{
			key = source.Key;
			value = source.Value;
		}
	}
}
