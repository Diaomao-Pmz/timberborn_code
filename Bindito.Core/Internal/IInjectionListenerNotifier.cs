using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x02000097 RID: 151
	public interface IInjectionListenerNotifier
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000178 RID: 376
		IReadOnlyList<IInjectionListener> Listeners { get; }

		// Token: 0x06000179 RID: 377
		void AddListener(IInjectionListener injectionListener);

		// Token: 0x0600017A RID: 378
		void NotifyAllListeners(object injectee);
	}
}
