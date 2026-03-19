using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.EntityNaming;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x0200000C RID: 12
	public class DistrictBuildingEntityBadge : BaseComponent, IAwakableComponent, IEntityBadge
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000248E File Offset: 0x0000068E
		public DistrictBuildingEntityBadge(EntitySelectionService entitySelectionService, DistanceToColorConverter distanceToColorConverter)
		{
			this._entitySelectionService = entitySelectionService;
			this._distanceToColorConverter = distanceToColorConverter;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000024A4 File Offset: 0x000006A4
		public int EntityBadgePriority
		{
			get
			{
				return 110;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024A8 File Offset: 0x000006A8
		public void Awake()
		{
			this._labeledEntity = base.GetComponent<LabeledEntity>();
			this._namedEntity = base.GetComponent<NamedEntity>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._districtBuildingDistance = base.GetComponent<DistrictBuildingDistance>();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024DC File Offset: 0x000006DC
		public string GetEntitySubtitle()
		{
			NamedEntity namedEntity = this._namedEntity;
			if (namedEntity == null || !namedEntity.IsEditable)
			{
				return "";
			}
			return this._labeledEntity.DisplayName;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000250C File Offset: 0x0000070C
		public ClickableSubtitle GetEntityClickableSubtitle()
		{
			DistrictCenter district = this._districtBuilding.GetInstantOrConstructionDistrict();
			if (district != null)
			{
				return ClickableSubtitle.Create(delegate()
				{
					this._entitySelectionService.SelectAndFocusOn(district);
				}, this.GetSubtitleText(district), this._districtBuildingDistance.DescribeDistance(), this._districtBuildingDistance.IsAboveThreshold());
			}
			return ClickableSubtitle.CreateEmpty();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002578 File Offset: 0x00000778
		public Sprite GetEntityAvatar()
		{
			return this._labeledEntity.Image;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002588 File Offset: 0x00000788
		public string GetSubtitleText(DistrictCenter district)
		{
			int num;
			if (this._districtBuildingDistance.TryGetDistanceToDistrict(out num))
			{
				string arg = ColorUtility.ToHtmlStringRGB(this._distanceToColorConverter.DistanceToColor((float)num));
				return string.Format("{0} <color=#{1}>({2})</color>", district.DistrictName, arg, num);
			}
			return district.DistrictName;
		}

		// Token: 0x04000010 RID: 16
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000011 RID: 17
		public readonly DistanceToColorConverter _distanceToColorConverter;

		// Token: 0x04000012 RID: 18
		public LabeledEntity _labeledEntity;

		// Token: 0x04000013 RID: 19
		public NamedEntity _namedEntity;

		// Token: 0x04000014 RID: 20
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000015 RID: 21
		public DistrictBuildingDistance _districtBuildingDistance;
	}
}
