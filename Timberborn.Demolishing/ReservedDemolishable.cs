using System;

namespace Timberborn.Demolishing
{
	// Token: 0x0200001F RID: 31
	public class ReservedDemolishable
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003A44 File Offset: 0x00001C44
		public Demolishable Demolishable { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003A4C File Offset: 0x00001C4C
		public bool ForceDemolish { get; }

		// Token: 0x060000DB RID: 219 RVA: 0x00003A54 File Offset: 0x00001C54
		public ReservedDemolishable(Demolishable demolishable, bool forceDemolish)
		{
			this.Demolishable = demolishable;
			this.ForceDemolish = forceDemolish;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003A6A File Offset: 0x00001C6A
		public bool CanBeDemolished
		{
			get
			{
				return this.Demolishable.IsMarked || this.ForceDemolish;
			}
		}
	}
}
