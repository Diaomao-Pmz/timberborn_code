using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Stockpiles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MapEditorStockpilesUI
{
	// Token: 0x02000007 RID: 7
	[Context("MapEditor")]
	internal class MapEditorStockpilesUIConfigurator : Configurator
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000028E0 File Offset: 0x00000AE0
		protected override void Configure()
		{
			base.Bind<FixedStockpileDropdownProvider>().AsTransient();
			base.Bind<FixedStockpileInventorySetter>().AsTransient();
			base.Bind<FixedStockpileFragment>().AsSingleton();
			base.Bind<FixedStockpileGoodProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MapEditorStockpilesUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<MapEditorStockpilesUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000294B File Offset: 0x00000B4B
		private static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Stockpile, FixedStockpileDropdownProvider>();
			builder.AddDecorator<Stockpile, FixedStockpileInventorySetter>();
			return builder.Build();
		}

		// Token: 0x0200000C RID: 12
		private class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000035 RID: 53 RVA: 0x000029BD File Offset: 0x00000BBD
			public EntityPanelModuleProvider(FixedStockpileFragment fixedStockpileFragment)
			{
				this._fixedStockpileFragment = fixedStockpileFragment;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x000029CC File Offset: 0x00000BCC
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddBottomFragment(this._fixedStockpileFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400002A RID: 42
			private readonly FixedStockpileFragment _fixedStockpileFragment;
		}
	}
}
