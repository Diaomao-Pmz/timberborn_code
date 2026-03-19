using System;

namespace Timberborn.DistributionSystem
{
	// Token: 0x02000017 RID: 23
	public readonly struct ImportableGood
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000413B File Offset: 0x0000233B
		public bool IsImportable { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004143 File Offset: 0x00002343
		public bool HasCapacity { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000414B File Offset: 0x0000234B
		public DistributableGood DistributableGood { get; }

		// Token: 0x060000C1 RID: 193 RVA: 0x00004153 File Offset: 0x00002353
		public ImportableGood(bool isImportable, bool hasCapacity, DistributableGood distributableGood)
		{
			this.IsImportable = isImportable;
			this.HasCapacity = hasCapacity;
			this.DistributableGood = distributableGood;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000416A File Offset: 0x0000236A
		public static ImportableGood CreateImportableWithCapacity(DistributableGood distributableGood)
		{
			return new ImportableGood(true, true, distributableGood);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004174 File Offset: 0x00002374
		public static ImportableGood CreateNonImportable()
		{
			return new ImportableGood(false, false, default(DistributableGood));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004194 File Offset: 0x00002394
		public static ImportableGood CreateNonImportableWithCapacity()
		{
			return new ImportableGood(false, true, default(DistributableGood));
		}
	}
}
