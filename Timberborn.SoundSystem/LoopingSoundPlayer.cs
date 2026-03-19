using System;
using Bindito.Core;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000013 RID: 19
	public class LoopingSoundPlayer : MonoBehaviour
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002B69 File Offset: 0x00000D69
		[Inject]
		public void InjectDependencies(IRandomNumberGenerator randomNumberGenerator, AudioSourceFader audioSourceFader)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._audioSourceFader = audioSourceFader;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B79 File Offset: 0x00000D79
		public void Awake()
		{
			this._sounds = base.GetComponent<Sounds>();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002B87 File Offset: 0x00000D87
		public void PlayLooping2D(string soundName, int priority)
		{
			this.PlayLooping(soundName, priority, 0f, Vector3.zero);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002B9B File Offset: 0x00000D9B
		public void PlayLooping3D(string soundName, int priority, Vector3 offset)
		{
			this.PlayLooping(soundName, priority, 1f, offset);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002BAB File Offset: 0x00000DAB
		public void Stop(string soundName)
		{
			if (this._currentlyPlaying && this._currentlyPlaying.name == soundName)
			{
				this._currentlyPlaying = null;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public void ThrowIfAlreadyLooping()
		{
			if (this._currentlyPlaying != null)
			{
				throw new InvalidOperationException("This SoundEmitter is currently playing another sound in a loop: " + this._currentlyPlaying.name);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C00 File Offset: 0x00000E00
		public void PlayLooping(string soundName, int priority, float spatialBlend, Vector3 offset)
		{
			this.ThrowIfAlreadyLooping();
			this._currentlyPlaying = this._sounds.GetRandomSound(soundName, offset);
			this._currentlyPlaying.name = soundName;
			this._currentlyPlaying.priority = priority;
			this._currentlyPlaying.spatialBlend = spatialBlend;
			this._currentlyPlaying.loop = true;
			this._currentlyPlaying.pitch = this._randomNumberGenerator.Range(0.9f, 1.1f);
			float num = this._randomNumberGenerator.Range(LoopingSoundPlayer.MinDelay, LoopingSoundPlayer.MaxDelay);
			this._currentlyPlaying.PlayDelayed(num);
			this._currentlyPlaying.volume = 0f;
			this._audioSourceFader.FadeIn(this._currentlyPlaying, num);
		}

		// Token: 0x0400002B RID: 43
		[HideInInspector]
		public static readonly float MinDelay = 0f;

		// Token: 0x0400002C RID: 44
		[HideInInspector]
		public static readonly float MaxDelay = 0.5f;

		// Token: 0x0400002D RID: 45
		[HideInInspector]
		public IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400002E RID: 46
		[HideInInspector]
		public AudioSourceFader _audioSourceFader;

		// Token: 0x0400002F RID: 47
		[HideInInspector]
		public Sounds _sounds;

		// Token: 0x04000030 RID: 48
		[HideInInspector]
		public AudioSource _currentlyPlaying;
	}
}
