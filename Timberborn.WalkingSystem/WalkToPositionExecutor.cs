using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000025 RID: 37
	public class WalkToPositionExecutor : BaseComponent, IAwakableComponent, IExecutor
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x000045B3 File Offset: 0x000027B3
		public WalkToPositionExecutor(PositionDestinationFactory positionDestinationFactory)
		{
			this._positionDestinationFactory = positionDestinationFactory;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000045C2 File Offset: 0x000027C2
		public void Awake()
		{
			this._walker = base.GetComponent<Walker>();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000045D0 File Offset: 0x000027D0
		public ExecutorStatus Launch(Vector3 position)
		{
			PositionDestination destination = this._positionDestinationFactory.Create(position, 0f);
			return this._walker.GoTo(destination);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000045FB File Offset: 0x000027FB
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
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

		// Token: 0x060000EC RID: 236 RVA: 0x0000461C File Offset: 0x0000281C
		public void Save(IEntitySaver entitySaver)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000461C File Offset: 0x0000281C
		public void Load(IEntityLoader entityLoader)
		{
		}

		// Token: 0x04000083 RID: 131
		public readonly PositionDestinationFactory _positionDestinationFactory;

		// Token: 0x04000084 RID: 132
		public Walker _walker;
	}
}
