using System;

namespace Timberborn.NeedSystem
{
	// Token: 0x0200000F RID: 15
	public class SerializedNeed
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003053 File Offset: 0x00001253
		public string Id { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000305B File Offset: 0x0000125B
		public float Points { get; }

		// Token: 0x06000074 RID: 116 RVA: 0x00003063 File Offset: 0x00001263
		public SerializedNeed(string id, float points)
		{
			this.Id = id;
			this.Points = points;
		}
	}
}
