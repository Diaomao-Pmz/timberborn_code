using System;
using Timberborn.CoreSound;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000017 RID: 23
	public class ShaftSoundController : ILoadableSingleton
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00004935 File Offset: 0x00002B35
		public ShaftSoundController(ISoundSystem soundSystem, RootObjectProvider rootObjectProvider)
		{
			this._soundSystem = soundSystem;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000494B File Offset: 0x00002B4B
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("ShaftSoundController").transform;
			this.Initialize();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000496E File Offset: 0x00002B6E
		public void AddEmitter(ShaftSoundEmitter emitter)
		{
			this._soundSystem.AddAreaEmitter(this._parent, emitter.GameObject);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004987 File Offset: 0x00002B87
		public void RemoveEmitter(ShaftSoundEmitter emitter)
		{
			this._soundSystem.RemoveAreaEmitter(this._parent, emitter.GameObject);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000049A0 File Offset: 0x00002BA0
		public void Initialize()
		{
			string buildingMixerNameKey = MixerNames.BuildingMixerNameKey;
			this._soundSystem.AddLimitedAreaSound(this._parent, "Environment.Buildings.ShaftWorking", 60, 10, buildingMixerNameKey);
		}

		// Token: 0x04000070 RID: 112
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000071 RID: 113
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000072 RID: 114
		public Transform _parent;
	}
}
