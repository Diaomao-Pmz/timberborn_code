using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200008B RID: 139
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DictionaryKeyCollectionDebugView<[Nullable(2)] TKey, [Nullable(2)] TValue>
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0000E43A File Offset: 0x0000C63A
		public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000E458 File Offset: 0x0000C658
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04000099 RID: 153
		private readonly ICollection<TKey> _collection;
	}
}
