using System;
using Bindito.Core;
using Timberborn.BottomBarSystem;

namespace Timberborn.DemolishingToolGroupSystem
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class DemolishingToolGroupSystemConfigurator : Configurator
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022E3 File Offset: 0x000004E3
		public override void Configure()
		{
			base.Bind<DemolishingButton>().AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<DemolishingToolGroupSystemConfigurator.BottomBarModuleProvider>().AsSingleton();
		}

		// Token: 0x02000007 RID: 7
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x06000011 RID: 17 RVA: 0x0000230A File Offset: 0x0000050A
			public BottomBarModuleProvider(DemolishingButton demolishingButton)
			{
				this._demolishingButton = demolishingButton;
			}

			// Token: 0x06000012 RID: 18 RVA: 0x00002319 File Offset: 0x00000519
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddLeftSectionElement(this._demolishingButton, 50);
				return builder.Build();
			}

			// Token: 0x04000017 RID: 23
			public readonly DemolishingButton _demolishingButton;
		}
	}
}
