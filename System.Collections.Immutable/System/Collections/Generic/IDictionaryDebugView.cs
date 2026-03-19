using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200008A RID: 138
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class IDictionaryDebugView<TKey, [Nullable(2)] TValue>
	{
		// Token: 0x0600058A RID: 1418 RVA: 0x0000E3C3 File Offset: 0x0000C5C3
		public IDictionaryDebugView(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0000E3E4 File Offset: 0x0000C5E4
		[Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})]
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public DebugViewDictionaryItem<TKey, TValue>[] Items
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
				KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this._dict.Count];
				this._dict.CopyTo(array, 0);
				DebugViewDictionaryItem<TKey, TValue>[] array2 = new DebugViewDictionaryItem<TKey, TValue>[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = new DebugViewDictionaryItem<TKey, TValue>(array[i]);
				}
				return array2;
			}
		}

		// Token: 0x04000098 RID: 152
		private readonly IDictionary<TKey, TValue> _dict;
	}
}
