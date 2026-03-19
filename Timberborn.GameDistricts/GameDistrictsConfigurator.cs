using System;
using Bindito.Core;
using Timberborn.Buildings;
using Timberborn.Characters;
using Timberborn.Illumination;
using Timberborn.TemplateInstantiation;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000028 RID: 40
	[Context("Game")]
	public class GameDistrictsConfigurator : Configurator
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00004610 File Offset: 0x00002810
		public override void Configure()
		{
			base.Bind<Citizen>().AsTransient();
			base.Bind<DistrictBuilding>().AsTransient();
			base.Bind<DistrictBuildingDistance>().AsTransient();
			base.Bind<DistrictBuildingIlluminator>().AsTransient();
			base.Bind<DistrictBuildingRegistry>().AsTransient();
			base.Bind<DistrictCenter>().AsTransient();
			base.Bind<DistrictCitizenLifecycleNotifier>().AsTransient();
			base.Bind<DistrictObstacle>().AsTransient();
			base.Bind<DistrictPopulation>().AsTransient();
			base.Bind<LifecycleFireController>().AsTransient();
			base.Bind<PreviewDistrictAdder>().AsTransient();
			base.Bind<DistrictBuildingAssigner>().AsSingleton();
			base.Bind<DistrictCenterRegistry>().AsSingleton();
			base.Bind<DistrictCitizenAssigner>().AsSingleton();
			base.Bind<DistrictConstructionAssigner>().AsSingleton();
			base.Bind<DistrictConnections>().AsSingleton();
			base.Bind<UnassignedCitizenRegistry>().AsSingleton();
			base.Bind<DistanceToDistrictDescriber>().AsSingleton();
			base.Bind<CitizenUnstucker>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(GameDistrictsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004720 File Offset: 0x00002920
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Character, Citizen>();
			builder.AddDecorator<DistrictCenterSpec, DistrictCenter>();
			builder.AddDecorator<DistrictCenter, DistrictPopulation>();
			builder.AddDecorator<DistrictCenter, PreviewDistrictAdder>();
			builder.AddDecorator<DistrictCenter, DistrictBuildingRegistry>();
			builder.AddDecorator<DistrictCenter, LifecycleFireController>();
			builder.AddDecorator<DistrictCenter, DistrictCitizenLifecycleNotifier>();
			builder.AddDecorator<LifecycleFireController, FireIntensityController>();
			builder.AddDecorator<BuildingAccessible, DistrictBuilding>();
			builder.AddDecorator<DistrictBuilding, DistrictBuildingDistance>();
			builder.AddDecorator<DistrictObstacleSpec, DistrictObstacle>();
			builder.AddDecorator<DistrictBuildingIlluminatorSpec, DistrictBuildingIlluminator>();
			builder.AddDecorator<DistrictBuildingIlluminator, Illuminator>();
			return builder.Build();
		}
	}
}
