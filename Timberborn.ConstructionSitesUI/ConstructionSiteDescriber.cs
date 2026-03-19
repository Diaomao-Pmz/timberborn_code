using System;
using Timberborn.BaseComponentSystem;
using Timberborn.ConstructionSites;
using Timberborn.Localization;
using Timberborn.UIFormatters;

namespace Timberborn.ConstructionSitesUI
{
	// Token: 0x02000005 RID: 5
	public class ConstructionSiteDescriber : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002179 File Offset: 0x00000379
		public ConstructionSiteDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002188 File Offset: 0x00000388
		public void Awake()
		{
			this._constructionSite = base.GetComponent<ConstructionSite>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002196 File Offset: 0x00000396
		public string GetProgressInfoShort()
		{
			return NumberFormatter.FormatAsPercentFloored((double)this._constructionSite.BuildTimeProgress) + " " + this.GetAdditionalInfo();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021BC File Offset: 0x000003BC
		public string GetProgressInfoFull()
		{
			string param = string.Format("{0:0}", this._constructionSite.BuildTimeProgress * 100f);
			return this._loc.T<string>(ConstructionSiteDescriber.ProgressLocKey, param) + " " + this.GetAdditionalInfo();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000220C File Offset: 0x0000040C
		public string GetAdditionalInfo()
		{
			if (this._constructionSite.MaterialProgress < 1f && !this._constructionSite.HasMaterialsToResumeBuilding)
			{
				return " " + this._loc.T(ConstructionSiteDescriber.WaitingForMaterialsLocKey);
			}
			return "";
		}

		// Token: 0x04000009 RID: 9
		public static readonly string ProgressLocKey = "ConstructionSites.Progress";

		// Token: 0x0400000A RID: 10
		public static readonly string WaitingForMaterialsLocKey = "ConstructionSites.Info.WaitingForMaterials";

		// Token: 0x0400000B RID: 11
		public readonly ILoc _loc;

		// Token: 0x0400000C RID: 12
		public ConstructionSite _constructionSite;
	}
}
