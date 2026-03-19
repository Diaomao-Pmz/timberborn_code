using System;

namespace Bindito.Core
{
	// Token: 0x0200007A RID: 122
	public interface IScopeAssignee
	{
		// Token: 0x06000107 RID: 263
		IExportAssignee AsSingleton();

		// Token: 0x06000108 RID: 264
		IExportAssignee AsTransient();
	}
}
