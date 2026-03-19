using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;
using UnityEngine.Audio;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000009 RID: 9
	public class AudioMixerGroupRetriever : ILoadableSingleton
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000222E File Offset: 0x0000042E
		public AudioMixerGroupRetriever(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002248 File Offset: 0x00000448
		public void Load()
		{
			this._audioMixer = this._specService.GetSingleSpec<AudioMixerGroupRetrieverSpec>().AudioMixer.Asset;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002268 File Offset: 0x00000468
		public AudioMixerGroup GetAudioMixerGroup(string audioMixerGroupName)
		{
			AudioMixerGroup result;
			if (!this._audioMixerGroups.TryGetValue(audioMixerGroupName, out result))
			{
				return this.AddAudioMixerGroup(audioMixerGroupName);
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002290 File Offset: 0x00000490
		public AudioMixerGroup GetAudioMixerGroupFromSoundName(string soundName)
		{
			string[] array = soundName.Split('.', StringSplitOptions.None);
			return this.GetAudioMixerGroup(array[0]);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022B0 File Offset: 0x000004B0
		public void SetMixerParameter(string parameterName, float value)
		{
			this._audioMixer.SetFloat(parameterName, value);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022C0 File Offset: 0x000004C0
		public float GetMixerParameter(string parameterName)
		{
			float result;
			if (this._audioMixer.GetFloat(parameterName, ref result))
			{
				return result;
			}
			return 1f;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022E4 File Offset: 0x000004E4
		public AudioMixerGroup AddAudioMixerGroup(string audioMixerGroupName)
		{
			AudioMixerGroup audioMixerGroup = this._audioMixer.FindMatchingGroups(audioMixerGroupName).Single((AudioMixerGroup group) => group.name == audioMixerGroupName);
			this._audioMixerGroups.Add(audioMixerGroupName, audioMixerGroup);
			return audioMixerGroup;
		}

		// Token: 0x0400000D RID: 13
		public readonly ISpecService _specService;

		// Token: 0x0400000E RID: 14
		public readonly Dictionary<string, AudioMixerGroup> _audioMixerGroups = new Dictionary<string, AudioMixerGroup>();

		// Token: 0x0400000F RID: 15
		public AudioMixer _audioMixer;
	}
}
