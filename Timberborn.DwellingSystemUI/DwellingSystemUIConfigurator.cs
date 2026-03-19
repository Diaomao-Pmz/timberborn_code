using System;
using Bindito.Core;
using Timberborn.DwellingSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.DwellingSystemUI
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	public class DwellingSystemUIConfigurator : Configurator
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000278C File Offset: 0x0000098C
		public override void Configure()
		{
			base.Bind<DwellingDescriber>().AsTransient();
			base.Bind<DwellerViewFactory>().AsSingleton();
			base.Bind<DwellingUserFragment>().AsSingleton();
			base.Bind<DwellingDebugFragment>().AsSingleton();
			base.Bind<DwellingBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(DwellingSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<DwellingSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002803 File Offset: 0x00000A03
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Dwelling, DwellingDescriber>();
			return builder.Build();
		}

		// Token: 0x0200000E RID: 14
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600002E RID: 46 RVA: 0x0000281D File Offset: 0x00000A1D
			public EntityPanelModuleProvider(DwellingUserFragment dwellingUserFragment, DwellingDebugFragment dwellingDebugFragment)
			{
				this._dwellingUserFragment = dwellingUserFragment;
				this._dwellingDebugFragment = dwellingDebugFragment;
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002833 File Offset: 0x00000A33
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._dwellingUserFragment, 0);
				builder.AddDiagnosticFragment(this._dwellingDebugFragment);
				return builder.Build();
			}

			// Token: 0x0400002A RID: 42
			public readonly DwellingUserFragment _dwellingUserFragment;

			// Token: 0x0400002B RID: 43
			public readonly DwellingDebugFragment _dwellingDebugFragment;
		}
	}
}
