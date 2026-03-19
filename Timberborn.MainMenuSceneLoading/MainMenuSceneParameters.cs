using System;
using Timberborn.SceneLoading;

namespace Timberborn.MainMenuSceneLoading
{
	// Token: 0x02000006 RID: 6
	public class MainMenuSceneParameters : ISceneParameters
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002166 File Offset: 0x00000366
		public bool ShowWelcomeScreen { get; }

		// Token: 0x0600000A RID: 10 RVA: 0x0000216E File Offset: 0x0000036E
		public MainMenuSceneParameters(bool showWelcomeScreen)
		{
			this.ShowWelcomeScreen = showWelcomeScreen;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217D File Offset: 0x0000037D
		public static MainMenuSceneParameters CreateWithWelcomeScreen()
		{
			return new MainMenuSceneParameters(true);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002185 File Offset: 0x00000385
		public static MainMenuSceneParameters CreateWithoutWelcomeScreen()
		{
			return new MainMenuSceneParameters(false);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000218D File Offset: 0x0000038D
		public int SceneIndex
		{
			get
			{
				return 1;
			}
		}
	}
}
