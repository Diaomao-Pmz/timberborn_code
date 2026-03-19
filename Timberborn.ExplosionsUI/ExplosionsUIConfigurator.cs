using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Explosions;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ExplosionsUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class ExplosionsUIConfigurator : Configurator
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002478 File Offset: 0x00000678
		public override void Configure()
		{
			base.Bind<DynamiteDescriber>().AsTransient();
			base.Bind<DynamiteFragment>().AsSingleton();
			base.Bind<UnstableCoreFragment>().AsSingleton();
			base.Bind<UnstableCoreDebugFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<ExplosionsUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ExplosionsUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024E3 File Offset: 0x000006E3
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Dynamite, DynamiteDescriber>();
			return builder.Build();
		}

		// Token: 0x02000008 RID: 8
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600001C RID: 28 RVA: 0x000024FD File Offset: 0x000006FD
			public EntityPanelModuleProvider(DynamiteFragment dynamiteFragment, UnstableCoreFragment unstableCoreFragment, UnstableCoreDebugFragment unstableCoreDebugFragment)
			{
				this._dynamiteFragment = dynamiteFragment;
				this._unstableCoreFragment = unstableCoreFragment;
				this._unstableCoreDebugFragment = unstableCoreDebugFragment;
			}

			// Token: 0x0600001D RID: 29 RVA: 0x0000251A File Offset: 0x0000071A
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._dynamiteFragment, 0);
				builder.AddTopFragment(this._unstableCoreFragment, 0);
				builder.AddDiagnosticFragment(this._unstableCoreDebugFragment);
				return builder.Build();
			}

			// Token: 0x0400001A RID: 26
			public readonly DynamiteFragment _dynamiteFragment;

			// Token: 0x0400001B RID: 27
			public readonly UnstableCoreFragment _unstableCoreFragment;

			// Token: 0x0400001C RID: 28
			public readonly UnstableCoreDebugFragment _unstableCoreDebugFragment;
		}
	}
}
