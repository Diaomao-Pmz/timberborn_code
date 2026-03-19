using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProtoBuf;

namespace Timberborn.TimbermeshDTO
{
	// Token: 0x0200000C RID: 12
	[UsedImplicitly]
	[ProtoContract]
	public class VertexAnimation : IAnimation
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000223C File Offset: 0x0000043C
		[ProtoMember(1)]
		public string Name { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002244 File Offset: 0x00000444
		[ProtoMember(2)]
		public float Framerate { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000224C File Offset: 0x0000044C
		[ProtoMember(3)]
		public int AnimatedVertexCount { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002254 File Offset: 0x00000454
		[ProtoMember(4)]
		public List<VertexAnimationFrame> Frames { get; } = new List<VertexAnimationFrame>();

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000225C File Offset: 0x0000045C
		public float Length
		{
			get
			{
				return (float)this.Frames.Count / this.Framerate;
			}
		}
	}
}
