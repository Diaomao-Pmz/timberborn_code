using System;

namespace Timberborn.DuplicationSystem
{
	// Token: 0x02000008 RID: 8
	public interface IDuplicable<T> : IDuplicable
	{
		// Token: 0x0600000C RID: 12
		void DuplicateFrom(T source);
	}
}
