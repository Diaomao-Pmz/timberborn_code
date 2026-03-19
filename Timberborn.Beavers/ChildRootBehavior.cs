using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.Beavers
{
	// Token: 0x02000015 RID: 21
	public class ChildRootBehavior : RootBehavior, IAwakableComponent, IStartableComponent, IPersistentEntity
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002BE6 File Offset: 0x00000DE6
		public void Awake()
		{
			this._child = base.GetComponent<Child>();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public void Start()
		{
			this._waitExecutor = base.GetComponent<WaitExecutor>();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002C04 File Offset: 0x00000E04
		public override Decision Decide(BehaviorAgent agent)
		{
			if (!this._waitedAfterBirth && this._child.IsNewborn)
			{
				this._waitExecutor.LaunchForIdleTime();
				this._waitedAfterBirth = true;
				return Decision.ReleaseWhenFinished(this._waitExecutor);
			}
			if (!this._child.GrowUpIfItIsTime())
			{
				return Decision.ReleaseNow();
			}
			return Decision.ReturnNextTick();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C5C File Offset: 0x00000E5C
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(ChildRootBehavior.ChildRootBehaviorKey).Set(ChildRootBehavior.WaitedAfterBirthKey, this._waitedAfterBirth);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002C7C File Offset: 0x00000E7C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(ChildRootBehavior.ChildRootBehaviorKey);
			this._waitedAfterBirth = component.Get(ChildRootBehavior.WaitedAfterBirthKey);
		}

		// Token: 0x04000036 RID: 54
		public static readonly ComponentKey ChildRootBehaviorKey = new ComponentKey("ChildRootBehavior");

		// Token: 0x04000037 RID: 55
		public static readonly PropertyKey<bool> WaitedAfterBirthKey = new PropertyKey<bool>("WaitedAfterBirth");

		// Token: 0x04000038 RID: 56
		public Child _child;

		// Token: 0x04000039 RID: 57
		public WaitExecutor _waitExecutor;

		// Token: 0x0400003A RID: 58
		public bool _waitedAfterBirth;
	}
}
