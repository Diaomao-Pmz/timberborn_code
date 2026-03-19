using System;
using Bindito.Core;
using Timberborn.Characters;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;

namespace Timberborn.InventorySystem
{
	// Token: 0x0200001C RID: 28
	[Context("Game")]
	[Context("MapEditor")]
	public class InventorySystemConfigurator : Configurator
	{
		// Token: 0x060000DE RID: 222 RVA: 0x0000472C File Offset: 0x0000292C
		public override void Configure()
		{
			base.Bind<SingleGoodAllower>().AsTransient();
			base.Bind<Inventories>().AsTransient();
			base.Bind<DistrictInventoryAssigner>().AsTransient();
			base.Bind<DistrictInventoryPicker>().AsTransient();
			base.Bind<DistrictInventoryRegistry>().AsTransient();
			base.Bind<GoodReserver>().AsTransient();
			base.Bind<Inventory>().AsTransient();
			base.Bind<InventoryFillCalculator>().AsSingleton();
			base.Bind<GoodReservationValueSerializer>().AsSingleton();
			base.Bind<InventoryInitializerFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(InventorySystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000047CE File Offset: 0x000029CE
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Character, GoodReserver>();
			builder.AddDecorator<DistrictCenter, DistrictInventoryRegistry>();
			builder.AddDecorator<DistrictCenter, DistrictInventoryPicker>();
			builder.AddDecorator<DistrictBuilding, DistrictInventoryAssigner>();
			builder.AddDecorator<Inventory, Inventories>();
			return builder.Build();
		}
	}
}
