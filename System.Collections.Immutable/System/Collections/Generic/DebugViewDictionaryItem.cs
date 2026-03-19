using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000089 RID: 137
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{Value}", Name = "[{Key}]")]
	internal readonly struct DebugViewDictionaryItem<[Nullable(2)] TKey, [Nullable(2)] TValue>
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x0000E387 File Offset: 0x0000C587
		public DebugViewDictionaryItem(TKey key, TValue value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000E397 File Offset: 0x0000C597
		public DebugViewDictionaryItem([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> keyValue)
		{
			this.Key = keyValue.Key;
			this.Value = keyValue.Value;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000E3B3 File Offset: 0x0000C5B3
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public TKey Key { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000E3BB File Offset: 0x0000C5BB
		[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
		public TValue Value { get; }
	}
}
