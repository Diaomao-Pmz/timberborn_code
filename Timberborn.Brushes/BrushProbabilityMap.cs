using System;
using Timberborn.Common;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Brushes
{
	// Token: 0x02000005 RID: 5
	public class BrushProbabilityMap : ILoadableSingleton
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		public BrushProbabilityMap(MapSize mapSize, IRandomNumberGenerator randomNumberGenerator)
		{
			this._mapSize = mapSize;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F6 File Offset: 0x000002F6
		public void Load()
		{
			this._size = this._mapSize.TerrainSize;
			this._probabilities = new float[this._size.x, this._size.y];
			this.Reset();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		public bool TestProbabilityAtCoordinates(Vector2Int coordinates, float density)
		{
			return Sizing.SizeContains(this._size, coordinates) && this._probabilities[coordinates.x, coordinates.y] <= density;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002164 File Offset: 0x00000364
		public void Reset()
		{
			for (int i = 0; i < this._size.y; i++)
			{
				for (int j = 0; j < this._size.x; j++)
				{
					this._probabilities[j, i] = this._randomNumberGenerator.Range(0f, 1f);
				}
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly MapSize _mapSize;

		// Token: 0x04000007 RID: 7
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000008 RID: 8
		public Vector3Int _size;

		// Token: 0x04000009 RID: 9
		public float[,] _probabilities;
	}
}
