using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x020000A0 RID: 160
	public class InjectionListenerNotifier : IInjectionListenerNotifier
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00003BAE File Offset: 0x00001DAE
		public IReadOnlyList<IInjectionListener> Listeners
		{
			get
			{
				return this._listeners.AsReadOnly();
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003BBB File Offset: 0x00001DBB
		public void AddListener(IInjectionListener injectionListener)
		{
			this._listeners.Add(injectionListener);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003BCC File Offset: 0x00001DCC
		public void NotifyAllListeners(object injectee)
		{
			foreach (IInjectionListener injectionListener in this._listeners)
			{
				injectionListener.Listen(injectee);
			}
		}

		// Token: 0x04000094 RID: 148
		private readonly List<IInjectionListener> _listeners = new List<IInjectionListener>();
	}
}
