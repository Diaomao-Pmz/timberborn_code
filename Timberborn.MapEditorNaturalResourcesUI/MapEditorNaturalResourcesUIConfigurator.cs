using System;
using Bindito.Core;
using Timberborn.ToolPanelSystem;

namespace Timberborn.MapEditorNaturalResourcesUI
{
	// Token: 0x02000007 RID: 7
	[Context("MapEditor")]
	public class MapEditorNaturalResourcesUIConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public override void Configure()
		{
			base.Bind<NaturalResourceSpawningBrushTool>().AsSingleton();
			base.Bind<NaturalResourceRemovalBrushTool>().AsSingleton();
			base.Bind<NaturalResourceLayerToggle>().AsSingleton();
			base.Bind<NaturalResourceSpawningBrushPanel>().AsSingleton();
			base.Bind<NaturalResourceBrushIterator>().AsSingleton();
			base.MultiBind<ToolPanelModule>().ToProvider<MapEditorNaturalResourcesUIConfigurator.ToolPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000008 RID: 8
		public class ToolPanelModuleProvider : IProvider<ToolPanelModule>
		{
			// Token: 0x06000009 RID: 9 RVA: 0x00002162 File Offset: 0x00000362
			public ToolPanelModuleProvider(NaturalResourceSpawningBrushPanel naturalResourceSpawningBrushPanel)
			{
				this._naturalResourceSpawningBrushPanel = naturalResourceSpawningBrushPanel;
			}

			// Token: 0x0600000A RID: 10 RVA: 0x00002171 File Offset: 0x00000371
			public ToolPanelModule Get()
			{
				ToolPanelModule.Builder builder = new ToolPanelModule.Builder();
				builder.AddFragment(this._naturalResourceSpawningBrushPanel, 50);
				return builder.Build();
			}

			// Token: 0x04000008 RID: 8
			public readonly NaturalResourceSpawningBrushPanel _naturalResourceSpawningBrushPanel;
		}
	}
}
