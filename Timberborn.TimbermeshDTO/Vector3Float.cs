using System;
using ProtoBuf;

namespace Timberborn.TimbermeshDTO
{
	// Token: 0x0200000B RID: 11
	[ProtoContract]
	public class Vector3Float
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002224 File Offset: 0x00000424
		[ProtoMember(1)]
		public float X { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000222C File Offset: 0x0000042C
		[ProtoMember(2)]
		public float Y { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002234 File Offset: 0x00000434
		[ProtoMember(3)]
		public float Z { get; }
	}
}
