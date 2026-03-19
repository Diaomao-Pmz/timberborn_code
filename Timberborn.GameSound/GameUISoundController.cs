using System;
using Timberborn.BlueprintSystem;
using Timberborn.GameFactionSystem;
using Timberborn.GameWonderCompletion;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.GameSound
{
	// Token: 0x0200000B RID: 11
	public class GameUISoundController : ILoadableSingleton
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000271A File Offset: 0x0000091A
		public GameUISoundController(ISoundSystem soundSystem, FactionService factionService, RootObjectProvider rootObjectProvider, ISpecService specService)
		{
			this._soundSystem = soundSystem;
			this._factionService = factionService;
			this._rootObjectProvider = rootObjectProvider;
			this._specService = specService;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000273F File Offset: 0x0000093F
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("GameUISoundController");
			this._spec = this._specService.GetSingleSpec<GameUISoundSpec>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002768 File Offset: 0x00000968
		public void PlayWellbeingHighscoreSound()
		{
			this.PlaySound2D(this._spec.WellbeingHighscore);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000277B File Offset: 0x0000097B
		public void PlayFieldPlacedSound()
		{
			this.PlaySound2D(this._spec.FieldPlaced);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000278E File Offset: 0x0000098E
		public void PlayBlinkingSound()
		{
			this.PlaySound2D(this._spec.BlinkingSoundKey);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027A1 File Offset: 0x000009A1
		public void PlayBadtideStartedSound()
		{
			this.PlaySound2D(this._spec.BadtideStartedSoundKey);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027B4 File Offset: 0x000009B4
		public void PlayDroughtStartedSound()
		{
			this.PlaySound2D(this._spec.DroughtStartedSoundKey);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027C7 File Offset: 0x000009C7
		public void PlayTemperateWeatherStartedSound()
		{
			this.PlaySound2D(this._spec.TemperateWeatherStartedSoundKey);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027DA File Offset: 0x000009DA
		public void PlayWonderLaunchSound()
		{
			this.PlaySound2D(this._factionService.Current.GetSpec<FactionWonderSpec>().WonderLaunchSound);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027F7 File Offset: 0x000009F7
		public void PlayWonderCongratulationSound()
		{
			this.PlaySound2D(this._spec.WonderCongratulationSoundKey);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000280A File Offset: 0x00000A0A
		public void PlaySound2D(string sound)
		{
			this._soundSystem.PlaySound2D(this._parent, sound, 10);
		}

		// Token: 0x0400001A RID: 26
		public readonly ISoundSystem _soundSystem;

		// Token: 0x0400001B RID: 27
		public readonly FactionService _factionService;

		// Token: 0x0400001C RID: 28
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400001D RID: 29
		public readonly ISpecService _specService;

		// Token: 0x0400001E RID: 30
		public GameObject _parent;

		// Token: 0x0400001F RID: 31
		public GameUISoundSpec _spec;
	}
}
