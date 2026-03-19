using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.LinkedBuildingSystem;
using UnityEngine;

namespace Timberborn.DistributionSystem
{
	// Token: 0x0200000B RID: 11
	public class DistrictCrossing : BaseComponent, IAwakableComponent, IFinishedStateListener, IRegisteredComponent
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000027FB File Offset: 0x000009FB
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002803 File Offset: 0x00000A03
		public DistrictDistributableGoodProvider DistrictDistributableGoodProvider { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000280C File Offset: 0x00000A0C
		public bool CanExport
		{
			get
			{
				return this._districtCrossingValidator.CanExport;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002819 File Offset: 0x00000A19
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._districtCrossingValidator = base.GetComponent<DistrictCrossingValidator>();
			base.GetComponent<LinkedBuilding>().BuildingLinked += this.OnBuildingLinked;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000284A File Offset: 0x00000A4A
		public void OnEnterFinishedState()
		{
			this._districtBuilding.ReassignedDistrict += this.OnReassignedDistrict;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002863 File Offset: 0x00000A63
		public void OnExitFinishedState()
		{
			this._districtBuilding.ReassignedDistrict -= this.OnReassignedDistrict;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000287C File Offset: 0x00000A7C
		public DistributableGood GetMyDistributableGood(string goodId)
		{
			return this.DistrictDistributableGoodProvider.GetDistributableGoodForExport(goodId);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000288A File Offset: 0x00000A8A
		public bool TryGetLinkedDistrictDistributableGood(string goodId, out DistributableGood distributableGood)
		{
			return this._linked.DistrictDistributableGoodProvider.TryGetDistributableGoodForImport(goodId, out distributableGood);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000289E File Offset: 0x00000A9E
		public void GetLinkedDistrictDistributableGoods(List<DistributableGood> distributableGoods)
		{
			this._linked.DistrictDistributableGoodProvider.GetDistributableGoodsForImport(distributableGoods);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028B1 File Offset: 0x00000AB1
		public bool CanExportGood(DistributableGood myDistributableGood, DistributableGood linkedDistributableGood)
		{
			return myDistributableGood.CanExport && myDistributableGood.FillRate > linkedDistributableGood.FillRate;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028CE File Offset: 0x00000ACE
		public int GetAmountToExport(DistributableGood myDistributableGood, DistributableGood linkedDistributableGood)
		{
			if (myDistributableGood.Capacity <= 0)
			{
				return Math.Max(0, linkedDistributableGood.FreeCapacity);
			}
			return DistrictCrossing.GetAmountToEqualizeFillRates(myDistributableGood, linkedDistributableGood);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028EF File Offset: 0x00000AEF
		public void OnBuildingLinked(object sender, LinkedBuilding e)
		{
			this._linked = e.GetComponent<DistrictCrossing>();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028FD File Offset: 0x00000AFD
		public void OnReassignedDistrict(object sender, EventArgs e)
		{
			this.DistrictDistributableGoodProvider = (this._districtBuilding.District ? this._districtBuilding.District.GetComponent<DistrictDistributableGoodProvider>() : null);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000292C File Offset: 0x00000B2C
		public static int GetAmountToEqualizeFillRates(DistributableGood myDistributableGood, DistributableGood linkedDistributableGood)
		{
			float num = myDistributableGood.FillRate - linkedDistributableGood.FillRate;
			int capacity = myDistributableGood.Capacity;
			int capacity2 = linkedDistributableGood.Capacity;
			return Mathf.FloorToInt(Mathf.Min((float)(capacity * capacity2) * num / (float)(capacity + capacity2), myDistributableGood.MaxExportAmount));
		}

		// Token: 0x04000013 RID: 19
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000014 RID: 20
		public DistrictCrossingValidator _districtCrossingValidator;

		// Token: 0x04000015 RID: 21
		public DistrictCrossing _linked;
	}
}
