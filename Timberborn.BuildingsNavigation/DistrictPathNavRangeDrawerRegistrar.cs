using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000017 RID: 23
	public class DistrictPathNavRangeDrawerRegistrar : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003AB3 File Offset: 0x00001CB3
		public DistrictPathNavRangeDrawerRegistrar(PathNavRangeDrawerInvalidator pathNavRangeDrawerInvalidator)
		{
			this._pathNavRangeDrawerInvalidator = pathNavRangeDrawerInvalidator;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003AC2 File Offset: 0x00001CC2
		public void Awake()
		{
			this._districtPathNavRangeDrawer = base.GetComponent<DistrictPathNavRangeDrawer>();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public void InitializeEntity()
		{
			this._pathNavRangeDrawerInvalidator.AddDistrictDrawer(this._districtPathNavRangeDrawer);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003AE3 File Offset: 0x00001CE3
		public void DeleteEntity()
		{
			this._pathNavRangeDrawerInvalidator.RemoveDistrictDrawer(this._districtPathNavRangeDrawer);
		}

		// Token: 0x04000057 RID: 87
		public readonly PathNavRangeDrawerInvalidator _pathNavRangeDrawerInvalidator;

		// Token: 0x04000058 RID: 88
		public DistrictPathNavRangeDrawer _districtPathNavRangeDrawer;
	}
}
