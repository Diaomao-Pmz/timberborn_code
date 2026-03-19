using System;
using System.Collections.Generic;
using System.Globalization;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.Localization;
using Timberborn.SaveMetadataSystem;
using Timberborn.UIFormatters;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000006 RID: 6
	public class GameSaveItemFactory
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000218A File Offset: 0x0000038A
		public GameSaveItemFactory(SaveMetadataSerializer saveMetadataSerializer, GameSaveDeserializer gameSaveDeserializer, GameSaveRepository gameSaveRepository, ILoc loc, TimestampFormatter timestampFormatter)
		{
			this._saveMetadataSerializer = saveMetadataSerializer;
			this._gameSaveDeserializer = gameSaveDeserializer;
			this._gameSaveRepository = gameSaveRepository;
			this._loc = loc;
			this._timestampFormatter = timestampFormatter;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B7 File Offset: 0x000003B7
		public IEnumerable<GameSaveItem> CreateForSettlement(SettlementReference settlementReference)
		{
			foreach (SaveReference saveReference in this._gameSaveRepository.GetSaves(settlementReference))
			{
				yield return this.Create(saveReference);
			}
			IEnumerator<SaveReference> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D0 File Offset: 0x000003D0
		public GameSaveItem Create(SaveReference saveReference)
		{
			SaveMetadata metadata = this._gameSaveDeserializer.ReadFromSaveFile<SaveMetadata>(saveReference, this._saveMetadataSerializer);
			bool isAutosave = saveReference.SaveName.Contains(GameSaveRepository.AutosaveNameSuffix);
			return new GameSaveItem(saveReference, this.GetDisplayName(saveReference, isAutosave), this.GetTimestamp(saveReference, metadata), this.GetGameTime(metadata), isAutosave);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000221F File Offset: 0x0000041F
		public string GetDisplayName(SaveReference saveReference, bool isAutosave)
		{
			if (!isAutosave)
			{
				return saveReference.SaveName;
			}
			return this._loc.T(GameSaveItemFactory.AutosaveLocKey);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000223C File Offset: 0x0000043C
		public string GetTimestamp(SaveReference saveReference, SaveMetadata metadata)
		{
			return this.GetSaveDateTime(saveReference, metadata).ToString(CultureInfo.InstalledUICulture);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000225E File Offset: 0x0000045E
		public DateTime GetSaveDateTime(SaveReference saveReference, SaveMetadata metadata)
		{
			if (metadata == null)
			{
				return this._gameSaveRepository.GetSaveLastWriteTime(saveReference);
			}
			return metadata.Timestamp;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002276 File Offset: 0x00000476
		public string GetGameTime(SaveMetadata metadata)
		{
			if (metadata == null)
			{
				return this._loc.T(GameSaveItemFactory.CycleUnknownLocKey);
			}
			return this._timestampFormatter.FormatLongLocalized(metadata.Cycle, metadata.Day);
		}

		// Token: 0x0400000C RID: 12
		public static readonly string AutosaveLocKey = "Saving.Autosave";

		// Token: 0x0400000D RID: 13
		public static readonly string CycleUnknownLocKey = "Saving.CycleUnknown";

		// Token: 0x0400000E RID: 14
		public readonly SaveMetadataSerializer _saveMetadataSerializer;

		// Token: 0x0400000F RID: 15
		public readonly GameSaveDeserializer _gameSaveDeserializer;

		// Token: 0x04000010 RID: 16
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000011 RID: 17
		public readonly ILoc _loc;

		// Token: 0x04000012 RID: 18
		public readonly TimestampFormatter _timestampFormatter;
	}
}
