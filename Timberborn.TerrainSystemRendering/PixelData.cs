using System;
using JetBrains.Annotations;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x02000013 RID: 19
	public readonly struct PixelData
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003BC5 File Offset: 0x00001DC5
		[UsedImplicitly]
		private byte R { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003BCD File Offset: 0x00001DCD
		[UsedImplicitly]
		private byte G { get; }

		// Token: 0x06000057 RID: 87 RVA: 0x00003BD5 File Offset: 0x00001DD5
		public PixelData(float r, float g)
		{
			this.R = (byte)(255f * r);
			this.G = (byte)(255f * g);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003BF3 File Offset: 0x00001DF3
		public float GNormalized
		{
			get
			{
				return (float)this.G / 255f;
			}
		}
	}
}
