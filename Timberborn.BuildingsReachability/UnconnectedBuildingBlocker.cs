using System;

namespace Timberborn.BuildingsReachability
{
	// Token: 0x02000011 RID: 17
	public class UnconnectedBuildingBlocker : IUnconnectedBuildingBlocker
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000038 RID: 56 RVA: 0x00002744 File Offset: 0x00000944
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x0000277C File Offset: 0x0000097C
		public event EventHandler IsUnconnectedBlockedChanged;

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000027B1 File Offset: 0x000009B1
		public bool IsUnconnectedBlocked
		{
			get
			{
				return true;
			}
		}
	}
}
