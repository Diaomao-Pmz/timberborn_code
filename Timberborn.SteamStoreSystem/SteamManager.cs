using System;
using System.Text;
using AOT;
using Steamworks;
using Timberborn.FeatureToggleSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.SteamStoreSystem
{
	// Token: 0x02000006 RID: 6
	public class SteamManager : ILoadableSingleton, IUnloadableSingleton, IUpdatableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000023B0 File Offset: 0x000005B0
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000023B8 File Offset: 0x000005B8
		public bool Initialized { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000023C1 File Offset: 0x000005C1
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000023C9 File Offset: 0x000005C9
		public bool GameIsAllowedToRun { get; private set; }

		// Token: 0x0600000A RID: 10 RVA: 0x000023D4 File Offset: 0x000005D4
		public void Load()
		{
			if (Application.isEditor && !FeatureToggles.SteamInEditor)
			{
				this.GameIsAllowedToRun = true;
				return;
			}
			this.TryToInitialize();
			if (this.Initialized && this._steamAPIWarningMessageHook == null)
			{
				this._steamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
				SteamClient.SetWarningMessageHook(this._steamAPIWarningMessageHook);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000242A File Offset: 0x0000062A
		public void Unload()
		{
			if (this.Initialized)
			{
				SteamAPI.Shutdown();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002439 File Offset: 0x00000639
		public void UpdateSingleton()
		{
			if (this.Initialized)
			{
				SteamAPI.RunCallbacks();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002448 File Offset: 0x00000648
		[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
		public static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
		{
			Debug.LogWarning(pchDebugText);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002450 File Offset: 0x00000650
		public void TryToInitialize()
		{
			this.RunPacksizeTest();
			this.RunDllCheckTest();
			if (this.RestartAppIfNecessary())
			{
				return;
			}
			this.GameIsAllowedToRun = true;
			if (this.InitializeSteamworksAPI())
			{
				this.Initialized = true;
				Debug.Log("Successfully connected to the Steam client.");
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002487 File Offset: 0x00000687
		public bool InitializeSteamworksAPI()
		{
			if (SteamAPI.Init())
			{
				return true;
			}
			if (Application.isEditor && FeatureToggles.SteamInEditor)
			{
				throw new InvalidOperationException("You are using SteamInEditor toggle, but the Steam couldn't be initialized. Make sure the Steam is running in the background");
			}
			Debug.Log("Couldn't connect to the Steam client. Is it running and do you have the game in your library?");
			return false;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024B8 File Offset: 0x000006B8
		public bool RestartAppIfNecessary()
		{
			try
			{
				if (SteamAPI.RestartAppIfNecessary(SteamAppId.AppId))
				{
					Application.Quit();
					return true;
				}
			}
			catch (DllNotFoundException ex)
			{
				string str = "[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n";
				DllNotFoundException ex2 = ex;
				Debug.LogError(str + ((ex2 != null) ? ex2.ToString() : null));
				Application.Quit();
				return true;
			}
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002518 File Offset: 0x00000718
		public void RunDllCheckTest()
		{
			if (!DllCheck.Test())
			{
				Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.");
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000252B File Offset: 0x0000072B
		public void RunPacksizeTest()
		{
			if (!Packsize.Test())
			{
				Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.");
			}
		}

		// Token: 0x0400000A RID: 10
		public SteamAPIWarningMessageHook_t _steamAPIWarningMessageHook;
	}
}
