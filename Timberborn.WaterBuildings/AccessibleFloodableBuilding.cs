using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.WaterObjects;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000007 RID: 7
	public class AccessibleFloodableBuilding : BaseComponent, IAwakableComponent, IWaterObjectSpecification
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public Vector3Int WaterCoordinates { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
		public void Awake()
		{
			BlockObjectSpec component = base.GetComponent<BlockObjectSpec>();
			this.WaterCoordinates = (component.Entrance.HasEntrance ? new Vector3Int(component.Entrance.Coordinates.x, component.Entrance.Coordinates.y, component.Entrance.Coordinates.z - component.BaseZ) : Vector3Int.zero);
		}
	}
}
