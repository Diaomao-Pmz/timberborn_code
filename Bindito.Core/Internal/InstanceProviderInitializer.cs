using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x020000A7 RID: 167
	public class InstanceProviderInitializer
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000424D File Offset: 0x0000244D
		public InstanceProviderInitializer(IInstanceProviderBank instanceProviderBank, IBinder binder)
		{
			this._instanceProviderBank = instanceProviderBank;
			this._binder = binder;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00004263 File Offset: 0x00002463
		public void InitializeAllInstanceProviders()
		{
			this.InitializeAllSingleInstanceProviders();
			this.InitializeAllMutliInstanceProviders();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00004274 File Offset: 0x00002474
		private void InitializeAllSingleInstanceProviders()
		{
			foreach (KeyValuePair<Type, Binding> keyValuePair in this._binder.Bindings)
			{
				Type key = keyValuePair.Key;
				InstanceProvider instanceProvider;
				if (!this._instanceProviderBank.TryGetInstanceProvider(key, out instanceProvider))
				{
					throw new InvalidOperationException("Failed to initialize InstanceProvider for " + TypeFormatting.Format(key) + ".");
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000042F4 File Offset: 0x000024F4
		private void InitializeAllMutliInstanceProviders()
		{
			foreach (KeyValuePair<Type, IReadOnlyList<Binding>> keyValuePair in this._binder.MultiBindings)
			{
				this._instanceProviderBank.GetInstanceProviders(keyValuePair.Key);
			}
		}

		// Token: 0x040000A7 RID: 167
		private readonly IBinder _binder;

		// Token: 0x040000A8 RID: 168
		private readonly IInstanceProviderBank _instanceProviderBank;
	}
}
