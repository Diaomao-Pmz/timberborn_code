using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Timberborn.FileSystem;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using UnityEngine;
using UnityEngine.Networking;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000041 RID: 65
	public class SpeakerCustomSoundLoader : IUnloadableSingleton
	{
		// Token: 0x060002DA RID: 730 RVA: 0x000080FE File Offset: 0x000062FE
		public SpeakerCustomSoundLoader(IFileService fileService, AudioClipService audioClipService)
		{
			this._fileService = fileService;
			this._audioClipService = audioClipService;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000811F File Offset: 0x0000631F
		public void Unload()
		{
			this.UnloadCustomSounds();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00008127 File Offset: 0x00006327
		public IEnumerable<AudioClip> LoadCustomSounds(string directory)
		{
			this._fileService.CreateDirectory(directory);
			this.UnloadCustomSounds();
			this.LoadSounds(directory);
			return this._loadedSounds;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00008148 File Offset: 0x00006348
		public void LoadSounds(string directory)
		{
			foreach (string path2 in from path in Directory.GetFiles(directory)
			where SpeakerCustomSoundLoader.ExtensionToAudioType.ContainsKey(SpeakerCustomSoundLoader.GetExtension(path))
			select path)
			{
				this.LoadAudioFile(path2);
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000081BC File Offset: 0x000063BC
		public void LoadAudioFile(string path)
		{
			try
			{
				long ticks = DateTime.Now.Ticks;
				AudioType audioType = SpeakerCustomSoundLoader.ExtensionToAudioType[SpeakerCustomSoundLoader.GetExtension(path)];
				using (UnityWebRequest audioClip = UnityWebRequestMultimedia.GetAudioClip("file://" + path, audioType))
				{
					audioClip.SendWebRequest();
					while (!audioClip.isDone && DateTime.Now.Ticks - ticks <= SpeakerCustomSoundLoader.LoadingTimeout.Ticks)
					{
					}
					if (audioClip.result == 1)
					{
						this.CreateAudioClip(audioClip);
					}
					else
					{
						Debug.LogError(string.Format("Request failure when loading audio from: {0}: {1}.", path, audioClip.result));
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Failed to load audio from: ",
					path,
					": ",
					ex.Message,
					"."
				}));
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000082B8 File Offset: 0x000064B8
		public void UnloadCustomSounds()
		{
			foreach (AudioClip audioClip in this._loadedSounds)
			{
				this._audioClipService.RemoveAudioClip(audioClip.name);
				Object.Destroy(audioClip);
			}
			this._loadedSounds.Clear();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00008328 File Offset: 0x00006528
		public static string GetExtension(string path)
		{
			return Path.GetExtension(path).ToLowerInvariant();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00008338 File Offset: 0x00006538
		public void CreateAudioClip(UnityWebRequest request)
		{
			AudioClip content = DownloadHandlerAudioClip.GetContent(request);
			string fileName = Path.GetFileName(request.url);
			content.name = fileName;
			this._audioClipService.AddAudioClip(fileName, content);
			this._loadedSounds.Add(content);
		}

		// Token: 0x04000162 RID: 354
		public static readonly TimeSpan LoadingTimeout = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000163 RID: 355
		public static readonly Dictionary<string, AudioType> ExtensionToAudioType = new Dictionary<string, AudioType>
		{
			{
				".wav",
				20
			},
			{
				".mp3",
				13
			}
		};

		// Token: 0x04000164 RID: 356
		public readonly IFileService _fileService;

		// Token: 0x04000165 RID: 357
		public readonly AudioClipService _audioClipService;

		// Token: 0x04000166 RID: 358
		public readonly List<AudioClip> _loadedSounds = new List<AudioClip>();
	}
}
