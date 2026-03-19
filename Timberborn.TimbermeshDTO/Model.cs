using System;
using ProtoBuf;

namespace Timberborn.TimbermeshDTO
{
	// Token: 0x02000006 RID: 6
	[ProtoContract]
	public class Model
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E3 File Offset: 0x000002E3
		[ProtoMember(1)]
		public int Version { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020EB File Offset: 0x000002EB
		[ProtoMember(2)]
		public string Name { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020F3 File Offset: 0x000002F3
		[ProtoMember(3)]
		public Node[] Nodes { get; }
	}
}
