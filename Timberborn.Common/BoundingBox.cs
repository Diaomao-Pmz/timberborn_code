using System;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x02000009 RID: 9
	public readonly struct BoundingBox
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000024CA File Offset: 0x000006CA
		public BoundingBox(int minX, int minY, int minZ, int maxX, int maxY, int maxZ)
		{
			this._minX = minX;
			this._minY = minY;
			this._minZ = minZ;
			this._maxX = maxX;
			this._maxY = maxY;
			this._maxZ = maxZ;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024FC File Offset: 0x000006FC
		public bool Contains(Vector3Int coordinates)
		{
			return coordinates.x >= this._minX && coordinates.x <= this._maxX && coordinates.y >= this._minY && coordinates.y <= this._maxY && coordinates.z >= this._minZ && coordinates.z <= this._maxZ;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002568 File Offset: 0x00000768
		public bool Intersects(in BoundingBox boundingBox)
		{
			return !this.Disconnected(boundingBox);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002574 File Offset: 0x00000774
		public bool Disconnected(in BoundingBox boundingBox)
		{
			return this._minX > boundingBox._maxX || this._maxX < boundingBox._minX || this._minY > boundingBox._maxY || this._maxY < boundingBox._minY || this._minZ > boundingBox._maxZ || this._maxZ < boundingBox._minZ;
		}

		// Token: 0x0400000B RID: 11
		public readonly int _minX;

		// Token: 0x0400000C RID: 12
		public readonly int _minY;

		// Token: 0x0400000D RID: 13
		public readonly int _minZ;

		// Token: 0x0400000E RID: 14
		public readonly int _maxX;

		// Token: 0x0400000F RID: 15
		public readonly int _maxY;

		// Token: 0x04000010 RID: 16
		public readonly int _maxZ;

		// Token: 0x0200000A RID: 10
		public struct Builder
		{
			// Token: 0x0600001D RID: 29 RVA: 0x000025D8 File Offset: 0x000007D8
			public void Expand(Vector3Int point)
			{
				int x = point.x;
				int y = point.y;
				int z = point.z;
				if (!this._expandedAtLeastOnce)
				{
					this._minX = x;
					this._minY = y;
					this._minZ = z;
					this._maxX = x;
					this._maxY = y;
					this._maxZ = z;
					this._expandedAtLeastOnce = true;
					return;
				}
				if (x < this._minX)
				{
					this._minX = x;
				}
				else if (x > this._maxX)
				{
					this._maxX = x;
				}
				if (y < this._minY)
				{
					this._minY = y;
				}
				else if (y > this._maxY)
				{
					this._maxY = y;
				}
				if (z < this._minZ)
				{
					this._minZ = z;
					return;
				}
				if (z > this._maxZ)
				{
					this._maxZ = z;
				}
			}

			// Token: 0x0600001E RID: 30 RVA: 0x0000269C File Offset: 0x0000089C
			public BoundingBox Build()
			{
				if (!this._expandedAtLeastOnce)
				{
					throw new InvalidOperationException("BoundingBox is empty");
				}
				return new BoundingBox(this._minX, this._minY, this._minZ, this._maxX, this._maxY, this._maxZ);
			}

			// Token: 0x04000011 RID: 17
			public int _minX;

			// Token: 0x04000012 RID: 18
			public int _minY;

			// Token: 0x04000013 RID: 19
			public int _minZ;

			// Token: 0x04000014 RID: 20
			public int _maxX;

			// Token: 0x04000015 RID: 21
			public int _maxY;

			// Token: 0x04000016 RID: 22
			public int _maxZ;

			// Token: 0x04000017 RID: 23
			public bool _expandedAtLeastOnce;
		}
	}
}
