using System;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200002A RID: 42
	public readonly struct HttpLeverSnapshot
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000056FE File Offset: 0x000038FE
		public string Name { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005706 File Offset: 0x00003906
		public bool State { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000570E File Offset: 0x0000390E
		public bool IsSpringReturn { get; }

		// Token: 0x060000ED RID: 237 RVA: 0x00005716 File Offset: 0x00003916
		public HttpLeverSnapshot(string name, bool state, bool isSpringReturn)
		{
			this.Name = name;
			this.State = state;
			this.IsSpringReturn = isSpringReturn;
		}
	}
}
