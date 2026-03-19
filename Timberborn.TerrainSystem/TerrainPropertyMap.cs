using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000013 RID: 19
	public class TerrainPropertyMap<T>
	{
		// Token: 0x06000082 RID: 130 RVA: 0x000030E7 File Offset: 0x000012E7
		public TerrainPropertyMap(Vector3Int size)
		{
			this._size = size;
			this._cells = new T[size.y, size.x, size.z];
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003118 File Offset: 0x00001318
		public T Get(Vector3Int coordinates)
		{
			if (!this.Contains(coordinates))
			{
				return default(T);
			}
			return this._cells[coordinates.y, coordinates.x, coordinates.z];
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003158 File Offset: 0x00001358
		public void Set(Vector3Int coordinates, T value)
		{
			if (this.Contains(coordinates))
			{
				this._cells[coordinates.y, coordinates.x, coordinates.z] = value;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003184 File Offset: 0x00001384
		public bool Contains(Vector3Int coordinates)
		{
			return Sizing.SizeContains(this._size, coordinates);
		}

		// Token: 0x0400002B RID: 43
		public readonly T[,,] _cells;

		// Token: 0x0400002C RID: 44
		public readonly Vector3Int _size;
	}
}
