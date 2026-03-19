using System;
using Timberborn.Common;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x02000016 RID: 22
	public interface IGoodStackService
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000056 RID: 86
		ReadOnlyList<GoodStack> GoodStacks { get; }
	}
}
