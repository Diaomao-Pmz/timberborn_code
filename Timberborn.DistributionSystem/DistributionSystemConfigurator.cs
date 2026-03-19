using System;
using Bindito.Core;
using Timberborn.GameDistricts;
using Timberborn.Hauling;
using Timberborn.InventoryNeedSystem;
using Timberborn.InventorySystem;
using Timberborn.LaborSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.DistributionSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class DistributionSystemConfigurator : Configurator
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000026E0 File Offset: 0x000008E0
		public override void Configure()
		{
			base.Bind<DistrictCrossingWorkplaceBehavior>().AsTransient();
			base.Bind<DistrictCrossingAutoExporter>().AsTransient();
			base.Bind<DistributionInventoryRegistry>().AsTransient();
			base.Bind<DistrictCrossing>().AsTransient();
			base.Bind<DistrictCrossingInventory>().AsTransient();
			base.Bind<DistrictCrossingValidator>().AsTransient();
			base.Bind<DistrictDistributableGoodProvider>().AsTransient();
			base.Bind<DistrictDistributionSetting>().AsTransient();
			base.Bind<DistrictCrossingInventoryInitializer>().AsSingleton();
			base.Bind<GoodDistributionSettingSerializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<DistributionSystemConfigurator.TemplateModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000A RID: 10
		public class TemplateModuleProvider : IProvider<TemplateModule>
		{
			// Token: 0x06000028 RID: 40 RVA: 0x0000277E File Offset: 0x0000097E
			public TemplateModuleProvider(DistrictCrossingInventoryInitializer districtCrossingInventoryInitializer)
			{
				this._districtCrossingInventoryInitializer = districtCrossingInventoryInitializer;
			}

			// Token: 0x06000029 RID: 41 RVA: 0x00002790 File Offset: 0x00000990
			public TemplateModule Get()
			{
				TemplateModule.Builder builder = new TemplateModule.Builder();
				builder.AddDedicatedDecorator<DistrictCrossingInventory, Inventory>(this._districtCrossingInventoryInitializer);
				builder.AddDecorator<DistrictCrossingSpec, DistrictCrossing>();
				builder.AddDecorator<DistrictCrossing, DistrictCrossingInventory>();
				builder.AddDecorator<DistrictCrossing, DistrictCrossingValidator>();
				builder.AddDecorator<DistrictCrossing, DistrictCrossingWorkplaceBehavior>();
				builder.AddDecorator<DistrictCrossing, DistrictCrossingAutoExporter>();
				builder.AddDecorator<DistrictCrossing, LaborWorkplaceBehavior>();
				builder.AddDecorator<DistrictCrossing, WaitInsideIdlyWorkplaceBehavior>();
				builder.AddDecorator<DistrictCrossing, WorkplaceWithBackpacks>();
				builder.AddDecorator<DistrictCrossing, InventoryNeedBehavior>();
				builder.AddDecorator<DistrictCenter, DistrictDistributableGoodProvider>();
				builder.AddDecorator<DistrictCenter, DistrictDistributionSetting>();
				builder.AddDecorator<DistrictDistributableGoodProvider, DistributionInventoryRegistry>();
				return builder.Build();
			}

			// Token: 0x04000011 RID: 17
			public readonly DistrictCrossingInventoryInitializer _districtCrossingInventoryInitializer;
		}
	}
}
