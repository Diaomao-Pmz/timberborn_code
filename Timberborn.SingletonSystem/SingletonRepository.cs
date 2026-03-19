using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.SingletonSystem
{
	// Token: 0x02000017 RID: 23
	public class SingletonRepository : ISingletonRepository
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000291C File Offset: 0x00000B1C
		public SingletonRepository(SingletonListener singletonListener)
		{
			this._singletonListener = singletonListener;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000292B File Offset: 0x00000B2B
		public IEnumerable<T> GetSingletons<T>()
		{
			return this._singletonListener.Collect().OfType<T>();
		}

		// Token: 0x04000023 RID: 35
		public readonly SingletonListener _singletonListener;
	}
}
