using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000008 RID: 8
	public class TubeAccessiblePathModifier : BaseComponent, IAwakableComponent, IPostInitializableEntity, IPathToAccessibleModifier
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000022FB File Offset: 0x000004FB
		public TubeAccessiblePathModifier(INavigationService navigationService)
		{
			this._navigationService = navigationService;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002315 File Offset: 0x00000515
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002323 File Offset: 0x00000523
		public void PostInitializeEntity()
		{
			this._accessible = base.GetEnabledComponent<Accessible>();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002334 File Offset: 0x00000534
		public void ModifyPath(Vector3 start, List<PathCorner> pathCorners, ref float distance)
		{
			if (this._accessible && this._blockObject.IsUnfinished)
			{
				float num;
				if (this._navigationService.FindRoadSpillOrTerrainPathUnlimitedRange(start, this._accessible.Accesses, this._pathCornerCache, out num) && num < distance)
				{
					pathCorners.Clear();
					pathCorners.AddRange(this._pathCornerCache);
					distance = num;
				}
				this._pathCornerCache.Clear();
			}
		}

		// Token: 0x0400000E RID: 14
		public readonly INavigationService _navigationService;

		// Token: 0x0400000F RID: 15
		public Accessible _accessible;

		// Token: 0x04000010 RID: 16
		public BlockObject _blockObject;

		// Token: 0x04000011 RID: 17
		public readonly List<PathCorner> _pathCornerCache = new List<PathCorner>();
	}
}
