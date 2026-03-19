using System;
using Bindito.Core;
using Timberborn.ToolPanelSystem;

namespace Timberborn.BlockObjectToolsUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class BlockObjectToolsUIConfigurator : Configurator
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000025B8 File Offset: 0x000007B8
		public override void Configure()
		{
			base.Bind<BlockObjectToolButtonFactory>().AsSingleton();
			base.Bind<BlockObjectToolGroupButtonFactory>().AsSingleton();
			base.Bind<BlockObjectPlacementPanel>().AsSingleton();
			base.Bind<BlockObjectToolWarningPanel>().AsSingleton();
			base.MultiBind<ToolPanelModule>().ToProvider<BlockObjectToolsUIConfigurator.ToolPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000008 RID: 8
		public class ToolPanelModuleProvider : IProvider<ToolPanelModule>
		{
			// Token: 0x06000018 RID: 24 RVA: 0x0000260E File Offset: 0x0000080E
			public ToolPanelModuleProvider(BlockObjectPlacementPanel blockObjectPlacementPanel, BlockObjectToolWarningPanel blockObjectToolWarningPanel)
			{
				this._blockObjectPlacementPanel = blockObjectPlacementPanel;
				this._blockObjectToolWarningPanel = blockObjectToolWarningPanel;
			}

			// Token: 0x06000019 RID: 25 RVA: 0x00002624 File Offset: 0x00000824
			public ToolPanelModule Get()
			{
				ToolPanelModule.Builder builder = new ToolPanelModule.Builder();
				builder.AddFragment(this._blockObjectPlacementPanel, 20);
				builder.AddFragment(this._blockObjectToolWarningPanel, 40);
				return builder.Build();
			}

			// Token: 0x0400001E RID: 30
			public readonly BlockObjectPlacementPanel _blockObjectPlacementPanel;

			// Token: 0x0400001F RID: 31
			public readonly BlockObjectToolWarningPanel _blockObjectToolWarningPanel;
		}
	}
}
