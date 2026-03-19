using System;
using Bindito.Unity;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000018 RID: 24
	public class SoundSystem : ISoundSystem, ILoadableSingleton
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003342 File Offset: 0x00001542
		public SoundSystem(AudioClipService audioClipService, VolumeController volumeController, IInstantiator instantiator, RootObjectProvider rootObjectProvider, SoundEmitterRetriever soundEmitterRetriever)
		{
			this._audioClipService = audioClipService;
			this._volumeController = volumeController;
			this._instantiator = instantiator;
			this._rootObjectProvider = rootObjectProvider;
			this._soundEmitterRetriever = soundEmitterRetriever;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000336F File Offset: 0x0000156F
		public Vector3 ListenerPosition
		{
			get
			{
				return this._audioListener.transform.position;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003381 File Offset: 0x00001581
		public void Load()
		{
			this._audioClipService.LoadAudioClips();
			this._audioListener = this._rootObjectProvider.CreateRootObject("AudioListener").AddComponent<AudioListener>();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000033A9 File Offset: 0x000015A9
		public void SetListenerPosition(Vector3 position, Quaternion rotation)
		{
			this._audioListener.transform.SetPositionAndRotation(position, rotation);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000033BD File Offset: 0x000015BD
		public void SetMixerVolume(string name, float level)
		{
			this._volumeController.SetVolume(name, level);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000033CC File Offset: 0x000015CC
		public float GetMixerVolume(string name)
		{
			return this._volumeController.GetVolume(name);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000033DA File Offset: 0x000015DA
		public void SetMasterVolume(float level)
		{
			this._volumeController.SetMasterVolume(level);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000033E8 File Offset: 0x000015E8
		public void SetMusicVolume(float level)
		{
			this._volumeController.SetMusicVolume(level);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000033F6 File Offset: 0x000015F6
		public void SetUIVolume(float level)
		{
			this._volumeController.SetUIVolume(level);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003404 File Offset: 0x00001604
		public void SetEnvironmentVolume(float level)
		{
			this._volumeController.SetEnvironmentVolume(level);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003412 File Offset: 0x00001612
		public void PlaySound2D(GameObject emitter, string soundName, int priority)
		{
			this._soundEmitterRetriever.GetSoundEmitter(emitter).Start2D(soundName, priority);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003427 File Offset: 0x00001627
		public void PlaySound2D(GameObject emitter, string soundName, int priority, float delay, Action callback)
		{
			this._soundEmitterRetriever.GetSoundEmitter(emitter).Start2D(soundName, priority, delay, callback);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003440 File Offset: 0x00001640
		public void PlaySound3D(GameObject emitter, string soundName, int priority)
		{
			this._soundEmitterRetriever.GetSoundEmitter(emitter).Start3D(soundName, priority);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003455 File Offset: 0x00001655
		public void PlaySound3D(GameObject emitter, string soundName, int priority, Action callback)
		{
			this._soundEmitterRetriever.GetSoundEmitter(emitter).Start3D(soundName, priority, callback);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000346C File Offset: 0x0000166C
		public void AddLimitedAreaSound(Transform parent, string soundName, int priority, int cutoffDistance, string customMixer)
		{
			this._instantiator.AddComponent<LimitedAreaSoundController>(parent.gameObject).Initialize(soundName, priority, cutoffDistance, customMixer);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000348A File Offset: 0x0000168A
		public void AddAreaEmitter(Transform parent, GameObject emitter)
		{
			parent.GetComponent<LimitedAreaSoundController>().Add(emitter);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003498 File Offset: 0x00001698
		public void RemoveAreaEmitter(Transform parent, GameObject emitter)
		{
			parent.GetComponent<LimitedAreaSoundController>().Remove(emitter);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000034A6 File Offset: 0x000016A6
		public void AddLargeAreaSound(Transform parent, IEmitterMap emitterMap, string soundName, int priority, int cutoffDistance, string customMixer)
		{
			this._instantiator.AddComponent<LargeAreaSoundController>(parent.gameObject).Initialize(soundName, emitterMap, priority, cutoffDistance, customMixer);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000034C6 File Offset: 0x000016C6
		public void LoopSingle3DSound(GameObject emitter, string soundName, int priority)
		{
			this.LoopSingle3DSound(emitter, soundName, priority, Vector3.zero);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000034D6 File Offset: 0x000016D6
		public void LoopSingle3DSound(GameObject emitter, string soundName, int priority, Vector3 soundOffset)
		{
			this._soundEmitterRetriever.GetSoundEmitter(emitter).LoopSingle3DSound(soundName, priority, soundOffset);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000034ED File Offset: 0x000016ED
		public void LoopSingle2DSound(GameObject emitter, string soundName, int priority)
		{
			this._soundEmitterRetriever.GetSoundEmitter(emitter).LoopSingle2DSound(soundName, priority);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003502 File Offset: 0x00001702
		public void StopSound(GameObject emitter, string soundName)
		{
			this._soundEmitterRetriever.GetSoundEmitter(emitter).Stop(soundName);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003516 File Offset: 0x00001716
		public void SetCustomMixer(GameObject emitter, string soundName, string mixerName)
		{
			this._soundEmitterRetriever.GetSoundEmitter(emitter).SetCustomMixer(soundName, mixerName);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000352B File Offset: 0x0000172B
		public void InvalidateSounds(GameObject emitter)
		{
			this._soundEmitterRetriever.GetSoundEmitter(emitter).InvalidateSounds();
		}

		// Token: 0x04000040 RID: 64
		public readonly AudioClipService _audioClipService;

		// Token: 0x04000041 RID: 65
		public readonly VolumeController _volumeController;

		// Token: 0x04000042 RID: 66
		public readonly IInstantiator _instantiator;

		// Token: 0x04000043 RID: 67
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000044 RID: 68
		public readonly SoundEmitterRetriever _soundEmitterRetriever;

		// Token: 0x04000045 RID: 69
		public AudioListener _audioListener;
	}
}
