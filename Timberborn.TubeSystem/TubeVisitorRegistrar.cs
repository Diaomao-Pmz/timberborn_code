using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000017 RID: 23
	public class TubeVisitorRegistrar : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000318C File Offset: 0x0000138C
		public TubeVisitorRegistrar(TubeVisitorRegistry tubeVisitorRegistry)
		{
			this._tubeVisitorRegistry = tubeVisitorRegistry;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000319B File Offset: 0x0000139B
		public void Awake()
		{
			this._tubeVisitor = base.GetComponent<TubeVisitor>();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000031A9 File Offset: 0x000013A9
		public void InitializeEntity()
		{
			this._tubeVisitorRegistry.Register(this._tubeVisitor);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000031BC File Offset: 0x000013BC
		public void DeleteEntity()
		{
			this._tubeVisitorRegistry.Unregister(this._tubeVisitor);
		}

		// Token: 0x04000039 RID: 57
		public readonly TubeVisitorRegistry _tubeVisitorRegistry;

		// Token: 0x0400003A RID: 58
		public TubeVisitor _tubeVisitor;
	}
}
