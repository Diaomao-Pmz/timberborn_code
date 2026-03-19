using System;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000011 RID: 17
	public class PositionDestinationFactory
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000028B5 File Offset: 0x00000AB5
		public PositionDestinationFactory(INavigationService navigationService, WalkerService walkerService)
		{
			this._navigationService = navigationService;
			this._walkerService = walkerService;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028CB File Offset: 0x00000ACB
		public PositionDestination Create(Vector3 position, float stoppingDistance)
		{
			return new PositionDestination(this._navigationService, this._walkerService, position, stoppingDistance);
		}

		// Token: 0x0400001C RID: 28
		public readonly INavigationService _navigationService;

		// Token: 0x0400001D RID: 29
		public readonly WalkerService _walkerService;
	}
}
