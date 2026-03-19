using System;
using ProtoBuf;

namespace Timberborn.TimbermeshDTO
{
	// Token: 0x0200000A RID: 10
	[ProtoContract]
	public class QuaternionFloat
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002204 File Offset: 0x00000404
		[ProtoMember(1)]
		public float X { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000220C File Offset: 0x0000040C
		[ProtoMember(2)]
		public float Y { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002214 File Offset: 0x00000414
		[ProtoMember(3)]
		public float Z { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000221C File Offset: 0x0000041C
		[ProtoMember(4)]
		public float W { get; }
	}
}
