using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x020000B1 RID: 177
	public class ProvisionListenerNotifier : IProvisionListenerNotifier
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00004875 File Offset: 0x00002A75
		public IReadOnlyList<IProvisionListener> Listeners
		{
			get
			{
				return this._listeners.AsReadOnly();
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00004882 File Offset: 0x00002A82
		public void AddListener(IProvisionListener provisionListener)
		{
			this._listeners.Add(provisionListener);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00004890 File Offset: 0x00002A90
		public void NotifyAllListeners(object providedObject)
		{
			foreach (IProvisionListener provisionListener in this._listeners)
			{
				provisionListener.Listen(providedObject);
			}
		}

		// Token: 0x040000B7 RID: 183
		private readonly List<IProvisionListener> _listeners = new List<IProvisionListener>();
	}
}
