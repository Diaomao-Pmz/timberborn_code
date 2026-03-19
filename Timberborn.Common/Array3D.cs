using System;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x02000006 RID: 6
	public class Array3D<TValue>
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002104 File Offset: 0x00000304
		public unsafe Array3D(Vector3Int size, Func<TValue> defaultValueProvider)
		{
			this._values = new TValue[size.x, size.y, size.z];
			this._size = size;
			this._defaultValue = defaultValueProvider();
			for (int i = 0; i < size.x; i++)
			{
				for (int j = 0; j < size.y; j++)
				{
					for (int k = 0; k < size.z; k++)
					{
						*this.GetRefAtWithoutBoundsCheck(new Vector3Int(i, j, k)) = defaultValueProvider();
					}
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002199 File Offset: 0x00000399
		public unsafe TValue GetCopyAtOrDefault(Vector3Int coordinates)
		{
			if (!this.Contains(coordinates))
			{
				return this._defaultValue;
			}
			return *this.GetRefAtWithoutBoundsCheck(coordinates);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B7 File Offset: 0x000003B7
		public ref TValue GetRefAt(Vector3Int coordinates)
		{
			if (!this.Contains(coordinates))
			{
				throw new ArgumentException(string.Format("{0} is out of bounds", coordinates));
			}
			return this.GetRefAtWithoutBoundsCheck(coordinates);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021DF File Offset: 0x000003DF
		public bool Contains(Vector3Int coordinates)
		{
			return Sizing.SizeContains(this._size, coordinates);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021ED File Offset: 0x000003ED
		public bool Contains(Vector2Int coordinates)
		{
			return Sizing.SizeContains(this._size, coordinates);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FB File Offset: 0x000003FB
		public void Clear()
		{
			Array.Clear(this._values, 0, this._values.Length);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002214 File Offset: 0x00000414
		public ref TValue GetRefAtWithoutBoundsCheck(Vector3Int coordinates)
		{
			return ref this._values[coordinates.x, coordinates.y, coordinates.z];
		}

		// Token: 0x04000006 RID: 6
		public readonly TValue[,,] _values;

		// Token: 0x04000007 RID: 7
		public readonly Vector3Int _size;

		// Token: 0x04000008 RID: 8
		public readonly TValue _defaultValue;
	}
}
