using System;
using Bindito.Core;
using Timberborn.Attractions;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.AttractionsUI
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	public class AttractionsUIConfigurator : Configurator
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002B7C File Offset: 0x00000D7C
		public override void Configure()
		{
			base.Bind<AttractionDescriber>().AsTransient();
			base.Bind<AttractionFragment>().AsSingleton();
			base.Bind<AttractionLoadRateFragment>().AsSingleton();
			base.Bind<AttractionBatchControlRowItemFactory>().AsSingleton();
			base.Bind<AttractionLoadRateBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(AttractionsUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<AttractionsUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BF3 File Offset: 0x00000DF3
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Attraction, AttractionDescriber>();
			return builder.Build();
		}

		// Token: 0x02000011 RID: 17
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000045 RID: 69 RVA: 0x00002C0D File Offset: 0x00000E0D
			public EntityPanelModuleProvider(AttractionFragment attractionFragment, AttractionLoadRateFragment attractionLoadRateFragment)
			{
				this._attractionFragment = attractionFragment;
				this._attractionLoadRateFragment = attractionLoadRateFragment;
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00002C23 File Offset: 0x00000E23
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._attractionLoadRateFragment, 0);
				builder.AddTopFragment(this._attractionFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400003C RID: 60
			public readonly AttractionFragment _attractionFragment;

			// Token: 0x0400003D RID: 61
			public readonly AttractionLoadRateFragment _attractionLoadRateFragment;
		}
	}
}
