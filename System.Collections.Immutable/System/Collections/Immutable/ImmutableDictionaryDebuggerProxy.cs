using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000038 RID: 56
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ImmutableDictionaryDebuggerProxy<TKey, [Nullable(2)] TValue>
	{
		// Token: 0x06000221 RID: 545 RVA: 0x000068A0 File Offset: 0x00004AA0
		public ImmutableDictionaryDebuggerProxy(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			Requires.NotNull<IReadOnlyDictionary<TKey, TValue>>(dictionary, "dictionary");
			this._dictionary = dictionary;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000068BC File Offset: 0x00004ABC
		[Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})]
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public DebugViewDictionaryItem<TKey, TValue>[] Contents
		{
			[return: Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})]
			get
			{
				DebugViewDictionaryItem<TKey, TValue>[] result;
				if ((result = this._cachedContents) == null)
				{
					result = (this._cachedContents = (from kv in this._dictionary
					select new DebugViewDictionaryItem<TKey, TValue>(kv)).ToArray(this._dictionary.Count));
				}
				return result;
			}
		}

		// Token: 0x04000032 RID: 50
		private readonly IReadOnlyDictionary<TKey, TValue> _dictionary;

		// Token: 0x04000033 RID: 51
		private DebugViewDictionaryItem<TKey, TValue>[] _cachedContents;
	}
}
