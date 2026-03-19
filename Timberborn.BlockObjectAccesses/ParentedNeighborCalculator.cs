using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.BlockObjectAccesses
{
	// Token: 0x02000017 RID: 23
	public class ParentedNeighborCalculator : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000036E3 File Offset: 0x000018E3
		public ParentedNeighborCalculator(NeighborCalculator neighborCalculator)
		{
			this._neighborCalculator = neighborCalculator;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000036F2 File Offset: 0x000018F2
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003700 File Offset: 0x00001900
		public IEnumerable<ParentedNeighbor2D> GetNonInternalParentedNeighbors()
		{
			return this._neighborCalculator.GetNonInternalParentedNeighborsWithDiagonal(this.GetBaseLevelOccupiedCoordinates()).Select(new Func<ParentedNeighbor, ParentedNeighbor2D>(ParentedNeighbor2D.From3D)).Distinct<ParentedNeighbor2D>();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003729 File Offset: 0x00001929
		public IEnumerable<Vector3Int> GetBaseLevelOccupiedCoordinates()
		{
			return from coords in this._blockObject.PositionedBlocks.GetOccupiedCoordinates()
			where coords.z == this._blockObject.CoordinatesAtBaseZ.z
			select coords;
		}

		// Token: 0x04000053 RID: 83
		public readonly NeighborCalculator _neighborCalculator;

		// Token: 0x04000054 RID: 84
		public BlockObject _blockObject;
	}
}
