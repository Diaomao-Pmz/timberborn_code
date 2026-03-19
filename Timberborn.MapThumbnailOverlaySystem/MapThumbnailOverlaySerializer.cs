using System;
using System.IO;
using Timberborn.SaveSystem;
using UnityEngine;

namespace Timberborn.MapThumbnailOverlaySystem
{
	// Token: 0x02000007 RID: 7
	public class MapThumbnailOverlaySerializer : ISaveEntryReader<byte[]>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002439 File Offset: 0x00000639
		public string EntryName
		{
			get
			{
				return "map_overlay.png";
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002440 File Offset: 0x00000640
		public void WriteToSaveEntryStream(Stream entryStream, Texture2D texture)
		{
			using (StreamWriter streamWriter = new StreamWriter(entryStream))
			{
				streamWriter.BaseStream.Write(ImageConversion.EncodeToPNG(texture));
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002488 File Offset: 0x00000688
		public byte[] ReadFromSaveEntryStream(Stream entryStream)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				entryStream.CopyTo(memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}
	}
}
