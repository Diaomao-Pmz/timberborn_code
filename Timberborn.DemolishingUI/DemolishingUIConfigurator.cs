using System;
using Bindito.Core;
using Timberborn.Demolishing;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.DemolishingUI
{
	// Token: 0x0200000F RID: 15
	[Context("Game")]
	public class DemolishingUIConfigurator : Configurator
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002F84 File Offset: 0x00001184
		public override void Configure()
		{
			base.Bind<DemolitionBlockedStatus>().AsTransient();
			base.Bind<DemolishableFragment>().AsSingleton();
			base.Bind<DemolishableSelectionTool>().AsSingleton();
			base.Bind<DemolishableUnselectionTool>().AsSingleton();
			base.Bind<DemolishableMarkerService>().AsSingleton();
			base.Bind<DemolishableScienceRewardLabelFactory>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<DemolishingUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(DemolishingUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003007 File Offset: 0x00001207
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Demolishable, DemolitionBlockedStatus>();
			return builder.Build();
		}

		// Token: 0x02000010 RID: 16
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000057 RID: 87 RVA: 0x00003021 File Offset: 0x00001221
			public EntityPanelModuleProvider(DemolishableFragment demolishableFragment)
			{
				this._demolishableFragment = demolishableFragment;
			}

			// Token: 0x06000058 RID: 88 RVA: 0x00003030 File Offset: 0x00001230
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._demolishableFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000043 RID: 67
			public readonly DemolishableFragment _demolishableFragment;
		}
	}
}
