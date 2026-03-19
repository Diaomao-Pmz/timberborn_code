using System;
using Timberborn.ApplicationLifetime;
using Timberborn.CoreUI;
using Timberborn.MainMenuSceneLoading;
using Timberborn.WebNavigation;

namespace Timberborn.GameExitSystem
{
	// Token: 0x02000006 RID: 6
	public class GoodbyeBoxFactory
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021B1 File Offset: 0x000003B1
		public GoodbyeBoxFactory(MainMenuSceneLoader mainMenuSceneLoader, VisualElementLoader visualElementLoader, PanelStack panelStack, UrlOpener urlOpener)
		{
			this._mainMenuSceneLoader = mainMenuSceneLoader;
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._urlOpener = urlOpener;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D6 File Offset: 0x000003D6
		public GoodbyeBox ShowExitToDesktop()
		{
			return this.GetController(new Action(GameQuitter.Quit));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021EA File Offset: 0x000003EA
		public GoodbyeBox ShowExitToMainMenu()
		{
			return this.GetController(new Action(this._mainMenuSceneLoader.SaveAndOpenMainMenu));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002203 File Offset: 0x00000403
		public GoodbyeBox GetController(Action action)
		{
			return new GoodbyeBox(this._visualElementLoader, this._panelStack, this._urlOpener, action);
		}

		// Token: 0x0400000A RID: 10
		public readonly MainMenuSceneLoader _mainMenuSceneLoader;

		// Token: 0x0400000B RID: 11
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000C RID: 12
		public readonly PanelStack _panelStack;

		// Token: 0x0400000D RID: 13
		public readonly UrlOpener _urlOpener;
	}
}
