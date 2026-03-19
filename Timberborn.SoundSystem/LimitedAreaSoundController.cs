using System;
using System.Collections.Generic;
using Bindito.Core;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000012 RID: 18
	public class LimitedAreaSoundController : MonoBehaviour
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000029AD File Offset: 0x00000BAD
		[Inject]
		public void InjectDependencies(AudioSourceFactory audioSourceFactory, ISoundSystem soundSystem, AudioMixerGroupRetriever audioMixerGroupRetriever)
		{
			this._audioSourceFactory = audioSourceFactory;
			this._soundSystem = soundSystem;
			this._audioMixerGroupRetriever = audioMixerGroupRetriever;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000029C4 File Offset: 0x00000BC4
		public void Update()
		{
			if (this._dirty || this._listenerPosition != this._soundSystem.ListenerPosition)
			{
				this.UpdateAudioSource();
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000029EC File Offset: 0x00000BEC
		public void Initialize(string soundName, int priority, int cutoffDistance, string customMixer)
		{
			this._audioSource = this._audioSourceFactory.Create(base.gameObject, soundName, cutoffDistance, null);
			this._audioSource.priority = priority;
			this._audioSource.outputAudioMixerGroup = this._audioMixerGroupRetriever.GetAudioMixerGroup(customMixer);
			this._cutoffDistance = cutoffDistance;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002A3E File Offset: 0x00000C3E
		public void Add(GameObject emitter)
		{
			this._stationaryEmitters.Add(emitter);
			this._dirty = true;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002A53 File Offset: 0x00000C53
		public void Remove(GameObject emitter)
		{
			this._stationaryEmitters.Remove(emitter);
			this._dirty = true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002A69 File Offset: 0x00000C69
		public void OnDestroy()
		{
			this._audioSource.Stop();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002A78 File Offset: 0x00000C78
		public void UpdateAudioSource()
		{
			this._listenerPosition = this._soundSystem.ListenerPosition;
			this._dirty = false;
			float num = this.ClosestEmitterDistance();
			if (num < (float)this._cutoffDistance)
			{
				if (!this._audioSource.isPlaying)
				{
					this._audioSource.Play();
				}
				this._audioSource.volume = 1f - num / (float)this._cutoffDistance;
				return;
			}
			if (num >= (float)this._cutoffDistance && this._audioSource.isPlaying)
			{
				this._audioSource.Stop();
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B04 File Offset: 0x00000D04
		public float ClosestEmitterDistance()
		{
			float num = float.MaxValue;
			for (int i = 0; i < this._stationaryEmitters.Count; i++)
			{
				float num2 = Vector3.Distance(this._stationaryEmitters[i].transform.position, this._listenerPosition);
				if (num2 < num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x04000023 RID: 35
		[HideInInspector]
		public AudioSourceFactory _audioSourceFactory;

		// Token: 0x04000024 RID: 36
		[HideInInspector]
		public ISoundSystem _soundSystem;

		// Token: 0x04000025 RID: 37
		[HideInInspector]
		public AudioMixerGroupRetriever _audioMixerGroupRetriever;

		// Token: 0x04000026 RID: 38
		[HideInInspector]
		public readonly List<GameObject> _stationaryEmitters = new List<GameObject>();

		// Token: 0x04000027 RID: 39
		[HideInInspector]
		public AudioSource _audioSource;

		// Token: 0x04000028 RID: 40
		[HideInInspector]
		public int _cutoffDistance;

		// Token: 0x04000029 RID: 41
		[HideInInspector]
		public bool _dirty;

		// Token: 0x0400002A RID: 42
		[HideInInspector]
		public Vector3 _listenerPosition;
	}
}
