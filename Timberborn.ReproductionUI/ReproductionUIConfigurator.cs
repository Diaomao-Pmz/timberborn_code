using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Reproduction;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ReproductionUI
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class ReproductionUIConfigurator : Configurator
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002618 File Offset: 0x00000818
		public override void Configure()
		{
			base.Bind<BreedingPodStatusInitializer>().AsTransient();
			base.Bind<BreedingPodDescriber>().AsTransient();
			base.Bind<BreedingPodFragment>().AsSingleton();
			base.Bind<BreedingPodBatchControlRowItemFactory>().AsSingleton();
			base.Bind<BreedingPodInventoryBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ReproductionUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<ReproductionUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000268F File Offset: 0x0000088F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BreedingPod, BreedingPodDescriber>();
			builder.AddDecorator<BreedingPod, BreedingPodStatusInitializer>();
			return builder.Build();
		}

		// Token: 0x0200000D RID: 13
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600002B RID: 43 RVA: 0x000026AF File Offset: 0x000008AF
			public EntityPanelModuleProvider(BreedingPodFragment breedingPodFragment)
			{
				this._breedingPodFragment = breedingPodFragment;
			}

			// Token: 0x0600002C RID: 44 RVA: 0x000026BE File Offset: 0x000008BE
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._breedingPodFragment, 20);
				return builder.Build();
			}

			// Token: 0x04000024 RID: 36
			public readonly BreedingPodFragment _breedingPodFragment;
		}
	}
}
