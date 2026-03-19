using System;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x0200001A RID: 26
	public class VolumeController
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x000035AE File Offset: 0x000017AE
		public VolumeController(AudioMixerGroupRetriever audioMixerGroupRetriever)
		{
			this._audioMixerGroupRetriever = audioMixerGroupRetriever;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000035BD File Offset: 0x000017BD
		public void SetMasterVolume(float level)
		{
			this.SetVolume("GameMaster_Volume", level);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000035CB File Offset: 0x000017CB
		public void SetMusicVolume(float level)
		{
			this.SetVolume("Music_Volume", level);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000035D9 File Offset: 0x000017D9
		public void SetUIVolume(float level)
		{
			this.SetVolume("UI_Volume", level);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000035E7 File Offset: 0x000017E7
		public void SetEnvironmentVolume(float level)
		{
			this.SetVolume("Environment_Volume", level);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000035F8 File Offset: 0x000017F8
		public void SetVolume(string parameter, float level)
		{
			float value = (level < 0.001f) ? -80f : (Mathf.Log(level) * (float)VolumeController.Attenuation);
			this._audioMixerGroupRetriever.SetMixerParameter(parameter, value);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000362F File Offset: 0x0000182F
		public float GetVolume(string parameter)
		{
			return Mathf.Exp(this._audioMixerGroupRetriever.GetMixerParameter(parameter) / (float)VolumeController.Attenuation);
		}

		// Token: 0x04000046 RID: 70
		public static readonly int Attenuation = 20;

		// Token: 0x04000047 RID: 71
		public readonly AudioMixerGroupRetriever _audioMixerGroupRetriever;
	}
}
