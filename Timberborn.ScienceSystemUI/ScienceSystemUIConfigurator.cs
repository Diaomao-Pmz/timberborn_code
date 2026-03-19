using System;
using Bindito.Core;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;
using Timberborn.ScienceSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ScienceSystemUI
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	[Context("MapEditor")]
	public class ScienceSystemUIConfigurator : Configurator
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000025AC File Offset: 0x000007AC
		public override void Configure()
		{
			base.Bind<NotEnoughScienceStatus>().AsTransient();
			base.Bind<ScienceNeedingBuildingDescriber>().AsTransient();
			base.Bind<UnlockableOnceDescriber>().AsTransient();
			base.Bind<ScienceCostPerHourFactory>().AsSingleton();
			base.Bind<ScienceNeedingBuildingFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<ScienceSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<IDevModule>().To<ScienceAdder>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ScienceSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002634 File Offset: 0x00000834
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ScienceNeedingBuilding, NotEnoughScienceStatus>();
			builder.AddDecorator<ScienceNeedingBuilding, ScienceNeedingBuildingDescriber>();
			builder.AddDecorator<UnlockableOnceSpec, UnlockableOnceDescriber>();
			return builder.Build();
		}

		// Token: 0x0200000C RID: 12
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000027 RID: 39 RVA: 0x0000265A File Offset: 0x0000085A
			public EntityPanelModuleProvider(ScienceNeedingBuildingFragment scienceNeedingBuildingFragment)
			{
				this._scienceNeedingBuildingFragment = scienceNeedingBuildingFragment;
			}

			// Token: 0x06000028 RID: 40 RVA: 0x00002669 File Offset: 0x00000869
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._scienceNeedingBuildingFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000026 RID: 38
			public readonly ScienceNeedingBuildingFragment _scienceNeedingBuildingFragment;
		}
	}
}
