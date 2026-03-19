using System;

namespace Bindito.Core
{
	// Token: 0x02000078 RID: 120
	public interface IProvider<out T>
	{
		// Token: 0x06000105 RID: 261
		T Get();
	}
}
