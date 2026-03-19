using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterObjects
{
	// Token: 0x02000012 RID: 18
	public class WaterObject : BaseComponent, IAwakableComponent, IFinishedPostLoadStateListener
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600006B RID: 107 RVA: 0x00002A7C File Offset: 0x00000C7C
		// (remove) Token: 0x0600006C RID: 108 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public event EventHandler WaterAboveBaseChanged;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002AE9 File Offset: 0x00000CE9
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public int WaterAboveBase { get; private set; }

		// Token: 0x0600006F RID: 111 RVA: 0x00002AFA File Offset: 0x00000CFA
		public WaterObject(WaterObjectService waterObjectService, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._waterObjectService = waterObjectService;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002B10 File Offset: 0x00000D10
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._specification = base.GetComponent<IWaterObjectSpecification>();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002B2A File Offset: 0x00000D2A
		public void OnEnterFinishedPostLoadState()
		{
			this._baseCoordinates = this.GetBaseCoordinates();
			this._waterObjectService.RegisterWaterObject(this);
			this.UpdateWaterAboveBase(this.CurrentWaterAboveBase(this._baseCoordinates));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B56 File Offset: 0x00000D56
		public void OnExitFinishedPostLoadState()
		{
			this._waterObjectService.UnregisterWaterObject(this);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002B64 File Offset: 0x00000D64
		public void UpdateWaterAboveBase()
		{
			int num = this.CurrentWaterAboveBase(this._baseCoordinates);
			if (num != this.WaterAboveBase)
			{
				this.UpdateWaterAboveBase(num);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002B8E File Offset: 0x00000D8E
		public bool IsPreviewUnderWater()
		{
			return this.CurrentWaterAboveBase(this.GetBaseCoordinates()) > 0;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002B9F File Offset: 0x00000D9F
		public Vector3Int GetBaseCoordinates()
		{
			return this._blockObject.TransformCoordinates(this._specification.WaterCoordinates) + new Vector3Int(0, 0, this._blockObject.BaseZ);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public int CurrentWaterAboveBase(Vector3Int coordinatesToCheck)
		{
			int num = this._threadSafeWaterMap.CeiledWaterHeight(coordinatesToCheck) - coordinatesToCheck.z;
			return Mathf.Max(0, num);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002BF9 File Offset: 0x00000DF9
		public void UpdateWaterAboveBase(int currentWaterAboveBase)
		{
			this.WaterAboveBase = currentWaterAboveBase;
			EventHandler waterAboveBaseChanged = this.WaterAboveBaseChanged;
			if (waterAboveBaseChanged == null)
			{
				return;
			}
			waterAboveBaseChanged(this, EventArgs.Empty);
		}

		// Token: 0x0400001A RID: 26
		public readonly WaterObjectService _waterObjectService;

		// Token: 0x0400001B RID: 27
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400001C RID: 28
		public BlockObject _blockObject;

		// Token: 0x0400001D RID: 29
		public IWaterObjectSpecification _specification;

		// Token: 0x0400001E RID: 30
		public Vector3Int _baseCoordinates;
	}
}
