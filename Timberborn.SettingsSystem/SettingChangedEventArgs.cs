using System;

namespace Timberborn.SettingsSystem
{
	// Token: 0x02000005 RID: 5
	public class SettingChangedEventArgs<T>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020BE File Offset: 0x000002BE
		public T Value { get; }

		// Token: 0x06000013 RID: 19 RVA: 0x000020C6 File Offset: 0x000002C6
		public SettingChangedEventArgs(T value)
		{
			this.Value = value;
		}
	}
}
