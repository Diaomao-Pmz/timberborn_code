using System;
using Bindito.Core;
using Timberborn.ToolPanelSystem;

namespace Timberborn.MapEditorPlacementRandomizingUI
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	internal class MapEditorPlacementRandomizingUIConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021BA File Offset: 0x000003BA
		protected override void Configure()
		{
			base.Bind<BlockObjectPlacementRandomizingPanel>().AsSingleton();
			base.MultiBind<ToolPanelModule>().ToProvider<MapEditorPlacementRandomizingUIConfigurator.ToolPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000007 RID: 7
		private class ToolPanelModuleProvider : IProvider<ToolPanelModule>
		{
			// Token: 0x0600000A RID: 10 RVA: 0x000021E1 File Offset: 0x000003E1
			public ToolPanelModuleProvider(BlockObjectPlacementRandomizingPanel blockObjectPlacementRandomizingPanel)
			{
				this._blockObjectPlacementRandomizingPanel = blockObjectPlacementRandomizingPanel;
			}

			// Token: 0x0600000B RID: 11 RVA: 0x000021F0 File Offset: 0x000003F0
			public ToolPanelModule Get()
			{
				ToolPanelModule.Builder builder = new ToolPanelModule.Builder();
				builder.AddFragment(this._blockObjectPlacementRandomizingPanel, 30);
				return builder.Build();
			}

			// Token: 0x0400000D RID: 13
			private readonly BlockObjectPlacementRandomizingPanel _blockObjectPlacementRandomizingPanel;
		}
	}
}
