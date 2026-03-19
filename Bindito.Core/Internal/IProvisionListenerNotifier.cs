using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x020000A9 RID: 169
	public interface IProvisionListenerNotifier
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001BA RID: 442
		IReadOnlyList<IProvisionListener> Listeners { get; }

		// Token: 0x060001BB RID: 443
		void AddListener(IProvisionListener provisionListener);

		// Token: 0x060001BC RID: 444
		void NotifyAllListeners(object providedObject);
	}
}
