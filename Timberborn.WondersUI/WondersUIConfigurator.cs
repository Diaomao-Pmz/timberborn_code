using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.Wonders;

namespace Timberborn.WondersUI
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class WondersUIConfigurator : Configurator
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000024A8 File Offset: 0x000006A8
		public override void Configure()
		{
			base.Bind<WonderDescriber>().AsTransient();
			base.Bind<WonderFragment>().AsSingleton();
			base.Bind<WonderDebugFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<WondersUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WondersUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002507 File Offset: 0x00000707
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Wonder, WonderDescriber>();
			return builder.Build();
		}

		// Token: 0x02000009 RID: 9
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000021 RID: 33 RVA: 0x00002521 File Offset: 0x00000721
			public EntityPanelModuleProvider(WonderFragment wonderFragment, WonderDebugFragment wonderDebugFragment)
			{
				this._wonderFragment = wonderFragment;
				this._wonderDebugFragment = wonderDebugFragment;
			}

			// Token: 0x06000022 RID: 34 RVA: 0x00002537 File Offset: 0x00000737
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._wonderFragment, 0);
				builder.AddDiagnosticFragment(this._wonderDebugFragment);
				return builder.Build();
			}

			// Token: 0x0400001E RID: 30
			public readonly WonderFragment _wonderFragment;

			// Token: 0x0400001F RID: 31
			public readonly WonderDebugFragment _wonderDebugFragment;
		}
	}
}
