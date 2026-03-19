using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Timberborn.Coordinates
{
	// Token: 0x02000019 RID: 25
	public readonly struct Placement
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003950 File Offset: 0x00001B50
		public Vector3Int Coordinates { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003958 File Offset: 0x00001B58
		public Orientation Orientation { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003960 File Offset: 0x00001B60
		public FlipMode FlipMode { get; }

		// Token: 0x0600006A RID: 106 RVA: 0x00003968 File Offset: 0x00001B68
		public Placement(Vector3Int coordinates)
		{
			this.Coordinates = coordinates;
			this.Orientation = 0;
			this.FlipMode = FlipMode.Unflipped;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003983 File Offset: 0x00001B83
		public Placement(Vector3Int coordinates, Orientation orientation, FlipMode flipMode)
		{
			this.Coordinates = coordinates;
			this.Orientation = orientation;
			this.FlipMode = flipMode;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000399C File Offset: 0x00001B9C
		[UsedImplicitly]
		public bool Equals(Placement other)
		{
			return this.Coordinates.Equals(other.Coordinates) && this.Orientation == other.Orientation && this.FlipMode.Equals(other.FlipMode);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000039E8 File Offset: 0x00001BE8
		public override bool Equals(object obj)
		{
			if (obj is Placement)
			{
				Placement other = (Placement)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override int GetHashCode()
		{
			return HashCode.Combine<Vector3Int, int, FlipMode>(this.Coordinates, (int)this.Orientation, this.FlipMode);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003A26 File Offset: 0x00001C26
		public static bool operator ==(Placement left, Placement right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003A30 File Offset: 0x00001C30
		public static bool operator !=(Placement left, Placement right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400004C RID: 76
		public static readonly Placement Zero;
	}
}
