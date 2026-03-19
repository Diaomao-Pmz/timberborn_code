using System;
using Timberborn.BlueprintSystem;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.CoreSound
{
	// Token: 0x0200000E RID: 14
	public class WindAmbientSound : ILoadableSingleton
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public WindAmbientSound(ISoundSystem soundSystem, RootObjectProvider rootObjectProvider, ISpecService specService)
		{
			this._soundSystem = soundSystem;
			this._rootObjectProvider = rootObjectProvider;
			this._specService = specService;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002C00 File Offset: 0x00000E00
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("WindAmbientSound");
			string windAmbientKey = this._specService.GetSingleSpec<CoreSoundSpec>().WindAmbientKey;
			this._soundSystem.LoopSingle2DSound(this._parent, windAmbientKey, 20);
			this._soundSystem.SetCustomMixer(this._parent, windAmbientKey, MixerNames.WindMixerNameKey);
		}

		// Token: 0x04000024 RID: 36
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000025 RID: 37
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000026 RID: 38
		public readonly ISpecService _specService;

		// Token: 0x04000027 RID: 39
		public GameObject _parent;
	}
}
