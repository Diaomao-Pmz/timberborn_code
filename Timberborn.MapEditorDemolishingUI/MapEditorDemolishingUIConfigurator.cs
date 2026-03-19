using System;
using Bindito.Core;
using Timberborn.Demolishing;
using Timberborn.DemolishingUI;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MapEditorDemolishingUI
{
	// Token: 0x02000007 RID: 7
	[Context("MapEditor")]
	public class MapEditorDemolishingUIConfigurator : Configurator
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000228C File Offset: 0x0000048C
		public override void Configure()
		{
			base.Bind<DemolishableScienceReward>().AsTransient();
			base.Bind<DemolishableScienceRewardDescriber>().AsTransient();
			base.Bind<DemolishableScienceRewardFragment>().AsSingleton();
			base.Bind<DemolishableScienceRewardLabelFactory>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<MapEditorDemolishingUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MapEditorDemolishingUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022F7 File Offset: 0x000004F7
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DemolishableScienceRewardSpec, DemolishableScienceRewardDescriber>();
			return builder.Build();
		}

		// Token: 0x02000008 RID: 8
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000016 RID: 22 RVA: 0x00002311 File Offset: 0x00000511
			public EntityPanelModuleProvider(DemolishableScienceRewardFragment demolishableScienceRewardFragment)
			{
				this._demolishableScienceRewardFragment = demolishableScienceRewardFragment;
			}

			// Token: 0x06000017 RID: 23 RVA: 0x00002320 File Offset: 0x00000520
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._demolishableScienceRewardFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000010 RID: 16
			public readonly DemolishableScienceRewardFragment _demolishableScienceRewardFragment;
		}
	}
}
