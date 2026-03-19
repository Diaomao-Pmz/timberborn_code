using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000024 RID: 36
	public class BlockOccupant : BaseComponent, IRegisteredComponent
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000047E9 File Offset: 0x000029E9
		public Vector3 GridCoordinates
		{
			get
			{
				return CoordinateSystem.WorldToGrid(base.Transform.position);
			}
		}
	}
}
