using System;
using System.IO;
using Timberborn.ErrorReporting;
using Timberborn.SaveSystem;
using Timberborn.WorldSerialization;

namespace Timberborn.MapRepositorySystem
{
	// Token: 0x02000004 RID: 4
	public class MapDeserializer
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public MapDeserializer(MapRepository mapRepository, SaveReader saveReader, WorldSerializer worldSerializer)
		{
			this._mapRepository = mapRepository;
			this._saveReader = saveReader;
			this._worldSerializer = worldSerializer;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
		public SerializedWorld Load(MapFileReference mapFileReference)
		{
			SerializedWorld result;
			using (Stream stream = this._mapRepository.OpenMap(mapFileReference))
			{
				if (!mapFileReference.Resource)
				{
					WorldDataService.SetFromStream(this._mapRepository.CustomMapNameToFileName(mapFileReference), stream);
				}
				result = this._saveReader.ReadFromSaveStreamUnsafe<SerializedWorld>(stream, this._worldSerializer);
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002144 File Offset: 0x00000344
		public T ReadFromMapFile<T>(MapFileReference mapFileReference, ISaveEntryReader<T> saveEntryReader)
		{
			T result;
			using (Stream stream = this._mapRepository.OpenMap(mapFileReference))
			{
				result = this._saveReader.ReadFromSaveStream<T>(stream, saveEntryReader);
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000218C File Offset: 0x0000038C
		public T ReadFromMapFileUnsafe<T>(MapFileReference mapFileReference, ISaveEntryReader<T> saveEntryReader)
		{
			T result;
			using (Stream stream = this._mapRepository.OpenMap(mapFileReference))
			{
				result = this._saveReader.ReadFromSaveStreamUnsafe<T>(stream, saveEntryReader);
			}
			return result;
		}

		// Token: 0x04000006 RID: 6
		public readonly MapRepository _mapRepository;

		// Token: 0x04000007 RID: 7
		public readonly SaveReader _saveReader;

		// Token: 0x04000008 RID: 8
		public readonly WorldSerializer _worldSerializer;
	}
}
