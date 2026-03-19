using System;
using Bindito.Core;
using Timberborn.BottomBarSystem;

namespace Timberborn.Options
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class OptionsConfigurator : Configurator
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<ShowOptionsButton>().AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<OptionsConfigurator.BottomBarModuleProvider>().AsSingleton();
		}

		// Token: 0x02000006 RID: 6
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x06000006 RID: 6 RVA: 0x000020E5 File Offset: 0x000002E5
			public BottomBarModuleProvider(ShowOptionsButton showOptionsButton)
			{
				this._showOptionsButton = showOptionsButton;
			}

			// Token: 0x06000007 RID: 7 RVA: 0x000020F4 File Offset: 0x000002F4
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddRightSectionElement(this._showOptionsButton);
				return builder.Build();
			}

			// Token: 0x04000006 RID: 6
			public readonly ShowOptionsButton _showOptionsButton;
		}
	}
}
