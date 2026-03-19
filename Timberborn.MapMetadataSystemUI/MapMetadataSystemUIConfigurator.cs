using System;
using Bindito.Core;
using Timberborn.SaveSystem;
using Timberborn.ToolPanelSystem;

namespace Timberborn.MapMetadataSystemUI
{
	// Token: 0x02000007 RID: 7
	[Context("MapEditor")]
	public class MapMetadataSystemUIConfigurator : Configurator
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000259C File Offset: 0x0000079C
		public override void Configure()
		{
			base.Bind<MapMetadataPanel>().AsSingleton();
			base.Bind<MapMetadataTool>().AsSingleton();
			base.Bind<MapMetadataSaveEntryWriter>().AsSingleton();
			base.MultiBind<ISaveEntryWriter>().ToExisting<MapMetadataSaveEntryWriter>();
			base.MultiBind<ToolPanelModule>().ToProvider<MapMetadataSystemUIConfigurator.ToolPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000008 RID: 8
		public class ToolPanelModuleProvider : IProvider<ToolPanelModule>
		{
			// Token: 0x06000022 RID: 34 RVA: 0x000025F2 File Offset: 0x000007F2
			public ToolPanelModuleProvider(MapMetadataPanel mapMetadataPanel)
			{
				this._mapMetadataPanel = mapMetadataPanel;
			}

			// Token: 0x06000023 RID: 35 RVA: 0x00002601 File Offset: 0x00000801
			public ToolPanelModule Get()
			{
				ToolPanelModule.Builder builder = new ToolPanelModule.Builder();
				builder.AddFragment(this._mapMetadataPanel, 210);
				return builder.Build();
			}

			// Token: 0x0400001E RID: 30
			public readonly MapMetadataPanel _mapMetadataPanel;
		}
	}
}
