using System;
using System.IO;
using Timberborn.TextureOperations;
using UnityEngine;

namespace Timberborn.ThumbnailSystem
{
	// Token: 0x02000006 RID: 6
	public class ThumbnailSerializer
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002180 File Offset: 0x00000380
		public ThumbnailSerializer(TextureFactory textureFactory)
		{
			this._textureFactory = textureFactory;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002190 File Offset: 0x00000390
		public void WriteToSaveEntryStream(Stream entryStream, Texture2D texture, IThumbnailConfiguration thumbnailConfiguration)
		{
			using (StreamWriter streamWriter = new StreamWriter(entryStream))
			{
				byte[] array = ImageConversion.EncodeToJPG(texture, thumbnailConfiguration.Quality);
				streamWriter.BaseStream.Write(array);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E0 File Offset: 0x000003E0
		public Texture2D ReadFromSaveEntryStream(Stream entryStream, IThumbnailConfiguration thumbnailConfiguration)
		{
			Texture2D result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				entryStream.CopyTo(memoryStream);
				TextureSettings textureSettings = new TextureSettings.Builder().SetSize(thumbnailConfiguration.Width, thumbnailConfiguration.Height).SetTextureFormat(thumbnailConfiguration.TextureFormat).SetGenerateMipmap(false).Build();
				result = this._textureFactory.CreateTexture(textureSettings, memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x04000008 RID: 8
		public readonly TextureFactory _textureFactory;
	}
}
