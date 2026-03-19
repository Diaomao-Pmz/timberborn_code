using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProtoBuf;

namespace Timberborn.TimbermeshDTO
{
	// Token: 0x0200000D RID: 13
	[UsedImplicitly]
	[ProtoContract]
	public class VertexAnimationFrame
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002284 File Offset: 0x00000484
		[ProtoMember(1)]
		public List<VertexProperty> VertexProperties { get; } = new List<VertexProperty>();
	}
}
