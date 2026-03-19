using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x02000089 RID: 137
	public class ContainerCreator : IContainerCreator
	{
		// Token: 0x0600014F RID: 335 RVA: 0x0000372C File Offset: 0x0000192C
		public ContainerCreator() : this(null, null)
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003736 File Offset: 0x00001936
		private ContainerCreator(IInstanceProviderBank parentInstanceProviderBank, IBinder parentBinder)
		{
			this._parentInstanceProviderBank = parentInstanceProviderBank;
			this._parentBinder = parentBinder;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000374C File Offset: 0x0000194C
		public IContainer CreateContainer(IEnumerable<IConfigurator> configurators)
		{
			if (this._created)
			{
				throw new InvalidOperationException("One ContainerCreator can only be used to create one container.");
			}
			Binder binder = new Binder(this._parentBinder);
			ProvisionListenerNotifier provisionListenerNotifier = new ProvisionListenerNotifier();
			InjectionListenerNotifier injectionListenerNotifier = new InjectionListenerNotifier();
			ConstructorRetriever constructorRetriever = new ConstructorRetriever();
			MethodRetriever methodRetriever = new MethodRetriever();
			IDependencyRetriever dependencyRetriever = new DependencyRetriever(constructorRetriever, methodRetriever);
			MultiBindingService multiBindingService = new MultiBindingService();
			BindingResolver bindingResolver = new BindingResolver(multiBindingService, binder, this._parentBinder);
			BindingValidator bindingValidator = new BindingValidator(new BindingAnalyser(dependencyRetriever, bindingResolver));
			InstanceProviderBank instanceProviderBank = new InstanceProviderBank(binder, this._parentInstanceProviderBank);
			InstanceBank instanceBank = new InstanceBank(instanceProviderBank);
			ParameterProvider parameterProvider = new ParameterProvider(instanceBank, multiBindingService);
			MethodInjector methodInjector = new MethodInjector(parameterProvider, methodRetriever, injectionListenerNotifier);
			IInstanceCreator instanceCreator = new InstanceCreator(parameterProvider, constructorRetriever);
			ValidatingMethodInjector validatingMethodInjector = new ValidatingMethodInjector(methodInjector, bindingValidator);
			IInstanceProviderFuncFactory instanceProviderFuncFactory = new InstanceProviderFuncFactory(instanceCreator, methodInjector, provisionListenerNotifier, validatingMethodInjector, instanceBank);
			Scoper scoper = new Scoper();
			InstanceProviderFactory instanceProviderFactory = new InstanceProviderFactory(instanceProviderFuncFactory, scoper);
			instanceProviderBank.InstanceProviderFactory = instanceProviderFactory;
			BoundInstanceService boundInstanceService = new BoundInstanceService(binder);
			Container container = new Container(instanceBank, validatingMethodInjector, boundInstanceService, this);
			ContainerCreator.ConfigureContainer(container, configurators, binder, provisionListenerNotifier, injectionListenerNotifier);
			ContainerCreator.ValidateConfiguration(bindingValidator, binder);
			ContainerCreator.InitializeInstanceProviders(instanceProviderBank, binder);
			this.PostCreate(instanceProviderBank, binder);
			return container;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003856 File Offset: 0x00001A56
		public IContainer CreateChildContainer(IEnumerable<IConfigurator> configurators)
		{
			if (!this._created)
			{
				throw new InvalidOperationException("Can't create a child container before parent.");
			}
			return new ContainerCreator(this._ownInstanceProviderBank, this._ownBinder).CreateContainer(configurators);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00003884 File Offset: 0x00001A84
		private static void ConfigureContainer(IContainer container, IEnumerable<IConfigurator> configurators, IBinder binder, IProvisionListenerNotifier provisionListenerNotifier, IInjectionListenerNotifier injectionListenerNotifier)
		{
			BindingBuilderRegistry bindingBuilderRegistry = new BindingBuilderRegistry(binder);
			ContainerDefinition containerDefinition = new ContainerDefinition(bindingBuilderRegistry, provisionListenerNotifier, injectionListenerNotifier);
			ConfiguratorRunner configuratorRunner = new ConfiguratorRunner(containerDefinition);
			containerDefinition.Bind<IContainer>().ToInstance(container);
			configuratorRunner.RunConfigurators(configurators);
			bindingBuilderRegistry.BuildAllBindings();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000038BF File Offset: 0x00001ABF
		private static void ValidateConfiguration(IBindingValidator bindingValidator, IBinder binder)
		{
			new BinderValidator(bindingValidator, binder).Validate();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000038CD File Offset: 0x00001ACD
		private static void InitializeInstanceProviders(InstanceProviderBank instanceProviderBank, Binder binder)
		{
			new InstanceProviderInitializer(instanceProviderBank, binder).InitializeAllInstanceProviders();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000038DB File Offset: 0x00001ADB
		private void PostCreate(IInstanceProviderBank instanceProviderBank, IBinder binder)
		{
			this._ownInstanceProviderBank = instanceProviderBank;
			this._ownBinder = binder;
			this._created = true;
		}

		// Token: 0x0400008A RID: 138
		private readonly IInstanceProviderBank _parentInstanceProviderBank;

		// Token: 0x0400008B RID: 139
		private readonly IBinder _parentBinder;

		// Token: 0x0400008C RID: 140
		private IInstanceProviderBank _ownInstanceProviderBank;

		// Token: 0x0400008D RID: 141
		private IBinder _ownBinder;

		// Token: 0x0400008E RID: 142
		private bool _created;
	}
}
