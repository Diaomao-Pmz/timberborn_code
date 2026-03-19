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
	// Token: 0x0200000D RID: 13
	public class DepthSensor : BaseComponent, IAwakableComponent, IInitializableEntity, IPersistentEntity, IDuplicable<DepthSensor>, IDuplicable, ISamplingTransmitter, ITransmitter
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002B69 File Offset: 0x00000D69
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002B71 File Offset: 0x00000D71
		public Vector3Int SensorCoordinates { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002B7A File Offset: 0x00000D7A
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002B82 File Offset: 0x00000D82
		public NumericComparisonMode Mode { get; private set; }

		// Token: 0x06000050 RID: 80 RVA: 0x00002B8B File Offset: 0x00000D8B
		public DepthSensor(IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002B9A File Offset: 0x00000D9A
		public int MinThreshold
		{
			get
			{
				return this._sampledFloor;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public float MaxThreshold
		{
			get
			{
				return (float)this.SensorCoordinates.z + this._depthSensorSpec.SensorHeightOffset;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002BCC File Offset: 0x00000DCC
		public float Threshold
		{
			get
			{
				float? rawThreshold = this._rawThreshold;
				if (rawThreshold == null)
				{
					throw new Exception("_rawThreshold not initialzed.");
				}
				return Mathf.Clamp(rawThreshold.GetValueOrDefault(), (float)this.MinThreshold, this.MaxThreshold);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002C0D File Offset: 0x00000E0D
		public float ThresholdFromFloor
		{
			get
			{
				return this.Threshold - (float)this._sampledFloor;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002C1D File Offset: 0x00000E1D
		public float Depth
		{
			get
			{
				return Mathf.Clamp(this._sampledWaterHeight, (float)this.MinThreshold, this.MaxThreshold);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002C37 File Offset: 0x00000E37
		public float DepthFromFloor
		{
			get
			{
				return Mathf.Clamp(this._sampledWaterHeight - (float)this._sampledFloor, 0f, this.MaxThreshold - (float)this.MinThreshold);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C5F File Offset: 0x00000E5F
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._depthSensorSpec = base.GetComponent<DepthSensorSpec>();
			base.DisableComponent();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002C8C File Offset: 0x00000E8C
		public void InitializeEntity()
		{
			this.InitializeSensorCoordinates();
			float value = this._rawThreshold.GetValueOrDefault();
			if (this._rawThreshold == null)
			{
				value = (float)this.SensorCoordinates.z - DepthSensor.DefaultDepthOffset;
				this._rawThreshold = new float?(value);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002CDA File Offset: 0x00000EDA
		public void SetThreshold(float value)
		{
			if (!this._rawThreshold.Equals(value))
			{
				this._rawThreshold = new float?(value);
				this.UpdateOutputState();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D07 File Offset: 0x00000F07
		public void SetMode(NumericComparisonMode mode)
		{
			if (this.Mode != mode)
			{
				this.Mode = mode;
				this.UpdateOutputState();
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D20 File Offset: 0x00000F20
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(DepthSensor.DepthSensorKey);
			if (this._rawThreshold != null)
			{
				component.Set(DepthSensor.ThresholdKey, this._rawThreshold.Value);
			}
			component.Set<NumericComparisonMode>(DepthSensor.ModeKey, this.Mode);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D70 File Offset: 0x00000F70
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(DepthSensor.DepthSensorKey);
			if (component.Has<float>(DepthSensor.ThresholdKey))
			{
				this._rawThreshold = new float?(component.Get(DepthSensor.ThresholdKey));
			}
			this.Mode = component.Get<NumericComparisonMode>(DepthSensor.ModeKey);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002DBD File Offset: 0x00000FBD
		public void DuplicateFrom(DepthSensor source)
		{
			this.InitializeSensorCoordinates();
			this._rawThreshold = new float?((float)this.GetCurrentFloor() + source.ThresholdFromFloor);
			this.Mode = source.Mode;
			this.UpdateOutputState();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public void Sample()
		{
			this._sampledWaterHeight = this._threadSafeWaterMap.WaterHeightOrFloor(this.SensorCoordinates);
			this._sampledFloor = this.GetCurrentFloor();
			this.UpdateOutputState();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E1B File Offset: 0x0000101B
		public void InitializeSensorCoordinates()
		{
			this.SensorCoordinates = this._blockObject.TransformCoordinates(this._depthSensorSpec.SensorCoordinates);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E39 File Offset: 0x00001039
		public void UpdateOutputState()
		{
			this._automator.SetState(this.Mode.Evaluate(this._sampledWaterHeight, this.Threshold));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E60 File Offset: 0x00001060
		public int GetCurrentFloor()
		{
			int result;
			if (!this._threadSafeWaterMap.TryGetColumnFloor(this.SensorCoordinates, out result))
			{
				return this.SensorCoordinates.z;
			}
			return result;
		}

		// Token: 0x04000025 RID: 37
		public static readonly ComponentKey DepthSensorKey = new ComponentKey("DepthSensor");

		// Token: 0x04000026 RID: 38
		public static readonly PropertyKey<float> ThresholdKey = new PropertyKey<float>("Threshold");

		// Token: 0x04000027 RID: 39
		public static readonly PropertyKey<NumericComparisonMode> ModeKey = new PropertyKey<NumericComparisonMode>("Mode");

		// Token: 0x04000028 RID: 40
		public static readonly float DefaultDepthOffset = 0.5f;

		// Token: 0x0400002B RID: 43
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400002C RID: 44
		public Automator _automator;

		// Token: 0x0400002D RID: 45
		public BlockObject _blockObject;

		// Token: 0x0400002E RID: 46
		public DepthSensorSpec _depthSensorSpec;

		// Token: 0x0400002F RID: 47
		public float? _rawThreshold;

		// Token: 0x04000030 RID: 48
		public float _sampledWaterHeight;

		// Token: 0x04000031 RID: 49
		public int _sampledFloor;
	}
}
