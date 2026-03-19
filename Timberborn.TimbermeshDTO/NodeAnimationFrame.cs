using System;
using JetBrains.Annotations;
using ProtoBuf;

namespace Timberborn.TimbermeshDTO
{
	// Token: 0x02000009 RID: 9
	[UsedImplicitly]
	[ProtoContract]
	public class NodeAnimationFrame
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021EC File Offset: 0x000003EC
		[ProtoMember(1)]
		public Vector3Float Position { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000021F4 File Offset: 0x000003F4
		[ProtoMember(2)]
		public QuaternionFloat Rotation { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021FC File Offset: 0x000003FC
		[ProtoMember(3)]
		public Vector3Float Scale { get; }
	}
}
