using System;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200001C RID: 28
	public readonly struct StatusSlot
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000039EE File Offset: 0x00001BEE
		public float ZCoordinate { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000039F6 File Offset: 0x00001BF6
		public bool InvalidInConstructionMode { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000039FE File Offset: 0x00001BFE
		public byte BaseZ { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003A06 File Offset: 0x00001C06
		public byte UnfinishedBaseZ { get; }

		// Token: 0x060000A9 RID: 169 RVA: 0x00003A0E File Offset: 0x00001C0E
		public StatusSlot(float zCoordinate, bool invalidInConstructionMode, byte baseZ, byte unfinishedBaseZ)
		{
			this.ZCoordinate = zCoordinate;
			this.InvalidInConstructionMode = invalidInConstructionMode;
			this.BaseZ = baseZ;
			this.UnfinishedBaseZ = unfinishedBaseZ;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003A2D File Offset: 0x00001C2D
		public static StatusSlot CreateAlwaysValid(float statusZCoordinate)
		{
			return new StatusSlot(statusZCoordinate, false, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003A40 File Offset: 0x00001C40
		public static StatusSlot CreateInvalidInConstructionMode(float statusZCoordinate, byte unfinishedBaseZ)
		{
			return new StatusSlot(statusZCoordinate, true, byte.MaxValue, unfinishedBaseZ);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A4F File Offset: 0x00001C4F
		public static StatusSlot CreateValidAboveMaxVisibilityLevel(float statusZCoordinate, byte baseZ)
		{
			return new StatusSlot(statusZCoordinate, false, baseZ, baseZ);
		}
	}
}
