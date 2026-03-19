using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterMovementSystem;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.WalkingSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000015 RID: 21
	public class TubeTracker : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity, IWaterResistor, IWaterPenaltyModifier
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002EF1 File Offset: 0x000010F1
		public TubeTracker(TubeMap tubeMap, EventBus eventBus)
		{
			this._tubeMap = tubeMap;
			this._eventBus = eventBus;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002F07 File Offset: 0x00001107
		public bool IsWaterResistant
		{
			get
			{
				return this._isInTube;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002F0F File Offset: 0x0000110F
		public float WaterPenaltyModifier
		{
			get
			{
				return (float)(this._isInTube ? 0 : 1);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002F1E File Offset: 0x0000111E
		public void Awake()
		{
			this._walker = base.GetComponent<Walker>();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002F2C File Offset: 0x0000112C
		public void InitializeEntity()
		{
			this.CheckTubeInPosition();
			this._walker.PathFollower.MovedAlongPath += delegate(object _, MovementEventArgs _)
			{
				this.CheckTubeInPosition();
			};
			this._eventBus.Register(this);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002F5C File Offset: 0x0000115C
		public void DeleteEntity()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002F6A File Offset: 0x0000116A
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitialized)
		{
			if (!this._isInTube && entityInitialized.Entity.GetComponent<Tube>())
			{
				this.CheckTubeInPosition();
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002F8C File Offset: 0x0000118C
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			if (this._isInTube && entityDeletedEvent.Entity.GetComponent<Tube>())
			{
				this.CheckTubeInPosition();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002FB0 File Offset: 0x000011B0
		public void CheckTubeInPosition()
		{
			Vector3Int gridPosition = CoordinateSystem.WorldToGridInt(base.Transform.position + TubeTracker.PositionOffset);
			Tube tubeAt = this._tubeMap.GetTubeAt(gridPosition);
			this._isInTube = (tubeAt != null && tubeAt.CanBeVisited);
		}

		// Token: 0x0400002F RID: 47
		public static readonly Vector3 PositionOffset = Vector3.up * 0.1f;

		// Token: 0x04000030 RID: 48
		public readonly TubeMap _tubeMap;

		// Token: 0x04000031 RID: 49
		public readonly EventBus _eventBus;

		// Token: 0x04000032 RID: 50
		public Walker _walker;

		// Token: 0x04000033 RID: 51
		public bool _isInTube;
	}
}
