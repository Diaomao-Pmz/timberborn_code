using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;

namespace Timberborn.Buildings
{
	// Token: 0x02000007 RID: 7
	public class Building : BaseComponent, IAwakableComponent, IRegisteredComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public BuildingSpec Spec { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002111 File Offset: 0x00000311
		public void Awake()
		{
			this.Spec = base.GetComponent<BuildingSpec>();
		}
	}
}
