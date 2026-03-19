using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Buildings;
using Timberborn.EnterableSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000023 RID: 35
	public class WalkInsideExecutor : BaseComponent, IAwakableComponent, IExecutor
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00004123 File Offset: 0x00002323
		public WalkInsideExecutor(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004132 File Offset: 0x00002332
		public void Awake()
		{
			this._walker = base.GetComponent<Walker>();
			this._enterer = base.GetComponent<Enterer>();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000414C File Offset: 0x0000234C
		public ExecutorStatus LaunchIgnoringAccessibleValidity(Enterable enterable)
		{
			return this.Launch(enterable, true, false);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004157 File Offset: 0x00002357
		public ExecutorStatus LaunchForLimitedTime(Enterable enterable)
		{
			return this.Launch(enterable, false, true);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004162 File Offset: 0x00002362
		public ExecutorStatus Launch(Enterable enterable)
		{
			return this.Launch(enterable, false, false);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004170 File Offset: 0x00002370
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (!this._buildingAccessible || !this._buildingAccessible.Accessible || (!this._ignoreAccessibleValidity && !this._buildingAccessible.Accessible.ValidAccessible) || !this._walker.CurrentDestinationReachable)
			{
				this._enterer.UnreserveSlot();
				return ExecutorStatus.Failure;
			}
			if (this._limitWalkTime)
			{
				this._currentWalkTime += deltaTimeInHours;
				if (this._currentWalkTime > WalkInsideExecutor.MaxWalkTimeInHours)
				{
					this._walker.StopNextTick();
					this._enterer.UnreserveSlot();
					return ExecutorStatus.Success;
				}
			}
			if (this._walker.Stopped())
			{
				this._enterer.Enter(this._enterable);
				return ExecutorStatus.Success;
			}
			return ExecutorStatus.Running;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000422C File Offset: 0x0000242C
		public void Save(IEntitySaver entitySaver)
		{
			if (this._enterable)
			{
				IObjectSaver component = entitySaver.GetComponent(WalkInsideExecutor.WalkInsideExecutorKey);
				component.Set<Enterable>(WalkInsideExecutor.EnterableKey, this._enterable, this._referenceSerializer.Of<Enterable>());
				component.Set(WalkInsideExecutor.IgnoreAccessibleValidityKey, this._ignoreAccessibleValidity);
				component.Set(WalkInsideExecutor.LimitWalkTimeKey, this._limitWalkTime);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004290 File Offset: 0x00002490
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			Enterable enterable;
			if (entityLoader.TryGetComponent(WalkInsideExecutor.WalkInsideExecutorKey, out objectLoader) && objectLoader.GetObsoletable<Enterable>(WalkInsideExecutor.EnterableKey, this._referenceSerializer.Of<Enterable>(), out enterable))
			{
				bool ignoreAccessibleValidity = objectLoader.Get(WalkInsideExecutor.IgnoreAccessibleValidityKey);
				bool limitWalkTime = objectLoader.Get(WalkInsideExecutor.LimitWalkTimeKey);
				this.Initialize(enterable, ignoreAccessibleValidity, limitWalkTime);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000042E8 File Offset: 0x000024E8
		public ExecutorStatus Launch(Enterable enterable, bool ignoreAccessibleValidity, bool limitWalkTime)
		{
			if (!enterable)
			{
				return ExecutorStatus.Failure;
			}
			this.Initialize(enterable, ignoreAccessibleValidity, limitWalkTime);
			if (this._enterer.CurrentBuilding == enterable)
			{
				return ExecutorStatus.Success;
			}
			this._enterer.UnreserveSlot();
			if (!this._enterable.CanReserveSlot)
			{
				return ExecutorStatus.Failure;
			}
			AccessibleDestination destination = new AccessibleDestination(this._buildingAccessible.Accessible);
			ExecutorStatus executorStatus = this._walker.GoTo(destination);
			if (executorStatus == ExecutorStatus.Success)
			{
				this._enterer.Enter(this._enterable);
				return ExecutorStatus.Success;
			}
			if (executorStatus == ExecutorStatus.Failure)
			{
				return ExecutorStatus.Failure;
			}
			this._enterer.ReserveSlot(this._enterable);
			return ExecutorStatus.Running;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000437E File Offset: 0x0000257E
		public void Initialize(Enterable enterable, bool ignoreAccessibleValidity, bool limitWalkTime)
		{
			this._enterable = enterable;
			this._ignoreAccessibleValidity = ignoreAccessibleValidity;
			this._limitWalkTime = limitWalkTime;
			this._buildingAccessible = this._enterable.GetComponent<BuildingAccessible>();
			this._currentWalkTime = 0f;
		}

		// Token: 0x0400006F RID: 111
		public static readonly ComponentKey WalkInsideExecutorKey = new ComponentKey("WalkInsideExecutor");

		// Token: 0x04000070 RID: 112
		public static readonly PropertyKey<Enterable> EnterableKey = new PropertyKey<Enterable>("Enterable");

		// Token: 0x04000071 RID: 113
		public static readonly PropertyKey<bool> IgnoreAccessibleValidityKey = new PropertyKey<bool>("IgnoreAccessibleValidity");

		// Token: 0x04000072 RID: 114
		public static readonly PropertyKey<bool> LimitWalkTimeKey = new PropertyKey<bool>("LimitWalkTime");

		// Token: 0x04000073 RID: 115
		public static readonly float MaxWalkTimeInHours = 0.15f;

		// Token: 0x04000074 RID: 116
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000075 RID: 117
		public Walker _walker;

		// Token: 0x04000076 RID: 118
		public Enterer _enterer;

		// Token: 0x04000077 RID: 119
		public Enterable _enterable;

		// Token: 0x04000078 RID: 120
		public BuildingAccessible _buildingAccessible;

		// Token: 0x04000079 RID: 121
		public bool _ignoreAccessibleValidity;

		// Token: 0x0400007A RID: 122
		public bool _limitWalkTime;

		// Token: 0x0400007B RID: 123
		public float _currentWalkTime;
	}
}
