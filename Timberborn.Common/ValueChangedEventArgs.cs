using System;

namespace Timberborn.Common
{
	// Token: 0x02000031 RID: 49
	public struct ValueChangedEventArgs<T>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000038EA File Offset: 0x00001AEA
		public readonly T OldValue { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000038F2 File Offset: 0x00001AF2
		public readonly T NewValue { get; }

		// Token: 0x060000B5 RID: 181 RVA: 0x000038FA File Offset: 0x00001AFA
		public ValueChangedEventArgs(T oldValue, T newValue)
		{
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}
	}
}
