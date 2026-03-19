using System;
using Bindito.Core;
using Timberborn.BottomBarSystem;

namespace Timberborn.WaterBrushesUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class WaterBrushesUIConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public override void Configure()
		{
			base.Bind<WaterHeightBrushTool>().AsSingleton();
			base.Bind<WaterHeightBrushButton>().AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<WaterBrushesUIConfigurator.BottomBarModuleProvider>().AsSingleton();
		}

		// Token: 0x02000008 RID: 8
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x06000009 RID: 9 RVA: 0x00002131 File Offset: 0x00000331
			public BottomBarModuleProvider(WaterHeightBrushButton waterHeightBrushButton)
			{
				this._waterHeightBrushButton = waterHeightBrushButton;
			}

			// Token: 0x0600000A RID: 10 RVA: 0x00002140 File Offset: 0x00000340
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddLeftSectionElement(this._waterHeightBrushButton, 90);
				return builder.Build();
			}

			// Token: 0x04000008 RID: 8
			public readonly WaterHeightBrushButton _waterHeightBrushButton;
		}
	}
}
