using System;

namespace Timberborn.MapIndexSystem
{
	// Token: 0x02000006 RID: 6
	public struct Index2DEnumerator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
		public Index2DEnumerator(int width, int height, int margin, int startingX)
		{
			int num = margin * 2;
			this._width = width + num;
			this._height = height + num;
			this._margin = margin;
			this._currentX = startingX;
			this._currentY = 0;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000214D File Offset: 0x0000034D
		public int Current
		{
			get
			{
				return this._currentY * this._width + this._currentX;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002163 File Offset: 0x00000363
		public Index2DEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000216C File Offset: 0x0000036C
		public bool MoveNext()
		{
			this._currentX++;
			if (this._currentX >= this._width - this._margin)
			{
				this._currentX = this._margin;
				this._currentY++;
			}
			return this._currentY < this._height - this._margin;
		}

		// Token: 0x04000006 RID: 6
		public readonly int _width;

		// Token: 0x04000007 RID: 7
		public readonly int _height;

		// Token: 0x04000008 RID: 8
		public readonly int _margin;

		// Token: 0x04000009 RID: 9
		public int _currentX;

		// Token: 0x0400000A RID: 10
		public int _currentY;
	}
}
