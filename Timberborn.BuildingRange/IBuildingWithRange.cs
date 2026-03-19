using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.BuildingRange
{
	// Token: 0x02000007 RID: 7
	public interface IBuildingWithRange
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7
		string RangeName { get; }

		// Token: 0x06000008 RID: 8
		IEnumerable<Vector3Int> GetBlocksInRange();

		// Token: 0x06000009 RID: 9
		IEnumerable<BaseComponent> GetObjectsInRange();
	}
}
