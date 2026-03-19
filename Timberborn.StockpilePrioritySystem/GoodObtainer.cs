using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.StockpilePrioritySystem
{
	// Token: 0x02000004 RID: 4
	public class GoodObtainer : BaseComponent, IPersistentEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public event EventHandler GoodObtainingChanged;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000212D File Offset: 0x0000032D
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002135 File Offset: 0x00000335
		public bool IsObtaining { get; private set; }

		// Token: 0x06000007 RID: 7 RVA: 0x0000213E File Offset: 0x0000033E
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsObtaining)
			{
				entitySaver.GetComponent(GoodObtainer.GoodObtainerKey).Set(GoodObtainer.IsObtainingKey, this.IsObtaining);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002164 File Offset: 0x00000364
		[BackwardCompatible(2025, 7, 4, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(GoodObtainer.GoodObtainerKey, out objectLoader))
			{
				this.IsObtaining = (objectLoader.Has<bool>(GoodObtainer.IsObtainingKey) ? objectLoader.Get(GoodObtainer.IsObtainingKey) : objectLoader.Get(new PropertyKey<bool>("GoodObtainingEnabled")));
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B0 File Offset: 0x000003B0
		public void EnableObtaining()
		{
			this.IsObtaining = true;
			EventHandler goodObtainingChanged = this.GoodObtainingChanged;
			if (goodObtainingChanged == null)
			{
				return;
			}
			goodObtainingChanged(this, EventArgs.Empty);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021CF File Offset: 0x000003CF
		public void DisableObtaining()
		{
			this.IsObtaining = false;
			EventHandler goodObtainingChanged = this.GoodObtainingChanged;
			if (goodObtainingChanged == null)
			{
				return;
			}
			goodObtainingChanged(this, EventArgs.Empty);
		}

		// Token: 0x04000006 RID: 6
		public static readonly ComponentKey GoodObtainerKey = new ComponentKey("GoodObtainer");

		// Token: 0x04000007 RID: 7
		public static readonly PropertyKey<bool> IsObtainingKey = new PropertyKey<bool>("IsObtaining");
	}
}
