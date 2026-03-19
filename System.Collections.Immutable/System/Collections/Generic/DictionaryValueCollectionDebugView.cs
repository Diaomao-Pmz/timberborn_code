using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200008C RID: 140
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DictionaryValueCollectionDebugView<[Nullable(2)] TKey, [Nullable(2)] TValue>
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x0000E484 File Offset: 0x0000C684
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400009A RID: 154
		private readonly ICollection<TValue> _collection;
	}
}
