using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.MechanicalSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000030 RID: 48
	public class PowerMeter : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<PowerMeter>, IDuplicable, ISamplingTransmitter, ITransmitter
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00006672 File Offset: 0x00004872
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000667A File Offset: 0x0000487A
		public PowerMeterMode Mode { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00006683 File Offset: 0x00004883
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000668B File Offset: 0x0000488B
		public NumericComparisonMode ComparisonMode { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00006694 File Offset: 0x00004894
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000669C File Offset: 0x0000489C
		public int IntThreshold { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000066A5 File Offset: 0x000048A5
		// (set) Token: 0x06000217 RID: 535 RVA: 0x000066AD File Offset: 0x000048AD
		public float PercentThreshold { get; private set; } = 0.5f;

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000066B6 File Offset: 0x000048B6
		// (set) Token: 0x06000219 RID: 537 RVA: 0x000066BE File Offset: 0x000048BE
		public int IntMeasurement { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000066C7 File Offset: 0x000048C7
		// (set) Token: 0x0600021B RID: 539 RVA: 0x000066CF File Offset: 0x000048CF
		public float PercentMeasurement { get; private set; }

		// Token: 0x0600021C RID: 540 RVA: 0x000066D8 File Offset: 0x000048D8
		public PowerMeter(TransputMap transputMap)
		{
			this._transputMap = transputMap;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000066F2 File Offset: 0x000048F2
		public bool IsPercentThreshold
		{
			get
			{
				return this.Mode == PowerMeterMode.BatteryChargeLevel;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000066FD File Offset: 0x000048FD
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006718 File Offset: 0x00004918
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(PowerMeter.ComponentKey);
			component.Set<PowerMeterMode>(PowerMeter.ModeKey, this.Mode);
			component.Set<NumericComparisonMode>(PowerMeter.ComparisonModeKey, this.ComparisonMode);
			component.Set(PowerMeter.IntThresholdKey, this.IntThreshold);
			component.Set(PowerMeter.PercentThresholdKey, this.PercentThreshold);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006774 File Offset: 0x00004974
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(PowerMeter.ComponentKey);
			this.Mode = component.Get<PowerMeterMode>(PowerMeter.ModeKey);
			this.ComparisonMode = component.Get<NumericComparisonMode>(PowerMeter.ComparisonModeKey);
			this.IntThreshold = component.Get(PowerMeter.IntThresholdKey);
			this.PercentThreshold = component.Get(PowerMeter.PercentThresholdKey);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000067D1 File Offset: 0x000049D1
		public void DuplicateFrom(PowerMeter source)
		{
			this.Mode = source.Mode;
			this.ComparisonMode = source.ComparisonMode;
			this.IntThreshold = source.IntThreshold;
			this.PercentThreshold = source.PercentThreshold;
			this.UpdateState();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006809 File Offset: 0x00004A09
		public void SetMode(PowerMeterMode mode)
		{
			if (this.Mode != mode)
			{
				this.Mode = mode;
				this.UpdateState();
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006821 File Offset: 0x00004A21
		public void SetComparisonMode(NumericComparisonMode comparisionMode)
		{
			if (this.ComparisonMode != comparisionMode)
			{
				this.ComparisonMode = comparisionMode;
				this.UpdateState();
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006839 File Offset: 0x00004A39
		public void SetIntThreshold(int value)
		{
			if (this.IntThreshold != value)
			{
				this.IntThreshold = value;
				this.UpdateState();
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006854 File Offset: 0x00004A54
		public void SetPercentThreshold(float value)
		{
			if (!this.PercentThreshold.Equals(value))
			{
				this.PercentThreshold = value;
				this.UpdateState();
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006880 File Offset: 0x00004A80
		public void Sample()
		{
			MechanicalGraph graph = this.GetGraph();
			this._sampledPowerSupply = ((graph != null) ? graph.PowerSupply : 0);
			this._sampledPowerDemand = ((graph != null) ? graph.PowerDemand : 0);
			this._sampledBatteryChargeLevel = ((graph != null) ? graph.BatteryChargeLevel : 0f);
			this.UpdateState();
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000068D4 File Offset: 0x00004AD4
		public MechanicalGraph GetGraph()
		{
			return this._mechanicalNode.Graph ?? this.GetGraphWhenUnfinished();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000068EC File Offset: 0x00004AEC
		public MechanicalGraph GetGraphWhenUnfinished()
		{
			Transput facingTransput = this._transputMap.GetFacingTransput(this._mechanicalNode.Transputs[0]);
			if (facingTransput == null)
			{
				return null;
			}
			return facingTransput.ParentNode.Graph;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006928 File Offset: 0x00004B28
		public void UpdateState()
		{
			this.UpdateMeasurement();
			Automator automator = this._automator;
			bool state;
			switch (this.Mode)
			{
			case PowerMeterMode.Supply:
				state = this.ComparisonMode.Evaluate(this.IntMeasurement, this.IntThreshold);
				break;
			case PowerMeterMode.Demand:
				state = this.ComparisonMode.Evaluate(this.IntMeasurement, this.IntThreshold);
				break;
			case PowerMeterMode.Surplus:
				state = this.ComparisonMode.Evaluate(this.IntMeasurement, this.IntThreshold);
				break;
			case PowerMeterMode.BatteryChargeLevel:
				state = this.ComparisonMode.Evaluate(this.PercentMeasurement, this.PercentThreshold);
				break;
			default:
				throw new ArgumentOutOfRangeException(this.Mode.ToString());
			}
			automator.SetState(state);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000069EC File Offset: 0x00004BEC
		public void UpdateMeasurement()
		{
			switch (this.Mode)
			{
			case PowerMeterMode.Supply:
				this.IntMeasurement = this._sampledPowerSupply;
				return;
			case PowerMeterMode.Demand:
				this.IntMeasurement = this._sampledPowerDemand;
				return;
			case PowerMeterMode.Surplus:
				this.IntMeasurement = this._sampledPowerSupply - this._sampledPowerDemand;
				return;
			case PowerMeterMode.BatteryChargeLevel:
				this.PercentMeasurement = this._sampledBatteryChargeLevel;
				return;
			default:
				throw new ArgumentOutOfRangeException(this.Mode.ToString());
			}
		}

		// Token: 0x040000F3 RID: 243
		public static readonly ComponentKey ComponentKey = new ComponentKey("PowerMeter");

		// Token: 0x040000F4 RID: 244
		public static readonly PropertyKey<PowerMeterMode> ModeKey = new PropertyKey<PowerMeterMode>("Mode");

		// Token: 0x040000F5 RID: 245
		public static readonly PropertyKey<NumericComparisonMode> ComparisonModeKey = new PropertyKey<NumericComparisonMode>("ComparisonMode");

		// Token: 0x040000F6 RID: 246
		public static readonly PropertyKey<int> IntThresholdKey = new PropertyKey<int>("IntThreshold");

		// Token: 0x040000F7 RID: 247
		public static readonly PropertyKey<float> PercentThresholdKey = new PropertyKey<float>("PercentThreshold");

		// Token: 0x040000FE RID: 254
		public readonly TransputMap _transputMap;

		// Token: 0x040000FF RID: 255
		public Automator _automator;

		// Token: 0x04000100 RID: 256
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000101 RID: 257
		public int _sampledPowerSupply;

		// Token: 0x04000102 RID: 258
		public int _sampledPowerDemand;

		// Token: 0x04000103 RID: 259
		public float _sampledBatteryChargeLevel;
	}
}
