using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.Attractions
{
	// Token: 0x02000009 RID: 9
	public class AttractionAttender : BaseComponent, IPersistentEntity
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022CF File Offset: 0x000004CF
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000022D7 File Offset: 0x000004D7
		public bool FirstVisit { get; set; }

		// Token: 0x06000019 RID: 25 RVA: 0x000022E0 File Offset: 0x000004E0
		public void Save(IEntitySaver entitySaver)
		{
			if (this.FirstVisit)
			{
				entitySaver.GetComponent(AttractionAttender.AttractionAttenderKey).Set(AttractionAttender.FirstVisitKey, this.FirstVisit);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002308 File Offset: 0x00000508
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(AttractionAttender.AttractionAttenderKey, out objectLoader))
			{
				this.FirstVisit = objectLoader.Get(AttractionAttender.FirstVisitKey);
			}
		}

		// Token: 0x04000010 RID: 16
		public static readonly ComponentKey AttractionAttenderKey = new ComponentKey("AttractionAttender");

		// Token: 0x04000011 RID: 17
		public static readonly PropertyKey<bool> FirstVisitKey = new PropertyKey<bool>("FirstVisit");
	}
}
