using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Timberborn.FileSystem;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using Timberborn.TextureOperations;
using UnityEngine;

namespace Timberborn.DecalSystem
{
	// Token: 0x02000018 RID: 24
	public class UserDecalTextureRepository : IUnloadableSingleton
	{
		// Token: 0x06000096 RID: 150 RVA: 0x0000331B File Offset: 0x0000151B
		public UserDecalTextureRepository(IFileService fileService, TextureFactory textureFactory)
		{
			this._fileService = fileService;
			this._textureFactory = textureFactory;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000333C File Offset: 0x0000153C
		public void Unload()
		{
			this.UnloadAllTextures();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003344 File Offset: 0x00001544
		public IEnumerable<Texture2D> LoadCustomTextures(string category)
		{
			if (!this._loadedTextures.ContainsKey(category))
			{
				this._loadedTextures[category] = new List<Texture2D>();
			}
			string text = Path.Combine(UserDataFolder.Folder, category);
			this._fileService.CreateDirectory(text);
			IEnumerable<string> paths = from path in Directory.GetFiles(text)
			where UserDecalTextureRepository.ValidExtensions.Contains(Path.GetExtension(path))
			select path;
			this.UnloadTextures(category);
			this.LoadTextures(category, paths);
			return this._loadedTextures[category];
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000033CE File Offset: 0x000015CE
		public string GetCustomDecalDirectory(string category)
		{
			return Path.Combine(UserDataFolder.Folder, category);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000033DC File Offset: 0x000015DC
		public void LoadTextures(string category, IEnumerable<string> paths)
		{
			foreach (string text in paths)
			{
				try
				{
					byte[] bytes = File.ReadAllBytes(text);
					TextureSettings textureSettings = new TextureSettings.Builder().SetFilterMode(1).SetName(Path.GetFileName(text)).Build();
					Texture2D item;
					if (this._textureFactory.TryCreateTexture(textureSettings, bytes, out item))
					{
						this._loadedTextures[category].Add(item);
					}
				}
				catch (IOException)
				{
					Debug.LogError("Failed to load tail texture from path: " + text);
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003488 File Offset: 0x00001688
		public void UnloadAllTextures()
		{
			foreach (string category in this._loadedTextures.Keys)
			{
				this.UnloadTextures(category);
			}
			this._loadedTextures.Clear();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000034EC File Offset: 0x000016EC
		public void UnloadTextures(string category)
		{
			List<Texture2D> list;
			if (this._loadedTextures.TryGetValue(category, out list))
			{
				foreach (Texture2D texture2D in list)
				{
					Object.Destroy(texture2D);
				}
				list.Clear();
			}
		}

		// Token: 0x04000036 RID: 54
		public static readonly string[] ValidExtensions = new string[]
		{
			".png",
			".jpg",
			".jpeg"
		};

		// Token: 0x04000037 RID: 55
		public readonly IFileService _fileService;

		// Token: 0x04000038 RID: 56
		public readonly TextureFactory _textureFactory;

		// Token: 0x04000039 RID: 57
		public readonly Dictionary<string, List<Texture2D>> _loadedTextures = new Dictionary<string, List<Texture2D>>();
	}
}
