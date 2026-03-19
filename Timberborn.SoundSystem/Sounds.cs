using System;
using System.Collections.Generic;
using System.Linq;
using Bindito.Core;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000017 RID: 23
	public class Sounds : MonoBehaviour
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000308F File Offset: 0x0000128F
		[Inject]
		public void InjectDependencies(AudioClipService audioClipService, IRandomNumberGenerator randomNumberGenerator, AudioSourceFactory audioSourceFactory)
		{
			this._audioClipService = audioClipService;
			this._randomNumberGenerator = randomNumberGenerator;
			this._audioSourceFactory = audioSourceFactory;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030A8 File Offset: 0x000012A8
		public AudioSource GetRandomSound(string soundName, Vector3 offset)
		{
			if (!this._sounds.ContainsKey(soundName))
			{
				this.AddSounds(soundName, offset);
			}
			List<AudioSource> list = this._sounds[soundName];
			AudioSource listElement = this._randomNumberGenerator.GetListElement<AudioSource>(list);
			if (list.Count > 1)
			{
				while (this._previousSound[soundName] == listElement.clip.name)
				{
					listElement = this._randomNumberGenerator.GetListElement<AudioSource>(list);
				}
			}
			this._previousSound[soundName] = listElement.clip.name;
			return listElement;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003134 File Offset: 0x00001334
		public IEnumerable<AudioSource> GetExistingSounds(string soundName)
		{
			List<AudioSource> result;
			if (this._sounds.TryGetValue(soundName, out result))
			{
				return result;
			}
			return Enumerable.Empty<AudioSource>();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003158 File Offset: 0x00001358
		public void OnDestroy()
		{
			this.InvalidateSounds();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003160 File Offset: 0x00001360
		public void SetCustomMixer(string customMixerName)
		{
			this._customMixerName = customMixerName;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000316C File Offset: 0x0000136C
		public void InvalidateSounds()
		{
			this._customMixerName = null;
			foreach (KeyValuePair<string, List<AudioSource>> keyValuePair in this._sounds)
			{
				foreach (AudioSource audioSource in keyValuePair.Value)
				{
					audioSource.Stop();
					Object.Destroy(audioSource);
				}
			}
			this._sounds.Clear();
			this._previousSound.Clear();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000321C File Offset: 0x0000141C
		public void AddSounds(string soundName, Vector3 offset)
		{
			GameObject audioSourceRoot = this.CreateAudioSourceRoot(soundName, offset);
			this.CreateAudioSources(soundName, audioSourceRoot);
			if (this._sounds[soundName].Count == 0)
			{
				throw new ArgumentException("No sound files for: " + soundName);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000325E File Offset: 0x0000145E
		public GameObject CreateAudioSourceRoot(string soundName, Vector3 offset)
		{
			return new GameObject("AudioSource " + soundName)
			{
				transform = 
				{
					parent = base.gameObject.transform,
					localPosition = offset
				}
			};
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003294 File Offset: 0x00001494
		public void CreateAudioSources(string soundName, GameObject audioSourceRoot)
		{
			this._sounds[soundName] = new List<AudioSource>();
			this._previousSound[soundName] = null;
			foreach (string soundName2 in this._audioClipService.GetAudioClipNames(soundName))
			{
				AudioSource item = this._audioSourceFactory.Create(audioSourceRoot, soundName2, this._customMixerName);
				this._sounds[soundName].Add(item);
			}
		}

		// Token: 0x0400003A RID: 58
		[HideInInspector]
		public AudioClipService _audioClipService;

		// Token: 0x0400003B RID: 59
		[HideInInspector]
		public IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400003C RID: 60
		[HideInInspector]
		public AudioSourceFactory _audioSourceFactory;

		// Token: 0x0400003D RID: 61
		[HideInInspector]
		public readonly Dictionary<string, List<AudioSource>> _sounds = new Dictionary<string, List<AudioSource>>();

		// Token: 0x0400003E RID: 62
		[HideInInspector]
		public readonly Dictionary<string, string> _previousSound = new Dictionary<string, string>();

		// Token: 0x0400003F RID: 63
		[HideInInspector]
		public string _customMixerName;
	}
}
