using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.CharacterNavigation
{
	// Token: 0x02000005 RID: 5
	public class Navigator : BaseComponent
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
		public Navigator(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002110 File Offset: 0x00000310
		public Vector3 CurrentAccessOrPosition()
		{
			Accessible accessible = this.OccupiedAccessible();
			Vector3? vector = (accessible != null) ? accessible.UnblockedSingleAccess : null;
			if (vector == null)
			{
				return base.Transform.position;
			}
			return vector.GetValueOrDefault();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
		public Accessible OccupiedAccessible()
		{
			Vector3Int coordinates = NavigationCoordinateSystem.WorldToGridInt(base.Transform.position);
			BlockObject bottomObjectAt = this._blockService.GetBottomObjectAt(coordinates);
			if (bottomObjectAt)
			{
				Accessible enabledComponent = bottomObjectAt.GetEnabledComponent<Accessible>();
				if (enabledComponent && enabledComponent.HasSingleAccess)
				{
					return enabledComponent;
				}
			}
			return null;
		}

		// Token: 0x04000006 RID: 6
		public readonly IBlockService _blockService;
	}
}
