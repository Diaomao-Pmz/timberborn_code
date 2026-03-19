using System;
using System.Collections.Frozen;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.BlockObjectAccesses
{
	// Token: 0x02000007 RID: 7
	public class BlockObjectAccesses : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectAccessesSpec = base.GetComponent<BlockObjectAccessesSpec>();
			this._positionedBlockingCoordinates = new Lazy<FrozenSet<Vector3Int>>(new Func<FrozenSet<Vector3Int>>(this.PositionBlockingCoordinates));
			this._positionedAllowedCoordinates = new Lazy<FrozenSet<Vector3Int>>(new Func<FrozenSet<Vector3Int>>(this.PositionAllowedCoordinates));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002153 File Offset: 0x00000353
		public bool IsBlocked(Vector3Int position)
		{
			return this._positionedBlockingCoordinates.Value.Contains(position);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002166 File Offset: 0x00000366
		public bool IsAllowed(Vector3Int position)
		{
			return this._positionedAllowedCoordinates.Value.Contains(position);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002179 File Offset: 0x00000379
		public FrozenSet<Vector3Int> PositionBlockingCoordinates()
		{
			return (from coordinate in this._blockObjectAccessesSpec.BlockingCoordinates
			select this._blockObject.TransformCoordinates(coordinate)).ToFrozenSet(null);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000219D File Offset: 0x0000039D
		public FrozenSet<Vector3Int> PositionAllowedCoordinates()
		{
			return (from coordinate in this._blockObjectAccessesSpec.AllowedCoordinates
			select this._blockObject.TransformCoordinates(coordinate)).ToFrozenSet(null);
		}

		// Token: 0x04000008 RID: 8
		public BlockObject _blockObject;

		// Token: 0x04000009 RID: 9
		public BlockObjectAccessesSpec _blockObjectAccessesSpec;

		// Token: 0x0400000A RID: 10
		public Lazy<FrozenSet<Vector3Int>> _positionedBlockingCoordinates;

		// Token: 0x0400000B RID: 11
		public Lazy<FrozenSet<Vector3Int>> _positionedAllowedCoordinates;
	}
}
