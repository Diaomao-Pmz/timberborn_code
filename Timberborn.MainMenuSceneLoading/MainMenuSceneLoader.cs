using System;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;

namespace Timberborn.MainMenuSceneLoading
{
	// Token: 0x02000004 RID: 4
	public class MainMenuSceneLoader
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public MainMenuSceneLoader(ISceneLoader sceneLoader, EventBus eventBus)
		{
			this._sceneLoader = sceneLoader;
			this._eventBus = eventBus;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public void SaveAndOpenMainMenu()
		{
			this._eventBus.Post(new PreMainMenuStartedEvent(false));
			this._sceneLoader.LoadSceneInstantly(this.CreateMainMenuSceneParameters());
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F8 File Offset: 0x000002F8
		public void OpenMainMenu()
		{
			this._eventBus.Post(new PreMainMenuStartedEvent(true));
			this._sceneLoader.LoadSceneInstantly(this.CreateMainMenuSceneParameters());
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000211C File Offset: 0x0000031C
		public MainMenuSceneParameters CreateMainMenuSceneParameters()
		{
			MainMenuSceneParameters mainMenuSceneParameters;
			if (!this._sceneLoader.HasAnySceneParameters() || this._sceneLoader.TryGetSceneParameters<MainMenuSceneParameters>(out mainMenuSceneParameters))
			{
				return MainMenuSceneParameters.CreateWithWelcomeScreen();
			}
			return MainMenuSceneParameters.CreateWithoutWelcomeScreen();
		}

		// Token: 0x04000006 RID: 6
		public readonly EventBus _eventBus;

		// Token: 0x04000007 RID: 7
		public readonly ISceneLoader _sceneLoader;
	}
}
