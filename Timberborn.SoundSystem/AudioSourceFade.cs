using System;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x0200000D RID: 13
	public readonly struct AudioSourceFade
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002532 File Offset: 0x00000732
		public float DelayEndTime { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000253A File Offset: 0x0000073A
		public float FadeEndTime { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002542 File Offset: 0x00000742
		public float TargetVolume { get; }

		// Token: 0x0600002E RID: 46 RVA: 0x0000254A File Offset: 0x0000074A
		public AudioSourceFade(float delayEndTime, float fadeEndTime, float targetVolume)
		{
			this.DelayEndTime = delayEndTime;
			this.FadeEndTime = fadeEndTime;
			this.TargetVolume = targetVolume;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002564 File Offset: 0x00000764
		public static AudioSourceFade FadeIn(float delay, float fadeLength)
		{
			float unscaledTime = Time.unscaledTime;
			return new AudioSourceFade(unscaledTime + delay, unscaledTime + delay + fadeLength, 1f);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002589 File Offset: 0x00000789
		public static AudioSourceFade FadeOut(float fadeLength)
		{
			return new AudioSourceFade(Time.unscaledTime, Time.unscaledTime + fadeLength, 0f);
		}
	}
}
