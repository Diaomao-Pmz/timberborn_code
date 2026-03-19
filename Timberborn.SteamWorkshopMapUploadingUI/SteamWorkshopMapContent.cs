using System;
using System.IO;
using Timberborn.Common;
using Timberborn.FileSystem;
using Timberborn.MapRepositorySystem;
using Timberborn.PlatformUtilities;
using UnityEngine;

namespace Timberborn.SteamWorkshopMapUploadingUI
{
	// Token: 0x02000004 RID: 4
	public class SteamWorkshopMapContent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public string ContentDirectory { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020D7 File Offset: 0x000002D7
		public string ThumbnailPath { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		public Texture2D Thumbnail { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		public SteamWorkshopMapContent(IFileService fileService, MapRepository mapRepository, Texture2D thumbnail, MapFileReference mapFileReference)
		{
			this._fileService = fileService;
			this._mapRepository = mapRepository;
			this.Thumbnail = thumbnail;
			this._mapFileReference = mapFileReference;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public void CreateTemporaryFiles(string workshopMapName)
		{
			Asserts.FieldIsNull<SteamWorkshopMapContent>(this, this.ContentDirectory, "ContentDirectory");
			this._fileService.CreateDirectory(SteamWorkshopMapContent.WorkshopDirectory);
			Guid guid = Guid.NewGuid();
			this.CreateContentDirectory(guid, workshopMapName);
			this.CreateThumbnailFile(guid);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002154 File Offset: 0x00000354
		public void DeleteTemporaryFiles()
		{
			Asserts.FieldIsNotNull<SteamWorkshopMapContent>(this, this.ContentDirectory, "ContentDirectory");
			this._fileService.DeleteDirectory(this.ContentDirectory);
			this._fileService.DeleteFile(this.ThumbnailPath);
			this.ContentDirectory = null;
			this.ThumbnailPath = null;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021A2 File Offset: 0x000003A2
		public static string WorkshopDirectory
		{
			get
			{
				return Path.Combine(UserDataFolder.Folder, SteamWorkshopMapContent.WorkshopDirectoryName);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B4 File Offset: 0x000003B4
		public void CreateContentDirectory(Guid guid, string workshopMapName)
		{
			this.ContentDirectory = Path.Combine(SteamWorkshopMapContent.WorkshopDirectory, guid.ToString());
			this._fileService.CreateDirectory(this.ContentDirectory);
			string sourceFileName = this._mapRepository.CustomMapNameToFileName(this._mapFileReference);
			string destinationFileName = Path.Combine(this.ContentDirectory, this._mapRepository.MapNameWithExtension(workshopMapName));
			this._fileService.CopyFile(sourceFileName, destinationFileName);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002228 File Offset: 0x00000428
		public void CreateThumbnailFile(Guid guid)
		{
			string workshopDirectory = SteamWorkshopMapContent.WorkshopDirectory;
			Guid guid2 = guid;
			this.ThumbnailPath = Path.Combine(workshopDirectory, guid2.ToString() + SteamWorkshopMapContent.ThumbnailExtension);
			using (Stream stream = this._fileService.CreateFile(this.ThumbnailPath))
			{
				byte[] array = ImageConversion.EncodeToPNG(this.Thumbnail);
				stream.Write(array);
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly string WorkshopDirectoryName = "Workshop_temp";

		// Token: 0x04000007 RID: 7
		public static readonly string ThumbnailExtension = ".png";

		// Token: 0x0400000B RID: 11
		public readonly IFileService _fileService;

		// Token: 0x0400000C RID: 12
		public readonly MapRepository _mapRepository;

		// Token: 0x0400000D RID: 13
		public readonly MapFileReference _mapFileReference;
	}
}
