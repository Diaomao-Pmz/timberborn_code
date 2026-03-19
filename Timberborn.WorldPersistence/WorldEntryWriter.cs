using System;
using System.IO;
using Timberborn.SaveSystem;
using Timberborn.WorldSerialization;

namespace Timberborn.WorldPersistence
{
	// Token: 0x0200001C RID: 28
	public class WorldEntryWriter : ISaveEntryWriter
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002A58 File Offset: 0x00000C58
		public WorldEntryWriter(SerializedWorldFactory serializedWorldFactory, WorldSerializer worldSerializer)
		{
			this._serializedWorldFactory = serializedWorldFactory;
			this._worldSerializer = worldSerializer;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002A6E File Offset: 0x00000C6E
		public string EntryName
		{
			get
			{
				return this._worldSerializer.EntryName;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A7C File Offset: 0x00000C7C
		public void WriteToSaveEntryStream(Stream entryStream)
		{
			SerializedWorld serializedWorld = this._serializedWorldFactory.Create();
			this._worldSerializer.WriteToSaveEntryStream(entryStream, serializedWorld);
		}

		// Token: 0x04000020 RID: 32
		public readonly SerializedWorldFactory _serializedWorldFactory;

		// Token: 0x04000021 RID: 33
		public readonly WorldSerializer _worldSerializer;
	}
}
