using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Navigation;

namespace Timberborn.WalkingSystem
{
	// Token: 0x0200000E RID: 14
	public class NavMeshProximityValidator : BaseComponent, INavMeshProximityValidator
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000025DC File Offset: 0x000007DC
		public NavMeshProximityValidator(INavigationService navigationService)
		{
			this._navigationService = navigationService;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025EB File Offset: 0x000007EB
		public bool IsOnNavMesh()
		{
			return this._navigationService.IsOnNavMesh(base.Transform.position);
		}

		// Token: 0x04000016 RID: 22
		public readonly INavigationService _navigationService;
	}
}
