using System;

namespace Timberborn.Coordinates
{
	// Token: 0x0200000D RID: 13
	public struct Direction3DEnumerator
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002A6D File Offset: 0x00000C6D
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002A75 File Offset: 0x00000C75
		public Direction3D Current { readonly get; private set; }

		// Token: 0x06000021 RID: 33 RVA: 0x00002A7E File Offset: 0x00000C7E
		public Direction3DEnumerator(Directions3D directions)
		{
			this.Current = Direction3D.Down;
			this._directions = directions;
			this._index = -1;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A95 File Offset: 0x00000C95
		public Direction3DEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public bool MoveNext()
		{
			for (;;)
			{
				int num = this._index + 1;
				this._index = num;
				if (num >= 6)
				{
					return false;
				}
				switch (this._index)
				{
				case 0:
					if (this._directions.HasFlag(Directions3D.Down))
					{
						goto Block_2;
					}
					break;
				case 1:
					if (this._directions.HasFlag(Directions3D.Left))
					{
						goto Block_3;
					}
					break;
				case 2:
					if (this._directions.HasFlag(Directions3D.Up))
					{
						goto Block_4;
					}
					break;
				case 3:
					if (this._directions.HasFlag(Directions3D.Right))
					{
						goto Block_5;
					}
					break;
				case 4:
					if (this._directions.HasFlag(Directions3D.Bottom))
					{
						goto Block_6;
					}
					break;
				case 5:
					if (this._directions.HasFlag(Directions3D.Top))
					{
						goto Block_7;
					}
					break;
				}
			}
			Block_2:
			this.Current = Direction3D.Down;
			return true;
			Block_3:
			this.Current = Direction3D.Left;
			return true;
			Block_4:
			this.Current = Direction3D.Up;
			return true;
			Block_5:
			this.Current = Direction3D.Right;
			return true;
			Block_6:
			this.Current = Direction3D.Bottom;
			return true;
			Block_7:
			this.Current = Direction3D.Top;
			return true;
		}

		// Token: 0x04000031 RID: 49
		public readonly Directions3D _directions;

		// Token: 0x04000032 RID: 50
		public int _index;
	}
}
