using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WaterSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200000B RID: 11
	public class ContaminationSensor : BaseComponent, IAwakableComponent, IInitializableEntity, IPersistentEntity, IDuplicable<ContaminationSensor>, IDuplicable, ISamplingTransmitter, ITransmitter
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002780 File Offset: 0x00000980
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002788 File Offset: 0x00000988
		public float Threshold { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002791 File Offset: 0x00000991
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002799 File Offset: 0x00000999
		public float? SampledContamination { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000027A2 File Offset: 0x000009A2
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000027AA File Offset: 0x000009AA
		public NumericComparisonMode Mode { get; private set; }

		// Token: 0x06000031 RID: 49 RVA: 0x000027B3 File Offset: 0x000009B3
		public ContaminationSensor(IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027C2 File Offset: 0x000009C2
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			this._blockObject = base.GetComponent<BlockObject>();
			this.Threshold = ContaminationSensor.DefaultThreshold;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027E7 File Offset: 0x000009E7
		public void InitializeEntity()
		{
			this._sensorCoordinates = this._blockObject.TransformCoordinates(base.GetComponent<ContaminationSensorSpec>().SensorCoordinates);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002808 File Offset: 0x00000A08
		public void SetThreshold(float value)
		{
			if (!this.Threshold.Equals(value))
			{
				this.Threshold = value;
				this.UpdateOutputState();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002833 File Offset: 0x00000A33
		public void SetMode(NumericComparisonMode mode)
		{
			if (this.Mode != mode)
			{
				this.Mode = mode;
				this.UpdateOutputState();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000284B File Offset: 0x00000A4B
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(ContaminationSensor.ContaminationSensorKey);
			component.Set(ContaminationSensor.ThresholdKey, this.Threshold);
			component.Set<NumericComparisonMode>(ContaminationSensor.ModeKey, this.Mode);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000287C File Offset: 0x00000A7C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(ContaminationSensor.ContaminationSensorKey, out objectLoader) || entityLoader.TryGetComponent(new ComponentKey("WaterContaminationSensor"), out objectLoader))
			{
				this.Threshold = objectLoader.Get(ContaminationSensor.ThresholdKey);
				if (objectLoader.Has<NumericComparisonMode>(ContaminationSensor.ModeKey))
				{
					this.Mode = objectLoader.Get<NumericComparisonMode>(ContaminationSensor.ModeKey);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028DB File Offset: 0x00000ADB
		public void DuplicateFrom(ContaminationSensor source)
		{
			this.Threshold = source.Threshold;
			this.Mode = source.Mode;
			this.UpdateOutputState();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028FC File Offset: 0x00000AFC
		public void Sample()
		{
			int floor;
			this.SampledContamination = (this.HasWaterBelow(out floor) ? new float?(this.GetContamination(floor)) : null);
			this.UpdateOutputState();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002936 File Offset: 0x00000B36
		public float GetContamination(int floor)
		{
			return Numbers.RoundToPrecision(this._threadSafeWaterMap.ColumnContamination(new Vector3Int(this._sensorCoordinates.x, this._sensorCoordinates.y, floor)), ContaminationSensor.Precision);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002969 File Offset: 0x00000B69
		public bool HasWaterBelow(out int floor)
		{
			return this._threadSafeWaterMap.TryGetColumnFloor(this._sensorCoordinates, out floor);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002980 File Offset: 0x00000B80
		public void UpdateOutputState()
		{
			this._automator.SetState(this.SampledContamination != null && this.Mode.Evaluate(this.SampledContamination.Value, Numbers.RoundToPrecision(this.Threshold, ContaminationSensor.Precision)));
		}

		// Token: 0x04000018 RID: 24
		public static readonly float Precision = 0.01f;

		// Token: 0x04000019 RID: 25
		public static readonly float DefaultThreshold = 0.05f;

		// Token: 0x0400001A RID: 26
		public static readonly ComponentKey ContaminationSensorKey = new ComponentKey("ContaminationSensor");

		// Token: 0x0400001B RID: 27
		public static readonly PropertyKey<float> ThresholdKey = new PropertyKey<float>("Threshold");

		// Token: 0x0400001C RID: 28
		public static readonly PropertyKey<NumericComparisonMode> ModeKey = new PropertyKey<NumericComparisonMode>("Mode");

		// Token: 0x04000020 RID: 32
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000021 RID: 33
		public Automator _automator;

		// Token: 0x04000022 RID: 34
		public BlockObject _blockObject;

		// Token: 0x04000023 RID: 35
		public Vector3Int _sensorCoordinates;
	}
}
