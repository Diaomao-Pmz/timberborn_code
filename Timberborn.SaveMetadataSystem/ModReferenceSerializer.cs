using System;
using Timberborn.Persistence;

namespace Timberborn.SaveMetadataSystem
{
	// Token: 0x02000005 RID: 5
	public class ModReferenceSerializer : IValueSerializer<ModReference>
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020ED File Offset: 0x000002ED
		public void Serialize(ModReference value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(ModReferenceSerializer.IdKey, value.Id);
			objectSaver.Set(ModReferenceSerializer.NameKey, value.Name);
			objectSaver.Set(ModReferenceSerializer.VersionKey, value.Version);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212C File Offset: 0x0000032C
		public Obsoletable<ModReference> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			return new ModReference(objectLoader.Get(ModReferenceSerializer.IdKey), objectLoader.Get(ModReferenceSerializer.NameKey), objectLoader.Get(ModReferenceSerializer.VersionKey));
		}

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<string> IdKey = new PropertyKey<string>("Id");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<string> NameKey = new PropertyKey<string>("Name");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<string> VersionKey = new PropertyKey<string>("Version");
	}
}
