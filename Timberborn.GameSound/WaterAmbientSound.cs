using System;
using Timberborn.BlueprintSystem;
using Timberborn.CoreSound;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.GameSound
{
	// Token: 0x02000011 RID: 17
	public class WaterAmbientSound : ILoadableSingleton, IEmitterMap
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003099 File Offset: 0x00001299
		public WaterAmbientSound(IThreadSafeWaterMap threadSafeWaterMap, ISoundSystem soundSystem, RootObjectProvider rootObjectProvider, ISpecService specService)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._soundSystem = soundSystem;
			this._rootObjectProvider = rootObjectProvider;
			this._specService = specService;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030BE File Offset: 0x000012BE
		public void Load()
		{
			this._ambientSpec = this._specService.GetSingleSpec<AmbientSpec>();
			this._parent = this._rootObjectProvider.CreateRootObject("WaterAmbientSound").transform;
			this.AddAreaSound();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000030F2 File Offset: 0x000012F2
		public bool IsEmitterAt(Vector2Int coordinates)
		{
			return this._threadSafeWaterMap.IsWaterOnAnyHeight(coordinates);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003100 File Offset: 0x00001300
		public void AddAreaSound()
		{
			string ambientMixerNameKey = MixerNames.AmbientMixerNameKey;
			this._soundSystem.AddLargeAreaSound(this._parent, this, this._ambientSpec.WaterAmbient, 50, 10, ambientMixerNameKey);
		}

		// Token: 0x04000032 RID: 50
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000033 RID: 51
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000034 RID: 52
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000035 RID: 53
		public readonly ISpecService _specService;

		// Token: 0x04000036 RID: 54
		public AmbientSpec _ambientSpec;

		// Token: 0x04000037 RID: 55
		public Transform _parent;
	}
}
