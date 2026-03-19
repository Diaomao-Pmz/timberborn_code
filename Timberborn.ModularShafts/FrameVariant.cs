using System;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000007 RID: 7
	public readonly struct FrameVariant : IEquatable<FrameVariant>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public bool Down { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public bool Left { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public bool Up { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002118 File Offset: 0x00000318
		public bool Right { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002120 File Offset: 0x00000320
		public bool Bottom { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002128 File Offset: 0x00000328
		public bool Support { get; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002130 File Offset: 0x00000330
		public FrameVariant(bool down, bool left, bool up, bool right, bool bottom, bool support)
		{
			this.Down = down;
			this.Left = left;
			this.Up = up;
			this.Right = right;
			this.Bottom = bottom;
			this.Support = support;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002160 File Offset: 0x00000360
		public string GetName()
		{
			return string.Concat(new string[]
			{
				FrameVariant.Value(this.Down),
				FrameVariant.Value(this.Left),
				FrameVariant.Value(this.Up),
				FrameVariant.Value(this.Right),
				FrameVariant.Value(this.Bottom),
				FrameVariant.Value(this.Support)
			});
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021CC File Offset: 0x000003CC
		public bool Equals(FrameVariant other)
		{
			return this.Up == other.Up && this.Down == other.Down && this.Left == other.Left && this.Right == other.Right && this.Bottom == other.Bottom && this.Support == other.Support;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002238 File Offset: 0x00000438
		public override bool Equals(object obj)
		{
			if (obj is FrameVariant)
			{
				FrameVariant other = (FrameVariant)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000225D File Offset: 0x0000045D
		public override int GetHashCode()
		{
			return HashCode.Combine<bool, bool, bool, bool, bool, bool>(this.Up, this.Down, this.Left, this.Right, this.Bottom, this.Support);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002288 File Offset: 0x00000488
		public static bool operator ==(FrameVariant left, FrameVariant right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002292 File Offset: 0x00000492
		public static bool operator !=(FrameVariant left, FrameVariant right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000229F File Offset: 0x0000049F
		public static string Value(bool isSet)
		{
			if (!isSet)
			{
				return "0";
			}
			return "1";
		}
	}
}
