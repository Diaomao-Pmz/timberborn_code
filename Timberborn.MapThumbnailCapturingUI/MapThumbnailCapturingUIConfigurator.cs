using System;
using Bindito.Core;
using Timberborn.ToolPanelSystem;

namespace Timberborn.MapThumbnailCapturingUI
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	public class MapThumbnailCapturingUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<ThumbnailCapturingPanel>().AsSingleton();
			base.Bind<ThumbnailCapturingTool>().AsSingleton();
			base.MultiBind<ToolPanelModule>().ToProvider<MapThumbnailCapturingUIConfigurator.ToolPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000005 RID: 5
		public class ToolPanelModuleProvider : IProvider<ToolPanelModule>
		{
			// Token: 0x06000005 RID: 5 RVA: 0x000020F1 File Offset: 0x000002F1
			public ToolPanelModuleProvider(ThumbnailCapturingPanel thumbnailCapturingPanel)
			{
				this._thumbnailCapturingPanel = thumbnailCapturingPanel;
			}

			// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
			public ToolPanelModule Get()
			{
				ToolPanelModule.Builder builder = new ToolPanelModule.Builder();
				builder.AddFragment(this._thumbnailCapturingPanel, 200);
				return builder.Build();
			}

			// Token: 0x04000006 RID: 6
			public readonly ThumbnailCapturingPanel _thumbnailCapturingPanel;
		}
	}
}
