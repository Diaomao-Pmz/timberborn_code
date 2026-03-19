using System;
using System.Collections.Generic;
using Bindito.Core;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000011 RID: 17
	public class LargeAreaSoundController : MonoBehaviour
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002776 File Offset: 0x00000976
		[Inject]
		public void InjectDependencies(AudioSourceFactory audioSourceFactory, ISoundSystem soundSystem, AudioMixerGroupRetriever audioMixerGroupRetriever)
		{
			this._audioSourceFactory = audioSourceFactory;
			this._soundSystem = soundSystem;
			this._audioMixerGroupRetriever = audioMixerGroupRetriever;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000278D File Offset: 0x0000098D
		public void Update()
		{
			if (this._listenerPosition != this._soundSystem.ListenerPosition)
			{
				this.UpdateAudioSource();
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000027B0 File Offset: 0x000009B0
		public void Initialize(string soundName, IEmitterMap emitterMap, int priority, int cutoffDistance, string customMixer)
		{
			this._emitterMap = emitterMap;
			this._audioSource = this._audioSourceFactory.Create(base.gameObject, soundName, cutoffDistance, null);
			this._audioSource.priority = priority;
			this._audioSource.outputAudioMixerGroup = this._audioMixerGroupRetriever.GetAudioMixerGroup(customMixer);
			this._cutoffDistance = cutoffDistance;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000280B File Offset: 0x00000A0B
		public void OnDestroy()
		{
			this._audioSource.Stop();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002818 File Offset: 0x00000A18
		public void UpdateAudioSource()
		{
			this._listenerPosition = this._soundSystem.ListenerPosition;
			float? num = this.ClosestEmitterDistance();
			if (num != null)
			{
				float valueOrDefault = num.GetValueOrDefault();
				if (!this._audioSource.isPlaying)
				{
					this._audioSource.Play();
				}
				this._audioSource.volume = Mathf.Clamp01(1f - valueOrDefault / (float)this._cutoffDistance);
				return;
			}
			if (this._audioSource.isPlaying)
			{
				this._audioSource.Stop();
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000028A0 File Offset: 0x00000AA0
		public float? ClosestEmitterDistance()
		{
			Vector2Int vector2Int = new Vector2(this._listenerPosition.x, this._listenerPosition.z).FloorToInt();
			this._tilesToCheck.Clear();
			this._tilesToCheck.Enqueue(vector2Int);
			int num = Mathf.CeilToInt((float)(this._cutoffDistance * this._cutoffDistance) * 3.14f);
			for (int i = 0; i < num; i++)
			{
				Vector2Int vector2Int2 = this._tilesToCheck.Dequeue();
				if (this._emitterMap.IsEmitterAt(vector2Int2))
				{
					return new float?(Vector2Int.Distance(vector2Int2, vector2Int));
				}
				this._tilesToCheck.Enqueue(vector2Int2 + Vector2Int.down);
				this._tilesToCheck.Enqueue(vector2Int2 + Vector2Int.left);
				this._tilesToCheck.Enqueue(vector2Int2 + Vector2Int.up);
				this._tilesToCheck.Enqueue(vector2Int2 + Vector2Int.right);
			}
			return null;
		}

		// Token: 0x0400001B RID: 27
		[HideInInspector]
		public AudioSourceFactory _audioSourceFactory;

		// Token: 0x0400001C RID: 28
		[HideInInspector]
		public ISoundSystem _soundSystem;

		// Token: 0x0400001D RID: 29
		[HideInInspector]
		public AudioMixerGroupRetriever _audioMixerGroupRetriever;

		// Token: 0x0400001E RID: 30
		[HideInInspector]
		public IEmitterMap _emitterMap;

		// Token: 0x0400001F RID: 31
		[HideInInspector]
		public AudioSource _audioSource;

		// Token: 0x04000020 RID: 32
		[HideInInspector]
		public int _cutoffDistance;

		// Token: 0x04000021 RID: 33
		[HideInInspector]
		public Vector3 _listenerPosition;

		// Token: 0x04000022 RID: 34
		[HideInInspector]
		public readonly Queue<Vector2Int> _tilesToCheck = new Queue<Vector2Int>();
	}
}
