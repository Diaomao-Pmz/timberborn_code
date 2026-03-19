using System;

namespace Timberborn.BonusSystem
{
	// Token: 0x02000011 RID: 17
	public readonly struct BonusValueChangedEventArgs
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002CE6 File Offset: 0x00000EE6
		public string BonusId { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002CEE File Offset: 0x00000EEE
		public float Value { get; }

		// Token: 0x06000062 RID: 98 RVA: 0x00002CF6 File Offset: 0x00000EF6
		public BonusValueChangedEventArgs(string bonusId, float value)
		{
			this.BonusId = bonusId;
			this.Value = value;
		}
	}
}
