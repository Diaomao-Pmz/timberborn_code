using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000008 RID: 8
	public class FillValve : TickableComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener, IUnfinishedStateListener, IPersistentEntity, IDuplicable<FillValve>, IDuplicable, ITerminal
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000218F File Offset: 0x0000038F
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002197 File Offset: 0x00000397
		public bool IsSynchronized { get; private set; } = true;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021A0 File Offset: 0x000003A0
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000021A8 File Offset: 0x000003A8
		public bool TargetHeightEnabled { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021B1 File Offset: 0x000003B1
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021B9 File Offset: 0x000003B9
		public float TargetHeight { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021C2 File Offset: 0x000003C2
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000021CA File Offset: 0x000003CA
		public bool AutomationTargetHeightEnabled { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021D3 File Offset: 0x000003D3
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021DB File Offset: 0x000003DB
		public float AutomationTargetHeight { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021E4 File Offset: 0x000003E4
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021EC File Offset: 0x000003EC
		public Vector3Int OutputCoordinates { get; private set; }

		// Token: 0x06000017 RID: 23 RVA: 0x000021F5 File Offset: 0x000003F5
		public FillValve(IWaterService waterService, IThreadSafeWaterMap threadSafeWaterMap, FillValveSynchronizer fillValveSynchronizer)
		{
			this._waterService = waterService;
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._fillValveSynchronizer = fillValveSynchronizer;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002219 File Offset: 0x00000419
		public bool IsAutomated
		{
			get
			{
				return this._automatable.IsAutomated;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002226 File Offset: 0x00000426
		public bool IsInputOn
		{
			get
			{
				return this._automatable.State == ConnectionState.On;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002238 File Offset: 0x00000438
		public int MinTargetHeight
		{
			get
			{
				int result;
				if (!this._threadSafeWaterMap.TryGetColumnFloor(this.OutputCoordinates, out result))
				{
					return this.MaxTargetHeight;
				}
				return result;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002264 File Offset: 0x00000464
		public int MaxTargetHeight
		{
			get
			{
				return this._blockObject.Coordinates.z + 1;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002286 File Offset: 0x00000486
		public float ActualHeight
		{
			get
			{
				return this._threadSafeWaterMap.WaterHeightOrFloor(this.OutputCoordinates);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002299 File Offset: 0x00000499
		public float ClampedTargetHeight
		{
			get
			{
				return Mathf.Clamp(this.TargetHeight, (float)this.MinTargetHeight, (float)this.MaxTargetHeight);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022B4 File Offset: 0x000004B4
		public float ClampedAutomationTargetHeight
		{
			get
			{
				return Mathf.Clamp(this.AutomationTargetHeight, (float)this.MinTargetHeight, (float)this.MaxTargetHeight);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022CF File Offset: 0x000004CF
		public float TargetDepth
		{
			get
			{
				return this.ClampedTargetHeight - (float)this.MinTargetHeight;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000022DF File Offset: 0x000004DF
		public float AutomationTargetDepth
		{
			get
			{
				return this.ClampedAutomationTargetHeight - (float)this.MinTargetHeight;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022F0 File Offset: 0x000004F0
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._automatable = base.GetComponent<Automatable>();
			this._waterObstacleController = base.GetComponent<WaterObstacleController>();
			this._fillValveSpec = base.GetComponent<FillValveSpec>();
			base.DisableComponent();
			this._automatable.InputReconnected += this.OnAutomatableInputReconnected;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000234C File Offset: 0x0000054C
		public void InitializeEntity()
		{
			this.InitializeOutputCoordinates();
			if (!this._isLoadedOrDuplicated)
			{
				this.TargetHeightEnabled = this._fillValveSpec.DefaultTargetHeightEnabled;
				this.TargetHeight = (float)this.MinTargetHeight + this._fillValveSpec.DefaultTargetHeightOffset;
				this.AutomationTargetHeightEnabled = this._fillValveSpec.DefaultAutomationTargetHeightEnabled;
				this.AutomationTargetHeight = (float)this.MinTargetHeight + this._fillValveSpec.DefaultAutomationTargetHeightOffset;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023BC File Offset: 0x000005BC
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(FillValve.ComponentKey);
			component.Set(FillValve.IsSynchronizedKey, this.IsSynchronized);
			component.Set(FillValve.TargetHeightEnabledKey, this.TargetHeightEnabled);
			component.Set(FillValve.TargetHeightKey, this.TargetHeight);
			component.Set(FillValve.AutomationTargetHeightEnabledKey, this.AutomationTargetHeightEnabled);
			component.Set(FillValve.AutomationTargetHeightKey, this.AutomationTargetHeight);
			component.Set<FlowControllerState>(FillValve.FlowControllerStateKey, this._flowControllerState);
			component.Set(FillValve.ObstacleAddedKey, this._obstacleAdded);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000244C File Offset: 0x0000064C
		[BackwardCompatible(2026, 3, 6, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(FillValve.ComponentKey);
			this.IsSynchronized = component.Get(FillValve.IsSynchronizedKey);
			this.SetTargetHeightEnabled(component.Get(FillValve.TargetHeightEnabledKey));
			this.SetTargetHeight(component.Get(FillValve.TargetHeightKey));
			this.SetAutomationTargetHeightEnabled(component.Get(FillValve.AutomationTargetHeightEnabledKey));
			this.SetAutomationTargetHeight(component.Get(FillValve.AutomationTargetHeightKey));
			this._flowControllerState = component.Get<FlowControllerState>(FillValve.FlowControllerStateKey);
			this._obstacleAdded = (component.Has<bool>(FillValve.ObstacleAddedKey) && component.Get(FillValve.ObstacleAddedKey));
			this._isLoadedOrDuplicated = true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024F4 File Offset: 0x000006F4
		public void DuplicateFrom(FillValve source)
		{
			this.InitializeOutputCoordinates();
			this.IsSynchronized = source.IsSynchronized;
			this.SetTargetHeightEnabled(source.TargetHeightEnabled);
			this.SetTargetHeight((float)this.MinTargetHeight + source.TargetDepth);
			this.SetAutomationTargetHeightEnabled(source.AutomationTargetHeightEnabled);
			this.SetAutomationTargetHeight((float)this.MinTargetHeight + source.AutomationTargetDepth);
			this.SynchronizeNeighbors();
			this._isLoadedOrDuplicated = true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002560 File Offset: 0x00000760
		public void OnEnterUnfinishedState()
		{
			this._fillValveSynchronizer.SynchronizeWithUnfinishedNeighbors(this);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000256E File Offset: 0x0000076E
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002570 File Offset: 0x00000770
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this._waterService.AddDirectionLimiter(this._blockObject.Coordinates, this._blockObject.Orientation.ToFlowDirection());
			this._waterObstacleController.UpdateState(this._obstacleAdded);
			if (this._flowControllerState == FlowControllerState.IncreaseFlow)
			{
				this._waterService.SetControllerToIncreaseFlow(this._blockObject.Coordinates);
				return;
			}
			if (this._flowControllerState == FlowControllerState.DecreaseFlow)
			{
				this._waterService.SetControllerToDecreaseFlow(this._blockObject.Coordinates);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025F9 File Offset: 0x000007F9
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._waterService.RemoveDirectionLimiter(this._blockObject.Coordinates);
			this._waterService.RemoveFlowController(this._blockObject.Coordinates);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002630 File Offset: 0x00000830
		public override void Tick()
		{
			if (this.TargetHeight < (float)this.MinTargetHeight)
			{
				this.SetTargetHeight((float)this.MinTargetHeight);
			}
			if (this.AutomationTargetHeight < (float)this.MinTargetHeight)
			{
				this.SetAutomationTargetHeight((float)this.MinTargetHeight);
			}
			if (this.EffectiveTargetHeight <= (float)this.MinTargetHeight)
			{
				this.RemoveFlowController();
				this.UpdateObstacle(true);
				return;
			}
			this.UpdateObstacle(false);
			if (this.EffectiveTargetHeight < float.PositiveInfinity || this._flowControllerState != FlowControllerState.NoControl)
			{
				this.TickOutflowLimit();
				return;
			}
			this.RemoveFlowController();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026BB File Offset: 0x000008BB
		public void SetTargetHeightEnabledAndSynchronize(bool value)
		{
			this.SetTargetHeightEnabled(value);
			this.SynchronizeNeighbors();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026CA File Offset: 0x000008CA
		public void SetTargetHeightEnabled(bool value)
		{
			this.TargetHeightEnabled = value;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026D3 File Offset: 0x000008D3
		public void SetTargetHeightAndSynchronize(float value)
		{
			this.SetTargetHeight(value);
			this.SynchronizeNeighbors();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026E2 File Offset: 0x000008E2
		public void SetTargetHeight(float value)
		{
			this.TargetHeight = value;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026EB File Offset: 0x000008EB
		public void SetAutomationTargetHeightEnabledAndSynchronize(bool value)
		{
			this.SetAutomationTargetHeightEnabled(value);
			this.SynchronizeNeighbors();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026FA File Offset: 0x000008FA
		public void SetAutomationTargetHeightEnabled(bool value)
		{
			this.AutomationTargetHeightEnabled = value;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002703 File Offset: 0x00000903
		public void SetAutomationTargetHeightAndSynchronize(float value)
		{
			this.SetAutomationTargetHeight(value);
			this.SynchronizeNeighbors();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002712 File Offset: 0x00000912
		public void SetAutomationTargetHeight(float value)
		{
			this.AutomationTargetHeight = value;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000271B File Offset: 0x0000091B
		public void ToggleSynchronization(bool value)
		{
			this.IsSynchronized = value;
			this._fillValveSynchronizer.SynchronizeWithAllNeighbors(this);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000256E File Offset: 0x0000076E
		public void Evaluate()
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002730 File Offset: 0x00000930
		public float EffectiveTargetHeight
		{
			get
			{
				if (!this.IsAutomated || !this.IsInputOn)
				{
					if (!this.TargetHeightEnabled)
					{
						return float.PositiveInfinity;
					}
					return this.TargetHeight;
				}
				else
				{
					if (!this.AutomationTargetHeightEnabled)
					{
						return float.PositiveInfinity;
					}
					return this.AutomationTargetHeight;
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000276B File Offset: 0x0000096B
		public void InitializeOutputCoordinates()
		{
			this.OutputCoordinates = this._blockObject.TransformCoordinates(this._fillValveSpec.OutputCoordinates);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000278C File Offset: 0x0000098C
		public void TickOutflowLimit()
		{
			float num = this._threadSafeWaterMap.WaterHeightOrFloor(this.OutputCoordinates) - this.EffectiveTargetHeight;
			bool flag = this._flowControllerState == FlowControllerState.IncreaseFlow;
			bool flag2 = flag ? (num < this._fillValveSpec.OverflowLimit) : (num < 0f);
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

		// Token: 0x06000038 RID: 56 RVA: 0x00002822 File Offset: 0x00000A22
		public void OnAutomatableInputReconnected(object sender, EventArgs e)
		{
			if (this.IsSynchronized)
			{
				this.SynchronizeNeighbors();
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002832 File Offset: 0x00000A32
		public void SynchronizeNeighbors()
		{
			this._fillValveSynchronizer.SynchronizeAllNeighbors(this);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002840 File Offset: 0x00000A40
		public void RemoveFlowController()
		{
			if (this._flowControllerState != FlowControllerState.NoControl)
			{
				this._waterService.RemoveFlowController(this._blockObject.Coordinates);
			}
			this._flowControllerState = FlowControllerState.NoControl;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002867 File Offset: 0x00000A67
		public void UpdateObstacle(bool add)
		{
			this._waterObstacleController.UpdateState(add);
			this._obstacleAdded = add;
		}

		// Token: 0x04000009 RID: 9
		public static readonly ComponentKey ComponentKey = new ComponentKey("FillValve");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<bool> IsSynchronizedKey = new PropertyKey<bool>("IsSynchronized");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<bool> TargetHeightEnabledKey = new PropertyKey<bool>("TargetHeightEnabled");

		// Token: 0x0400000C RID: 12
		public static readonly PropertyKey<float> TargetHeightKey = new PropertyKey<float>("TargetHeight");

		// Token: 0x0400000D RID: 13
		public static readonly PropertyKey<bool> AutomationTargetHeightEnabledKey = new PropertyKey<bool>("AutomationTargetHeightEnabled");

		// Token: 0x0400000E RID: 14
		public static readonly PropertyKey<float> AutomationTargetHeightKey = new PropertyKey<float>("AutomationTargetHeight");

		// Token: 0x0400000F RID: 15
		public static readonly PropertyKey<FlowControllerState> FlowControllerStateKey = new PropertyKey<FlowControllerState>("FlowControllerState");

		// Token: 0x04000010 RID: 16
		public static readonly PropertyKey<bool> ObstacleAddedKey = new PropertyKey<bool>("ObstacleAdded");

		// Token: 0x04000017 RID: 23
		public readonly IWaterService _waterService;

		// Token: 0x04000018 RID: 24
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000019 RID: 25
		public readonly FillValveSynchronizer _fillValveSynchronizer;

		// Token: 0x0400001A RID: 26
		public BlockObject _blockObject;

		// Token: 0x0400001B RID: 27
		public Automatable _automatable;

		// Token: 0x0400001C RID: 28
		public WaterObstacleController _waterObstacleController;

		// Token: 0x0400001D RID: 29
		public FillValveSpec _fillValveSpec;

		// Token: 0x0400001E RID: 30
		public FlowControllerState _flowControllerState;

		// Token: 0x0400001F RID: 31
		public bool _obstacleAdded;

		// Token: 0x04000020 RID: 32
		public bool _isLoadedOrDuplicated;
	}
}
