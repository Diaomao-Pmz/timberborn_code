using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CoreSound;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Illumination;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200003E RID: 62
	public class Speaker : BaseComponent, IAwakableComponent, IInitializableEntity, IPersistentEntity, IDuplicable<Speaker>, IDuplicable, IAutomatableNeeder, IFinishedStateListener, ITerminal, IRegisteredComponent
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002AB RID: 683 RVA: 0x00007A30 File Offset: 0x00005C30
		// (remove) Token: 0x060002AC RID: 684 RVA: 0x00007A68 File Offset: 0x00005C68
		public event EventHandler PlaybackStateChanged;

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00007A9D File Offset: 0x00005C9D
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00007AA5 File Offset: 0x00005CA5
		public SpeakerPlaybackMode PlaybackMode { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00007AAE File Offset: 0x00005CAE
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00007AB6 File Offset: 0x00005CB6
		public SpeakerSpatialMode SpatialMode { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00007ABF File Offset: 0x00005CBF
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00007AC7 File Offset: 0x00005CC7
		public string SoundId { get; private set; }

		// Token: 0x060002B3 RID: 691 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public Speaker(SpeakerSoundService speakerSoundService, SpeakerPlayer speakerPlayer, ISoundSystem soundSystem, EventBus eventBus)
		{
			this._speakerSoundService = speakerSoundService;
			this._speakerPlayer = speakerPlayer;
			this._soundSystem = soundSystem;
			this._eventBus = eventBus;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00003095 File Offset: 0x00001295
		public bool NeedsAutomatable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00007AF5 File Offset: 0x00005CF5
		public void Awake()
		{
			this._automatable = base.GetComponent<Automatable>();
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00007B14 File Offset: 0x00005D14
		public void InitializeEntity()
		{
			this.ValidateSoundId();
			this._eventBus.Register(this);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00007B28 File Offset: 0x00005D28
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Speaker.ComponentKey);
			component.Set<SpeakerPlaybackMode>(Speaker.PlaybackModeKey, this.PlaybackMode);
			component.Set<SpeakerSpatialMode>(Speaker.SpatialModeKey, this.SpatialMode);
			component.Set(Speaker.SoundIdKey, this.SoundId);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00007B68 File Offset: 0x00005D68
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Speaker.ComponentKey);
			this.PlaybackMode = component.Get<SpeakerPlaybackMode>(Speaker.PlaybackModeKey);
			this.SpatialMode = component.Get<SpeakerSpatialMode>(Speaker.SpatialModeKey);
			this.SoundId = component.Get(Speaker.SoundIdKey);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00007BB4 File Offset: 0x00005DB4
		public void OnEnterFinishedState()
		{
			this._speakerPlayer.AddSpeaker(this);
			this.PlayIfContinuous();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public void OnExitFinishedState()
		{
			this._speakerPlayer.RemoveSpeaker(this);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00007BD8 File Offset: 0x00005DD8
		public void Evaluate()
		{
			bool flag = this._automatable.State == ConnectionState.On;
			bool? previousState = this._previousState;
			bool flag2 = flag;
			if (!(previousState.GetValueOrDefault() == flag2 & previousState != null))
			{
				this.EvaluatePlayback(flag);
				this._illuminatorToggle.Toggle(flag);
				this._previousState = new bool?(flag);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00007C30 File Offset: 0x00005E30
		public void DuplicateFrom(Speaker source)
		{
			this.SoundId = source.SoundId;
			this.PlaybackMode = source.PlaybackMode;
			this.SpatialMode = source.SpatialMode;
			this.StopAndPlayIfContinuous();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00007C5C File Offset: 0x00005E5C
		public void SetPlaybackMode(SpeakerPlaybackMode playbackMode)
		{
			this.PlaybackMode = playbackMode;
			this.StopAndPlayIfContinuous();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00007C6B File Offset: 0x00005E6B
		public void SetSpatialMode(SpeakerSpatialMode spatialMode)
		{
			this.SpatialMode = spatialMode;
			this.StopAndPlayIfContinuous();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007C7A File Offset: 0x00005E7A
		public void SetSoundId(string soundId)
		{
			this.SoundId = this._speakerSoundService.GetValidatedSoundId(soundId);
			this.StopAndPlayIfContinuous();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00007C94 File Offset: 0x00005E94
		[OnEvent]
		public void OnSpeakerSoundsReloaded(SpeakerSoundsReloadedEvent speakerSoundsReloadedEvent)
		{
			this.Stop();
			this._soundSystem.InvalidateSounds(base.GameObject);
			this.ValidateSoundId();
			this.PlayIfContinuous();
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00007CB9 File Offset: 0x00005EB9
		public bool IsPlaying
		{
			get
			{
				return Speaker.IsSoundIdValid(this._playedSoundId);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00007CC8 File Offset: 0x00005EC8
		public void PlayIfRequested()
		{
			if (this._playRequested)
			{
				this.UpdateMixer(this.SoundId);
				if (this.PlaybackMode == SpeakerPlaybackMode.Once)
				{
					this.PlayOnce(this.SoundId);
				}
				else
				{
					this.PlayLooped(this.SoundId);
				}
				this._playedSoundId = this.SoundId;
				this._playRequested = false;
				EventHandler playbackStateChanged = this.PlaybackStateChanged;
				if (playbackStateChanged == null)
				{
					return;
				}
				playbackStateChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00007D34 File Offset: 0x00005F34
		public void ValidateSoundId()
		{
			this.SoundId = this._speakerSoundService.GetValidatedSoundId(this.SoundId);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00007D50 File Offset: 0x00005F50
		public void EvaluatePlayback(bool isOn)
		{
			if (isOn)
			{
				if (this.PlaybackMode == SpeakerPlaybackMode.Once)
				{
					bool? previousState = this._previousState;
					bool flag = false;
					if (previousState.GetValueOrDefault() == flag & previousState != null)
					{
						goto IL_31;
					}
				}
				if (this.PlaybackMode != SpeakerPlaybackMode.Continuously)
				{
					goto IL_38;
				}
				IL_31:
				this.Play();
				return;
			}
			IL_38:
			if (!isOn && this.PlaybackMode == SpeakerPlaybackMode.Continuously)
			{
				this.Stop();
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00007DA7 File Offset: 0x00005FA7
		public void StopAndPlayIfContinuous()
		{
			this.Stop();
			this.PlayIfContinuous();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00007DB5 File Offset: 0x00005FB5
		public void PlayIfContinuous()
		{
			if (this._automatable.State == ConnectionState.On && this.PlaybackMode == SpeakerPlaybackMode.Continuously)
			{
				this.Play();
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00007DD4 File Offset: 0x00005FD4
		public void Stop()
		{
			if (Speaker.IsSoundIdValid(this._playedSoundId))
			{
				this._soundSystem.StopSound(base.GameObject, this._playedSoundId);
				this._playedSoundId = string.Empty;
				EventHandler playbackStateChanged = this.PlaybackStateChanged;
				if (playbackStateChanged == null)
				{
					return;
				}
				playbackStateChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00007E26 File Offset: 0x00006026
		public void Play()
		{
			if (Speaker.IsSoundIdValid(this.SoundId))
			{
				if (!string.IsNullOrWhiteSpace(this._playedSoundId))
				{
					this.Stop();
				}
				this._playRequested = true;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00007E4F File Offset: 0x0000604F
		public void UpdateMixer(string soundIdToPlay)
		{
			this._soundSystem.SetCustomMixer(base.GameObject, soundIdToPlay, (this.SpatialMode == SpeakerSpatialMode.Spatial) ? MixerNames.EnvironmentMixerNameKey : MixerNames.UIMixerNameKey);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00007E78 File Offset: 0x00006078
		public void PlayOnce(string soundId)
		{
			if (this.SpatialMode == SpeakerSpatialMode.NonSpatial)
			{
				this._soundSystem.PlaySound2D(base.GameObject, soundId, Speaker.Priority, 0f, new Action(this.OnPlaybackFinished));
				return;
			}
			this._soundSystem.PlaySound3D(base.GameObject, soundId, Speaker.Priority, new Action(this.OnPlaybackFinished));
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00007EDA File Offset: 0x000060DA
		public void PlayLooped(string soundId)
		{
			if (this.SpatialMode == SpeakerSpatialMode.NonSpatial)
			{
				this._soundSystem.LoopSingle2DSound(base.GameObject, soundId, Speaker.Priority);
				return;
			}
			this._soundSystem.LoopSingle3DSound(base.GameObject, soundId, Speaker.Priority);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007F14 File Offset: 0x00006114
		public void OnPlaybackFinished()
		{
			this._playedSoundId = string.Empty;
			EventHandler playbackStateChanged = this.PlaybackStateChanged;
			if (playbackStateChanged == null)
			{
				return;
			}
			playbackStateChanged(this, EventArgs.Empty);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00007F37 File Offset: 0x00006137
		public static bool IsSoundIdValid(string id)
		{
			return !string.IsNullOrWhiteSpace(id);
		}

		// Token: 0x04000149 RID: 329
		public static readonly int Priority = 5;

		// Token: 0x0400014A RID: 330
		public static readonly ComponentKey ComponentKey = new ComponentKey("Speaker");

		// Token: 0x0400014B RID: 331
		public static readonly PropertyKey<SpeakerPlaybackMode> PlaybackModeKey = new PropertyKey<SpeakerPlaybackMode>("PlaybackMode");

		// Token: 0x0400014C RID: 332
		public static readonly PropertyKey<SpeakerSpatialMode> SpatialModeKey = new PropertyKey<SpeakerSpatialMode>("SpatialMode");

		// Token: 0x0400014D RID: 333
		public static readonly PropertyKey<string> SoundIdKey = new PropertyKey<string>("SoundId");

		// Token: 0x04000152 RID: 338
		public readonly SpeakerSoundService _speakerSoundService;

		// Token: 0x04000153 RID: 339
		public readonly SpeakerPlayer _speakerPlayer;

		// Token: 0x04000154 RID: 340
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000155 RID: 341
		public readonly EventBus _eventBus;

		// Token: 0x04000156 RID: 342
		public Automatable _automatable;

		// Token: 0x04000157 RID: 343
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x04000158 RID: 344
		public bool? _previousState;

		// Token: 0x04000159 RID: 345
		public string _playedSoundId;

		// Token: 0x0400015A RID: 346
		public bool _playRequested;
	}
}
