using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x020000A6 RID: 166
	public class InstanceProviderFuncFactory : IInstanceProviderFuncFactory
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x00004028 File Offset: 0x00002228
		public InstanceProviderFuncFactory(IInstanceCreator instanceCreator, IMethodInjector methodInjector, IProvisionListenerNotifier provisionListenerNotifier, IValidatingMethodInjector validatingMethodInjector, IInstanceBank instanceBank)
		{
			this._instanceCreator = instanceCreator;
			this._methodInjector = methodInjector;
			this._provisionListenerNotifier = provisionListenerNotifier;
			this._validatingMethodInjector = validatingMethodInjector;
			this._instanceBank = instanceBank;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00004060 File Offset: 0x00002260
		public Func<object> CreateInstanceProviderFunc(ProvisionBinding provisionBinding)
		{
			if (provisionBinding.Instance != null)
			{
				return () => this.ProvideBoundInstance(provisionBinding.Instance);
			}
			if (provisionBinding.ProviderType != null)
			{
				return () => this.ProvideInstanceFromProviderType(provisionBinding.ProviderType);
			}
			if (provisionBinding.ProviderInstance != null)
			{
				return () => this.ProvideInstanceFromProviderInstance(provisionBinding.ProviderInstance);
			}
			if (provisionBinding.ProvidingMethod != null)
			{
				return () => this.ProvideInstanceFromProvidingMethod(provisionBinding.ProvidingMethod);
			}
			if (provisionBinding.Type != null)
			{
				return () => this.ProvideCreatedInstance(provisionBinding.Type);
			}
			if (provisionBinding.ExistingType != null)
			{
				return () => this.ProvideInstanceFromExistingBinding(provisionBinding.ExistingType);
			}
			throw new InvalidOperationException(string.Format("{0} has an unknown binding. This is an internal Bindito error!", provisionBinding));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00004144 File Offset: 0x00002344
		private object ProvideBoundInstance(object instance)
		{
			return this.PostInitializeValidatedInstance(instance);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00004150 File Offset: 0x00002350
		private object ProvideInstanceFromProviderType(Type providerType)
		{
			IProvider<object> provider = this.GetProvider(providerType);
			return this.ProvideInstanceFromProviderInstance(provider);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000416C File Offset: 0x0000236C
		private IProvider<object> GetProvider(Type providerType)
		{
			IProvider<object> provider;
			if (!this._providers.TryGetValue(providerType, out provider))
			{
				provider = (this.ProvideCreatedInstance(providerType) as IProvider<object>);
				this._providers[providerType] = provider;
			}
			return provider;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000041A4 File Offset: 0x000023A4
		private object ProvideInstanceFromProviderInstance(IProvider<object> provider)
		{
			return this.ProvideInstanceFromProvidingMethod(new Func<object>(provider.Get));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000041B9 File Offset: 0x000023B9
		private object ProvideInstanceFromProvidingMethod(Func<object> providingMethod)
		{
			return this.PostInitializeUnvalidatedInstance(providingMethod());
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000041C7 File Offset: 0x000023C7
		private object ProvideCreatedInstance(Type type)
		{
			return this.PostInitializeValidatedInstance(this._instanceCreator.CreateInstance(type));
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000041DB File Offset: 0x000023DB
		private object PostInitializeValidatedInstance(object instance)
		{
			this._methodInjector.Inject(instance);
			return this.PostInitializeInstance(instance);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000041F0 File Offset: 0x000023F0
		private object PostInitializeUnvalidatedInstance(object instance)
		{
			this._validatingMethodInjector.Inject(instance);
			return this.PostInitializeInstance(instance);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004205 File Offset: 0x00002405
		private object PostInitializeInstance(object instance)
		{
			this._provisionListenerNotifier.NotifyAllListeners(instance);
			return instance;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004214 File Offset: 0x00002414
		private object ProvideInstanceFromExistingBinding(Type existingType)
		{
			object result;
			if (this._instanceBank.TryGetInstance(existingType, out result))
			{
				return result;
			}
			throw new InvalidOperationException("Couldn't get an instance of " + TypeFormatting.Format(existingType) + " from an existing binding. This is an internal Bindito error!");
		}

		// Token: 0x040000A1 RID: 161
		private readonly IInstanceCreator _instanceCreator;

		// Token: 0x040000A2 RID: 162
		private readonly IMethodInjector _methodInjector;

		// Token: 0x040000A3 RID: 163
		private readonly IProvisionListenerNotifier _provisionListenerNotifier;

		// Token: 0x040000A4 RID: 164
		private readonly IValidatingMethodInjector _validatingMethodInjector;

		// Token: 0x040000A5 RID: 165
		private readonly IInstanceBank _instanceBank;

		// Token: 0x040000A6 RID: 166
		private readonly Dictionary<Type, IProvider<object>> _providers = new Dictionary<Type, IProvider<object>>();
	}
}
