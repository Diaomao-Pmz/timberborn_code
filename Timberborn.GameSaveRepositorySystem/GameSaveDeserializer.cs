using System;
using System.IO;
using Timberborn.ErrorReporting;
using Timberborn.SaveSystem;
using Timberborn.WorldSerialization;

namespace Timberborn.GameSaveRepositorySystem
{
	// Token: 0x02000007 RID: 7
	public class GameSaveDeserializer
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public GameSaveDeserializer(GameSaveRepository gameSaveRepository, SaveReader saveReader, WorldSerializer worldSerializer)
		{
			this._gameSaveRepository = gameSaveRepository;
			this._saveReader = saveReader;
			this._worldSerializer = worldSerializer;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211C File Offset: 0x0000031C
		public SerializedWorld Load(SaveReference saveReference)
		{
			string fileName = this._gameSaveRepository.SaveNameToFileName(saveReference);
			SerializedWorld result;
			using (Stream stream = this._gameSaveRepository.OpenSave(saveReference))
			{
				WorldDataService.SetFromStream(fileName, stream);
				result = this._saveReader.ReadFromSaveStreamUnsafe<SerializedWorld>(stream, this._worldSerializer);
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000217C File Offset: 0x0000037C
		public T ReadFromSaveFile<T>(SaveReference saveReference, ISaveEntryReader<T> saveEntryReader)
		{
			T result;
			using (Stream stream = this._gameSaveRepository.OpenSaveWithoutLogging(saveReference))
			{
				result = this._saveReader.ReadFromSaveStream<T>(stream, saveEntryReader);
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C4 File Offset: 0x000003C4
		public T ReadFromSaveFileUnsafe<T>(SaveReference saveReference, ISaveEntryReader<T> saveEntryReader)
		{
			T result;
			using (Stream stream = this._gameSaveRepository.OpenSaveWithoutLogging(saveReference))
			{
				result = this._saveReader.ReadFromSaveStreamUnsafe<T>(stream, saveEntryReader);
			}
			return result;
		}

		// Token: 0x04000008 RID: 8
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000009 RID: 9
		public readonly SaveReader _saveReader;

		// Token: 0x0400000A RID: 10
		public readonly WorldSerializer _worldSerializer;
	}
}
