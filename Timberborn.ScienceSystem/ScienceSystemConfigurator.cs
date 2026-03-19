using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ScienceSystem
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	[Context("MapEditor")]
	public class ScienceSystemConfigurator : Configurator
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000028C8 File Offset: 0x00000AC8
		public override void Configure()
		{
			base.Bind<ScienceNeedingBuilding>().AsTransient();
			base.Bind<BuildingUnlockingService>().AsSingleton();
			base.Bind<ScienceService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ScienceSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002916 File Offset: 0x00000B16
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ScienceNeedingBuildingSpec, ScienceNeedingBuilding>();
			return builder.Build();
		}
	}
}
