using System;
using Timberborn.CoreSound;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.SleepSystem
{
	// Token: 0x0200000D RID: 13
	public class SleepSoundController : ILoadableSingleton
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000284F File Offset: 0x00000A4F
		public SleepSoundController(ISoundSystem soundSystem, RootObjectProvider rootObjectProvider)
		{
			this._soundSystem = soundSystem;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002868 File Offset: 0x00000A68
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("SleepSoundController").transform;
			string ambientMixerNameKey = MixerNames.AmbientMixerNameKey;
			this._soundSystem.AddLimitedAreaSound(this._parent, "Environment.Beavers.Sleeping", 40, 4, ambientMixerNameKey);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028B0 File Offset: 0x00000AB0
		public void AddSleepingBeaver(SleepSoundEmitter sleepSoundEmitter)
		{
			this._soundSystem.AddAreaEmitter(this._parent, sleepSoundEmitter.GameObject);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028C9 File Offset: 0x00000AC9
		public void RemoveSleepingBeaver(SleepSoundEmitter sleepSoundEmitter)
		{
			this._soundSystem.RemoveAreaEmitter(this._parent, sleepSoundEmitter.GameObject);
		}

		// Token: 0x04000023 RID: 35
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000024 RID: 36
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000025 RID: 37
		public Transform _parent;
	}
}
