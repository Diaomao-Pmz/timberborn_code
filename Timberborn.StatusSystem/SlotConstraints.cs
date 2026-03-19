using System;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200000D RID: 13
	public readonly struct SlotConstraints
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000024B7 File Offset: 0x000006B7
		public bool IsOccupied { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024BF File Offset: 0x000006BF
		public byte BaseZ { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000024C7 File Offset: 0x000006C7
		public bool InvalidInConstructionMode { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024CF File Offset: 0x000006CF
		public bool ForceValidInConstructionMode { get; }

		// Token: 0x0600002E RID: 46 RVA: 0x000024D7 File Offset: 0x000006D7
		public SlotConstraints(bool isOccupied, byte baseZ, bool invalidInConstructionMode, bool forceValidInConstructionMode)
		{
			this.IsOccupied = isOccupied;
			this.BaseZ = baseZ;
			this.InvalidInConstructionMode = invalidInConstructionMode;
			this.ForceValidInConstructionMode = forceValidInConstructionMode;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024F6 File Offset: 0x000006F6
		public static SlotConstraints GetOccupied(byte baseZ, bool invalidInConstructionMode = false, bool forceValidInConstructionMode = false)
		{
			return new SlotConstraints(true, baseZ, invalidInConstructionMode, forceValidInConstructionMode);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002501 File Offset: 0x00000701
		public static SlotConstraints GetUnoccupied(byte baseZ)
		{
			return new SlotConstraints(false, baseZ, false, false);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000250C File Offset: 0x0000070C
		public SlotConstraints Merge(SlotConstraints other)
		{
			bool isOccupied = this.IsOccupied || other.IsOccupied;
			byte baseZ = (byte)Mathf.Min((int)this.BaseZ, (int)other.BaseZ);
			bool invalidInConstructionMode = this.BothInvalidInConstructionModeOrEmpty(other);
			bool forceValidInConstructionMode = this.ForceValidInConstructionMode || other.ForceValidInConstructionMode;
			return new SlotConstraints(isOccupied, baseZ, invalidInConstructionMode, forceValidInConstructionMode);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002562 File Offset: 0x00000762
		public bool BothInvalidInConstructionModeOrEmpty(SlotConstraints other)
		{
			return (this.InvalidInConstructionMode || other.InvalidInConstructionMode) && (!this.IsOccupied || this.InvalidInConstructionMode) && (!other.IsOccupied || other.InvalidInConstructionMode);
		}
	}
}
