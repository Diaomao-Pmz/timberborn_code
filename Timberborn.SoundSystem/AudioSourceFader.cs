using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x0200000E RID: 14
	public class AudioSourceFader : IUpdatableSingleton
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000025A1 File Offset: 0x000007A1
		public void UpdateSingleton()
		{
			this.ProgressFades();
			this.RemoveFinished();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025AF File Offset: 0x000007AF
		public void FadeIn(AudioSource audioSource, float delay)
		{
			this._audioSourceFades[audioSource] = AudioSourceFade.FadeIn(delay, AudioSourceFader.FadeLengthInSeconds);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025C8 File Offset: 0x000007C8
		public void FadeOut(AudioSource audioSource)
		{
			this._audioSourceFades[audioSource] = AudioSourceFade.FadeOut(AudioSourceFader.FadeLengthInSeconds);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025E0 File Offset: 0x000007E0
		public void RemoveFaders(AudioSource audioSource)
		{
			this._audioSourceFades.Remove(audioSource);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025F0 File Offset: 0x000007F0
		public void ProgressFades()
		{
			foreach (KeyValuePair<AudioSource, AudioSourceFade> keyValuePair in this._audioSourceFades)
			{
				AudioSource audioSource;
				AudioSourceFade audioSourceFade;
				keyValuePair.Deconstruct(ref audioSource, ref audioSourceFade);
				AudioSource audioSource2 = audioSource;
				AudioSourceFade audioSourceFade2 = audioSourceFade;
				if (audioSourceFade2.DelayEndTime < Time.unscaledTime)
				{
					if (audioSource2 && audioSourceFade2.FadeEndTime > Time.unscaledTime)
					{
						AudioSourceFader.ProgressFade(audioSource2, audioSourceFade2);
					}
					else
					{
						this.EndFade(audioSource2, audioSourceFade2);
					}
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002684 File Offset: 0x00000884
		public static void ProgressFade(AudioSource audioSource, AudioSourceFade audioSourceFade)
		{
			float num = AudioSourceFader.FadeLengthInSeconds - (audioSourceFade.FadeEndTime - Time.unscaledTime);
			audioSource.volume = Mathf.Lerp(audioSource.volume, audioSourceFade.TargetVolume, num / AudioSourceFader.FadeLengthInSeconds);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026C4 File Offset: 0x000008C4
		public void EndFade(AudioSource audioSource, AudioSourceFade audioSourceFade)
		{
			if (audioSource)
			{
				audioSource.volume = audioSourceFade.TargetVolume;
			}
			this._fadesToRemove.Add(audioSource);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026E8 File Offset: 0x000008E8
		public void RemoveFinished()
		{
			foreach (AudioSource key in this._fadesToRemove)
			{
				this._audioSourceFades.Remove(key);
			}
			this._fadesToRemove.Clear();
		}

		// Token: 0x04000018 RID: 24
		public static readonly float FadeLengthInSeconds = 3f;

		// Token: 0x04000019 RID: 25
		public readonly Dictionary<AudioSource, AudioSourceFade> _audioSourceFades = new Dictionary<AudioSource, AudioSourceFade>();

		// Token: 0x0400001A RID: 26
		public readonly List<AudioSource> _fadesToRemove = new List<AudioSource>();
	}
}
