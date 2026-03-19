using System;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;

namespace Timberborn.TimeSystem
{
	// Token: 0x0200000D RID: 13
	public class GameSpeedSoundController : ILoadableSingleton
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002AAD File Offset: 0x00000CAD
		public GameSpeedSoundController(ISoundSystem soundSystem, EventBus eventBus)
		{
			this._soundSystem = soundSystem;
			this._eventBus = eventBus;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002AC3 File Offset: 0x00000CC3
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002AD1 File Offset: 0x00000CD1
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateSoundState(currentSpeedChangedEvent.CurrentSpeed);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002ADF File Offset: 0x00000CDF
		public void UpdateSoundState(float currentSpeed)
		{
			if (currentSpeed == 0f)
			{
				this.MuteSound();
				return;
			}
			this.UnmuteSound();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002AF6 File Offset: 0x00000CF6
		public void MuteSound()
		{
			this._soundSystem.SetMixerVolume(GameSpeedSoundController.EnvironmentRootMixerVolumeKey, 0f);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002B0D File Offset: 0x00000D0D
		public void UnmuteSound()
		{
			this._soundSystem.SetMixerVolume(GameSpeedSoundController.EnvironmentRootMixerVolumeKey, 1f);
		}

		// Token: 0x04000023 RID: 35
		public static readonly string EnvironmentRootMixerVolumeKey = "EnvironmentRoot_Volume";

		// Token: 0x04000024 RID: 36
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000025 RID: 37
		public readonly EventBus _eventBus;
	}
}
