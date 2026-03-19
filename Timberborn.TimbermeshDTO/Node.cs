using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Timberborn.TimbermeshDTO
{
	// Token: 0x02000007 RID: 7
	[ProtoContract]
	public class Node
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020FB File Offset: 0x000002FB
		[ProtoMember(1)]
		public int Parent { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002103 File Offset: 0x00000303
		[ProtoMember(2)]
		public string Name { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000210B File Offset: 0x0000030B
		[ProtoMember(3)]
		public Vector3Float Position { get; } = new Vector3Float();

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002113 File Offset: 0x00000313
		[ProtoMember(4)]
		public QuaternionFloat Rotation { get; } = new QuaternionFloat();

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000211B File Offset: 0x0000031B
		[ProtoMember(5)]
		public Vector3Float Scale { get; } = new Vector3Float();

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002123 File Offset: 0x00000323
		[ProtoMember(6)]
		public int VertexCount { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000212B File Offset: 0x0000032B
		[ProtoMember(7)]
		public List<VertexProperty> VertexProperties { get; } = new List<VertexProperty>();

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002133 File Offset: 0x00000333
		[ProtoMember(8)]
		public List<Mesh> Meshes { get; } = new List<Mesh>();

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000213B File Offset: 0x0000033B
		[ProtoMember(9)]
		public List<VertexAnimation> VertexAnimations { get; } = new List<VertexAnimation>();

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002143 File Offset: 0x00000343
		[ProtoMember(10)]
		public List<NodeAnimation> NodeAnimations { get; } = new List<NodeAnimation>();
	}
}
