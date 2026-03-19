using System;
using Bindito.Core;
using Timberborn.BottomBarSystem;
using Timberborn.ToolSystem;

namespace Timberborn.CursorToolSystem
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	[Context("MapEditor")]
	public class CursorToolSystemConfigurator : Configurator
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002988 File Offset: 0x00000B88
		public override void Configure()
		{
			base.Bind<CursorCoordinatesPicker>().AsSingleton();
			base.Bind<CursorDebuggingPanel>().AsSingleton();
			base.Bind<CursorTool>().AsSingleton();
			base.Bind<CursorButton>().AsSingleton();
			base.Bind<CursorVisibilityToggler>().AsSingleton();
			base.Bind<CursorDebugger>().AsSingleton();
			base.Bind<IDefaultToolProvider>().To<CursorDefaultToolProvider>().AsSingleton();
			base.MultiBind<BottomBarModule>().ToProvider<CursorToolSystemConfigurator.BottomBarModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000E RID: 14
		public class BottomBarModuleProvider : IProvider<BottomBarModule>
		{
			// Token: 0x0600003C RID: 60 RVA: 0x00002A07 File Offset: 0x00000C07
			public BottomBarModuleProvider(CursorButton cursorButton)
			{
				this._cursorButton = cursorButton;
			}

			// Token: 0x0600003D RID: 61 RVA: 0x00002A16 File Offset: 0x00000C16
			public BottomBarModule Get()
			{
				BottomBarModule.Builder builder = new BottomBarModule.Builder();
				builder.AddLeftSectionElement(this._cursorButton, 10);
				return builder.Build();
			}

			// Token: 0x04000032 RID: 50
			public readonly CursorButton _cursorButton;
		}
	}
}
