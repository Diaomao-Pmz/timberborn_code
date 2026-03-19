using System;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000015 RID: 21
	public readonly struct HttpAdapterSnapshot
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003866 File Offset: 0x00001A66
		public string Name { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000386E File Offset: 0x00001A6E
		public bool State { get; }

		// Token: 0x06000070 RID: 112 RVA: 0x00003876 File Offset: 0x00001A76
		public HttpAdapterSnapshot(string name, bool state)
		{
			this.Name = name;
			this.State = state;
		}
	}
}
