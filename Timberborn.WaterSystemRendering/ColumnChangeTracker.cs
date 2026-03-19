using System;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x02000007 RID: 7
	public class ColumnChangeTracker
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Update(bool anyColumnChanged)
		{
			for (int i = ColumnChangeTracker.FrameCapacity - 1; i > 0; i--)
			{
				this._frameStorage[i] = this._frameStorage[i - 1];
			}
			this._frameStorage[0] = anyColumnChanged;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000213C File Offset: 0x0000033C
		public bool AnyColumnChanged()
		{
			for (int i = 0; i < ColumnChangeTracker.FrameCapacity; i++)
			{
				if (this._frameStorage[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000008 RID: 8
		public static readonly int FrameCapacity = 3;

		// Token: 0x04000009 RID: 9
		public readonly bool[] _frameStorage = new bool[ColumnChangeTracker.FrameCapacity];
	}
}
