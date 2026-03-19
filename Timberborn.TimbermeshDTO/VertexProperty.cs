using System;
using ProtoBuf;

namespace Timberborn.TimbermeshDTO
{
	// Token: 0x0200000F RID: 15
	[ProtoContract]
	public class VertexProperty
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000229F File Offset: 0x0000049F
		[ProtoMember(1)]
		public string Name { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000022A7 File Offset: 0x000004A7
		[ProtoMember(2)]
		public ScalarType ScalarType { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000022AF File Offset: 0x000004AF
		[ProtoMember(3)]
		public int ScalarTypeDimension { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000022B7 File Offset: 0x000004B7
		[ProtoMember(4)]
		public byte[] Data { get; }
	}
}
