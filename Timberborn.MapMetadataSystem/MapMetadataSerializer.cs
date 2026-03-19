using System;
using System.IO;
using Timberborn.Common;
using Timberborn.SaveSystem;
using Timberborn.SerializationSystem;

namespace Timberborn.MapMetadataSystem
{
	// Token: 0x02000005 RID: 5
	public class MapMetadataSerializer : ISaveEntryReader<MapMetadata>
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002150 File Offset: 0x00000350
		public MapMetadataSerializer(SerializedObjectReaderWriter serializedObjectReaderWriter)
		{
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000215F File Offset: 0x0000035F
		public string EntryName
		{
			get
			{
				return "map_metadata.json";
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002166 File Offset: 0x00000366
		public void WriteToSaveEntryStream(Stream entryStream, MapMetadata mapMetadata)
		{
			this._serializedObjectReaderWriter.WriteJson(MapMetadataSerializer.GetMapMetadataSerializedObject(mapMetadata), entryStream);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000217A File Offset: 0x0000037A
		public MapMetadata ReadFromSaveEntryStream(Stream entryStream)
		{
			return MapMetadataSerializer.Deserialize(this._serializedObjectReaderWriter.ReadJson(entryStream));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002190 File Offset: 0x00000390
		public static SerializedObject GetMapMetadataSerializedObject(MapMetadata mapMetadata)
		{
			SerializedObject serializedObject = new SerializedObject();
			serializedObject.Set<int>(MapMetadataSerializer.WidthKey, mapMetadata.Width);
			serializedObject.Set<int>(MapMetadataSerializer.HeightKey, mapMetadata.Height);
			serializedObject.Set<string>(MapMetadataSerializer.MapNameLocKeyKey, mapMetadata.MapNameLocKey);
			serializedObject.Set<string>(MapMetadataSerializer.MapDescriptionLocKeyKey, mapMetadata.MapDescriptionLocKey);
			serializedObject.Set<string>(MapMetadataSerializer.MapDescriptionKey, mapMetadata.MapDescription);
			serializedObject.Set<bool>(MapMetadataSerializer.IsRecommendedKey, mapMetadata.IsRecommended);
			serializedObject.Set<bool>(MapMetadataSerializer.IsUnconventional, mapMetadata.IsUnconventional);
			serializedObject.Set<bool>(MapMetadataSerializer.IsDevKey, mapMetadata.IsDev);
			return serializedObject;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000222C File Offset: 0x0000042C
		[BackwardCompatible(2025, 9, 25, Compatibility.Map)]
		public static MapMetadata Deserialize(SerializedObject serializedObject)
		{
			return new MapMetadata(serializedObject.Get<int>(MapMetadataSerializer.WidthKey), serializedObject.Get<int>(MapMetadataSerializer.HeightKey), serializedObject.Get<string>(MapMetadataSerializer.MapNameLocKeyKey), serializedObject.Get<string>(MapMetadataSerializer.MapDescriptionLocKeyKey), serializedObject.Get<string>(MapMetadataSerializer.MapDescriptionKey), serializedObject.Get<bool>(MapMetadataSerializer.IsRecommendedKey), serializedObject.GetOrDefault<bool>(MapMetadataSerializer.IsUnconventional, false), serializedObject.Get<bool>(MapMetadataSerializer.IsDevKey));
		}

		// Token: 0x0400000E RID: 14
		public static readonly string WidthKey = "Width";

		// Token: 0x0400000F RID: 15
		public static readonly string HeightKey = "Height";

		// Token: 0x04000010 RID: 16
		public static readonly string MapNameLocKeyKey = "MapNameLocKey";

		// Token: 0x04000011 RID: 17
		public static readonly string MapDescriptionLocKeyKey = "MapDescriptionLocKey";

		// Token: 0x04000012 RID: 18
		public static readonly string MapDescriptionKey = "MapDescription";

		// Token: 0x04000013 RID: 19
		public static readonly string IsRecommendedKey = "IsRecommended";

		// Token: 0x04000014 RID: 20
		public static readonly string IsUnconventional = "IsUnconventional";

		// Token: 0x04000015 RID: 21
		public static readonly string IsDevKey = "IsDev";

		// Token: 0x04000016 RID: 22
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;
	}
}
