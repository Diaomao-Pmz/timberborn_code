using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Rendering;
using Timberborn.Ruins;
using Timberborn.SimpleOutputBuildingsUI;
using Timberborn.TemplateInstantiation;
using Timberborn.YielderFinding;

namespace Timberborn.RuinsUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class RuinsUIConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000222D File Offset: 0x0000042D
		public override void Configure()
		{
			base.Bind<RuinFragment>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(RuinsUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<RuinsUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002269 File Offset: 0x00000469
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ScavengerSpec, YieldStatus>();
			builder.AddDecorator<ScavengerSpec, SimpleOutputInventoryFragmentEnabler>();
			builder.AddDecorator<Ruin, MarkerPosition>();
			builder.AddDecorator<Ruin, StartableMarkerPositionUpdater>();
			return builder.Build();
		}

		// Token: 0x02000006 RID: 6
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600000B RID: 11 RVA: 0x00002295 File Offset: 0x00000495
			public EntityPanelModuleProvider(RuinFragment ruinFragment)
			{
				this._ruinFragment = ruinFragment;
			}

			// Token: 0x0600000C RID: 12 RVA: 0x000022A4 File Offset: 0x000004A4
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._ruinFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400000D RID: 13
			public readonly RuinFragment _ruinFragment;
		}
	}
}
