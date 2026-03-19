using System;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.HazardousWeatherSystem;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using Timberborn.WeatherSystem;
using UnityEngine;

namespace Timberborn.GameSound
{
	// Token: 0x02000009 RID: 9
	public class GameMusicPlayer : ILoadableSingleton
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002454 File Offset: 0x00000654
		public GameMusicPlayer(ISoundSystem soundSystem, IRandomNumberGenerator randomNumberGenerator, WeatherService weatherService, EventBus eventBus, RootObjectProvider rootObjectProvider, ISpecService specService)
		{
			this._soundSystem = soundSystem;
			this._randomNumberGenerator = randomNumberGenerator;
			this._weatherService = weatherService;
			this._eventBus = eventBus;
			this._rootObjectProvider = rootObjectProvider;
			this._specService = specService;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000248C File Offset: 0x0000068C
		public void Load()
		{
			this._musicSpec = this._specService.GetSingleSpec<MusicSpec>();
			this._parent = this._rootObjectProvider.CreateRootObject("GameMusicPlayer");
			if (this._weatherService.IsHazardousWeather)
			{
				this.StartDroughtMusic();
			}
			else
			{
				this.StartStandardMusic();
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024E7 File Offset: 0x000006E7
		[OnEvent]
		public void OnHazardousWeatherStarted(HazardousWeatherStartedEvent hazardousWeatherStartedEvent)
		{
			this.StopStandardMusic();
			this.StartDroughtMusic();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024F5 File Offset: 0x000006F5
		[OnEvent]
		public void OnHazardousWeatherEnded(HazardousWeatherEndedEvent hazardousWeatherEndedEvent)
		{
			this.StopDroughtMusic();
			this.StartStandardMusic();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002503 File Offset: 0x00000703
		public void StartStandardMusic()
		{
			this.PlaySound(this._musicSpec.StandardTrack, new Action(this.PlayStandardPhrase), new float?(this._musicSpec.MinDelay));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002532 File Offset: 0x00000732
		public void StopStandardMusic()
		{
			this.StopSound(this._musicSpec.StandardTrack);
			this.StopSound(this._musicSpec.StandardPhrase);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002558 File Offset: 0x00000758
		public void PlayStandardTrack()
		{
			this.PlaySound(this._musicSpec.StandardTrack, new Action(this.PlayStandardPhrase), null);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000258C File Offset: 0x0000078C
		public void PlayStandardPhrase()
		{
			this.PlaySound(this._musicSpec.StandardPhrase, new Action(this.PlayStandardTrack), null);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025BF File Offset: 0x000007BF
		public void StartDroughtMusic()
		{
			this.PlaySound(this._musicSpec.DroughtTrack, new Action(this.PlayDroughtTrack), new float?(this._musicSpec.MinDelay));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025EE File Offset: 0x000007EE
		public void StopDroughtMusic()
		{
			this.StopSound(this._musicSpec.DroughtTrack);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002604 File Offset: 0x00000804
		public void PlayDroughtTrack()
		{
			this.PlaySound(this._musicSpec.DroughtTrack, new Action(this.PlayDroughtTrack), null);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002638 File Offset: 0x00000838
		public void PlaySound(string soundName, Action callback, float? delay = null)
		{
			this._soundSystem.PlaySound2D(this._parent, soundName, 0, delay ?? this.RandomDelay(), callback);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002673 File Offset: 0x00000873
		public void StopSound(string soundName)
		{
			this._soundSystem.StopSound(this._parent, soundName);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002687 File Offset: 0x00000887
		public float RandomDelay()
		{
			return this._randomNumberGenerator.Range(this._musicSpec.MinDelay, this._musicSpec.MaxDelay);
		}

		// Token: 0x04000012 RID: 18
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000013 RID: 19
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000014 RID: 20
		public readonly WeatherService _weatherService;

		// Token: 0x04000015 RID: 21
		public readonly EventBus _eventBus;

		// Token: 0x04000016 RID: 22
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000017 RID: 23
		public readonly ISpecService _specService;

		// Token: 0x04000018 RID: 24
		public MusicSpec _musicSpec;

		// Token: 0x04000019 RID: 25
		public GameObject _parent;
	}
}
