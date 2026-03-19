using System;
using Timberborn.BlueprintSystem;
using Timberborn.CoreSound;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.GameSound
{
	// Token: 0x02000008 RID: 8
	public class DayNightAmbientSound : ILoadableSingleton
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000232D File Offset: 0x0000052D
		public DayNightAmbientSound(EventBus eventBus, ISoundSystem soundSystem, IDayNightCycle dayNightCycle, RootObjectProvider rootObjectProvider, ISpecService specService)
		{
			this._eventBus = eventBus;
			this._soundSystem = soundSystem;
			this._dayNightCycle = dayNightCycle;
			this._rootObjectProvider = rootObjectProvider;
			this._specService = specService;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000235C File Offset: 0x0000055C
		public void Load()
		{
			this._ambientSpec = this._specService.GetSingleSpec<AmbientSpec>();
			this._parent = this._rootObjectProvider.CreateRootObject("DayNightAmbientSound");
			this._eventBus.Register(this);
			string sound = this._dayNightCycle.IsDaytime ? this._ambientSpec.DayAmbient : this._ambientSpec.NightAmbient;
			this.StartSound(sound);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023C9 File Offset: 0x000005C9
		[OnEvent]
		public void OnDaytimeStartEvent(DaytimeStartEvent daytimeStartEvent)
		{
			this._soundSystem.StopSound(this._parent, this._ambientSpec.NightAmbient);
			this.StartSound(this._ambientSpec.DayAmbient);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023F8 File Offset: 0x000005F8
		[OnEvent]
		public void OnNighttimeStartEvent(NighttimeStartEvent nighttimeStartEvent)
		{
			this._soundSystem.StopSound(this._parent, this._ambientSpec.DayAmbient);
			this.StartSound(this._ambientSpec.NightAmbient);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002427 File Offset: 0x00000627
		public void StartSound(string sound)
		{
			this._soundSystem.LoopSingle2DSound(this._parent, sound, 20);
			this._soundSystem.SetCustomMixer(this._parent, sound, MixerNames.AmbientMixerNameKey);
		}

		// Token: 0x0400000B RID: 11
		public readonly EventBus _eventBus;

		// Token: 0x0400000C RID: 12
		public readonly ISoundSystem _soundSystem;

		// Token: 0x0400000D RID: 13
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400000E RID: 14
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400000F RID: 15
		public readonly ISpecService _specService;

		// Token: 0x04000010 RID: 16
		public AmbientSpec _ambientSpec;

		// Token: 0x04000011 RID: 17
		public GameObject _parent;
	}
}
