using System;
using System.IO;
using Timberborn.SaveSystem;
using Timberborn.Versioning;
using Timberborn.WorldSerialization;

namespace Timberborn.VersioningSerialization
{
	// Token: 0x02000005 RID: 5
	public class VersionSerializer : IBackwardCompatibleSaveEntryReader<Version>, ISaveEntryReader<Version>, ISaveEntryWriter
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		public VersionSerializer(SaveReader saveReader, WorldSerializer worldSerializer)
		{
			this._saveReader = saveReader;
			this._worldSerializer = worldSerializer;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020F6 File Offset: 0x000002F6
		public string EntryName
		{
			get
			{
				return "version.txt";
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void WriteToSaveEntryStream(Stream entryStream)
		{
			using (StreamWriter streamWriter = new StreamWriter(entryStream))
			{
				streamWriter.WriteLine(GameVersions.CurrentVersion.Full);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002144 File Offset: 0x00000344
		public Version ReadFromSaveEntryStream(Stream entryStream)
		{
			Version result;
			using (StreamReader streamReader = new StreamReader(entryStream))
			{
				result = Version.Create(streamReader.ReadLine());
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002184 File Offset: 0x00000384
		public Version BackwardCompatibleRead(Stream fileStream)
		{
			SerializedWorld serializedWorld = this._saveReader.ReadFromSaveStream<SerializedWorld>(fileStream, this._worldSerializer);
			if (serializedWorld == null)
			{
				return Version.Create("0");
			}
			return serializedWorld.Version;
		}

		// Token: 0x04000006 RID: 6
		public readonly SaveReader _saveReader;

		// Token: 0x04000007 RID: 7
		public readonly WorldSerializer _worldSerializer;
	}
}
