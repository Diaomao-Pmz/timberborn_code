using System;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x0200000C RID: 12
	public readonly struct GroupIdUpdatedEventArgs
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000027BE File Offset: 0x000009BE
		public int GroupId { get; }

		// Token: 0x06000036 RID: 54 RVA: 0x000027C6 File Offset: 0x000009C6
		public GroupIdUpdatedEventArgs(int groupId)
		{
			this.GroupId = groupId;
		}
	}
}
