using System;
using Bindito.Core;
using Timberborn.Demolishing;
using Timberborn.DemolishingUI;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MapEditorDemolishingUI
{
	// Token: 0x02000005 RID: 5
	[Context("MapEditor")]
	internal class MapEditorDemolishingUIConfigurator : Configurator
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021A4 File Offset: 0x000003A4
		protected override void Configure()
		{
			base.Bind<DemolishableScienceReward>().AsTransient();
			base.Bind<DemolishableScienceRewardDescriber>().AsTransient();
			base.Bind<DemolishableScienceRewardFragment>().AsSingleton();
			base.Bind<DemolishableScienceRewardLabelFactory>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<MapEditorDemolishingUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MapEditorDemolishingUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000220F File Offset: 0x0000040F
		private static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DemolishableScienceRewardSpec, DemolishableScienceRewardDescriber>();
			return builder.Build();
		}

		// Token: 0x02000009 RID: 9
		private class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000016 RID: 22 RVA: 0x0000231F File Offset: 0x0000051F
			public EntityPanelModuleProvider(DemolishableScienceRewardFragment demolishableScienceRewardFragment)
			{
				this._demolishableScienceRewardFragment = demolishableScienceRewardFragment;
			}

			// Token: 0x06000017 RID: 23 RVA: 0x0000232E File Offset: 0x0000052E
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._demolishableScienceRewardFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000012 RID: 18
			private readonly DemolishableScienceRewardFragment _demolishableScienceRewardFragment;
		}
	}
}
