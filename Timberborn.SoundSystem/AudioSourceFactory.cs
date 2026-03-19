using System;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x0200000C RID: 12
	public class AudioSourceFactory
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002485 File Offset: 0x00000685
		public AudioSourceFactory(AudioClipService audioClipService, AudioMixerGroupRetriever audioMixerGroupRetriever)
		{
			this._audioClipService = audioClipService;
			this._audioMixerGroupRetriever = audioMixerGroupRetriever;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000249B File Offset: 0x0000069B
		public AudioSource Create(GameObject emitter, string soundName, string mixerName)
		{
			return this.Create(emitter, soundName, AudioSourceFactory.SoundCutOffDistance, mixerName);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024AC File Offset: 0x000006AC
		public AudioSource Create(GameObject emitter, string soundName, int cutoffDistance, string mixerName = null)
		{
			AudioClip audioClip = this._audioClipService.GetAudioClip(soundName);
			AudioSource audioSource = emitter.AddComponent<AudioSource>();
			audioSource.clip = audioClip;
			audioSource.playOnAwake = false;
			audioSource.dopplerLevel = 0f;
			audioSource.minDistance = 2f;
			audioSource.maxDistance = (float)cutoffDistance;
			audioSource.rolloffMode = 1;
			audioSource.outputAudioMixerGroup = (string.IsNullOrWhiteSpace(mixerName) ? this._audioMixerGroupRetriever.GetAudioMixerGroupFromSoundName(soundName) : this._audioMixerGroupRetriever.GetAudioMixerGroup(mixerName));
			return audioSource;
		}

		// Token: 0x04000012 RID: 18
		public static readonly int SoundCutOffDistance = 10;

		// Token: 0x04000013 RID: 19
		public readonly AudioClipService _audioClipService;

		// Token: 0x04000014 RID: 20
		public readonly AudioMixerGroupRetriever _audioMixerGroupRetriever;
	}
}
