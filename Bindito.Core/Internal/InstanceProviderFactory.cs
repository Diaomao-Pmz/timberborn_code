using System;

namespace Bindito.Core.Internal
{
	// Token: 0x020000A5 RID: 165
	public class InstanceProviderFactory : IInstanceProviderFactory
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00003FD4 File Offset: 0x000021D4
		public InstanceProviderFactory(IInstanceProviderFuncFactory instanceProviderFuncFactory, IScoper scoper)
		{
			this._instanceProviderFuncFactory = instanceProviderFuncFactory;
			this._scoper = scoper;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00003FEC File Offset: 0x000021EC
		public InstanceProvider CreateInstanceProvider(Binding binding)
		{
			Func<object> provider = this._instanceProviderFuncFactory.CreateInstanceProviderFunc(binding.ProvisionBinding);
			return new InstanceProvider(this._scoper.PlaceInScope(provider, binding.Scope), binding.Exported);
		}

		// Token: 0x0400009F RID: 159
		private readonly IInstanceProviderFuncFactory _instanceProviderFuncFactory;

		// Token: 0x040000A0 RID: 160
		private readonly IScoper _scoper;
	}
}
