using System;
using Timberborn.FactionSystem;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.MainMenuPanels
{
	// Token: 0x02000009 RID: 9
	public class MainMenuSoundController : ILoadableSingleton
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000031ED File Offset: 0x000013ED
		public MainMenuSoundController(ISoundSystem soundSystem, RootObjectProvider rootObjectProvider)
		{
			this._soundSystem = soundSystem;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003203 File Offset: 0x00001403
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("MainMenuSoundController");
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000321B File Offset: 0x0000141B
		public void PlayThemeMusic()
		{
			this.StopAllMusic();
			this._soundSystem.PlaySound2D(this._parent, MainMenuSoundController.MainMenuThemeSoundName, 0);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000323A File Offset: 0x0000143A
		public void PlayCreditsMusic()
		{
			this.StopAllMusic();
			this._soundSystem.PlaySound2D(this._parent, MainMenuSoundController.MainMenuCreditsSoundName, 0);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000325C File Offset: 0x0000145C
		public void PlayFactionSelectedSound(FactionSpec factionSpec)
		{
			string soundName = "UI.Beavers." + factionSpec.SoundId + ".Selected.Adult_Content";
			this._soundSystem.PlaySound2D(this._parent, soundName, 10);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003293 File Offset: 0x00001493
		public void StopAllMusic()
		{
			this._soundSystem.StopSound(this._parent, MainMenuSoundController.MainMenuThemeSoundName);
			this._soundSystem.StopSound(this._parent, MainMenuSoundController.MainMenuCreditsSoundName);
		}

		// Token: 0x04000049 RID: 73
		public static readonly string MainMenuThemeSoundName = "Music_MainMenu.Theme";

		// Token: 0x0400004A RID: 74
		public static readonly string MainMenuCreditsSoundName = "Music_MainMenu.Credits";

		// Token: 0x0400004B RID: 75
		public readonly ISoundSystem _soundSystem;

		// Token: 0x0400004C RID: 76
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400004D RID: 77
		public GameObject _parent;
	}
}
