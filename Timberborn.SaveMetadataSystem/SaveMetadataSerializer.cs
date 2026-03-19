using System;
using System.IO;
using Timberborn.Persistence;
using Timberborn.SaveSystem;
using Timberborn.SerializationSystem;

namespace Timberborn.SaveMetadataSystem
{
	// Token: 0x02000007 RID: 7
	public class SaveMetadataSerializer : ISaveEntryReader<SaveMetadata>
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000021DF File Offset: 0x000003DF
		public SaveMetadataSerializer(SerializedObjectReaderWriter serializedObjectReaderWriter, ModReferenceSerializer modReferenceSerializer, InvariantDateTimeSerializer invariantDateTimeSerializer)
		{
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
			this._modReferenceSerializer = modReferenceSerializer;
			this._invariantDateTimeSerializer = invariantDateTimeSerializer;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021FC File Offset: 0x000003FC
		public string EntryName
		{
			get
			{
				return "save_metadata.json";
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002203 File Offset: 0x00000403
		public void WriteToSaveEntryStream(Stream entryStream, SaveMetadata saveMetadata)
		{
			this._serializedObjectReaderWriter.WriteJson(this.GetSaveMetadataSerializedObject(saveMetadata), entryStream);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002218 File Offset: 0x00000418
		public SaveMetadata ReadFromSaveEntryStream(Stream entryStream)
		{
			return this.Deserialize(this._serializedObjectReaderWriter.ReadJson(entryStream));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000222C File Offset: 0x0000042C
		public SerializedObject GetSaveMetadataSerializedObject(SaveMetadata saveMetadata)
		{
			SerializedObject serializedObject = new SerializedObject();
			this.SaveMetadata(saveMetadata, new ObjectSaver(serializedObject));
			return serializedObject;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000224D File Offset: 0x0000044D
		public SaveMetadata Deserialize(SerializedObject serializedObject)
		{
			return this.LoadMetadata(new ObjectLoader(serializedObject));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000225C File Offset: 0x0000045C
		public void SaveMetadata(SaveMetadata saveMetadata, IObjectSaver objectSaver)
		{
			objectSaver.Set<DateTime>(SaveMetadataSerializer.TimestampKey, saveMetadata.Timestamp, this._invariantDateTimeSerializer);
			objectSaver.Set(SaveMetadataSerializer.CycleKey, saveMetadata.Cycle);
			objectSaver.Set(SaveMetadataSerializer.DayKey, saveMetadata.Day);
			objectSaver.Set<ModReference>(SaveMetadataSerializer.ModsKey, saveMetadata.Mods, this._modReferenceSerializer);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022B9 File Offset: 0x000004B9
		public SaveMetadata LoadMetadata(IObjectLoader objectLoader)
		{
			return new SaveMetadata(objectLoader.Get<DateTime>(SaveMetadataSerializer.TimestampKey, this._invariantDateTimeSerializer), objectLoader.Get(SaveMetadataSerializer.CycleKey), objectLoader.Get(SaveMetadataSerializer.DayKey), this.LoadMods(objectLoader));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022EE File Offset: 0x000004EE
		public ModReference[] LoadMods(IObjectLoader objectLoader)
		{
			if (!objectLoader.Has<ModReference>(SaveMetadataSerializer.ModsKey))
			{
				return Array.Empty<ModReference>();
			}
			return objectLoader.Get<ModReference>(SaveMetadataSerializer.ModsKey, this._modReferenceSerializer).ToArray();
		}

		// Token: 0x04000010 RID: 16
		public static readonly PropertyKey<DateTime> TimestampKey = new PropertyKey<DateTime>("Timestamp");

		// Token: 0x04000011 RID: 17
		public static readonly PropertyKey<int> CycleKey = new PropertyKey<int>("Cycle");

		// Token: 0x04000012 RID: 18
		public static readonly PropertyKey<int> DayKey = new PropertyKey<int>("Day");

		// Token: 0x04000013 RID: 19
		public static readonly ListKey<ModReference> ModsKey = new ListKey<ModReference>("Mods");

		// Token: 0x04000014 RID: 20
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;

		// Token: 0x04000015 RID: 21
		public readonly ModReferenceSerializer _modReferenceSerializer;

		// Token: 0x04000016 RID: 22
		public readonly InvariantDateTimeSerializer _invariantDateTimeSerializer;
	}
}
