using System;
using Bindito.Core;
using Timberborn.ToolPanelSystem;

namespace Timberborn.ToolSystemUI
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	[Context("MapEditor")]
	public class ToolSystemUIConfigurator : Configurator
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000026C0 File Offset: 0x000008C0
		public override void Configure()
		{
			base.Bind<DescriptionPanelController>().AsSingleton();
			base.Bind<DescriptionPanels>().AsSingleton();
			base.Bind<PanelToolSwitcher>().AsSingleton();
			base.Bind<ToolWaterToggler>().AsSingleton();
			base.MultiBind<ToolPanelModule>().ToProvider<ToolSystemUIConfigurator.ToolPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000F RID: 15
		public class ToolPanelModuleProvider : IProvider<ToolPanelModule>
		{
			// Token: 0x0600003C RID: 60 RVA: 0x00002716 File Offset: 0x00000916
			public ToolPanelModuleProvider(DescriptionPanelController descriptionPanelController)
			{
				this._descriptionPanelController = descriptionPanelController;
			}

			// Token: 0x0600003D RID: 61 RVA: 0x00002725 File Offset: 0x00000925
			public ToolPanelModule Get()
			{
				ToolPanelModule.Builder builder = new ToolPanelModule.Builder();
				builder.AddFragment(this._descriptionPanelController, 10);
				return builder.Build();
			}

			// Token: 0x0400001D RID: 29
			public readonly DescriptionPanelController _descriptionPanelController;
		}
	}
}
