using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AssetSystem;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000007 RID: 7
	public class AudioClipService
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AudioClipService(IAssetLoader assetLoader)
		{
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211C File Offset: 0x0000031C
		public void LoadAudioClips()
		{
			foreach (LoadedAsset<AudioClip> loadedAsset in this._assetLoader.LoadAll<AudioClip>(AudioClipService.SoundsDirectoryKey))
			{
				this._audioClips[loadedAsset.Asset.name] = loadedAsset.Asset;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000218C File Offset: 0x0000038C
		public void AddAudioClip(string id, AudioClip audioClip)
		{
			this._audioClips[id] = audioClip;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000219B File Offset: 0x0000039B
		public void RemoveAudioClip(string id)
		{
			this._audioClips.Remove(id);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021AA File Offset: 0x000003AA
		public AudioClip GetAudioClip(string soundName)
		{
			return this._audioClips[soundName];
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B8 File Offset: 0x000003B8
		public IEnumerable<string> GetAudioClipNames(string soundName)
		{
			string appendedName = soundName + "_";
			return from name in this._audioClips.Keys
			where name == soundName || name.StartsWith(appendedName)
			select name;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string SoundsDirectoryKey = "Sounds";

		// Token: 0x04000009 RID: 9
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000A RID: 10
		public readonly Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
	}
}
