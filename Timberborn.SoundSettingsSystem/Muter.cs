using System;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.SoundSettingsSystem
{
	// Token: 0x02000004 RID: 4
	public class Muter : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public Muter(ISoundSystem soundSystem, SoundSettings soundSettings, LoadingScreen loadingScreen)
		{
			this._soundSystem = soundSystem;
			this._soundSettings = soundSettings;
			this._loadingScreen = loadingScreen;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
		public void Load()
		{
			if (Application.isPlaying)
			{
				Application.focusChanged += this.OnFocusChanged;
				this._loadingScreen.LoadingScreenEnabled += this.OnLoadingScreenEnabled;
				this._loadingScreen.LoadingScreenDisabled += this.OnLoadingScreenDisabled;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		public void Unload()
		{
			Application.focusChanged -= this.OnFocusChanged;
			this._loadingScreen.LoadingScreenEnabled -= this.OnLoadingScreenEnabled;
			this._loadingScreen.LoadingScreenDisabled -= this.OnLoadingScreenDisabled;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000217C File Offset: 0x0000037C
		public void OnFocusChanged(bool hasFocus)
		{
			if (hasFocus)
			{
				this.Unmute();
				return;
			}
			if (this._soundSettings.MuteWhenMinimized)
			{
				this.Mute();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000219B File Offset: 0x0000039B
		public void OnLoadingScreenEnabled(object sender, EventArgs e)
		{
			this.Mute();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021A3 File Offset: 0x000003A3
		public void OnLoadingScreenDisabled(object sender, EventArgs e)
		{
			this.UnmuteIfInFocus();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021AB File Offset: 0x000003AB
		public void Mute()
		{
			this._soundSystem.SetMasterVolume(0f);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BD File Offset: 0x000003BD
		public void UnmuteIfInFocus()
		{
			if (Application.isFocused || !this._soundSettings.MuteWhenMinimized)
			{
				this.Unmute();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D9 File Offset: 0x000003D9
		public void Unmute()
		{
			this._soundSystem.SetMasterVolume(this._soundSettings.MasterVolume);
		}

		// Token: 0x04000006 RID: 6
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000007 RID: 7
		public readonly SoundSettings _soundSettings;

		// Token: 0x04000008 RID: 8
		public readonly LoadingScreen _loadingScreen;
	}
}
