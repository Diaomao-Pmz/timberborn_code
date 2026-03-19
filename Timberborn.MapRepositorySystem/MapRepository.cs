using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Timberborn.AssetSystem;
using Timberborn.FileSystem;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;

namespace Timberborn.MapRepositorySystem
{
	// Token: 0x02000006 RID: 6
	public class MapRepository
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000239C File Offset: 0x0000059C
		public MapRepository(IFileService fileService, IAssetLoader assetLoader, FilenameValidator filenameValidator, EventBus eventBus)
		{
			this._fileService = fileService;
			this._assetLoader = assetLoader;
			this._filenameValidator = filenameValidator;
			this._eventBus = eventBus;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023C1 File Offset: 0x000005C1
		public static string UserMapsDirectory
		{
			get
			{
				return Path.Combine(UserDataFolder.Folder, MapRepository.MapsDirectory);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D4 File Offset: 0x000005D4
		public Stream CreateUserMap(string mapName)
		{
			if (this._filenameValidator.NameIsInvalid(mapName))
			{
				throw new ArgumentException(mapName + " contains an illegal character");
			}
			this.CreateUserMapsDirectory();
			string fileName = this.UserMapNameToFileName(mapName);
			return this._fileService.CreateFile(fileName);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000241C File Offset: 0x0000061C
		public bool UserMapExists(string mapName)
		{
			string fileName = this.UserMapNameToFileName(mapName);
			return this._fileService.FileExists(fileName);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000243D File Offset: 0x0000063D
		public Stream OpenMap(MapFileReference mapFileReference)
		{
			if (!mapFileReference.Resource)
			{
				return this.OpenDiskMap(mapFileReference);
			}
			return this.OpenResourceMap(mapFileReference);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002457 File Offset: 0x00000657
		public IEnumerable<string> GetBuiltinMapNames()
		{
			return from loadedAsset in this._assetLoader.LoadAll<BinaryData>(MapRepository.MapsDirectory)
			select loadedAsset.Asset.name;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002490 File Offset: 0x00000690
		public IEnumerable<string> GetUserMapNames()
		{
			this.CreateUserMapsDirectory();
			return (from path in Directory.GetFiles(MapRepository.UserMapsDirectory)
			where Path.GetExtension(path) == MapRepository.MapExtension
			select path).Select(new Func<string, string>(Path.GetFileNameWithoutExtension));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024E2 File Offset: 0x000006E2
		public IEnumerable<FileInfo> GetMapFilesFromDirectory(DirectoryInfo directoryInfo)
		{
			return from fileInfo in directoryInfo.GetFiles()
			where fileInfo.Extension == MapRepository.MapExtension
			select fileInfo;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002510 File Offset: 0x00000710
		public void DeleteMap(MapFileReference mapFileReference)
		{
			if (mapFileReference.UserFolder)
			{
				string fileName = this.CustomMapNameToFileName(mapFileReference);
				this._fileService.DeleteFile(fileName);
				this.NotifyMapRepositoryChanged();
				return;
			}
			throw new NotSupportedException("Only user maps can be deleted.");
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000254B File Offset: 0x0000074B
		public void NotifyMapRepositoryChanged()
		{
			this._eventBus.Post(new MapRepositoryChangedEvent());
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000255D File Offset: 0x0000075D
		public string CustomMapNameToFileName(MapFileReference mapFileReference)
		{
			if (!mapFileReference.UserFolder)
			{
				return mapFileReference.Path;
			}
			return this.UserMapNameToFileName(mapFileReference.Name);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000257D File Offset: 0x0000077D
		public string MapNameWithExtension(string mapName)
		{
			return mapName + MapRepository.MapExtension;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000258C File Offset: 0x0000078C
		public Stream OpenDiskMap(MapFileReference mapFileReference)
		{
			string fileName = this.CustomMapNameToFileName(mapFileReference);
			return this._fileService.OpenFile(fileName);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025AD File Offset: 0x000007AD
		public string UserMapNameToFileName(string mapName)
		{
			return this._fileService.CombineIntoPath(MapRepository.UserMapsDirectory, this.MapNameWithExtension(mapName), "");
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025CB File Offset: 0x000007CB
		public void CreateUserMapsDirectory()
		{
			this._fileService.CreateDirectory(MapRepository.UserMapsDirectory);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025E0 File Offset: 0x000007E0
		public Stream OpenResourceMap(MapFileReference mapFileReference)
		{
			string text = MapRepository.MapsDirectory + "/" + mapFileReference.Name;
			BinaryData binaryData = this._assetLoader.Load<BinaryData>(text);
			if (binaryData == null)
			{
				throw new ArgumentException("Resource map " + text + " not found.");
			}
			return new MemoryStream(binaryData.Bytes);
		}

		// Token: 0x0400000D RID: 13
		public static readonly string MapsDirectory = "Maps";

		// Token: 0x0400000E RID: 14
		public static readonly string MapExtension = ".timber";

		// Token: 0x0400000F RID: 15
		public readonly IFileService _fileService;

		// Token: 0x04000010 RID: 16
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000011 RID: 17
		public readonly FilenameValidator _filenameValidator;

		// Token: 0x04000012 RID: 18
		public readonly EventBus _eventBus;
	}
}
