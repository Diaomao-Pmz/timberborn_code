using System;
using System.Collections.Generic;
using Bindito.Core;
using UnityEngine;
using UnityEngine.Audio;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000014 RID: 20
	public class SoundEmitter : MonoBehaviour
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002CD9 File Offset: 0x00000ED9
		[Inject]
		public void InjectDependencies(AudioMixerGroupRetriever audioMixerGroupRetriever, AudioSourceFader audioSourceFader)
		{
			this._audioMixerGroupRetriever = audioMixerGroupRetriever;
			this._audioSourceFader = audioSourceFader;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002CE9 File Offset: 0x00000EE9
		public void Awake()
		{
			this._sounds = base.GetComponent<Sounds>();
			this._loopingSoundPlayer = base.GetComponent<LoopingSoundPlayer>();
			base.enabled = false;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002D0A File Offset: 0x00000F0A
		public void Update()
		{
			this.ProcessCallbackSounds();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002D12 File Offset: 0x00000F12
		public void Start2D(string soundName, int priority)
		{
			this.StartSound(soundName, priority, 0f, 0f);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002D28 File Offset: 0x00000F28
		public void Start2D(string soundName, int priority, float delay, Action callback)
		{
			AudioSource key = this.StartSound(soundName, priority, 0f, delay);
			this._callbackSounds[key] = new SoundEmitter.CallbackSound(callback);
			base.enabled = true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D5E File Offset: 0x00000F5E
		public void Start3D(string soundName, int priority)
		{
			this.StartSound(soundName, priority, 1f, 0f);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002D74 File Offset: 0x00000F74
		public void Start3D(string soundName, int priority, Action callback)
		{
			AudioSource key = this.StartSound(soundName, priority, 1f, 0f);
			this._callbackSounds[key] = new SoundEmitter.CallbackSound(callback);
			base.enabled = true;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002DAD File Offset: 0x00000FAD
		public void LoopSingle2DSound(string soundName, int priority)
		{
			this._loopingSoundPlayer.PlayLooping2D(soundName, priority);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002DBC File Offset: 0x00000FBC
		public void LoopSingle3DSound(string soundName, int priority, Vector3 offset)
		{
			this._loopingSoundPlayer.PlayLooping3D(soundName, priority, offset);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002DCC File Offset: 0x00000FCC
		public void Stop(string soundName)
		{
			foreach (AudioSource audioSource in this._sounds.GetExistingSounds(soundName))
			{
				if (audioSource.isPlaying)
				{
					this._audioSourceFader.FadeOut(audioSource);
				}
				this.RemoveCallbackSound(audioSource);
			}
			this._loopingSoundPlayer.Stop(soundName);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E40 File Offset: 0x00001040
		public void SetCustomMixer(string soundName, string customMixerName)
		{
			AudioMixerGroup audioMixerGroup = this._audioMixerGroupRetriever.GetAudioMixerGroup(customMixerName);
			foreach (AudioSource audioSource in this._sounds.GetExistingSounds(soundName))
			{
				audioSource.outputAudioMixerGroup = audioMixerGroup;
			}
			this._sounds.SetCustomMixer(customMixerName);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002EAC File Offset: 0x000010AC
		public void InvalidateSounds()
		{
			this._sounds.InvalidateSounds();
			this._callbackSounds.Clear();
			this._callbackSoundsToProcess.Clear();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002ED0 File Offset: 0x000010D0
		public void ProcessCallbackSounds()
		{
			this._callbackSoundsToProcess.AddRange(this._callbackSounds.Keys);
			foreach (AudioSource audioSource in this._callbackSoundsToProcess)
			{
				SoundEmitter.CallbackSound callbackSound = this._callbackSounds[audioSource];
				float time = audioSource.time;
				if (time < callbackSound.PreviousTime)
				{
					this.RemoveCallbackSound(audioSource);
					callbackSound.Action();
				}
				else
				{
					callbackSound.PreviousTime = time;
				}
			}
			this._callbackSoundsToProcess.Clear();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002F78 File Offset: 0x00001178
		public AudioSource StartSound(string soundName, int priority, float spatialBlend, float delay)
		{
			AudioSource randomSound = this._sounds.GetRandomSound(soundName, Vector3.zero);
			this._audioSourceFader.RemoveFaders(randomSound);
			randomSound.priority = priority;
			randomSound.spatialBlend = spatialBlend;
			randomSound.volume = 1f;
			randomSound.pitch = 1f;
			randomSound.loop = false;
			randomSound.PlayDelayed(delay);
			return randomSound;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002FD7 File Offset: 0x000011D7
		public void RemoveCallbackSound(AudioSource audioSource)
		{
			this._callbackSounds.Remove(audioSource);
			if (this._callbackSounds.Count == 0)
			{
				base.enabled = false;
			}
		}

		// Token: 0x04000031 RID: 49
		[HideInInspector]
		public AudioMixerGroupRetriever _audioMixerGroupRetriever;

		// Token: 0x04000032 RID: 50
		[HideInInspector]
		public AudioSourceFader _audioSourceFader;

		// Token: 0x04000033 RID: 51
		[HideInInspector]
		public Sounds _sounds;

		// Token: 0x04000034 RID: 52
		[HideInInspector]
		public readonly Dictionary<AudioSource, SoundEmitter.CallbackSound> _callbackSounds = new Dictionary<AudioSource, SoundEmitter.CallbackSound>();

		// Token: 0x04000035 RID: 53
		[HideInInspector]
		public LoopingSoundPlayer _loopingSoundPlayer;

		// Token: 0x04000036 RID: 54
		[HideInInspector]
		public readonly List<AudioSource> _callbackSoundsToProcess = new List<AudioSource>();

		// Token: 0x02000015 RID: 21
		public class CallbackSound
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600007B RID: 123 RVA: 0x00003018 File Offset: 0x00001218
			public Action Action { get; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600007C RID: 124 RVA: 0x00003020 File Offset: 0x00001220
			// (set) Token: 0x0600007D RID: 125 RVA: 0x00003028 File Offset: 0x00001228
			public float PreviousTime { get; set; }

			// Token: 0x0600007E RID: 126 RVA: 0x00003031 File Offset: 0x00001231
			public CallbackSound(Action action)
			{
				this.Action = action;
			}
		}
	}
}
