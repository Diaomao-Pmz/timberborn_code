using System;
using Timberborn.Common;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.TubeSystem
{
	// Token: 0x0200000B RID: 11
	public class TubeMap : ILoadableSingleton
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000251A File Offset: 0x0000071A
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002522 File Offset: 0x00000722
		public bool AnyTubeBuilt { get; private set; }

		// Token: 0x06000026 RID: 38 RVA: 0x0000252B File Offset: 0x0000072B
		public TubeMap(MapSize mapSize)
		{
			this._mapSize = mapSize;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000253A File Offset: 0x0000073A
		public void Load()
		{
			this._tubeMap = new Array3D<Tube>(this._mapSize.TotalSize, () => null);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002571 File Offset: 0x00000771
		public unsafe void SetTube(Tube tube, Vector3Int coordinates)
		{
			*this._tubeMap.GetRefAt(coordinates) = tube;
			this.AnyTubeBuilt = true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002588 File Offset: 0x00000788
		public unsafe void UnsetTube(Vector3Int coordinates)
		{
			*this._tubeMap.GetRefAt(coordinates) = null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002598 File Offset: 0x00000798
		public unsafe Tube GetTubeAt(Vector3Int gridPosition)
		{
			if (!this._tubeMap.Contains(gridPosition))
			{
				return null;
			}
			return *this._tubeMap.GetRefAt(gridPosition);
		}

		// Token: 0x0400001B RID: 27
		public readonly MapSize _mapSize;

		// Token: 0x0400001C RID: 28
		public Array3D<Tube> _tubeMap;
	}
}
