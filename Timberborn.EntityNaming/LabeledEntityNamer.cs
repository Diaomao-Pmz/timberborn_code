using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;

namespace Timberborn.EntityNaming
{
	// Token: 0x0200000A RID: 10
	public class LabeledEntityNamer : BaseComponent, IAwakableComponent, IEntityNamer
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021CE File Offset: 0x000003CE
		public int EntityNamerPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021D1 File Offset: 0x000003D1
		public void Awake()
		{
			this._labeledEntity = base.GetComponent<LabeledEntity>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021DF File Offset: 0x000003DF
		public string GenerateEntityName()
		{
			return this._labeledEntity.DisplayName;
		}

		// Token: 0x04000009 RID: 9
		public LabeledEntity _labeledEntity;
	}
}
