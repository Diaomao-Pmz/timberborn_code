using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WaterSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000011 RID: 17
	public class FlowSensor : BaseComponent, IAwakableComponent, IInitializableEntity, IPersistentEntity, IDuplicable<FlowSensor>, IDuplicable, ISamplingTransmitter, ITransmitter
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000032C6 File Offset: 0x000014C6
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000032CE File Offset: 0x000014CE
		public float Threshold { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000032D7 File Offset: 0x000014D7
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000032DF File Offset: 0x000014DF
		public float? SampledFlow { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000032E8 File Offset: 0x000014E8
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000032F0 File Offset: 0x000014F0
		public NumericComparisonMode Mode { get; private set; }

		// Token: 0x0600008E RID: 142 RVA: 0x000032F9 File Offset: 0x000014F9
		public FlowSensor(IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003308 File Offset: 0x00001508
		public float MaxThreshold
		{
			get
			{
				return this._flowSensorSpec.MaxThreshold;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003315 File Offset: 0x00001515
		public void Awake()
		{
			this._flowSensorSpec = base.GetComponent<FlowSensorSpec>();
			this._automator = base.GetComponent<Automator>();
			this._blockObject = base.GetComponent<BlockObject>();
			this.Threshold = FlowSensor.DefaultThreshold;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003346 File Offset: 0x00001546
		public void InitializeEntity()
		{
			this._sensorCoordinates = this._blockObject.TransformCoordinates(this._flowSensorSpec.SensorCoordinates);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003364 File Offset: 0x00001564
		public void SetThreshold(float value)
		{
			if (!this.Threshold.Equals(value))
			{
				this.Threshold = value;
				this.UpdateOutputState();
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000338F File Offset: 0x0000158F
		public void SetMode(NumericComparisonMode mode)
		{
			if (this.Mode != mode)
			{
				this.Mode = mode;
				this.UpdateOutputState();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000033A7 File Offset: 0x000015A7
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(FlowSensor.ContaminationSensorKey);
			component.Set(FlowSensor.ThresholdKey, this.Threshold);
			component.Set<NumericComparisonMode>(FlowSensor.ModeKey, this.Mode);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000033D8 File Offset: 0x000015D8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(FlowSensor.ContaminationSensorKey);
			this.Threshold = component.Get(FlowSensor.ThresholdKey);
			this.Mode = component.Get<NumericComparisonMode>(FlowSensor.ModeKey);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003413 File Offset: 0x00001613
		public void DuplicateFrom(FlowSensor source)
		{
			this.Threshold = source.Threshold;
			this.Mode = source.Mode;
			this.UpdateOutputState();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003434 File Offset: 0x00001634
		public void Sample()
		{
			int floor;
			this.SampledFlow = (this.HasWaterBelow(out floor) ? new float?(this.GetFlow(floor)) : null);
			this.UpdateOutputState();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003470 File Offset: 0x00001670
		public float GetFlow(int floor)
		{
			return this._threadSafeWaterMap.WaterFlowDirection(new Vector3Int(this._sensorCoordinates.x, this._sensorCoordinates.y, floor)).magnitude;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000034AC File Offset: 0x000016AC
		public bool HasWaterBelow(out int floor)
		{
			return this._threadSafeWaterMap.TryGetColumnFloor(this._sensorCoordinates, out floor);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000034C0 File Offset: 0x000016C0
		public void UpdateOutputState()
		{
			this._automator.SetState(this.SampledFlow != null && this.Mode.Evaluate(this.SampledFlow.Value, this.Threshold));
		}

		// Token: 0x0400003C RID: 60
		public static readonly ComponentKey ContaminationSensorKey = new ComponentKey("FlowSensor");

		// Token: 0x0400003D RID: 61
		public static readonly PropertyKey<float> ThresholdKey = new PropertyKey<float>("Threshold");

		// Token: 0x0400003E RID: 62
		public static readonly PropertyKey<NumericComparisonMode> ModeKey = new PropertyKey<NumericComparisonMode>("Mode");

		// Token: 0x0400003F RID: 63
		public static readonly float DefaultThreshold = 0f;

		// Token: 0x04000043 RID: 67
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000044 RID: 68
		public FlowSensorSpec _flowSensorSpec;

		// Token: 0x04000045 RID: 69
		public Automator _automator;

		// Token: 0x04000046 RID: 70
		public BlockObject _blockObject;

		// Token: 0x04000047 RID: 71
		public Vector3Int _sensorCoordinates;
	}
}
