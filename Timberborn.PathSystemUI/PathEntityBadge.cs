using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.PathSystemUI
{
	// Token: 0x02000004 RID: 4
	public class PathEntityBadge : BaseComponent, IAwakableComponent, IEntityBadge
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public PathEntityBadge(EntitySelectionService entitySelectionService, DistanceToColorConverter distanceToColorConverter, DistanceToDistrictDescriber distanceToDistrictDescriber)
		{
			this._entitySelectionService = entitySelectionService;
			this._distanceToColorConverter = distanceToColorConverter;
			this._distanceToDistrictDescriber = distanceToDistrictDescriber;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020D8 File Offset: 0x000002D8
		public int EntityBadgePriority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DC File Offset: 0x000002DC
		public void Awake()
		{
			this._labeledEntity = base.GetComponent<LabeledEntity>();
			this._pathDistrictRetriever = base.GetComponent<PathDistrictRetriever>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F6 File Offset: 0x000002F6
		public string GetEntitySubtitle()
		{
			return "";
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public ClickableSubtitle GetEntityClickableSubtitle()
		{
			DistrictCenter district;
			float distance;
			if (this._pathDistrictRetriever.TryGetDistanceToDistrictCenter(out district, out distance))
			{
				return ClickableSubtitle.Create(delegate()
				{
					this._entitySelectionService.SelectAndFocusOn(district);
				}, this.GetSubtitleText(district, distance), this._distanceToDistrictDescriber.DescribeDistance(distance), false);
			}
			return ClickableSubtitle.CreateEmpty();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002160 File Offset: 0x00000360
		public Sprite GetEntityAvatar()
		{
			return this._labeledEntity.Image;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002170 File Offset: 0x00000370
		public string GetSubtitleText(DistrictCenter district, float distance)
		{
			string arg = ColorUtility.ToHtmlStringRGB(this._distanceToColorConverter.DistanceToColor(distance));
			int num = Mathf.RoundToInt(distance);
			return string.Format("{0} <color=#{1}>({2})</color>", district.DistrictName, arg, num);
		}

		// Token: 0x04000006 RID: 6
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000007 RID: 7
		public readonly DistanceToColorConverter _distanceToColorConverter;

		// Token: 0x04000008 RID: 8
		public readonly DistanceToDistrictDescriber _distanceToDistrictDescriber;

		// Token: 0x04000009 RID: 9
		public LabeledEntity _labeledEntity;

		// Token: 0x0400000A RID: 10
		public PathDistrictRetriever _pathDistrictRetriever;
	}
}
