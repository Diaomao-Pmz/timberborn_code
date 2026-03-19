using System;
using Bindito.Core;
using Timberborn.BottomBarSystem;

namespace Timberborn.GameBlockObjectButtonsSystem
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class GameBlockObjectButtonsSystemConfigurator : Configurator
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000024B3 File Offset: 0x000006B3
		public override void Configure()
		{
			base.Bind<GameBlockObjectButtons>().AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<GameBlockObjectButtonsSystemConfigurator.BottomBarModuleProvider>().AsSingleton();
		}

		// Token: 0x02000009 RID: 9
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x0600001F RID: 31 RVA: 0x000024DA File Offset: 0x000006DA
			public BottomBarModuleProvider(GameBlockObjectButtons objectButtons)
			{
				this._objectButtons = objectButtons;
			}

			// Token: 0x06000020 RID: 32 RVA: 0x000024E9 File Offset: 0x000006E9
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddMiddleSectionElements(this._objectButtons);
				return builder.Build();
			}

			// Token: 0x04000014 RID: 20
			public readonly GameBlockObjectButtons _objectButtons;
		}
	}
}
