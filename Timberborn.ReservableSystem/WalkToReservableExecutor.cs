using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Persistence;
using Timberborn.WalkingSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.ReservableSystem
{
	// Token: 0x02000007 RID: 7
	public class WalkToReservableExecutor : BaseComponent, IAwakableComponent, IExecutor
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002133 File Offset: 0x00000333
		public WalkToReservableExecutor(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002142 File Offset: 0x00000342
		public void Awake()
		{
			this._walker = base.GetComponent<Walker>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002150 File Offset: 0x00000350
		public ExecutorStatus Launch(ReservableReacher reservableReacher)
		{
			this.SetTarget(reservableReacher);
			return this._walker.GoTo(this._reservableReacher.Destination);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002170 File Offset: 0x00000370
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (!this._walker.CurrentDestinationReachable || !this._reservable || !this._reservable.Reserved)
			{
				return ExecutorStatus.Failure;
			}
			if (this._walker.Stopped())
			{
				this._reservableReacher.NotifyReservableReached(this._walker);
				return ExecutorStatus.Success;
			}
			return ExecutorStatus.Running;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021C7 File Offset: 0x000003C7
		public void Save(IEntitySaver entitySaver)
		{
			if (this._reservableReacher)
			{
				entitySaver.GetComponent(WalkToReservableExecutor.WalkToReservableExecutorKey).Set<ReservableReacher>(WalkToReservableExecutor.ReservableReacherKey, this._reservableReacher, this._referenceSerializer.Of<ReservableReacher>());
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021FC File Offset: 0x000003FC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			ReservableReacher target;
			if (entityLoader.TryGetComponent(WalkToReservableExecutor.WalkToReservableExecutorKey, out objectLoader) && objectLoader.GetObsoletable<ReservableReacher>(WalkToReservableExecutor.ReservableReacherKey, this._referenceSerializer.Of<ReservableReacher>(), out target))
			{
				this.SetTarget(target);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002239 File Offset: 0x00000439
		public void SetTarget(ReservableReacher reservableReacher)
		{
			this._reservable = reservableReacher.GetComponent<Reservable>();
			this._reservableReacher = reservableReacher;
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey WalkToReservableExecutorKey = new ComponentKey("WalkToReservableExecutor");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<ReservableReacher> ReservableReacherKey = new PropertyKey<ReservableReacher>("ReservableReacher");

		// Token: 0x0400000A RID: 10
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x0400000B RID: 11
		public Walker _walker;

		// Token: 0x0400000C RID: 12
		public ReservableReacher _reservableReacher;

		// Token: 0x0400000D RID: 13
		public Reservable _reservable;
	}
}
