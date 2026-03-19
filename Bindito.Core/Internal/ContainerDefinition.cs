using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bindito.Core.Internal
{
	// Token: 0x0200008A RID: 138
	public class ContainerDefinition : IContainerDefinition
	{
		// Token: 0x06000157 RID: 343 RVA: 0x000038F2 File Offset: 0x00001AF2
		public ContainerDefinition(IBindingBuilderRegistry bindingBuilderRegistry, IProvisionListenerNotifier provisionListenerNotifier, IInjectionListenerNotifier injectionListenerNotifier)
		{
			this._bindingBuilderRegistry = bindingBuilderRegistry;
			this._provisionListenerNotifier = provisionListenerNotifier;
			this._injectionListenerNotifier = injectionListenerNotifier;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00003910 File Offset: 0x00001B10
		public ISingleBindingBuilder<T> Bind<T>() where T : class
		{
			BindingBuilder<T> bindingBuilder = new BindingBuilder<T>();
			this._bindingBuilderRegistry.RegisterBindingBuilder<T>(bindingBuilder);
			return bindingBuilder;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003930 File Offset: 0x00001B30
		public IMultiBindingBuilder<T> MultiBind<T>() where T : class
		{
			BindingBuilder<T> bindingBuilder = new BindingBuilder<T>();
			this._bindingBuilderRegistry.RegisterMultiBindingBuilder<T>(bindingBuilder);
			return bindingBuilder;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00003950 File Offset: 0x00001B50
		public void AddProvisionListener(IProvisionListener provisionListener)
		{
			this._provisionListenerNotifier.AddListener(provisionListener);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000395E File Offset: 0x00001B5E
		public void AddInjectionListener(IInjectionListener injectionListener)
		{
			this._injectionListenerNotifier.AddListener(injectionListener);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000396C File Offset: 0x00001B6C
		public void Install(IConfigurator configurator)
		{
			configurator.Configure(this);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003978 File Offset: 0x00001B78
		public void InstallAll(string contextName)
		{
			foreach (Type type in ContainerDefinition.FindConfigurators())
			{
				if (ContainerDefinition.HasContext(type, contextName))
				{
					this.Install((IConfigurator)Activator.CreateInstance(type));
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000039D8 File Offset: 0x00001BD8
		private static IEnumerable<Type> FindConfigurators()
		{
			Type configuratorType = typeof(IConfigurator);
			return from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly assembly) => assembly.GetTypes())
			where !type.IsAbstract && configuratorType.IsAssignableFrom(type)
			select type;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003A3C File Offset: 0x00001C3C
		private static bool HasContext(Type type, string contextName)
		{
			return type.GetCustomAttributes<ContextAttribute>().Any((ContextAttribute attribute) => attribute.ContextName == contextName);
		}

		// Token: 0x0400008F RID: 143
		private readonly IBindingBuilderRegistry _bindingBuilderRegistry;

		// Token: 0x04000090 RID: 144
		private readonly IProvisionListenerNotifier _provisionListenerNotifier;

		// Token: 0x04000091 RID: 145
		private readonly IInjectionListenerNotifier _injectionListenerNotifier;
	}
}
