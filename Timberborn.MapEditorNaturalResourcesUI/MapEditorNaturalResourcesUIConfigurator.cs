using System;
using Bindito.Core;
using Timberborn.ToolPanelSystem;

namespace Timberborn.MapEditorNaturalResourcesUI
{
	// Token: 0x02000006 RID: 6
	[Context("MapEditor")]
	internal class MapEditorNaturalResourcesUIConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		protected override void Configure()
		{
			base.Bind<NaturalResourceSpawningBrushTool>().AsSingleton();
			base.Bind<NaturalResourceRemovalBrushTool>().AsSingleton();
			base.Bind<NaturalResourceLayerToggle>().AsSingleton();
			base.Bind<NaturalResourceSpawningBrushPanel>().AsSingleton();
			base.Bind<NaturalResourceBrushIterator>().AsSingleton();
			base.MultiBind<ToolPanelModule>().ToProvider<MapEditorNaturalResourcesUIConfigurator.ToolPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000F RID: 15
		private class ToolPanelModuleProvider : IProvider<ToolPanelModule>
		{
			// Token: 0x06000057 RID: 87 RVA: 0x00002ED0 File Offset: 0x000010D0
			public ToolPanelModuleProvider(NaturalResourceSpawningBrushPanel naturalResourceSpawningBrushPanel)
			{
				this._naturalResourceSpawningBrushPanel = naturalResourceSpawningBrushPanel;
			}

			// Token: 0x06000058 RID: 88 RVA: 0x00002EDF File Offset: 0x000010DF
			public ToolPanelModule Get()
			{
				ToolPanelModule.Builder builder = new ToolPanelModule.Builder();
				builder.AddFragment(this._naturalResourceSpawningBrushPanel, 50);
				return builder.Build();
			}

			// Token: 0x0400004E RID: 78
			private readonly NaturalResourceSpawningBrushPanel _naturalResourceSpawningBrushPanel;
		}
	}
}
