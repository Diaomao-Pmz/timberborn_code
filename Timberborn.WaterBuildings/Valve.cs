using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000029 RID: 41
	public class Valve : TickableComponent, IAwakableComponent, IFinishedStateListener, IUnfinishedStateListener, IPersistentEntity, IDuplicable<Valve>, IDuplicable, ITerminal
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000055B9 File Offset: 0x000037B9
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x000055C1 File Offset: 0x000037C1
		public bool IsSynchronized { get; private set; } = true;

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000055CA File Offset: 0x000037CA
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000055D2 File Offset: 0x000037D2
		public bool OutflowLimitEnabled { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000055DB File Offset: 0x000037DB
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000055E3 File Offset: 0x000037E3
		public float OutflowLimit { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000055EC File Offset: 0x000037EC
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000055F4 File Offset: 0x000037F4
		public bool AutomationOutflowLimitEnabled { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000055FD File Offset: 0x000037FD
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00005605 File Offset: 0x00003805
		public float AutomationOutflowLimit { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000560E File Offset: 0x0000380E
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00005616 File Offset: 0x00003816
		public float ReactionSpeed { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000561F File Offset: 0x0000381F
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00005627 File Offset: 0x00003827
		public float? CurrentOutflowLimit { get; private set; }

		// Token: 0x060001C5 RID: 453 RVA: 0x00005630 File Offset: 0x00003830
		public Valve(IWaterService waterService, ValveSynchronizer valveSynchronizer)
		{
			this._waterService = waterService;
			this._valveSynchronizer = valveSynchronizer;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000564D File Offset: 0x0000384D
		public float MaxOutflowLimit
		{
			get
			{
				return this._valveSpec.MaxOutflowLimit;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000565A File Offset: 0x0000385A
		public float OutflowLimitStep
		{
			get
			{
				return this._valveSpec.OutflowLimitStep;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00005667 File Offset: 0x00003867
		public float ReactionSpeedStep
		{
			get
			{
				return this._valveSpec.ReactionSpeedStep;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00005674 File Offset: 0x00003874
		public bool IsAutomated
		{
			get
			{
				return this._automatable.IsAutomated;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00005681 File Offset: 0x00003881
		public bool IsInputOn
		{
			get
			{
				return this._automatable.State == ConnectionState.On;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00005694 File Offset: 0x00003894
		public ValveState? State
		{
			get
			{
				if (base.Enabled && this._automatable.IsAutomated)
				{
					float num = this.GetTargetOutflowLimit() ?? float.PositiveInfinity;
					float num2 = this.CurrentOutflowLimit ?? float.PositiveInfinity;
					if (num.Equals(num2))
					{
						return new ValveState?(ValveState.Idle);
					}
					if (num > num2)
					{
						return new ValveState?(ValveState.Opening);
					}
					if (num < num2)
					{
						return new ValveState?(ValveState.Closing);
					}
				}
				return null;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005724 File Offset: 0x00003924
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._automatable = base.GetComponent<Automatable>();
			this._valveSpec = base.GetComponent<ValveSpec>();
			this._waterObstacleController = base.GetComponent<WaterObstacleController>();
			this.OutflowLimitEnabled = this._valveSpec.DefaultOutflowLimitEnabled;
			this.OutflowLimit = this._valveSpec.DefaultOutflowLimit;
			this.AutomationOutflowLimitEnabled = this._valveSpec.DefaultAutomationOutflowLimitEnabled;
			this.AutomationOutflowLimit = this._valveSpec.DefaultAutomationOutflowLimit;
			this.ReactionSpeed = 1f;
			base.DisableComponent();
			this._automatable.InputReconnected += this.OnAutomatableInputReconnected;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000057D0 File Offset: 0x000039D0
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Valve.ComponentKey);
			component.Set(Valve.IsSynchronizedKey, this.IsSynchronized);
			component.Set(Valve.OutflowLimitEnabledKey, this.OutflowLimitEnabled);
			component.Set(Valve.OutflowLimitKey, this.OutflowLimit);
			component.Set(Valve.AutomationOutflowLimitEnabledKey, this.AutomationOutflowLimitEnabled);
			component.Set(Valve.AutomationOutflowLimitKey, this.AutomationOutflowLimit);
			if (this.CurrentOutflowLimit != null)
			{
				component.Set(Valve.CurrentOutflowLimitKey, this.CurrentOutflowLimit.Value);
			}
			component.Set(Valve.ReactionSpeedKey, this.ReactionSpeed);
			component.Set(Valve.LastSignKey, this._lastSign);
			component.Set(Valve.TicksWithCurrentSignKey, this._ticksWithCurrentSign);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000589C File Offset: 0x00003A9C
		[BackwardCompatible(2026, 2, 6, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Valve.ComponentKey);
			this.IsSynchronized = component.Get(Valve.IsSynchronizedKey);
			this.SetOutflowLimitEnabled(component.Has<bool>(Valve.OutflowLimitEnabledKey) && component.Get(Valve.OutflowLimitEnabledKey));
			this.SetOutflowLimit(component.Has<float>(Valve.OutflowLimitKey) ? component.Get(Valve.OutflowLimitKey) : 0f);
			this.SetAutomationOutflowLimitEnabled(component.Has<bool>(Valve.AutomationOutflowLimitEnabledKey) && component.Get(Valve.AutomationOutflowLimitEnabledKey));
			this.SetAutomationOutflowLimit(component.Has<float>(Valve.AutomationOutflowLimitKey) ? component.Get(Valve.AutomationOutflowLimitKey) : 0f);
			this.SetReactionSpeed(component.Has<float>(Valve.ReactionSpeedKey) ? component.Get(Valve.ReactionSpeedKey) : Valve.ReactionSpeedMax);
			this.CurrentOutflowLimit = (component.Has<float>(Valve.CurrentOutflowLimitKey) ? new float?(component.Get(Valve.CurrentOutflowLimitKey)) : null);
			this._lastSign = (component.Has<int>(Valve.LastSignKey) ? component.Get(Valve.LastSignKey) : 0);
			this._ticksWithCurrentSign = (component.Has<int>(Valve.TicksWithCurrentSignKey) ? component.Get(Valve.TicksWithCurrentSignKey) : 0);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000059E8 File Offset: 0x00003BE8
		public void DuplicateFrom(Valve source)
		{
			this.IsSynchronized = source.IsSynchronized;
			this.SetOutflowLimit(source.OutflowLimit);
			this.SetOutflowLimitEnabled(source.OutflowLimitEnabled);
			this.SetAutomationOutflowLimit(source.AutomationOutflowLimit);
			this.SetAutomationOutflowLimitEnabled(source.AutomationOutflowLimitEnabled);
			this.SetReactionSpeed(source.ReactionSpeed);
			this.SynchronizeNeighbors();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005A43 File Offset: 0x00003C43
		public void OnEnterUnfinishedState()
		{
			this._valveSynchronizer.SynchronizeWithUnfinishedNeighbors(this);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000256E File Offset: 0x0000076E
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005A51 File Offset: 0x00003C51
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this._waterService.AddDirectionLimiter(this._blockObject.Coordinates, this._blockObject.Orientation.ToFlowDirection());
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005A7F File Offset: 0x00003C7F
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this.ClearLimit();
			this._waterService.RemoveDirectionLimiter(this._blockObject.Coordinates);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005AA3 File Offset: 0x00003CA3
		public override void Tick()
		{
			this.TickCurrentOutflowLimit();
			this.ApplyCurrentOutflowLimit();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005AB1 File Offset: 0x00003CB1
		public void SetOutflowLimitEnabledAndSynchronize(bool value)
		{
			this.SetOutflowLimitEnabled(value);
			this.SynchronizeNeighbors();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public void SetOutflowLimitEnabled(bool value)
		{
			this.OutflowLimitEnabled = value;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005AC9 File Offset: 0x00003CC9
		public void SetOutflowLimitAndSynchronize(float value)
		{
			this.SetOutflowLimit(value);
			this.SynchronizeNeighbors();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005AD8 File Offset: 0x00003CD8
		public void SetOutflowLimit(float value)
		{
			this.OutflowLimit = value;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005AE1 File Offset: 0x00003CE1
		public void SetAutomationOutflowLimitEnabledAndSynchronize(bool value)
		{
			this.SetAutomationOutflowLimitEnabled(value);
			this.SynchronizeNeighbors();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public void SetAutomationOutflowLimitEnabled(bool value)
		{
			this.AutomationOutflowLimitEnabled = value;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005AF9 File Offset: 0x00003CF9
		public void SetAutomationOutflowLimitAndSynchronize(float value)
		{
			this.SetAutomationOutflowLimit(value);
			this.SynchronizeNeighbors();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005B08 File Offset: 0x00003D08
		public void SetAutomationOutflowLimit(float value)
		{
			this.AutomationOutflowLimit = value;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00005B11 File Offset: 0x00003D11
		public void SetReactionSpeedAndSynchronize(float value)
		{
			this.SetReactionSpeed(value);
			this.SynchronizeNeighbors();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00005B20 File Offset: 0x00003D20
		public void SetReactionSpeed(float value)
		{
			this.ReactionSpeed = Mathf.Clamp(value, Valve.ReactionSpeedMin, Valve.ReactionSpeedMax);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005B38 File Offset: 0x00003D38
		public void ToggleSynchronization(bool value)
		{
			this.IsSynchronized = value;
			this._valveSynchronizer.SynchronizeWithAllNeighbors(this);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000256E File Offset: 0x0000076E
		public void Evaluate()
		{
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00005B4D File Offset: 0x00003D4D
		public float EffectiveReactionSpeed
		{
			get
			{
				if (!this._automatable.IsAutomated)
				{
					return 1f;
				}
				return this.ReactionSpeed;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005B68 File Offset: 0x00003D68
		public void OnAutomatableInputReconnected(object sender, EventArgs e)
		{
			if (this.IsSynchronized)
			{
				this.SynchronizeNeighbors();
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005B78 File Offset: 0x00003D78
		public void TickCurrentOutflowLimit()
		{
			float? targetOutflowLimit = this.GetTargetOutflowLimit();
			this.UpdateTicksWithCurrentSign(targetOutflowLimit);
			if (!this.CurrentOutflowLimit.Equals(targetOutflowLimit))
			{
				float num = targetOutflowLimit ?? float.PositiveInfinity;
				float? currentOutflowLimit = this.CurrentOutflowLimit;
				float num2 = currentOutflowLimit.GetValueOrDefault();
				if (currentOutflowLimit == null)
				{
					num2 = this.MaxOutflowLimit;
					float? currentOutflowLimit2 = new float?(num2);
					this.CurrentOutflowLimit = currentOutflowLimit2;
				}
				currentOutflowLimit = this.CurrentOutflowLimit;
				num2 = num;
				if (currentOutflowLimit.GetValueOrDefault() < num2 & currentOutflowLimit != null)
				{
					this.CurrentOutflowLimit = new float?(Mathf.Min(this.CurrentOutflowLimit.Value + this.RateOfChange(), num));
				}
				else
				{
					currentOutflowLimit = this.CurrentOutflowLimit;
					num2 = num;
					if (currentOutflowLimit.GetValueOrDefault() > num2 & currentOutflowLimit != null)
					{
						this.CurrentOutflowLimit = new float?(Mathf.Max(this.CurrentOutflowLimit.Value - this.RateOfChange(), num));
					}
				}
				currentOutflowLimit = this.CurrentOutflowLimit;
				num2 = this.MaxOutflowLimit;
				if (currentOutflowLimit.GetValueOrDefault() > num2 & currentOutflowLimit != null)
				{
					this.CurrentOutflowLimit = null;
				}
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005CB4 File Offset: 0x00003EB4
		public void ClearLimit()
		{
			this.CurrentOutflowLimit = null;
			this.ApplyCurrentOutflowLimit();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005CD8 File Offset: 0x00003ED8
		public void ApplyCurrentOutflowLimit()
		{
			WaterObstacleController waterObstacleController = this._waterObstacleController;
			float? currentOutflowLimit = this.CurrentOutflowLimit;
			float num = 0f;
			waterObstacleController.UpdateState(currentOutflowLimit.GetValueOrDefault() == num & currentOutflowLimit != null);
			if (this.CurrentOutflowLimit != null)
			{
				this._waterService.SetOutflowLimit(this._blockObject.Coordinates, this.CurrentOutflowLimit.Value);
				return;
			}
			this._waterService.RemoveOutflowLimit(this._blockObject.Coordinates);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005D5C File Offset: 0x00003F5C
		public float? GetTargetOutflowLimit()
		{
			if (!base.Enabled)
			{
				return null;
			}
			if (this._automatable.IsAutomated && this._automatable.State == ConnectionState.On)
			{
				if (!this.AutomationOutflowLimitEnabled)
				{
					return null;
				}
				return new float?(Mathf.Min(this.AutomationOutflowLimit, this.MaxOutflowLimit));
			}
			else
			{
				if (!this.OutflowLimitEnabled)
				{
					return null;
				}
				return new float?(Mathf.Min(this.OutflowLimit, this.MaxOutflowLimit));
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public void UpdateTicksWithCurrentSign(float? targetOutflowLimit)
		{
			float num = targetOutflowLimit ?? float.PositiveInfinity;
			int num2 = (this.CurrentOutflowLimit != null) ? Math.Sign(num - this.CurrentOutflowLimit.Value) : ((targetOutflowLimit != null) ? -1 : 0);
			if (num2 != this._lastSign)
			{
				this._lastSign = num2;
				this._ticksWithCurrentSign = 0;
				return;
			}
			this._ticksWithCurrentSign++;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005E6C File Offset: 0x0000406C
		public float RateOfChange()
		{
			float num = Mathf.Pow(this.EffectiveReactionSpeed, this._valveSpec.ReactionSpeedExponent);
			float num2 = Mathf.Lerp(this._valveSpec.RateOfChangeLowPrimary, this._valveSpec.RateOfChangeHighPrimary, num);
			float num3 = Mathf.Lerp(this._valveSpec.RateOfChangeLowSecondary, this._valveSpec.RateOfChangeHighSecondary, num);
			return Mathf.Lerp(num2, num3, (float)(this._ticksWithCurrentSign - this._valveSpec.RateOfChangePrimaryTicks) / (float)this._valveSpec.RateOfChangePrimaryToSecondaryTicks);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005EEF File Offset: 0x000040EF
		public void SynchronizeNeighbors()
		{
			this._valveSynchronizer.SynchronizeAllNeighbors(this);
		}

		// Token: 0x0400009A RID: 154
		public static readonly float ReactionSpeedMin = 0.01f;

		// Token: 0x0400009B RID: 155
		public static readonly float ReactionSpeedMax = 1f;

		// Token: 0x0400009C RID: 156
		public static readonly ComponentKey ComponentKey = new ComponentKey("Valve");

		// Token: 0x0400009D RID: 157
		public static readonly PropertyKey<bool> IsSynchronizedKey = new PropertyKey<bool>("IsSynchronized");

		// Token: 0x0400009E RID: 158
		public static readonly PropertyKey<bool> OutflowLimitEnabledKey = new PropertyKey<bool>("OutflowLimitEnabled");

		// Token: 0x0400009F RID: 159
		public static readonly PropertyKey<float> OutflowLimitKey = new PropertyKey<float>("OutflowLimit");

		// Token: 0x040000A0 RID: 160
		public static readonly PropertyKey<bool> AutomationOutflowLimitEnabledKey = new PropertyKey<bool>("AutomationOutflowLimitEnabled");

		// Token: 0x040000A1 RID: 161
		public static readonly PropertyKey<float> AutomationOutflowLimitKey = new PropertyKey<float>("AutomationOutflowLimit");

		// Token: 0x040000A2 RID: 162
		public static readonly PropertyKey<float> ReactionSpeedKey = new PropertyKey<float>("ReactionSpeed");

		// Token: 0x040000A3 RID: 163
		public static readonly PropertyKey<float> CurrentOutflowLimitKey = new PropertyKey<float>("CurrentOutflowLimit");

		// Token: 0x040000A4 RID: 164
		public static readonly PropertyKey<int> LastSignKey = new PropertyKey<int>("LastSign");

		// Token: 0x040000A5 RID: 165
		public static readonly PropertyKey<int> TicksWithCurrentSignKey = new PropertyKey<int>("TicksWithCurrentSign");

		// Token: 0x040000AD RID: 173
		public readonly IWaterService _waterService;

		// Token: 0x040000AE RID: 174
		public readonly ValveSynchronizer _valveSynchronizer;

		// Token: 0x040000AF RID: 175
		public BlockObject _blockObject;

		// Token: 0x040000B0 RID: 176
		public Automatable _automatable;

		// Token: 0x040000B1 RID: 177
		public ValveSpec _valveSpec;

		// Token: 0x040000B2 RID: 178
		public WaterObstacleController _waterObstacleController;

		// Token: 0x040000B3 RID: 179
		public int _lastSign;

		// Token: 0x040000B4 RID: 180
		public int _ticksWithCurrentSign;
	}
}
