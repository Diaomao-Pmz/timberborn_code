using System;
using System.Collections.Generic;

namespace Timberborn.SingletonSystem
{
	// Token: 0x0200000E RID: 14
	public interface ISingletonRepository
	{
		// Token: 0x0600001C RID: 28
		IEnumerable<T> GetSingletons<T>();
	}
}
