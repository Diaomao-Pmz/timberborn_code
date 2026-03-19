using System;
using System.IO;
using System.Linq;
using Timberborn.GameCycleSystem;
using Timberborn.Modding;
using Timberborn.SaveMetadataSystem;
using Timberborn.SaveSystem;

namespace Timberborn.SaveMetadataSaving
{
	// Token: 0x02000004 RID: 4
	public class SaveMetadataSaveEntryWriter : ISaveEntryWriter
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public SaveMetadataSaveEntryWriter(SaveMetadataSerializer saveMetadataSerializer, GameCycleService gameCycleService, ModRepository modRepository)
		{
			this._saveMetadataSerializer = saveMetadataSerializer;
			this._gameCycleService = gameCycleService;
			this._modRepository = modRepository;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020D8 File Offset: 0x000002D8
		public string EntryName
		{
			get
			{
				return this._saveMetadataSerializer.EntryName;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E8 File Offset: 0x000002E8
		public void WriteToSaveEntryStream(Stream entryStream)
		{
			SaveMetadata saveMetadata = new SaveMetadata(DateTime.Now, this._gameCycleService.Cycle, this._gameCycleService.CycleDay, this.GetMods());
			this._saveMetadataSerializer.WriteToSaveEntryStream(entryStream, saveMetadata);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002129 File Offset: 0x00000329
		public ModReference[] GetMods()
		{
			return (from enabledMod in this._modRepository.EnabledMods
			select new ModReference(enabledMod.Manifest.Id, enabledMod.Manifest.Name, enabledMod.Manifest.Version.Full)).ToArray<ModReference>();
		}

		// Token: 0x04000006 RID: 6
		public readonly SaveMetadataSerializer _saveMetadataSerializer;

		// Token: 0x04000007 RID: 7
		public readonly GameCycleService _gameCycleService;

		// Token: 0x04000008 RID: 8
		public readonly ModRepository _modRepository;
	}
}
