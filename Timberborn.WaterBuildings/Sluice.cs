using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200001F RID: 31
	public class Sluice : TickableComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener, IPersistentEntity, IDuplicable<Sluice>, IDuplicable
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003FBA File Offset: 0x000021BA
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00003FC2 File Offset: 0x000021C2
		public Vector3Int TargetCoordinates { get; private set; }

		// Token: 0x0600010E RID: 270 RVA: 0x00003FCB File Offset: 0x000021CB
		public Sluice(IWaterService waterService, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._waterService = waterService;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00003FE1 File Offset: 0x000021E1
		public bool IsOpen
		{
			get
			{
				return !this._obstacleAdded && this._flowControllerState != FlowControllerState.DecreaseFlow;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003FFC File Offset: 0x000021FC
		public int MinHeight
		{
			get
			{
				int result;
				if (!this._threadSafeWaterMap.TryGetColumnFloor(this.TargetCoordinates, out result))
				{
					return this.MaxHeight;
				}
				return result;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004028 File Offset: 0x00002228
		public int MaxHeight
		{
			get
			{
				return this._blockObject.Coordinates.z + 1;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000404A File Offset: 0x0000224A
		public float TargetDepth
		{
			get
			{
				return this._threadSafeWaterMap.WaterDepth(this.TargetCoordinates);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000405D File Offset: 0x0000225D
		public float Contamination
		{
			get
			{
				return this._threadSafeWaterMap.ColumnContamination(this._sourceCoordinates);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004070 File Offset: 0x00002270
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._sluiceState = base.GetComponent<SluiceState>();
			this._waterObstacleController = base.GetComponent<WaterObstacleController>();
			base.DisableComponent();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000409C File Offset: 0x0000229C
		public void InitializeEntity()
		{
			this._sourceCoordinates = this._blockObject.TransformCoordinates(new Vector3Int(0, -1, 0));
			this.TargetCoordinates = this._blockObject.TransformCoordinates(new Vector3Int(0, 1, 0));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000040D0 File Offset: 0x000022D0
		public override void Tick()
		{
			if (this._sluiceState.OutflowLimit < (float)(this.MinHeight - this.MaxHeight))
			{
				this._sluiceState.SetOutflowLimit((float)(this.MinHeight - this.MaxHeight));
			}
			if (this._sluiceState.AutoMode)
			{
				this.UpdateAutoMode();
				return;
			}
			this.UpdateObstacle(!this._sluiceState.IsOpen);
			this.RemoveFlowController();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004140 File Offset: 0x00002340
		public void OnEnterFinishedState()
		{
			this._waterService.AddDirectionLimiter(this._blockObject.Coordinates, this._blockObject.Orientation.ToFlowDirection());
			this._waterObstacleController.UpdateState(this._obstacleAdded);
			if (this._flowControllerState == FlowControllerState.IncreaseFlow)
			{
				this._waterService.SetControllerToIncreaseFlow(this._blockObject.Coordinates);
			}
			else if (this._flowControllerState == FlowControllerState.DecreaseFlow)
			{
				this._waterService.SetControllerToDecreaseFlow(this._blockObject.Coordinates);
			}
			base.EnableComponent();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000041CA File Offset: 0x000023CA
		public void OnExitFinishedState()
		{
			this._waterService.RemoveFlowController(this._blockObject.Coordinates);
			this._waterService.RemoveDirectionLimiter(this._blockObject.Coordinates);
			base.DisableComponent();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000041FE File Offset: 0x000023FE
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Sluice.SluiceKey);
			component.Set(Sluice.ObstacleAddedKey, this._obstacleAdded);
			component.Set<FlowControllerState>(Sluice.FlowControllerStateKey, this._flowControllerState);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000422C File Offset: 0x0000242C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Sluice.SluiceKey);
			this._obstacleAdded = component.Get(Sluice.ObstacleAddedKey);
			this._flowControllerState = component.Get<FlowControllerState>(Sluice.FlowControllerStateKey);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004267 File Offset: 0x00002467
		public void DuplicateFrom(Sluice source)
		{
			this._sluiceState.SetStateAndSynchronize(source._sluiceState, this.MinHeight - this.MaxHeight);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004288 File Offset: 0x00002488
		public void UpdateAutoMode()
		{
			bool flag = !this.IsAutoClosedByContamination();
			this.UpdateObstacle(!flag);
			if (flag && this._sluiceState.AutoCloseOnOutflow)
			{
				this.UpdateOutflowLimit();
				return;
			}
			this.RemoveFlowController();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000042C8 File Offset: 0x000024C8
		public bool IsAutoClosedByContamination()
		{
			if (this._sluiceState.AutoCloseOnAbove)
			{
				return this.GetContamination() > this._sluiceState.OnAboveLimit;
			}
			return this._sluiceState.AutoCloseOnBelow && this.GetContamination() < this._sluiceState.OnBelowLimit;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004318 File Offset: 0x00002518
		public float GetContamination()
		{
			return (float)Math.Round((double)this._threadSafeWaterMap.ColumnContamination(this._sourceCoordinates), 2);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004334 File Offset: 0x00002534
		public void UpdateOutflowLimit()
		{
			float num = this._threadSafeWaterMap.WaterHeightOrFloor(this.TargetCoordinates) - ((float)this.MaxHeight + this._sluiceState.OutflowLimit);
			bool flag = this._flowControllerState == FlowControllerState.IncreaseFlow;
			bool flag2 = flag ? (num < Sluice.SluiceOverflowLimit) : (num < 0f);
			if (flag2 != flag || this._flowControllerState == FlowControllerState.NoControl)
			{
				if (flag2)
				{
					this._waterService.SetControllerToIncreaseFlow(this._blockObject.Coordinates);
					this._flowControllerState = FlowControllerState.IncreaseFlow;
					return;
				}
				this._waterService.SetControllerToDecreaseFlow(this._blockObject.Coordinates);
				this._flowControllerState = FlowControllerState.DecreaseFlow;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000043D1 File Offset: 0x000025D1
		public void RemoveFlowController()
		{
			if (this._flowControllerState != FlowControllerState.NoControl)
			{
				this._waterService.RemoveFlowController(this._blockObject.Coordinates);
			}
			this._flowControllerState = FlowControllerState.NoControl;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000043F8 File Offset: 0x000025F8
		public void UpdateObstacle(bool add)
		{
			this._waterObstacleController.UpdateState(add);
			this._obstacleAdded = add;
		}

		// Token: 0x04000059 RID: 89
		public static readonly ComponentKey SluiceKey = new ComponentKey("Sluice");

		// Token: 0x0400005A RID: 90
		public static readonly PropertyKey<bool> ObstacleAddedKey = new PropertyKey<bool>("ObstacleAdded");

		// Token: 0x0400005B RID: 91
		public static readonly PropertyKey<FlowControllerState> FlowControllerStateKey = new PropertyKey<FlowControllerState>("FlowControllerState");

		// Token: 0x0400005C RID: 92
		public static readonly float SluiceOverflowLimit = 0.02f;

		// Token: 0x0400005E RID: 94
		public readonly IWaterService _waterService;

		// Token: 0x0400005F RID: 95
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000060 RID: 96
		public BlockObject _blockObject;

		// Token: 0x04000061 RID: 97
		public SluiceState _sluiceState;

		// Token: 0x04000062 RID: 98
		public WaterObstacleController _waterObstacleController;

		// Token: 0x04000063 RID: 99
		public Vector3Int _sourceCoordinates;

		// Token: 0x04000064 RID: 100
		public FlowDirection _flowDirection;

		// Token: 0x04000065 RID: 101
		public bool _obstacleAdded;

		// Token: 0x04000066 RID: 102
		public FlowControllerState _flowControllerState;
	}
}
