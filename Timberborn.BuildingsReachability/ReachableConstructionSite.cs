using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;
using Timberborn.BuildingsNavigation;
using Timberborn.ConstructionSites;
using Timberborn.Navigation;

namespace Timberborn.BuildingsReachability
{
	// Token: 0x02000010 RID: 16
	public class ReachableConstructionSite : BaseComponent, IAwakableComponent, IUnreachableEntity
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002689 File Offset: 0x00000889
		public ReachableConstructionSite(IDistrictService districtService)
		{
			this._districtService = districtService;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002698 File Offset: 0x00000898
		public void Awake()
		{
			this._constructionSiteAccessible = base.GetComponent<ConstructionSiteAccessible>();
			this._constructionSite = base.GetComponent<ConstructionSite>();
			this._buildingSpec = base.GetComponent<BuildingSpec>();
			this._expandedConstructionSiteReachability = base.GetComponent<IExpandedConstructionSiteReachability>();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026CA File Offset: 0x000008CA
		public bool IsUnreachable()
		{
			return !this._buildingSpec.PlaceFinished && this._constructionSite.IsOn && !this.IsReachableByBuilders();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026F1 File Offset: 0x000008F1
		public bool IsReachableByBuilders()
		{
			return !this._constructionSiteAccessible.Accessible.Enabled || this.IsReachableFromBuilderHub() || this.IsReachableByExpandedConstructionSite();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002715 File Offset: 0x00000915
		public bool IsReachableFromBuilderHub()
		{
			return this._districtService.IsOnInstantDistrictRoadSpill(this._constructionSiteAccessible.Accessible);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000272D File Offset: 0x0000092D
		public bool IsReachableByExpandedConstructionSite()
		{
			return this._expandedConstructionSiteReachability != null && this._expandedConstructionSiteReachability.IsReachable();
		}

		// Token: 0x04000020 RID: 32
		public readonly IDistrictService _districtService;

		// Token: 0x04000021 RID: 33
		public ConstructionSiteAccessible _constructionSiteAccessible;

		// Token: 0x04000022 RID: 34
		public ConstructionSite _constructionSite;

		// Token: 0x04000023 RID: 35
		public BuildingSpec _buildingSpec;

		// Token: 0x04000024 RID: 36
		public IExpandedConstructionSiteReachability _expandedConstructionSiteReachability;
	}
}
