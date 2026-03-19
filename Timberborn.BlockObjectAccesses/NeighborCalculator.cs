using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.BlockObjectAccesses
{
	// Token: 0x02000012 RID: 18
	public class NeighborCalculator
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000031AE File Offset: 0x000013AE
		public IEnumerable<Vector3Int> GetNonInternalNeighborsWithoutDiagonal(IEnumerable<Vector3Int> blocks)
		{
			return from neighbor in this.GetNeighbors(blocks, Deltas.Neighbors4Vector3Int, false)
			select neighbor.Neighbor;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000031E1 File Offset: 0x000013E1
		public IEnumerable<ParentedNeighbor> GetNonInternalParentedNeighborsWithDiagonal(IEnumerable<Vector3Int> blocks)
		{
			return this.GetNeighbors(blocks, Deltas.Neighbors8Vector3IntOrdered, true);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000031F0 File Offset: 0x000013F0
		public IEnumerable<ParentedNeighbor> GetNeighbors(IEnumerable<Vector3Int> blocks, IEnumerable<Vector3Int> neighborDeltas, bool allowNeighboursDuplicate = false)
		{
			this._blocks.AddRange(blocks);
			this._checkedNeighbors.AddRange(this._blocks);
			foreach (Vector3Int delta in neighborDeltas)
			{
				foreach (Vector3Int vector3Int in this._blocks)
				{
					Vector3Int vector3Int2 = vector3Int + delta;
					if (!this._blocks.Contains(vector3Int2) && (allowNeighboursDuplicate || this._checkedNeighbors.Add(vector3Int2)))
					{
						yield return new ParentedNeighbor(vector3Int2, vector3Int);
					}
				}
				HashSet<Vector3Int>.Enumerator enumerator2 = default(HashSet<Vector3Int>.Enumerator);
				delta = default(Vector3Int);
			}
			IEnumerator<Vector3Int> enumerator = null;
			this._blocks.Clear();
			this._checkedNeighbors.Clear();
			yield break;
			yield break;
		}

		// Token: 0x0400003E RID: 62
		public readonly HashSet<Vector3Int> _checkedNeighbors = new HashSet<Vector3Int>();

		// Token: 0x0400003F RID: 63
		public readonly HashSet<Vector3Int> _blocks = new HashSet<Vector3Int>();
	}
}
