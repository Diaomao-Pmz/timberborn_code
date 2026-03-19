using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.DropdownSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using UnityEngine;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x0200000E RID: 14
	public class ManualMigrationDistrictDropdownProvider : IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000267F File Offset: 0x0000087F
		public ManualMigrationDistrictDropdownProvider(DistrictCenterRegistry districtCenterRegistry, Action<DistrictCenter> districtChangedAction)
		{
			this._districtCenterRegistry = districtCenterRegistry;
			this._districtChangedAction = districtChangedAction;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000026A0 File Offset: 0x000008A0
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._districtKeys.AsReadOnlyList<string>();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026B2 File Offset: 0x000008B2
		public void SetDistrict(DistrictCenter selectedDistrict)
		{
			this._selectedDistrict = selectedDistrict;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026BC File Offset: 0x000008BC
		public void UpdateDistrictsList()
		{
			this._districtKeys.Clear();
			for (int i = 0; i < this._districtCenterRegistry.FinishedDistrictCenters.Count; i++)
			{
				this._districtKeys.Add(i.ToString());
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002704 File Offset: 0x00000904
		public string GetValue()
		{
			return this._districtCenterRegistry.FinishedDistrictCenters.IndexOf(this._selectedDistrict).ToString();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002734 File Offset: 0x00000934
		public void SetValue(string value)
		{
			this.SelectDistrict(this.GetDistrict(value));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002743 File Offset: 0x00000943
		public string FormatDisplayText(string value, bool selected)
		{
			return this.GetDistrict(value).DistrictName;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002751 File Offset: 0x00000951
		public Sprite GetIcon(string value)
		{
			return this.GetDistrict(value).GetComponent<LabeledEntity>().Image;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002764 File Offset: 0x00000964
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ManualMigrationDistrictDropdownProvider.DropdownItemClasses;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000276C File Offset: 0x0000096C
		public DistrictCenter GetDistrict(string value)
		{
			int index = int.Parse(value);
			return this._districtCenterRegistry.FinishedDistrictCenters[index];
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002794 File Offset: 0x00000994
		public void SelectDistrict(DistrictCenter districtCenter)
		{
			this._districtChangedAction(districtCenter);
		}

		// Token: 0x04000023 RID: 35
		public static readonly ImmutableArray<string> DropdownItemClasses = ImmutableArray.Create<string>("dropdown-item--large");

		// Token: 0x04000024 RID: 36
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000025 RID: 37
		public readonly Action<DistrictCenter> _districtChangedAction;

		// Token: 0x04000026 RID: 38
		public readonly List<string> _districtKeys = new List<string>();

		// Token: 0x04000027 RID: 39
		public DistrictCenter _selectedDistrict;
	}
}
