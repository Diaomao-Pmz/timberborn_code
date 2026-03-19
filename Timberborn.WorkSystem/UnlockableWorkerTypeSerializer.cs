using System;
using Timberborn.Common;
using Timberborn.Persistence;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000012 RID: 18
	public class UnlockableWorkerTypeSerializer : IValueSerializer<UnlockableWorkerType>
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002A5F File Offset: 0x00000C5F
		public void Serialize(UnlockableWorkerType value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(UnlockableWorkerTypeSerializer.WorkplaceTemplateNameKey, value.WorkplaceTemplateName);
			objectSaver.Set(UnlockableWorkerTypeSerializer.WorkerTypeKey, value.WorkerType);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A8C File Offset: 0x00000C8C
		[BackwardCompatible(2025, 9, 16, Compatibility.Save)]
		public Obsoletable<UnlockableWorkerType> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			PropertyKey<string> key = new PropertyKey<string>("WorkplacePrefabName");
			return new UnlockableWorkerType(objectLoader.Has<string>(UnlockableWorkerTypeSerializer.WorkplaceTemplateNameKey) ? objectLoader.Get(UnlockableWorkerTypeSerializer.WorkplaceTemplateNameKey) : objectLoader.Get(key), objectLoader.Get(UnlockableWorkerTypeSerializer.WorkerTypeKey));
		}

		// Token: 0x0400001B RID: 27
		public static readonly PropertyKey<string> WorkplaceTemplateNameKey = new PropertyKey<string>("WorkplaceTemplateName");

		// Token: 0x0400001C RID: 28
		public static readonly PropertyKey<string> WorkerTypeKey = new PropertyKey<string>("WorkerType");
	}
}
