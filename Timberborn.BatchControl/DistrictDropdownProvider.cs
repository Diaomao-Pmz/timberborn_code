using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.AssetSystem;
using Timberborn.Common;
using Timberborn.DropdownSystem;
using Timberborn.GameDistricts;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BatchControl
{
	// Token: 0x0200001D RID: 29
	public class DistrictDropdownProvider : IExtendedDropdownProvider, IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003C11 File Offset: 0x00001E11
		public DistrictDropdownProvider(BatchControlDistrict batchControlDistrict, DistrictCenterRegistry districtCenterRegistry, ILoc loc, IAssetLoader assetLoader)
		{
			this._batchControlDistrict = batchControlDistrict;
			this._districtCenterRegistry = districtCenterRegistry;
			this._loc = loc;
			this._assetLoader = assetLoader;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003C41 File Offset: 0x00001E41
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._districtKeys.AsReadOnlyList<string>();
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003C53 File Offset: 0x00001E53
		public void Load()
		{
			this._districtIcon = this._assetLoader.Load<Sprite>("UI/Images/Game/ico-district");
			this._globalIcon = this._assetLoader.Load<Sprite>("UI/Images/Game/ico-global");
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003C84 File Offset: 0x00001E84
		public void UpdateDistrictsList()
		{
			this._districtKeys.Clear();
			this._districtKeys.Add(DistrictDropdownProvider.GlobalViewKey);
			for (int i = 0; i < this._districtCenterRegistry.FinishedDistrictCenters.Count; i++)
			{
				this._districtKeys.Add(i.ToString());
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003CDC File Offset: 0x00001EDC
		public string GetValue()
		{
			DistrictCenter selectedDistrict = this._batchControlDistrict.SelectedDistrict;
			if (selectedDistrict)
			{
				return this._districtCenterRegistry.FinishedDistrictCenters.IndexOf(selectedDistrict).ToString();
			}
			return DistrictDropdownProvider.GlobalViewKey;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003D21 File Offset: 0x00001F21
		public void SetValue(string value)
		{
			if (value == DistrictDropdownProvider.GlobalViewKey)
			{
				this.SelectDistrict(null);
				return;
			}
			this.SelectDistrict(this.GetDistrict(value));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003D45 File Offset: 0x00001F45
		public string FormatDisplayText(string value, bool selected)
		{
			if (value == DistrictDropdownProvider.GlobalViewKey)
			{
				return this._loc.T(DistrictDropdownProvider.GlobalViewLocKey);
			}
			return this.GetDistrict(value).DistrictName;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003D71 File Offset: 0x00001F71
		public Sprite GetIcon(string value)
		{
			if (!(value == DistrictDropdownProvider.GlobalViewKey))
			{
				return this._districtIcon;
			}
			return this._globalIcon;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003D8D File Offset: 0x00001F8D
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return DistrictDropdownProvider.DropdownItemClasses;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003D94 File Offset: 0x00001F94
		public DistrictCenter GetDistrict(string value)
		{
			int index = int.Parse(value);
			return this._districtCenterRegistry.FinishedDistrictCenters[index];
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003DBC File Offset: 0x00001FBC
		public void SelectDistrict(DistrictCenter districtCenter)
		{
			this._batchControlDistrict.SetDistrict(districtCenter);
		}

		// Token: 0x04000062 RID: 98
		public static readonly string GlobalViewLocKey = "Districts.GlobalView";

		// Token: 0x04000063 RID: 99
		public static readonly string GlobalViewKey = "Global";

		// Token: 0x04000064 RID: 100
		public static readonly ImmutableArray<string> DropdownItemClasses = ImmutableArray.Create<string>("dropdown-item--medium");

		// Token: 0x04000065 RID: 101
		public readonly BatchControlDistrict _batchControlDistrict;

		// Token: 0x04000066 RID: 102
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000067 RID: 103
		public readonly ILoc _loc;

		// Token: 0x04000068 RID: 104
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000069 RID: 105
		public readonly List<string> _districtKeys = new List<string>();

		// Token: 0x0400006A RID: 106
		public Sprite _districtIcon;

		// Token: 0x0400006B RID: 107
		public Sprite _globalIcon;
	}
}
