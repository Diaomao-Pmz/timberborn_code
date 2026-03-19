using System;

namespace Timberborn.Common
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	public class BackwardCompatibleAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000249C File Offset: 0x0000069C
		public Compatibility Compatibility { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024A4 File Offset: 0x000006A4
		public DateTime Date { get; }

		// Token: 0x06000018 RID: 24 RVA: 0x000024AC File Offset: 0x000006AC
		public BackwardCompatibleAttribute(int year, int month, int day, Compatibility compatibility)
		{
			this.Date = new DateTime(year, month, day);
			this.Compatibility = compatibility;
		}
	}
}
