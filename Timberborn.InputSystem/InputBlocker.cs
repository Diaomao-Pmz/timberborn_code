using System;

namespace Timberborn.InputSystem
{
	// Token: 0x0200000C RID: 12
	public class InputBlocker
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025FC File Offset: 0x000007FC
		public bool IsBlocked
		{
			get
			{
				return this._blockersCount > 0;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002607 File Offset: 0x00000807
		public void Block()
		{
			this._blockersCount++;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002617 File Offset: 0x00000817
		public void Unblock()
		{
			this._blockersCount--;
		}

		// Token: 0x04000016 RID: 22
		public int _blockersCount;
	}
}
