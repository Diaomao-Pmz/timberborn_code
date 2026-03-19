using System;
using System.Collections.Generic;
using System.Linq;

namespace Bindito.Core.Internal
{
	// Token: 0x020000A1 RID: 161
	public class InstanceBank : IInstanceBank
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00003C33 File Offset: 0x00001E33
		public InstanceBank(IInstanceProviderBank instanceProviderBank)
		{
			this._instanceProviderBank = instanceProviderBank;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00003C44 File Offset: 0x00001E44
		public bool TryGetInstance(Type type, out object instance)
		{
			InstanceProvider instanceProvider;
			if (this._instanceProviderBank.TryGetInstanceProvider(type, out instanceProvider))
			{
				instance = instanceProvider.GetInstance();
				return true;
			}
			instance = null;
			return false;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00003C70 File Offset: 0x00001E70
		public bool TryGetExportedInstance(Type type, out object instance)
		{
			InstanceProvider instanceProvider;
			if (this._instanceProviderBank.TryGetExportedInstanceProvider(type, out instanceProvider))
			{
				instance = instanceProvider.GetInstance();
				return true;
			}
			instance = null;
			return false;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00003C9B File Offset: 0x00001E9B
		public IEnumerable<object> GetInstances(Type type)
		{
			return from provider in this._instanceProviderBank.GetInstanceProviders(type)
			select provider.GetInstance();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00003CCD File Offset: 0x00001ECD
		public IEnumerable<object> GetExportedInstances(Type type)
		{
			return from provider in this._instanceProviderBank.GetExportedInstanceProviders(type)
			select provider.GetInstance();
		}

		// Token: 0x04000095 RID: 149
		private readonly IInstanceProviderBank _instanceProviderBank;
	}
}
