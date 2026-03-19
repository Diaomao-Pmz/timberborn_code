using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProtoBuf;

namespace Timberborn.TimbermeshDTO
{
	// Token: 0x02000008 RID: 8
	[UsedImplicitly]
	[ProtoContract]
	public class NodeAnimation : IAnimation
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021AC File Offset: 0x000003AC
		[ProtoMember(1)]
		public string Name { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000021B4 File Offset: 0x000003B4
		[ProtoMember(2)]
		public float Framerate { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021BC File Offset: 0x000003BC
		[ProtoMember(3)]
		public List<NodeAnimationFrame> Frames { get; } = new List<NodeAnimationFrame>();

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021C4 File Offset: 0x000003C4
		public float Length
		{
			get
			{
				return (float)this.Frames.Count / this.Framerate;
			}
		}
	}
}
