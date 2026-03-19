using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Timberborn.ErrorReporting
{
	// Token: 0x02000004 RID: 4
	public static class CrashSceneLoader
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C7 File Offset: 0x000002C7
		public static bool DevModeEnabled { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public static bool Enabled
		{
			get
			{
				return CrashSceneLoader.FullCrashScreen;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020D6 File Offset: 0x000002D6
		public static void LoadCrashSceneIfEnabled()
		{
			if (CrashSceneLoader.Enabled)
			{
				CrashSceneLoader.LoadCrashScene();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E4 File Offset: 0x000002E4
		public static void LoadCrashScene()
		{
			Cursor.SetCursor(null, Vector2.zero, 0);
			SceneManager.LoadSceneAsync(CrashSceneLoader.CrashSceneIndex);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020FD File Offset: 0x000002FD
		public static bool FullCrashScreen
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly int CrashSceneIndex = 3;
	}
}
