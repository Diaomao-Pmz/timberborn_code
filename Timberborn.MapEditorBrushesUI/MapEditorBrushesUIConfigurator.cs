using System;
using Bindito.Core;
using Timberborn.ToolPanelSystem;

namespace Timberborn.MapEditorBrushesUI
{
	// Token: 0x0200000B RID: 11
	[Context("MapEditor")]
	public class MapEditorBrushesUIConfigurator : Configurator
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002D74 File Offset: 0x00000F74
		public override void Configure()
		{
			base.Bind<AbsoluteTerrainHeightBrushTool>().AsSingleton();
			base.Bind<RelativeTerrainHeightBrushTool>().AsSingleton();
			base.Bind<SculptingTerrainBrushTool>().AsSingleton();
			base.Bind<TerrainIntegrityService>().AsSingleton();
			base.Bind<TerrainIntegrityWarningPanel>().AsSingleton();
			base.MultiBind<ToolPanelModule>().ToProvider<MapEditorBrushesUIConfigurator.ToolPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000C RID: 12
		public class ToolPanelModuleProvider : IProvider<ToolPanelModule>
		{
			// Token: 0x06000046 RID: 70 RVA: 0x00002DD6 File Offset: 0x00000FD6
			public ToolPanelModuleProvider(TerrainIntegrityWarningPanel terrainIntegrityWarningPanel)
			{
				this._terrainIntegrityWarningPanel = terrainIntegrityWarningPanel;
			}

			// Token: 0x06000047 RID: 71 RVA: 0x00002DE5 File Offset: 0x00000FE5
			public ToolPanelModule Get()
			{
				ToolPanelModule.Builder builder = new ToolPanelModule.Builder();
				builder.AddFragment(this._terrainIntegrityWarningPanel, 100);
				return builder.Build();
			}

			// Token: 0x04000034 RID: 52
			public readonly TerrainIntegrityWarningPanel _terrainIntegrityWarningPanel;
		}
	}
}
