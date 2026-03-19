using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x02000012 RID: 18
	public class GoodStackService<T> : IGoodStackService
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002B37 File Offset: 0x00000D37
		public ReadOnlyList<GoodStack> GoodStacks
		{
			get
			{
				return this._goodStacks.AsReadOnlyList<GoodStack>();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B44 File Offset: 0x00000D44
		public void Add(GoodStack goodStack)
		{
			this._goodStacks.Add(goodStack);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B52 File Offset: 0x00000D52
		public void Remove(GoodStack goodStack)
		{
			this._goodStacks.Remove(goodStack);
		}

		// Token: 0x04000030 RID: 48
		public readonly List<GoodStack> _goodStacks = new List<GoodStack>();
	}
}
