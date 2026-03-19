using System;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.MechanicalSystem;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000036 RID: 54
	public class WaterMover : TickableComponent, IAwakableComponent, IPersistentEntity, IDuplicable<WaterMover>, IDuplicable
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00007A59 File Offset: 0x00005C59
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00007A61 File Offset: 0x00005C61
		public bool CleanWaterMovement { get; set; } = true;

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00007A6A File Offset: 0x00005C6A
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00007A72 File Offset: 0x00005C72
		public bool ContaminatedWaterMovement { get; set; } = true;

		// Token: 0x06000280 RID: 640 RVA: 0x00007A7B File Offset: 0x00005C7B
		public WaterMover(ITickService tickService)
		{
			this._tickService = tickService;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00007A98 File Offset: 0x00005C98
		public bool CanMoveWater
		{
			get
			{
				return this._mechanicalBuilding.ActiveAndPowered && this.IsWaterFlowPossible();
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007AAF File Offset: 0x00005CAF
		public void Awake()
		{
			this._mechanicalBuilding = base.GetComponent<MechanicalBuilding>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._waterInput = base.GetComponent<WaterInput>();
			this._waterOutput = base.GetComponent<WaterOutput>();
			this._waterMoverSpec = base.GetComponent<WaterMoverSpec>();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public override void Tick()
		{
			if (this.CanMoveWater)
			{
				float waterAmount = this._tickService.TickIntervalInSeconds * this._waterMoverSpec.WaterPerSecond * this._mechanicalNode.PowerEfficiency;
				this.MoveWater(waterAmount);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007B30 File Offset: 0x00005D30
		public bool IsWaterFlowPossible()
		{
			if (!this._waterOutput.HasSpaceForWater)
			{
				return false;
			}
			if (!this.CleanWaterMovement)
			{
				return this._waterInput.ContaminatedWaterAmount > WaterMover.WaterMovementThreshold;
			}
			if (!this.ContaminatedWaterMovement)
			{
				return this._waterInput.CleanWaterAmount > WaterMover.WaterMovementThreshold;
			}
			return this._waterInput.IsUnderwater;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007B8D File Offset: 0x00005D8D
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(WaterMover.WaterMoverKey);
			component.Set(WaterMover.CleanWaterMovementKey, this.CleanWaterMovement);
			component.Set(WaterMover.ContaminatedWaterMovementKey, this.ContaminatedWaterMovement);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007BBC File Offset: 0x00005DBC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WaterMover.WaterMoverKey);
			this.CleanWaterMovement = component.Get(WaterMover.CleanWaterMovementKey);
			this.ContaminatedWaterMovement = component.Get(WaterMover.ContaminatedWaterMovementKey);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007BF7 File Offset: 0x00005DF7
		public void DuplicateFrom(WaterMover source)
		{
			this.CleanWaterMovement = source.CleanWaterMovement;
			this.ContaminatedWaterMovement = source.ContaminatedWaterMovement;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00007C14 File Offset: 0x00005E14
		public void MoveWater(float waterAmount)
		{
			float num = waterAmount * this.GetCleanMovementScaler();
			float num2 = waterAmount - num;
			float availableSpace = this._waterOutput.AvailableSpace;
			float num3 = Mathf.Max(Mathf.Min(new float[]
			{
				num,
				this._waterInput.CleanWaterAmount,
				availableSpace
			}), 0f);
			float num4 = Mathf.Max(Mathf.Min(new float[]
			{
				num2,
				this._waterInput.ContaminatedWaterAmount,
				availableSpace
			}), 0f);
			this._waterInput.RemoveCleanWater(num3);
			this._waterInput.RemoveContaminatedWater(num4);
			this._waterOutput.AddWater(num3, num4);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007CBC File Offset: 0x00005EBC
		public float GetCleanMovementScaler()
		{
			if (!this.CleanWaterMovement)
			{
				return 0f;
			}
			float contaminationPercentage = this._waterInput.ContaminationPercentage;
			if (!this.ContaminatedWaterMovement)
			{
				return 1f;
			}
			return 1f - contaminationPercentage;
		}

		// Token: 0x040000FC RID: 252
		public static readonly float WaterMovementThreshold = 0.0001f;

		// Token: 0x040000FD RID: 253
		public static readonly ComponentKey WaterMoverKey = new ComponentKey("WaterMover");

		// Token: 0x040000FE RID: 254
		public static readonly PropertyKey<bool> CleanWaterMovementKey = new PropertyKey<bool>("CleanWaterMovement");

		// Token: 0x040000FF RID: 255
		public static readonly PropertyKey<bool> ContaminatedWaterMovementKey = new PropertyKey<bool>("ContaminatedWaterMovement");

		// Token: 0x04000102 RID: 258
		public readonly ITickService _tickService;

		// Token: 0x04000103 RID: 259
		public MechanicalBuilding _mechanicalBuilding;

		// Token: 0x04000104 RID: 260
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000105 RID: 261
		public WaterInput _waterInput;

		// Token: 0x04000106 RID: 262
		public WaterOutput _waterOutput;

		// Token: 0x04000107 RID: 263
		public WaterMoverSpec _waterMoverSpec;
	}
}
