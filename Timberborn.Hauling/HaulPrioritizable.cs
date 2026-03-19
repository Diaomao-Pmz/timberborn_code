using System;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.Hauling
{
	// Token: 0x0200000F RID: 15
	public class HaulPrioritizable : BaseComponent, IPersistentEntity, IDuplicable<HaulPrioritizable>, IDuplicable
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002960 File Offset: 0x00000B60
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002968 File Offset: 0x00000B68
		public bool Prioritized { get; set; }

		// Token: 0x06000043 RID: 67 RVA: 0x00002971 File Offset: 0x00000B71
		public void Save(IEntitySaver entitySaver)
		{
			if (this.Prioritized)
			{
				entitySaver.GetComponent(HaulPrioritizable.HaulPrioritizableKey).Set(HaulPrioritizable.PrioritizedKey, this.Prioritized);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002998 File Offset: 0x00000B98
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(HaulPrioritizable.HaulPrioritizableKey, out objectLoader))
			{
				this.Prioritized = objectLoader.Get(HaulPrioritizable.PrioritizedKey);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029C5 File Offset: 0x00000BC5
		public void DuplicateFrom(HaulPrioritizable source)
		{
			this.Prioritized = source.Prioritized;
		}

		// Token: 0x04000018 RID: 24
		public static readonly ComponentKey HaulPrioritizableKey = new ComponentKey("HaulPrioritizable");

		// Token: 0x04000019 RID: 25
		public static readonly PropertyKey<bool> PrioritizedKey = new PropertyKey<bool>("Prioritized");
	}
}
