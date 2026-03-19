using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000023 RID: 35
	public class StreamGauge : TickableComponent, IAwakableComponent, IFinishedPostLoadStateListener, IPersistentEntity
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004B3E File Offset: 0x00002D3E
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00004B46 File Offset: 0x00002D46
		public float WaterLevel { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004B4F File Offset: 0x00002D4F
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00004B57 File Offset: 0x00002D57
		public float HighestWaterLevel { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004B60 File Offset: 0x00002D60
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00004B68 File Offset: 0x00002D68
		public float WaterCurrent { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004B71 File Offset: 0x00002D71
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00004B79 File Offset: 0x00002D79
		public float ContaminationLevel { get; private set; }

		// Token: 0x06000167 RID: 359 RVA: 0x00004B82 File Offset: 0x00002D82
		public StreamGauge(IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004B91 File Offset: 0x00002D91
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._streamGaugeAnimationController = base.GetComponent<StreamGaugeAnimationController>();
			this._streamGaugeSpec = base.GetComponent<StreamGaugeSpec>();
			base.DisableComponent();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004BBD File Offset: 0x00002DBD
		public override void Tick()
		{
			this.Update();
			this.UpdateHighestWaterLevel();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004BCB File Offset: 0x00002DCB
		public void ResetHighestWaterLevel()
		{
			this.HighestWaterLevel = 0f;
			this.UpdateMarkerHeight();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004BDE File Offset: 0x00002DDE
		public void OnEnterFinishedPostLoadState()
		{
			this.SetCoordinates();
			this.Update();
			this.UpdateHighestWaterLevel();
			this.UpdateMarkerHeight();
			base.EnableComponent();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004BFE File Offset: 0x00002DFE
		public void OnExitFinishedPostLoadState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004C06 File Offset: 0x00002E06
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(StreamGauge.StreamGaugeKey).Set(StreamGauge.HighestWaterLevelKey, this.HighestWaterLevel);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004C24 File Offset: 0x00002E24
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(StreamGauge.StreamGaugeKey);
			this.HighestWaterLevel = component.Get(StreamGauge.HighestWaterLevelKey);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004C50 File Offset: 0x00002E50
		public void Update()
		{
			if (this._threadSafeWaterMap.WaterHeightOrFloor(this._coordinates) > (float)this._blockObject.CoordinatesAtBaseZ.z)
			{
				this.SetValues();
				return;
			}
			this.ResetValues();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004C94 File Offset: 0x00002E94
		public void SetValues()
		{
			float num = this._threadSafeWaterMap.WaterHeightOrFloor(this._coordinates) - (float)this._blockObject.CoordinatesAtBaseZ.z;
			this.WaterLevel = Mathf.Clamp(num, 0f, this._streamGaugeSpec.MaxWaterLevel);
			Vector2 vector = this._threadSafeWaterMap.WaterFlowDirection(this._coordinates);
			this.WaterCurrent = Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
			this.ContaminationLevel = this._threadSafeWaterMap.ColumnContamination(this._coordinates);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004D2E File Offset: 0x00002F2E
		public void ResetValues()
		{
			this.WaterLevel = 0f;
			this.WaterCurrent = 0f;
			this.ContaminationLevel = 0f;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004D51 File Offset: 0x00002F51
		public void UpdateHighestWaterLevel()
		{
			if (this.WaterLevel > this.HighestWaterLevel)
			{
				this.HighestWaterLevel = this.WaterLevel;
				this.UpdateMarkerHeight();
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004D73 File Offset: 0x00002F73
		public void UpdateMarkerHeight()
		{
			this._streamGaugeAnimationController.SetHeight(this.HighestWaterLevel);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004D86 File Offset: 0x00002F86
		public void SetCoordinates()
		{
			this._coordinates = this._blockObject.PositionedBlocks.GetOccupiedCoordinates().First<Vector3Int>();
		}

		// Token: 0x0400007F RID: 127
		public static readonly ComponentKey StreamGaugeKey = new ComponentKey("StreamGauge");

		// Token: 0x04000080 RID: 128
		public static readonly PropertyKey<float> HighestWaterLevelKey = new PropertyKey<float>("HighestWaterLevel");

		// Token: 0x04000085 RID: 133
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000086 RID: 134
		public BlockObject _blockObject;

		// Token: 0x04000087 RID: 135
		public StreamGaugeAnimationController _streamGaugeAnimationController;

		// Token: 0x04000088 RID: 136
		public StreamGaugeSpec _streamGaugeSpec;

		// Token: 0x04000089 RID: 137
		public Vector3Int _coordinates;
	}
}
