using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000040 RID: 64
	public interface IOrderable<in T>
	{
		// Token: 0x0600015A RID: 346
		bool IsLessThan(T other);
	}
}
