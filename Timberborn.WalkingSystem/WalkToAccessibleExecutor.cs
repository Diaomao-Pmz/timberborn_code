using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Navigation;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000024 RID: 36
	public class WalkToAccessibleExecutor : BaseComponent, IAwakableComponent, IExecutor
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00004407 File Offset: 0x00002607
		public WalkToAccessibleExecutor(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004416 File Offset: 0x00002616
		public void Awake()
		{
			this._walker = base.GetComponent<Walker>();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004424 File Offset: 0x00002624
		public ExecutorStatus LaunchIgnoringAccessibleValidity(Accessible accessible)
		{
			return this.Launch(accessible, true);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000442E File Offset: 0x0000262E
		public ExecutorStatus Launch(Accessible accessible)
		{
			return this.Launch(accessible, false);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004438 File Offset: 0x00002638
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (!this._accessible || (!this._accessible.ValidAccessible && !this._ignoreAccessibleValidity))
			{
				return ExecutorStatus.Failure;
			}
			if (!this._walker.CurrentDestinationReachable)
			{
				return ExecutorStatus.Failure;
			}
			if (this._walker.Stopped())
			{
				return ExecutorStatus.Success;
			}
			return ExecutorStatus.Running;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004488 File Offset: 0x00002688
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(WalkToAccessibleExecutor.WalkToAccessibleExecutorKey);
			if (this._accessible)
			{
				component.Set<Accessible>(WalkToAccessibleExecutor.AccessibleKey, this._accessible, this._referenceSerializer.Of<Accessible>());
			}
			component.Set(WalkToAccessibleExecutor.IgnoreAccessibleValidityKey, this._ignoreAccessibleValidity);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000044DC File Offset: 0x000026DC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WalkToAccessibleExecutor.WalkToAccessibleExecutorKey);
			Accessible accessible;
			if (component.Has<Accessible>(WalkToAccessibleExecutor.AccessibleKey) && component.GetObsoletable<Accessible>(WalkToAccessibleExecutor.AccessibleKey, this._referenceSerializer.Of<Accessible>(), out accessible))
			{
				this._accessible = accessible;
			}
			this._ignoreAccessibleValidity = component.Get(WalkToAccessibleExecutor.IgnoreAccessibleValidityKey);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004534 File Offset: 0x00002734
		public ExecutorStatus Launch(Accessible accessible, bool ignoreAccessibleValidity)
		{
			if (!accessible || (!accessible.ValidAccessible && !ignoreAccessibleValidity))
			{
				return ExecutorStatus.Failure;
			}
			this._accessible = accessible;
			this._ignoreAccessibleValidity = ignoreAccessibleValidity;
			AccessibleDestination destination = new AccessibleDestination(this._accessible);
			ExecutorStatus executorStatus = this._walker.GoTo(destination);
			if (executorStatus != ExecutorStatus.Running)
			{
				return executorStatus;
			}
			return ExecutorStatus.Running;
		}

		// Token: 0x0400007C RID: 124
		public static readonly ComponentKey WalkToAccessibleExecutorKey = new ComponentKey("WalkToAccessibleExecutor");

		// Token: 0x0400007D RID: 125
		public static readonly PropertyKey<Accessible> AccessibleKey = new PropertyKey<Accessible>("Accessible");

		// Token: 0x0400007E RID: 126
		public static readonly PropertyKey<bool> IgnoreAccessibleValidityKey = new PropertyKey<bool>("IgnoreAccessibleValidity");

		// Token: 0x0400007F RID: 127
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000080 RID: 128
		public Walker _walker;

		// Token: 0x04000081 RID: 129
		public Accessible _accessible;

		// Token: 0x04000082 RID: 130
		public bool _ignoreAccessibleValidity;
	}
}
