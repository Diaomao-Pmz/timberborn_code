using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Timberborn.SaveSystem;
using Timberborn.SerializationSystem;
using Timberborn.Versioning;

namespace Timberborn.WorldSerialization
{
	// Token: 0x0200000A RID: 10
	public class WorldSerializer : ISaveEntryReader<SerializedWorld>
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000240D File Offset: 0x0000060D
		public WorldSerializer(SerializedObjectReaderWriter serializedObjectReaderWriter)
		{
			this._serializedObjectReaderWriter = serializedObjectReaderWriter;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000241C File Offset: 0x0000061C
		public string EntryName
		{
			get
			{
				return "world.json";
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002423 File Offset: 0x00000623
		public void WriteToSaveEntryStream(Stream entryStream, SerializedWorld serializedWorld)
		{
			this._serializedObjectReaderWriter.WriteJson(WorldSerializer.SerializeSave(serializedWorld), entryStream);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002437 File Offset: 0x00000637
		public SerializedWorld ReadFromSaveEntryStream(Stream entryStream)
		{
			return WorldSerializer.DeserializeSave(this._serializedObjectReaderWriter.ReadJson(entryStream));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000244C File Offset: 0x0000064C
		public static SerializedObject SerializeSave(SerializedWorld serializedWorld)
		{
			SerializedObject value = WorldSerializer.SerializedSingletons(serializedWorld);
			IEnumerable<SerializedObject> source = WorldSerializer.SerializedEntities(serializedWorld);
			SerializedObject serializedObject = new SerializedObject();
			serializedObject.Set<string>("GameVersion", serializedWorld.Version.Full);
			serializedObject.Set<string>("Timestamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
			serializedObject.Set<SerializedObject>("Singletons", value);
			serializedObject.SetArray("Entities", source.ToArray<SerializedObject>());
			return serializedObject;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024C0 File Offset: 0x000006C0
		public static SerializedObject SerializedSingletons(SerializedWorld serializedWorld)
		{
			SerializedObject serializedObject = new SerializedObject();
			foreach (SerializedSingleton serializedSingleton in serializedWorld.Singletons())
			{
				serializedObject.Set<SerializedObject>(serializedSingleton.Name, serializedSingleton.Value);
			}
			return serializedObject;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002520 File Offset: 0x00000720
		public static IEnumerable<SerializedObject> SerializedEntities(SerializedWorld serializedWorld)
		{
			return serializedWorld.Entities().Select(new Func<SerializedEntity, SerializedObject>(WorldSerializer.SerializeEntity));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000253C File Offset: 0x0000073C
		public static SerializedObject SerializeEntity(SerializedEntity serializedEntity)
		{
			SerializedObject value = WorldSerializer.SerializedComponents(serializedEntity);
			SerializedObject serializedObject = new SerializedObject();
			serializedObject.Set<Guid>("Id", serializedEntity.Id);
			serializedObject.Set<string>("Template", serializedEntity.TemplateName);
			serializedObject.Set<SerializedObject>("Components", value);
			return serializedObject;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002584 File Offset: 0x00000784
		public static SerializedObject SerializedComponents(SerializedEntity serializedEntity)
		{
			SerializedObject serializedObject = new SerializedObject();
			foreach (string name in serializedEntity.Components())
			{
				SerializedComponent component = serializedEntity.GetComponent(name);
				serializedObject.Set<SerializedObject>(name, component.Value);
			}
			return serializedObject;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025E8 File Offset: 0x000007E8
		public static SerializedWorld DeserializeSave(SerializedObject save)
		{
			SerializedWorld serializedWorld = new SerializedWorld(Version.Create(save.Get<string>("GameVersion")));
			SerializedObject serializedObject = save.Get<SerializedObject>("Singletons");
			SerializedObject[] array = save.GetArray<SerializedObject>("Entities");
			foreach (string name in serializedObject.Properties())
			{
				SerializedObject value = serializedObject.Get<SerializedObject>(name);
				serializedWorld.AddSingleton(new SerializedSingleton(name, value));
			}
			foreach (SerializedObject serializedObject2 in array)
			{
				serializedWorld.AddEntity(WorldSerializer.DeserializeEntity(serializedObject2));
			}
			return serializedWorld;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026A4 File Offset: 0x000008A4
		public static SerializedEntity DeserializeEntity(SerializedObject serializedObject)
		{
			Guid id = serializedObject.Get<Guid>("Id");
			string templateName = serializedObject.Has("Template") ? serializedObject.Get<string>("Template") : serializedObject.Get<string>("TemplateName");
			SerializedEntity serializedEntity = new SerializedEntity(id, templateName);
			SerializedObject serializedObject2 = serializedObject.Get<SerializedObject>("Components");
			foreach (string name in serializedObject2.Properties())
			{
				serializedEntity.AddComponent(new SerializedComponent(name, serializedObject2.Get<SerializedObject>(name)));
			}
			return serializedEntity;
		}

		// Token: 0x04000011 RID: 17
		public readonly SerializedObjectReaderWriter _serializedObjectReaderWriter;
	}
}
