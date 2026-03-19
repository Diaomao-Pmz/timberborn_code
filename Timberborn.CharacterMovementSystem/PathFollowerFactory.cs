using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Navigation;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x02000013 RID: 19
	public class PathFollowerFactory
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00003770 File Offset: 0x00001970
		public PathFollowerFactory(INavigationService navigationService)
		{
			this._navigationService = navigationService;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000377F File Offset: 0x0000197F
		public PathFollower Create(BaseComponent owner)
		{
			return new PathFollower(this._navigationService, owner.GetComponent<MovementAnimator>(), owner.Transform);
		}

		// Token: 0x04000048 RID: 72
		public readonly INavigationService _navigationService;
	}
}
