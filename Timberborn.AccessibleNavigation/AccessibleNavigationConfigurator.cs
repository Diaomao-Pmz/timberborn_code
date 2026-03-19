using System;
using Bindito.Core;
using Timberborn.Navigation;
using Timberborn.TemplateInstantiation;

namespace Timberborn.AccessibleNavigation
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class AccessibleNavigationConfigurator : Configurator
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D3 File Offset: 0x000002D3
		public override void Configure()
		{
			base.Bind<AccessibleInitializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<AccessibleNavigationConfigurator.TemplateModuleProvider>().AsSingleton();
		}

		// Token: 0x02000006 RID: 6
		public class TemplateModuleProvider : IProvider<TemplateModule>
		{
			// Token: 0x06000007 RID: 7 RVA: 0x000020FA File Offset: 0x000002FA
			public TemplateModuleProvider(AccessibleInitializer accessibleInitializer)
			{
				this._accessibleInitializer = accessibleInitializer;
			}

			// Token: 0x06000008 RID: 8 RVA: 0x00002109 File Offset: 0x00000309
			public TemplateModule Get()
			{
				TemplateModule.Builder builder = new TemplateModule.Builder();
				builder.AddDedicatedDecorator<IAccessibleNeeder, Accessible>(this._accessibleInitializer);
				return builder.Build();
			}

			// Token: 0x04000006 RID: 6
			public readonly AccessibleInitializer _accessibleInitializer;
		}
	}
}
