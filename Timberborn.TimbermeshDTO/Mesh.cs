using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Timberborn.TimbermeshDTO
{
	// Token: 0x02000005 RID: 5
	[ProtoContract]
	public class Mesh
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020C0 File Offset: 0x000002C0
		[ProtoMember(1)]
		public List<int> Indices { get; } = new List<int>();

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020C8 File Offset: 0x000002C8
		[ProtoMember(2)]
		public string Material { get; }
	}
}
