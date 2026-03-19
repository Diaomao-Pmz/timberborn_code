using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterNavigation;
using Timberborn.Common;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x0200000F RID: 15
	public class OccupiedAccessiblePathStart : BaseComponent, IAwakableComponent, IPathStartProvider
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002603 File Offset: 0x00000803
		public void Awake()
		{
			this._navigator = base.GetComponent<Navigator>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002614 File Offset: 0x00000814
		public bool TryGetPathStart(IDestination destination, List<PathCorner> pathCorners, out Vector3 start)
		{
			Accessible accessible = this._navigator.OccupiedAccessible();
			if (accessible != null)
			{
				AccessibleDestination accessibleDestination = destination as AccessibleDestination;
				if (((accessibleDestination != null) ? accessibleDestination.Accessible : null) != accessible)
				{
					start = accessible.FindExitPath(base.Transform.position, pathCorners);
					if (!pathCorners.IsEmpty<PathCorner>())
					{
						pathCorners.RemoveLast();
					}
					return true;
				}
			}
			start = default(Vector3);
			return false;
		}

		// Token: 0x04000017 RID: 23
		public Navigator _navigator;
	}
}
