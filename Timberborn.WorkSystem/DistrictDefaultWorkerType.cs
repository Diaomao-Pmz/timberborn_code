using System;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000009 RID: 9
	public class DistrictDefaultWorkerType : BaseComponent, IPersistentEntity, IDuplicable<DistrictDefaultWorkerType>, IDuplicable
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000212E File Offset: 0x0000032E
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002136 File Offset: 0x00000336
		public string WorkerType { get; private set; } = DistrictDefaultWorkerType.DefaultWorkerType;

		// Token: 0x0600000D RID: 13 RVA: 0x0000213F File Offset: 0x0000033F
		public void SetWorkerType(string workerType)
		{
			if (workerType != this.WorkerType)
			{
				this.WorkerType = workerType;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002156 File Offset: 0x00000356
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(DistrictDefaultWorkerType.DistrictDefaultWorkerTypeKey).Set(DistrictDefaultWorkerType.WorkerTypeKey, this.WorkerType);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002174 File Offset: 0x00000374
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(DistrictDefaultWorkerType.DistrictDefaultWorkerTypeKey);
			this.WorkerType = component.Get(DistrictDefaultWorkerType.WorkerTypeKey);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000219E File Offset: 0x0000039E
		public void DuplicateFrom(DistrictDefaultWorkerType source)
		{
			this.SetWorkerType(source.WorkerType);
		}

		// Token: 0x0400000A RID: 10
		public static readonly string DefaultWorkerType = "Beaver";

		// Token: 0x0400000B RID: 11
		public static readonly ComponentKey DistrictDefaultWorkerTypeKey = new ComponentKey("DistrictDefaultWorkerType");

		// Token: 0x0400000C RID: 12
		public static readonly PropertyKey<string> WorkerTypeKey = new PropertyKey<string>("WorkerType");
	}
}
