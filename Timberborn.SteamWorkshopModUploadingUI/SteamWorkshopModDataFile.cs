using System;
using System.IO;
using Timberborn.Persistence;
using Timberborn.SerializationSystem;
using Timberborn.SteamWorkshop;

namespace Timberborn.SteamWorkshopModUploadingUI
{
	// Token: 0x02000004 RID: 4
	public class SteamWorkshopModDataFile
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public SteamWorkshopItem SteamWorkshopItem { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public SteamWorkshopModDataFile(SteamWorkshopItemSerializer steamWorkshopItemSerializer, SerializedObjectReaderWriter serializedObjectReaderWriter, FileInfo fileInfo)
		{
			this._steamWorkshopItemSerializer = steamWorkshopItemSerializer;
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
			this._fileInfo = fileInfo;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EC File Offset: 0x000002EC
		public static SteamWorkshopModDataFile Create(SteamWorkshopItemSerializer steamWorkshopItemSerializer, SerializedObjectReaderWriter serializedObjectReaderWriter, string originPath)
		{
			FileInfo fileInfo = new FileInfo(Path.Combine(originPath, SteamWorkshopModDataFile.WorkshopDataFileName));
			SteamWorkshopModDataFile steamWorkshopModDataFile = new SteamWorkshopModDataFile(steamWorkshopItemSerializer, serializedObjectReaderWriter, fileInfo);
			steamWorkshopModDataFile.LoadFromFile();
			return steamWorkshopModDataFile;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002118 File Offset: 0x00000318
		public void SaveSteamWorkshopItem(SteamWorkshopItem steamWorkshopItem)
		{
			this.SteamWorkshopItem = steamWorkshopItem;
			File.WriteAllText(this._fileInfo.FullName, this.GetSerializedData());
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public void LoadFromFile()
		{
			if (this._fileInfo.Exists)
			{
				string text = File.ReadAllText(this._fileInfo.FullName);
				ValueLoader valueLoader = new ValueLoader(this._serializedObjectReaderWriter.ReadJson(text));
				this.SteamWorkshopItem = this._steamWorkshopItemSerializer.Deserialize(valueLoader).Value;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002190 File Offset: 0x00000390
		public string GetSerializedData()
		{
			ValueSaver valueSaver = new ValueSaver();
			this._steamWorkshopItemSerializer.Serialize(this.SteamWorkshopItem, valueSaver);
			return this._serializedObjectReaderWriter.WriteJson((SerializedObject)valueSaver.Value);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string WorkshopDataFileName = "workshop_data.json";

		// Token: 0x04000008 RID: 8
		public readonly SteamWorkshopItemSerializer _steamWorkshopItemSerializer;

		// Token: 0x04000009 RID: 9
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;

		// Token: 0x0400000A RID: 10
		public readonly FileInfo _fileInfo;
	}
}
