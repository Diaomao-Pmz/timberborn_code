using System;
using Bindito.Core;
using Timberborn.ToolPanelSystem;

namespace Timberborn.BrushesUI
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class BrushesUIConfigurator : Configurator
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000024C4 File Offset: 0x000006C4
		public override void Configure()
		{
			base.Bind<BrushDirectionPanel>().AsSingleton();
			base.Bind<BrushHeightPanel>().AsSingleton();
			base.Bind<BrushSizePanel>().AsSingleton();
			base.Bind<BrushShapePanel>().AsSingleton();
			base.MultiBind<ToolPanelModule>().ToProvider<BrushesUIConfigurator.ToolPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000B RID: 11
		public class ToolPanelModuleProvider : IProvider<ToolPanelModule>
		{
			// Token: 0x06000023 RID: 35 RVA: 0x0000251A File Offset: 0x0000071A
			public ToolPanelModuleProvider(BrushDirectionPanel brushDirectionPanel, BrushHeightPanel brushHeightPanel, BrushSizePanel brushSizePanel, BrushShapePanel brushShapePanel)
			{
				this._brushDirectionPanel = brushDirectionPanel;
				this._brushHeightPanel = brushHeightPanel;
				this._brushSizePanel = brushSizePanel;
				this._brushShapePanel = brushShapePanel;
			}

			// Token: 0x06000024 RID: 36 RVA: 0x00002540 File Offset: 0x00000740
			public ToolPanelModule Get()
			{
				ToolPanelModule.Builder builder = new ToolPanelModule.Builder();
				builder.AddFragment(this._brushDirectionPanel, 60);
				builder.AddFragment(this._brushHeightPanel, 70);
				builder.AddFragment(this._brushSizePanel, 80);
				builder.AddFragment(this._brushShapePanel, 90);
				return builder.Build();
			}

			// Token: 0x04000018 RID: 24
			public readonly BrushDirectionPanel _brushDirectionPanel;

			// Token: 0x04000019 RID: 25
			public readonly BrushHeightPanel _brushHeightPanel;

			// Token: 0x0400001A RID: 26
			public readonly BrushSizePanel _brushSizePanel;

			// Token: 0x0400001B RID: 27
			public readonly BrushShapePanel _brushShapePanel;
		}
	}
}
