using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Stockpiles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MapEditorStockpilesUI
{
	// Token: 0x0200000A RID: 10
	[Context("MapEditor")]
	public class MapEditorStockpilesUIConfigurator : Configurator
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002924 File Offset: 0x00000B24
		public override void Configure()
		{
			base.Bind<FixedStockpileDropdownProvider>().AsTransient();
			base.Bind<FixedStockpileInventorySetter>().AsTransient();
			base.Bind<FixedStockpileFragment>().AsSingleton();
			base.Bind<FixedStockpileGoodProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MapEditorStockpilesUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<MapEditorStockpilesUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000298F File Offset: 0x00000B8F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Stockpile, FixedStockpileDropdownProvider>();
			builder.AddDecorator<Stockpile, FixedStockpileInventorySetter>();
			return builder.Build();
		}

		// Token: 0x0200000B RID: 11
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000035 RID: 53 RVA: 0x000029AF File Offset: 0x00000BAF
			public EntityPanelModuleProvider(FixedStockpileFragment fixedStockpileFragment)
			{
				this._fixedStockpileFragment = fixedStockpileFragment;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x000029BE File Offset: 0x00000BBE
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddBottomFragment(this._fixedStockpileFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000028 RID: 40
			public readonly FixedStockpileFragment _fixedStockpileFragment;
		}
	}
}
